using System.Data;
using GSTS.Data;
using GSTS.Models;
using MySqlConnector;

namespace GSTS.Services;

// The ticket lifecycle: create, assign, process, resubmit, track and search.
public static class TicketService
{
    // Service types = the service types that have template steps (Report §3.0).
    public static List<string> GetServiceTypes()
    {
        DataTable table = Db.Query(
            "SELECT DISTINCT service_type FROM Steps WHERE ticket_id IS NULL ORDER BY service_type");

        var list = new List<string>();
        foreach (DataRow row in table.Rows)
        {
            list.Add(Convert.ToString(row["service_type"]) ?? "");
        }
        return list;
    }

    // FR3 and FR4 – Create Ticket and Assign Officer (Report §3.4 CreateTicket, Figure 3)
    public static Result CreateTicket(int citizenId, string serviceType, string details)
    {
        details = details.Trim();
        if (serviceType == "" || details == "")
        {
            return Result.Fail("Please complete all fields");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            // StepTemplates(service_type) ORDER BY step_order
            DataTable templates = Db.Query(conn, tx,
                @"SELECT step_name FROM Steps
                  WHERE ticket_id IS NULL AND service_type = @service
                  ORDER BY step_order",
                Db.P("@service", serviceType));

            if (templates.Rows.Count == 0)
            {
                tx.Rollback();
                return Result.Fail("This service type has no processing steps");
            }

            string ticketNo = CommonService.GenerateTicketNo(conn, tx);

            int ticketId = Db.Insert(conn, tx,
                @"INSERT INTO Tickets (ticket_no, citizen_id, service_type, details, status, created_at)
                  VALUES (@no, @citizen, @service, @details, 'Open', NOW())",
                Db.P("@no", ticketNo),
                Db.P("@citizen", citizenId),
                Db.P("@service", serviceType),
                Db.P("@details", details));

            // Copy the template steps to the ticket as 'Pending'.
            // The ticket's steps are numbered 1, 2, 3 ... (see DEVIATIONS.md, D7).
            int order = 1;
            foreach (DataRow template in templates.Rows)
            {
                Db.Insert(conn, tx,
                    @"INSERT INTO Steps (ticket_id, service_type, step_order, step_name, status)
                      VALUES (@ticket, @service, @order, @name, 'Pending')",
                    Db.P("@ticket", ticketId),
                    Db.P("@service", serviceType),
                    Db.P("@order", order),
                    Db.P("@name", template["step_name"]));
                order++;
            }

            AssignOfficer(conn, tx, ticketId, 1, citizenId);
            CommonService.CreateNotification(conn, tx, citizenId, ticketId, "Ticket " + ticketNo + " received");

            tx.Commit();
            return Result.Ok("Ticket created. Reference number: " + ticketNo);
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The ticket could not be saved. Please try again");
        }
    }

    // FR4 – Automatic Ticket Assignment (Report §3.4 AssignOfficer)
    // Chooses the active officer who handles this step and has the fewest 'Active' steps.
    // performedBy = the person whose action caused the assignment (for the log, D1).
    public static bool AssignOfficer(MySqlConnection conn, MySqlTransaction tx, int ticketId, int stepOrder, int performedBy)
    {
        DataRow? stepRow = Db.QueryRow(conn, tx,
            "SELECT * FROM Steps WHERE ticket_id = @ticket AND step_order = @order",
            Db.P("@ticket", ticketId),
            Db.P("@order", stepOrder));
        Step step = Step.FromRow(stepRow!);

        DataRow? officer = Db.QueryRow(conn, tx,
            @"SELECT o.officer_id, o.person_id FROM Officers o
              WHERE o.is_active = TRUE AND o.handles_step = @stepName
              ORDER BY (SELECT COUNT(*) FROM Steps s
                        WHERE s.officer_id = o.officer_id AND s.status = 'Active') ASC,
                       o.officer_id ASC
              LIMIT 1",
            Db.P("@stepName", step.StepName));

        // The current step is recorded even when nobody can take it yet (DEVIATIONS.md, D2).
        Db.Execute(conn, tx,
            "UPDATE Tickets SET current_step = @order WHERE ticket_id = @ticket",
            Db.P("@order", stepOrder),
            Db.P("@ticket", ticketId));

        if (officer == null)
        {
            Db.Execute(conn, tx, "UPDATE Steps SET status = 'Waiting' WHERE step_id = @step",
                Db.P("@step", step.StepId));
            return false;
        }

        int officerId = Convert.ToInt32(officer["officer_id"]);
        int officerPersonId = Convert.ToInt32(officer["person_id"]);

        Db.Execute(conn, tx,
            "UPDATE Steps SET officer_id = @officer, status = 'Active', started_at = NOW() WHERE step_id = @step",
            Db.P("@officer", officerId),
            Db.P("@step", step.StepId));

        Db.Execute(conn, tx,
            "UPDATE Tickets SET status = 'In progress', current_step = @order WHERE ticket_id = @ticket",
            Db.P("@order", stepOrder),
            Db.P("@ticket", ticketId));

        CommonService.InsertLog(conn, tx, ticketId, step.StepId, officerId, performedBy, "ASSIGNED", "");
        CommonService.CreateNotification(conn, tx, officerPersonId, ticketId, "A new ticket has been assigned to you");
        return true;
    }

