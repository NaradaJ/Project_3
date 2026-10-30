using System.Data;
using GSTS.Data;
using GSTS.Helpers;
using GSTS.Models;
using MySqlConnector;

namespace GSTS.Services;

// Administration: officers, service steps, reassignment and reports (FR12–FR15).
public static class AdminService
{
    // ===================== FR12 – Manage Officers (ManageOfficer, Figure 8) =====================

    // Search officers by name, NIC, designation or department.
    public static DataTable SearchOfficers(string keyword)
    {
        return Db.Query(
            @"SELECT o.officer_id, p.name AS `Name`, p.nic AS `NIC`, p.contact AS `Contact`,
                     o.designation AS `Designation`, o.department AS `Department`,
                     o.handles_step AS `Handles Step`, IF(o.is_active, 'Yes', 'No') AS `Active`,
                     (SELECT COUNT(*) FROM Steps s WHERE s.officer_id = o.officer_id AND s.status = 'Active') AS `Active Tickets`
              FROM Officers o
              JOIN Persons p ON p.person_id = o.person_id
              WHERE p.name LIKE @keyword OR p.nic LIKE @keyword
                 OR o.designation LIKE @keyword OR o.department LIKE @keyword
              ORDER BY p.name",
            Db.P("@keyword", "%" + keyword.Trim() + "%"));
    }

    public static Officer? GetOfficer(int officerId)
    {
        DataRow? row = Db.QueryRow(
            @"SELECT o.*, p.name, p.nic, p.contact FROM Officers o
              JOIN Persons p ON p.person_id = o.person_id
              WHERE o.officer_id = @officer",
            Db.P("@officer", officerId));
        return row == null ? null : Officer.FromRow(row);
    }

    // All step names used in the service templates (for the "Handles step" list).
    public static List<string> GetStepNames()
    {
        DataTable table = Db.Query(
            "SELECT DISTINCT step_name FROM Steps WHERE ticket_id IS NULL ORDER BY step_name");
        var list = new List<string>();
        foreach (DataRow row in table.Rows)
        {
            list.Add(Convert.ToString(row["step_name"]) ?? "");
        }
        return list;
    }

    // ManageOfficer('ADD')
    // The officer logs in with their NIC and the initial password given here (DEVIATIONS.md, D5).
    public static Result AddOfficer(int adminId, string name, string nic, string contact,
        string designation, string department, string handlesStep, string password)
    {
        name = name.Trim();
        nic = nic.Trim().ToUpper();
        contact = contact.Trim();
        designation = designation.Trim();
        department = department.Trim();
        handlesStep = handlesStep.Trim();

        if (name == "" || nic == "" || contact == "" || designation == "" ||
            department == "" || handlesStep == "" || password == "")
        {
            return Result.Fail("All fields are required");
        }
        if (!Validator.ValidNIC(nic) || !Validator.ValidPhone(contact) || !Validator.ValidPassword(password))
        {
            return Result.Fail("Invalid NIC, contact number or password");
        }
        if (Db.Scalar("SELECT 1 FROM Persons WHERE nic = @nic", Db.P("@nic", nic)) != null)
        {
            return Result.Fail("This NIC is already registered");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            int personId = Db.Insert(conn, tx,
                @"INSERT INTO Persons (name, nic, contact, address, username, password_hash, role, is_active)
                  VALUES (@name, @nic, @contact, NULL, @nic, @hash, 'Officer', TRUE)",
                Db.P("@name", name),
                Db.P("@nic", nic),
                Db.P("@contact", contact),
                Db.P("@hash", PasswordHasher.HashPassword(password)));

            int officerId = Db.Insert(conn, tx,
                @"INSERT INTO Officers (person_id, designation, department, handles_step, is_active)
                  VALUES (@person, @designation, @department, @step, TRUE)",
                Db.P("@person", personId),
                Db.P("@designation", designation),
                Db.P("@department", department),
                Db.P("@step", handlesStep));

            CommonService.InsertLog(conn, tx, null, null, officerId, adminId, "OFFICER_ADD", name);

            // D2 – tickets that were waiting for this step can now be assigned.
            TicketService.AssignWaitingSteps(conn, tx, handlesStep, adminId);

            tx.Commit();
            return Result.Ok("Saved");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The officer could not be saved. Please try again");
        }
    }

