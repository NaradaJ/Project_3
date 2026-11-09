using System.Data;
using GSTS.Data;
using GSTS.Models;
using MySqlConnector;

namespace GSTS.Services;

// F2 – Requirement verification and step definition (FR4, Report §3.4.2, Flowchart 2)
// F3 – Step completion and progression (FR5, Report §3.4.3, Flowchart 3)
public static class StepService
{
    public const int MaxRequirement = 500;
    public const int MaxDescription = 200;

    // ---------------------------------------------------------------------
    // F2 – Verify requirement and define steps
    // ---------------------------------------------------------------------

    // INPUT and SAVE the requirement verified with the citizen.
    public static Result SaveVerifiedRequirement(int officerId, int ticketId, string requirement, string note)
    {
        requirement = requirement.Trim();
        note = note.Trim();
        if (requirement == "")
        {
            return Result.Fail("Please enter the verified requirement");
        }
        if (requirement.Length > MaxRequirement || note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The requirement or note is too long");
        }

        return InTicketTransaction(officerId, ticketId, (conn, tx, ticket) =>
        {
            Db.Execute(conn, tx,
                "UPDATE Tickets SET verified_requirement = @req WHERE ticket_id = @ticket",
                Db.P("@req", requirement),
                Db.P("@ticket", ticketId));

            LogService.WriteLog(conn, tx, ticketId, officerId, LogService.RequirementVerified,
                LogService.WithNote(requirement, note));
            return Result.Ok("Verified requirement saved");
        });
    }

    // FR4 – ADD a step or document, with its stage and whether it is compulsory.
    public static Result AddStep(int officerId, int ticketId, string description, int stage, bool isCompulsory, string note)
    {
        description = description.Trim();
        note = note.Trim();
        string? problem = CheckStepInput(description, note);
        if (problem != null)
        {
            return Result.Fail(problem);
        }

        return InTicketTransaction(officerId, ticketId, (conn, tx, ticket) =>
        {
            if (stage < ticket.CurrentStage)
            {
                return Result.Fail("The stage must be " + ticket.CurrentStage + " or later (the ticket is already in stage " + ticket.CurrentStage + ")");
            }

            // next_order = 1 + the highest step_order of this ticket (1 if there are no steps yet)
            int nextOrder = 1 + Convert.ToInt32(Db.Scalar(conn, tx,
                "SELECT COALESCE(MAX(step_order), 0) FROM Steps WHERE ticket_id = @ticket",
                Db.P("@ticket", ticketId)));

            Db.Insert(conn, tx,
                @"INSERT INTO Steps (ticket_id, stage, step_order, description, is_compulsory, status, defined_by)
                  VALUES (@ticket, @stage, @order, @desc, @compulsory, 'Pending', @officer)",
                Db.P("@ticket", ticketId),
                Db.P("@stage", stage),
                Db.P("@order", nextOrder),
                Db.P("@desc", description),
                Db.P("@compulsory", isCompulsory),
                Db.P("@officer", officerId));

            // The first officer who defines steps is the originating officer of the workflow;
            // FR8 notifications go to them (report design point 5).
            if (ticket.WorkflowDefinedBy == null)
            {
                Db.Execute(conn, tx,
                    "UPDATE Tickets SET workflow_defined_by = @officer WHERE ticket_id = @ticket",
                    Db.P("@officer", officerId),
                    Db.P("@ticket", ticketId));
            }

            LogService.WriteLog(conn, tx, ticketId, officerId, LogService.StepAdded,
                LogService.WithNote(StepLabel(stage, description, isCompulsory), note));
            return Result.Ok("Step added");
        });
    }

    // FR4 – EDIT a step's description, stage and compulsory flag.
    public static Result EditStep(int officerId, int stepId, string description, int stage, bool isCompulsory, string note)
    {
        description = description.Trim();
        note = note.Trim();
        string? problem = CheckStepInput(description, note);
        if (problem != null)
        {
            return Result.Fail(problem);
        }

        Step? step = GetStep(stepId);
        if (step == null)
        {
            return Result.Fail("Step not found");
        }

        return InTicketTransaction(officerId, step.TicketId, (conn, tx, ticket) =>
        {
            Step current = LoadStep(conn, tx, stepId)!;
            if (current.Status != "Pending")
            {
                return Result.Fail("Only a pending step can be edited");
            }
            if (stage < ticket.CurrentStage)
            {
                return Result.Fail("The stage must be " + ticket.CurrentStage + " or later (the ticket is already in stage " + ticket.CurrentStage + ")");
            }

            Db.Execute(conn, tx,
                @"UPDATE Steps SET description = @desc, stage = @stage, is_compulsory = @compulsory
                  WHERE step_id = @step",
                Db.P("@desc", description),
                Db.P("@stage", stage),
                Db.P("@compulsory", isCompulsory),
                Db.P("@step", stepId));

            LogService.WriteLog(conn, tx, ticket.TicketId, officerId, LogService.StepEdited,
                LogService.WithNote(StepLabel(current.Stage, current.Description, current.IsCompulsory)
                    + " changed to " + StepLabel(stage, description, isCompulsory), note));
            return Result.Ok("Step updated");
        });
    }