    // DEVIATIONS.md, D2 – When an officer is added or edited, any 'Waiting' steps
    // that this step name was waiting for are assigned now (oldest ticket first).
    public static void AssignWaitingSteps(MySqlConnection conn, MySqlTransaction tx, string stepName, int performedBy)
    {
        DataTable waiting = Db.Query(conn, tx,
            @"SELECT s.ticket_id, s.step_order FROM Steps s
              JOIN Tickets t ON t.ticket_id = s.ticket_id
              WHERE s.status = 'Waiting' AND s.step_name = @stepName
              ORDER BY t.created_at ASC",
            Db.P("@stepName", stepName));

        foreach (DataRow row in waiting.Rows)
        {
            AssignOfficer(conn, tx, Convert.ToInt32(row["ticket_id"]), Convert.ToInt32(row["step_order"]), performedBy);
        }
    }

    // FR5 – View Assigned Tickets (Report §3.4 ViewAssignedTickets)
    public static DataTable ViewAssignedTickets(int officerId)
    {
        return Db.Query(
            @"SELECT t.ticket_id, t.ticket_no AS `Ticket No`, t.service_type AS `Service Type`,
                     p.name AS `Citizen`, s.step_name AS `Current Step`, t.created_at AS `Date Received`
              FROM Steps s
              JOIN Tickets t ON t.ticket_id = s.ticket_id
              JOIN Persons p ON p.person_id = t.citizen_id
              WHERE s.officer_id = @officer AND s.status = 'Active'
              ORDER BY t.created_at ASC",
            Db.P("@officer", officerId));
    }

    // Used by ProcessStep: the 'Active' step of this ticket that is held by this officer.
    public static Step? GetActiveStepForOfficer(int officerId, int ticketId)
    {
        DataRow? row = Db.QueryRow(
            "SELECT * FROM Steps WHERE ticket_id = @ticket AND officer_id = @officer AND status = 'Active'",
            Db.P("@ticket", ticketId),
            Db.P("@officer", officerId));
        return row == null ? null : Step.FromRow(row);
    }

    // FR7 – the "view" action is logged when an officer opens a ticket.
    public static void LogView(int ticketId, int stepId, int officerId, int personId)
    {
        using MySqlConnection conn = Db.GetConnection();
        CommonService.InsertLog(conn, null, ticketId, stepId, officerId, personId, "VIEWED", "");
    }