    // ManageOfficer('EDIT')
    public static Result EditOfficer(int adminId, int officerId, string name, string contact,
        string designation, string department, string handlesStep)
    {
        name = name.Trim();
        contact = contact.Trim();
        designation = designation.Trim();
        department = department.Trim();
        handlesStep = handlesStep.Trim();

        if (name == "" || contact == "" || designation == "" || department == "" || handlesStep == "")
        {
            return Result.Fail("All fields are required");
        }
        if (!Validator.ValidPhone(contact))
        {
            return Result.Fail("Invalid contact number");
        }

        Officer? officer = GetOfficer(officerId);
        if (officer == null)
        {
            return Result.Fail("Officer not found");
        }

        // An officer who is holding active tickets cannot be moved to a different step.
        if (officer.HandlesStep != handlesStep && ActiveTicketCount(officerId) > 0)
        {
            return Result.Fail("Reassign this officer's tickets first");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            Db.Execute(conn, tx,
                "UPDATE Persons SET name = @name, contact = @contact WHERE person_id = @person",
                Db.P("@name", name),
                Db.P("@contact", contact),
                Db.P("@person", officer.PersonId));

            Db.Execute(conn, tx,
                @"UPDATE Officers SET designation = @designation, department = @department, handles_step = @step
                  WHERE officer_id = @officer",
                Db.P("@designation", designation),
                Db.P("@department", department),
                Db.P("@step", handlesStep),
                Db.P("@officer", officerId));

            CommonService.InsertLog(conn, tx, null, null, officerId, adminId, "OFFICER_EDIT", name);

            if (officer.IsActive)
            {
                TicketService.AssignWaitingSteps(conn, tx, handlesStep, adminId); // D2
            }

            tx.Commit();
            return Result.Ok("Saved");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The officer could not be saved. Please try again");
        }
    }

    // ManageOfficer('DEACTIVATE')
    public static Result DeactivateOfficer(int adminId, int officerId)
    {
        Officer? officer = GetOfficer(officerId);
        if (officer == null)
        {
            return Result.Fail("Officer not found");
        }
        if (!officer.IsActive)
        {
            return Result.Fail("This officer is already inactive");
        }
        if (ActiveTicketCount(officerId) > 0)
        {
            return Result.Fail("Reassign this officer's tickets first");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            Db.Execute(conn, tx, "UPDATE Officers SET is_active = FALSE WHERE officer_id = @officer",
                Db.P("@officer", officerId));

            // The deactivated officer can no longer log in (DEVIATIONS.md, D6).
            Db.Execute(conn, tx, "UPDATE Persons SET is_active = FALSE WHERE person_id = @person",
                Db.P("@person", officer.PersonId));

            CommonService.InsertLog(conn, tx, null, null, officerId, adminId, "OFFICER_DEACTIVATE", officer.Name);

            tx.Commit();
            return Result.Ok("Saved");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The officer could not be saved. Please try again");
        }
    }

    private static int ActiveTicketCount(int officerId)
    {
        return Convert.ToInt32(Db.Scalar(
            "SELECT COUNT(*) FROM Steps WHERE officer_id = @officer AND status = 'Active'",
            Db.P("@officer", officerId)));
    }

    // ============ FR13 – Manage Service Types and Steps (ManageServiceSteps, Figure 8) ============
    // Only the template rows (ticket_id IS NULL) are changed, so existing tickets are not affected.

    public static DataTable GetTemplateSteps(string serviceType)
    {
        return Db.Query(
            @"SELECT step_id, step_order AS `Order`, step_name AS `Step Name`
              FROM Steps
              WHERE ticket_id IS NULL AND service_type = @service
              ORDER BY step_order",
            Db.P("@service", serviceType));
    }