    // FR4 – REORDER: move a step up or down among the steps of the same stage.
    public static Result MoveStep(int officerId, int stepId, bool up, string note)
    {
        note = note.Trim();
        if (note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        Step? step = GetStep(stepId);
        if (step == null)
        {
            return Result.Fail("Step not found");
        }

        return InTicketTransaction(officerId, step.TicketId, (conn, tx, ticket) =>
        {
            Step current = LoadStep(conn, tx, stepId)!;

            // The neighbouring step in the same stage, above or below this one.
            DataRow? neighbourRow = Db.QueryRow(conn, tx,
                up
                    ? @"SELECT * FROM Steps WHERE ticket_id = @ticket AND stage = @stage AND step_order < @order
                        ORDER BY step_order DESC LIMIT 1"
                    : @"SELECT * FROM Steps WHERE ticket_id = @ticket AND stage = @stage AND step_order > @order
                        ORDER BY step_order ASC LIMIT 1",
                Db.P("@ticket", current.TicketId),
                Db.P("@stage", current.Stage),
                Db.P("@order", current.StepOrder));

            if (neighbourRow == null)
            {
                return Result.Fail(up ? "This step is already first in its stage" : "This step is already last in its stage");
            }

            // Swap the two step_order values.
            Step neighbour = Step.FromRow(neighbourRow);
            Db.Execute(conn, tx, "UPDATE Steps SET step_order = @order WHERE step_id = @step",
                Db.P("@order", neighbour.StepOrder), Db.P("@step", current.StepId));
            Db.Execute(conn, tx, "UPDATE Steps SET step_order = @order WHERE step_id = @step",
                Db.P("@order", current.StepOrder), Db.P("@step", neighbour.StepId));

            LogService.WriteLog(conn, tx, ticket.TicketId, officerId, LogService.StepReordered,
                LogService.WithNote("Moved " + (up ? "up: " : "down: ") + current.Description, note));
            return Result.Ok("Step moved");
        });
    }

    // ---------------------------------------------------------------------
    // F3 – Complete steps and progress the ticket
    // ---------------------------------------------------------------------

    // RECORD that a step of the current stage is completed.
    public static Result CompleteStep(int officerId, int stepId, string note)
    {
        return SetStepDone(officerId, stepId, note, skip: false);
    }

    // RECORD that an optional step of the current stage is skipped.
    // Compulsory steps cannot be skipped (FR5).
    public static Result SkipStep(int officerId, int stepId, string note)
    {
        return SetStepDone(officerId, stepId, note, skip: true);
    }

    private static Result SetStepDone(int officerId, int stepId, string note, bool skip)
    {
        note = note.Trim();
        if (note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        Step? step = GetStep(stepId);
        if (step == null)
        {
            return Result.Fail("Step not found");
        }

        return InTicketTransaction(officerId, step.TicketId, (conn, tx, ticket) =>
        {
            Step current = LoadStep(conn, tx, stepId)!;
            if (current.Status != "Pending")
            {
                return Result.Fail("This step is already " + current.Status.ToLower());
            }
            if (current.Stage != ticket.CurrentStage)
            {
                return Result.Fail("This step is in stage " + current.Stage + ". The ticket is in stage " + ticket.CurrentStage);
            }
            if (skip && current.IsCompulsory)
            {
                return Result.Fail("A compulsory step cannot be skipped");
            }

            Db.Execute(conn, tx,
                @"UPDATE Steps SET status = @status, completed_by = @officer, completed_at = NOW()
                  WHERE step_id = @step",
                Db.P("@status", skip ? "Skipped" : "Completed"),
                Db.P("@officer", officerId),
                Db.P("@step", stepId));

            LogService.WriteLog(conn, tx, ticket.TicketId, officerId,
                skip ? LogService.StepSkipped : LogService.StepCompleted,
                LogService.WithNote(StepLabel(current.Stage, current.Description, current.IsCompulsory), note));
            return Result.Ok(skip ? "Step skipped" : "Step completed");
        });
    }

    // FR5 – Progress the ticket (Report §3.4.3, Flowchart 3).
    // IF any compulsory step in the current stage is incomplete THEN do not allow progression.
    // ELSE allow progression to the next stage; IF all required steps are complete THEN Completed.
    public static Result ProgressTicket(int officerId, int ticketId, string note)
    {
        note = note.Trim();
        if (note.Length > TicketService.MaxNote)
        {
            return Result.Fail("The note is too long");
        }

        return InTicketTransaction(officerId, ticketId, (conn, tx, ticket) =>
        {
            long stepCount = Convert.ToInt64(Db.Scalar(conn, tx,
                "SELECT COUNT(*) FROM Steps WHERE ticket_id = @ticket", Db.P("@ticket", ticketId)));
            if (stepCount == 0)
            {
                return Result.Fail("Please define the steps for this ticket first");
            }

            DataTable pending = Db.Query(conn, tx,
                @"SELECT description FROM Steps
                  WHERE ticket_id = @ticket AND stage = @stage AND is_compulsory = TRUE AND status = 'Pending'
                  ORDER BY step_order",
                Db.P("@ticket", ticketId),
                Db.P("@stage", ticket.CurrentStage));

            if (pending.Rows.Count > 0)
            {
                // DISPLAY the remaining compulsory steps; keep the current stage.
                var names = pending.Rows.Cast<DataRow>().Select(r => "• " + Convert.ToString(r["description"]));
                return Result.Fail("Cannot progress. Compulsory steps in stage " + ticket.CurrentStage
                    + " are still incomplete:\n" + string.Join("\n", names));
            }

            object? nextStage = Db.Scalar(conn, tx,
                "SELECT MIN(stage) FROM Steps WHERE ticket_id = @ticket AND stage > @stage",
                Db.P("@ticket", ticketId),
                Db.P("@stage", ticket.CurrentStage));

            if (nextStage != null)
            {
                // ALLOW progression to the next stage.
                int next = Convert.ToInt32(nextStage);
                Db.Execute(conn, tx,
                    "UPDATE Tickets SET current_stage = @next WHERE ticket_id = @ticket",
                    Db.P("@next", next),
                    Db.P("@ticket", ticketId));

                LogService.WriteLog(conn, tx, ticketId, officerId, LogService.StageProgressed,
                    LogService.WithNote("Stage " + ticket.CurrentStage + " to stage " + next, note));
                return Result.Ok("The ticket has moved to stage " + next);
            }

            // All required steps are complete: SET status Completed and completed_at.
            Db.Execute(conn, tx,
                "UPDATE Tickets SET status = 'Completed', completed_at = NOW() WHERE ticket_id = @ticket",
                Db.P("@ticket", ticketId));
            DateTime completedAt = Convert.ToDateTime(Db.Scalar(conn, tx,
                "SELECT completed_at FROM Tickets WHERE ticket_id = @ticket", Db.P("@ticket", ticketId)));

            LogService.WriteLog(conn, tx, ticketId, officerId, LogService.TicketCompleted, note);

            // CALCULATE elapsed time = completed_at - created_at (FR10)
            string elapsed = LogService.FormatElapsed(LogService.ElapsedTime(ticket.CreatedAt, completedAt));
            return Result.Ok("Ticket completed in " + elapsed);
        });
    }

    // ---------------------------------------------------------------------
    // Reading steps
    // ---------------------------------------------------------------------

    // FR4, FR11 – the steps of a ticket in stage and sequence order.
    // Compulsory steps are marked with * (Report 1.4 / FR4).
    public static DataTable GetSteps(int ticketId)
    {
        return Db.Query(
            @"SELECT step_id, stage AS `Stage`,
                     CONCAT(description, IF(is_compulsory, ' *', '')) AS `Step / Document`,
                     IF(is_compulsory, 'Compulsory', 'Optional') AS `Type`,
                     status AS `Status`
              FROM Steps WHERE ticket_id = @ticket
              ORDER BY stage, step_order",
            Db.P("@ticket", ticketId));
    }

    public static Step? GetStep(int stepId)
    {
        using var conn = Db.GetConnection();
        return LoadStep(conn, null, stepId);
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static Step? LoadStep(MySqlConnection conn, MySqlTransaction? tx, int stepId)
    {
        DataRow? row = Db.QueryRow(conn, tx, "SELECT * FROM Steps WHERE step_id = @step", Db.P("@step", stepId));
        return row == null ? null : Step.FromRow(row);
    }

    private static string? CheckStepInput(string description, string note)
    {
        if (description == "")
        {
            return "Description is required";
        }
        if (description.Length > MaxDescription || note.Length > TicketService.MaxNote)
        {
            return "The description or note is too long";
        }
        return null;
    }

    // e.g. "Stage 1: Original birth certificate *"
    private static string StepLabel(int stage, string description, bool isCompulsory)
    {
        return "Stage " + stage + ": " + description + (isCompulsory ? " *" : "");
    }

    // Runs one change to a ticket inside a transaction: locks the ticket, checks that the
    // officer may change it (FR6), runs the change, and commits only when it succeeds.
    private static Result InTicketTransaction(int officerId, int ticketId,
        Func<MySqlConnection, MySqlTransaction, Ticket, Result> change)
    {
        using var conn = Db.GetConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            Ticket? ticket = TicketService.LoadTicket(conn, tx, ticketId, forUpdate: true);
            string? problem = TicketService.CheckEditable(conn, tx, ticket, officerId);
            if (problem != null)
            {
                tx.Rollback();
                return Result.Fail(problem);
            }

            Result result = change(conn, tx, ticket!);
            if (result.Success)
            {
                tx.Commit();
            }
            else
            {
                tx.Rollback();
            }
            return result;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }
}