    // FR6, FR7 and FR8 – Process a Ticket Step, Log and Notify (Report §3.4 ProcessStep, Figure 4)
    // decision is 'APPROVE', 'RETURN' or 'REJECT'.
    public static Result ProcessStep(int officerId, int officerPersonId, int ticketId, string decision, string remarks)
    {
        remarks = remarks.Trim();
        if ((decision == "RETURN" || decision == "REJECT") && remarks == "")
        {
            return Result.Fail("Remarks are required");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            // Read the step again inside the transaction, so it cannot be processed twice.
            DataRow? stepRow = Db.QueryRow(conn, tx,
                @"SELECT * FROM Steps
                  WHERE ticket_id = @ticket AND officer_id = @officer AND status = 'Active'
                  FOR UPDATE",
                Db.P("@ticket", ticketId),
                Db.P("@officer", officerId));

            if (stepRow == null)
            {
                tx.Rollback();
                return Result.Fail("This ticket is not assigned to you");
            }
            Step step = Step.FromRow(stepRow);

            string message;
            switch (decision)
            {
                case "APPROVE":
                    Db.Execute(conn, tx,
                        "UPDATE Steps SET status = 'Approved', remarks = @remarks, completed_at = NOW() WHERE step_id = @step",
                        Db.P("@remarks", remarks),
                        Db.P("@step", step.StepId));

                    int lastStepOrder = Convert.ToInt32(Db.Scalar(conn, tx,
                        "SELECT MAX(step_order) FROM Steps WHERE ticket_id = @ticket",
                        Db.P("@ticket", ticketId)));

                    if (step.StepOrder == lastStepOrder)
                    {
                        Db.Execute(conn, tx,
                            "UPDATE Tickets SET status = 'Completed', closed_at = NOW() WHERE ticket_id = @ticket",
                            Db.P("@ticket", ticketId));
                        message = "Your request has been completed";
                    }
                    else
                    {
                        AssignOfficer(conn, tx, ticketId, step.StepOrder + 1, officerPersonId);
                        message = "Your request has moved to the next step";
                    }
                    break;

                case "RETURN":
                    Db.Execute(conn, tx,
                        "UPDATE Steps SET status = 'Returned', remarks = @remarks WHERE step_id = @step",
                        Db.P("@remarks", remarks),
                        Db.P("@step", step.StepId));
                    Db.Execute(conn, tx,
                        "UPDATE Tickets SET status = 'Returned' WHERE ticket_id = @ticket",
                        Db.P("@ticket", ticketId));
                    message = "Please correct your request: " + remarks;
                    break;

                case "REJECT":
                    Db.Execute(conn, tx,
                        "UPDATE Steps SET status = 'Rejected', remarks = @remarks WHERE step_id = @step",
                        Db.P("@remarks", remarks),
                        Db.P("@step", step.StepId));
                    Db.Execute(conn, tx,
                        "UPDATE Tickets SET status = 'Rejected', closed_at = NOW() WHERE ticket_id = @ticket",
                        Db.P("@ticket", ticketId));
                    message = "Your request was rejected: " + remarks;
                    break;

                default:
                    tx.Rollback();
                    return Result.Fail("Please choose Approve, Return or Reject");
            }

            CommonService.InsertLog(conn, tx, ticketId, step.StepId, officerId, officerPersonId, decision, remarks);
            CommonService.CreateNotification(conn, tx, CommonService.CitizenOf(conn, tx, ticketId), ticketId, message);

            tx.Commit();
            return Result.Ok("Decision saved");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The decision could not be saved");
        }
    }

    // FR11 – Resubmit a Returned Ticket (Report §3.4 ResubmitTicket)
    public static Result ResubmitTicket(int citizenId, int ticketId, string newDetails)
    {
        DataRow? ticket = Db.QueryRow(
            "SELECT * FROM Tickets WHERE ticket_id = @ticket AND citizen_id = @citizen",
            Db.P("@ticket", ticketId),
            Db.P("@citizen", citizenId));

        if (ticket == null || Convert.ToString(ticket["status"]) != "Returned")
        {
            return Result.Fail("This ticket cannot be edited");
        }

        newDetails = newDetails.Trim();
        if (newDetails == "")
        {
            return Result.Fail("Details are required");
        }

        using MySqlConnection conn = Db.GetConnection();
        using MySqlTransaction tx = conn.BeginTransaction();
        try
        {
            Db.Execute(conn, tx,
                "UPDATE Tickets SET details = @details, status = 'In progress' WHERE ticket_id = @ticket",
                Db.P("@details", newDetails),
                Db.P("@ticket", ticketId));

            Step step = Step.FromRow(Db.QueryRow(conn, tx,
                "SELECT * FROM Steps WHERE ticket_id = @ticket AND status = 'Returned'",
                Db.P("@ticket", ticketId))!);

            // The step goes back to the SAME officer.
            Db.Execute(conn, tx, "UPDATE Steps SET status = 'Active' WHERE step_id = @step",
                Db.P("@step", step.StepId));

            CommonService.InsertLog(conn, tx, ticketId, step.StepId, step.OfficerId, citizenId,
                "RESUBMITTED", "Citizen corrected the request");
            CommonService.CreateNotification(conn, tx, CommonService.PersonOf(conn, tx, step.OfficerId!.Value),
                ticketId, "A corrected ticket is waiting");

            tx.Commit();
            return Result.Ok("Ticket resubmitted");
        }
        catch (Exception)
        {
            tx.Rollback();
            return Result.Fail("The ticket could not be resubmitted. Please try again");
        }
    }