    // ManageServiceSteps('ADD') – adding the first step of a new name creates a new service type.
    public static Result AddTemplateStep(string serviceType, string stepName, string stepOrderText)
    {
        Result? error = ValidateTemplateStep(serviceType, stepName, stepOrderText, null, out int stepOrder);
        if (error != null)
        {
            return error;
        }

        Db.Execute(
            @"INSERT INTO Steps (ticket_id, service_type, step_order, step_name, status)
              VALUES (NULL, @service, @order, @name, 'Template')",
            Db.P("@service", serviceType.Trim()),
            Db.P("@order", stepOrder),
            Db.P("@name", stepName.Trim()));

        return Result.Ok("Saved. Changes apply to new tickets only");
    }

    // ManageServiceSteps('EDIT' / 'REORDER') – change the name and/or the order of a step.
    public static Result EditTemplateStep(int stepId, string serviceType, string stepName, string stepOrderText)
    {
        Result? error = ValidateTemplateStep(serviceType, stepName, stepOrderText, stepId, out int stepOrder);
        if (error != null)
        {
            return error;
        }

        Db.Execute(
            @"UPDATE Steps SET step_name = @name, step_order = @order
              WHERE step_id = @step AND ticket_id IS NULL",
            Db.P("@name", stepName.Trim()),
            Db.P("@order", stepOrder),
            Db.P("@step", stepId));

        return Result.Ok("Saved. Changes apply to new tickets only");
    }

    // ManageServiceSteps('REMOVE')
    public static Result RemoveTemplateStep(int stepId)
    {
        Db.Execute("DELETE FROM Steps WHERE step_id = @step AND ticket_id IS NULL", Db.P("@step", stepId));
        return Result.Ok("Saved. Changes apply to new tickets only");
    }

    // Validation from the pseudocode: name required, order a positive integer, order unique.
    // excludeStepId = the step being edited (it may keep its own order number).
    private static Result? ValidateTemplateStep(string serviceType, string stepName, string stepOrderText,
        int? excludeStepId, out int stepOrder)
    {
        stepOrder = 0;
        if (serviceType.Trim() == "")
        {
            return Result.Fail("Please enter or select a service type");
        }
        if (stepName.Trim() == "" || !int.TryParse(stepOrderText.Trim(), out stepOrder) || stepOrder <= 0)
        {
            return Result.Fail("Step name is required and step order must be a positive whole number");
        }

        object? used = Db.Scalar(
            @"SELECT 1 FROM Steps
              WHERE ticket_id IS NULL AND service_type = @service AND step_order = @order
                AND step_id <> @exclude",
            Db.P("@service", serviceType.Trim()),
            Db.P("@order", stepOrder),
            Db.P("@exclude", excludeStepId ?? 0));
        if (used != null)
        {
            return Result.Fail("Step order must be unique");
        }
        return null;
    }

    // ===================== FR14 – Reassign Ticket (ReassignTicket, Figure 6) =====================

    // Open tickets with their active step and the officer holding it.
    public static DataTable GetOpenTickets()
    {
        return Db.Query(
            @"SELECT t.ticket_id, t.ticket_no AS `Ticket No`, t.service_type AS `Service Type`,
                     t.status AS `Status`, s.step_name AS `Active Step`, p.name AS `Current Officer`
              FROM Tickets t
              LEFT JOIN Steps s ON s.ticket_id = t.ticket_id AND s.status = 'Active'
              LEFT JOIN Officers o ON o.officer_id = s.officer_id
              LEFT JOIN Persons p ON p.person_id = o.person_id
              WHERE t.status NOT IN ('Completed', 'Rejected')
              ORDER BY t.created_at");
    }

    // Active officers with the step each one handles.
    public static DataTable GetActiveOfficers()
    {
        return Db.Query(
            @"SELECT o.officer_id, CONCAT(p.name, ' – ', o.designation, ' (', o.handles_step, ')') AS display
              FROM Officers o
              JOIN Persons p ON p.person_id = o.person_id
              WHERE o.is_active = TRUE
              ORDER BY p.name");
    }

    public static Result ReassignTicket(int adminId, int ticketId, int newOfficerId)
    {
        DataRow? ticket = Db.QueryRow("SELECT * FROM Tickets WHERE ticket_id = @ticket", Db.P("@ticket", ticketId));
        string status = ticket == null ? "" : Convert.ToString(ticket["status"]) ?? "";
        if (ticket == null || status == "Completed" || status == "Rejected")
        {
            return Result.Fail("Only open tickets can be reassigned");
        }

        DataRow? stepRow = Db.QueryRow(
            "SELECT * FROM Steps WHERE ticket_id = @ticket AND status = 'Active'",
            Db.P("@ticket", ticketId));
        if (stepRow == null)
        {
            return Result.Fail("This ticket has no active step to reassign");
        }
        Step step = Step.FromRow(stepRow);

        Officer? officer = GetOfficer(newOfficerId);
        if (officer == null || !officer.IsActive || officer.HandlesStep != step.StepName)
        {
            return Result.Fail("This officer cannot handle the current step");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            Db.Execute(conn, tx, "UPDATE Steps SET officer_id = @officer WHERE step_id = @step",
                Db.P("@officer", newOfficerId),
                Db.P("@step", step.StepId));

            CommonService.InsertLog(conn, tx, ticketId, step.StepId, newOfficerId, adminId,
                "REASSIGNED", "To officer " + newOfficerId);
            CommonService.CreateNotification(conn, tx, officer.PersonId, ticketId, "A ticket has been assigned to you");
            CommonService.CreateNotification(conn, tx, CommonService.CitizenOf(conn, tx, ticketId), ticketId,
                "Your ticket is now with another officer");

            tx.Commit();
            return Result.Ok("Ticket reassigned");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The ticket could not be reassigned. Please try again");
        }
    }

    // ===================== FR15 – Reports (GenerateReport, Figure 7) =====================
    // reportType: BY_STATUS, BY_SERVICE, AVG_TIME or PENDING_LONG.
    // Returns the rows in "rows", or a message when the input is invalid or there is no data.
    public static Result GenerateReport(string reportType, DateTime fromDate, DateTime toDate, int days, out DataTable rows)
    {
        rows = new DataTable();
        if (fromDate.Date > toDate.Date)
        {
            return Result.Fail("Please enter a valid date range");
        }

        // Whole days: from 00:00 on the first date up to the end of the last date.
        MySqlParameter from = Db.P("@from", fromDate.Date);
        MySqlParameter to = Db.P("@to", toDate.Date.AddDays(1));

        switch (reportType)
        {
            case "BY_STATUS":
                rows = Db.Query(
                    @"SELECT status AS `Status`, COUNT(*) AS `Tickets` FROM Tickets
                      WHERE created_at >= @from AND created_at < @to
                      GROUP BY status ORDER BY status", from, to);
                break;

            case "BY_SERVICE":
                rows = Db.Query(
                    @"SELECT service_type AS `Service Type`, COUNT(*) AS `Tickets` FROM Tickets
                      WHERE created_at >= @from AND created_at < @to
                      GROUP BY service_type ORDER BY service_type", from, to);
                break;

            case "AVG_TIME":
                rows = Db.Query(
                    @"SELECT service_type AS `Service Type`,
                             ROUND(AVG(DATEDIFF(closed_at, created_at)), 2) AS `Average Days`,
                             COUNT(*) AS `Completed Tickets`
                      FROM Tickets
                      WHERE status = 'Completed' AND created_at >= @from AND created_at < @to
                      GROUP BY service_type ORDER BY service_type", from, to);
                break;

            case "PENDING_LONG":
                if (days < 0)
                {
                    return Result.Fail("Please enter a valid number of days");
                }
                rows = Db.Query(
                    @"SELECT ticket_no AS `Ticket No`, service_type AS `Service Type`, status AS `Status`,
                             created_at AS `Created`, DATEDIFF(NOW(), created_at) AS `Days Pending`
                      FROM Tickets
                      WHERE status IN ('Open', 'In progress') AND DATEDIFF(NOW(), created_at) > @days
                      ORDER BY created_at",
                    Db.P("@days", days));
                break;

            default:
                return Result.Fail("Please select a report");
        }

        if (rows.Rows.Count == 0)
        {
            return Result.Fail("No data for this period");
        }
        return Result.Ok("");
    }
}