    // FR10 – Search and Filter Tickets (Report §3.4 SearchTickets, Figure 5)
    // The role restriction is ALWAYS added, so users only see what they are allowed to see.
    // Pass "" or null to skip a filter.
    public static DataTable SearchTickets(string keyword, string status, string serviceType, DateTime? fromDate, DateTime? toDate)
    {
        string sql =
            @"SELECT t.ticket_id, t.ticket_no AS `Ticket No`, p.name AS `Citizen`, p.nic AS `NIC`,
                     t.service_type AS `Service Type`, t.status AS `Status`,
                     t.current_step AS `Current Step`, t.created_at AS `Created`, t.closed_at AS `Closed`
              FROM Tickets t
              JOIN Persons p ON p.person_id = t.citizen_id
              WHERE 1 = 1";
        var parameters = new List<MySqlParameter>();

        switch (Session.Role)
        {
            case "Citizen":
                sql += " AND t.citizen_id = @me";
                parameters.Add(Db.P("@me", Session.PersonId));
                break;
            case "Officer":
                sql += " AND t.ticket_id IN (SELECT ticket_id FROM Steps WHERE officer_id = @officer)";
                parameters.Add(Db.P("@officer", Session.OfficerId));
                break;
            case "Admin":
                break; // no restriction
            default:
                sql += " AND 1 = 0"; // not logged in: show nothing
                break;
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            sql += " AND (t.ticket_no LIKE @keyword OR p.name LIKE @keyword OR p.nic LIKE @keyword)";
            parameters.Add(Db.P("@keyword", "%" + keyword.Trim() + "%"));
        }
        if (!string.IsNullOrEmpty(status))
        {
            sql += " AND t.status = @status";
            parameters.Add(Db.P("@status", status));
        }
        if (!string.IsNullOrEmpty(serviceType))
        {
            sql += " AND t.service_type = @service";
            parameters.Add(Db.P("@service", serviceType));
        }
        if (fromDate != null && toDate != null)
        {
            // Whole days: from 00:00 on the first date up to the end of the last date.
            sql += " AND t.created_at >= @from AND t.created_at < @to";
            parameters.Add(Db.P("@from", fromDate.Value.Date));
            parameters.Add(Db.P("@to", toDate.Value.Date.AddDays(1)));
        }

        sql += " ORDER BY t.created_at DESC";
        return Db.Query(sql, parameters.ToArray());
    }

    // FR9 – Track Ticket Status: loads one ticket.
    // A citizen can only open their own tickets (NFR3).
    public static Ticket? GetTicket(int ticketId)
    {
        DataRow? row = Db.QueryRow(
            @"SELECT t.*, p.name, p.nic FROM Tickets t
              JOIN Persons p ON p.person_id = t.citizen_id
              WHERE t.ticket_id = @ticket",
            Db.P("@ticket", ticketId));
        if (row == null)
        {
            return null;
        }

        Ticket ticket = Ticket.FromRow(row);
        if (Session.Role == "Citizen" && ticket.CitizenId != Session.PersonId)
        {
            return null;
        }
        return ticket;
    }

    // FR9 – name of the current step and designation of the officer holding the active step.
    public static string GetCurrentStepText(int ticketId)
    {
        DataRow? row = Db.QueryRow(
            @"SELECT s.step_name, s.status, o.designation FROM Tickets t
              JOIN Steps s ON s.ticket_id = t.ticket_id AND s.step_order = t.current_step
              LEFT JOIN Officers o ON o.officer_id = s.officer_id
              WHERE t.ticket_id = @ticket",
            Db.P("@ticket", ticketId));
        if (row == null)
        {
            return "-";
        }

        string text = Convert.ToString(row["step_name"]) + " (" + Convert.ToString(row["status"]) + ")";
        if (row["designation"] is not DBNull)
        {
            text += " – handled by: " + Convert.ToString(row["designation"]);
        }
        return text;
    }

    // FR9 – history of completed steps (step name, result, remarks, date).
    public static DataTable GetCompletedSteps(int ticketId)
    {
        return Db.Query(
            @"SELECT step_order AS `Step`, step_name AS `Step Name`, status AS `Result`,
                     remarks AS `Remarks`, completed_at AS `Date`
              FROM Steps
              WHERE ticket_id = @ticket AND status IN ('Approved', 'Returned', 'Rejected')
              ORDER BY step_order",
            Db.P("@ticket", ticketId));
    }

    // FR6 – all steps of a ticket with their remarks (shown to the officer before deciding).
    public static DataTable GetAllSteps(int ticketId)
    {
        return Db.Query(
            @"SELECT s.step_order AS `Step`, s.step_name AS `Step Name`, s.status AS `Status`,
                     o.designation AS `Officer`, s.remarks AS `Remarks`, s.completed_at AS `Completed`
              FROM Steps s
              LEFT JOIN Officers o ON o.officer_id = s.officer_id
              WHERE s.ticket_id = @ticket
              ORDER BY s.step_order",
            Db.P("@ticket", ticketId));
    }
}
