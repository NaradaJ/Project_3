using System.Data;
using GSTS;
using GSTS.Data;
using GSTS.Models;
using GSTS.Services;
using MySqlConnector;

// =====================================================================
// GSTS automated checks
// Runs the real service code against a SEPARATE test database (gsts_test),
// which is dropped and recreated from Database/01_schema.sql and 02_seed.sql.
// Your real gsts database is never touched.
//
// Run from the GSTS folder:   dotnet run --project Tests
// Optional: set GSTS_TEST_SERVER to a server/connection prefix,
//   e.g. "Server=localhost;Port=3306;User ID=root;Password=;"
// =====================================================================

const string TestDb = "gsts_test";
string server = Environment.GetEnvironmentVariable("GSTS_TEST_SERVER")
                ?? "Server=localhost;Port=3306;User ID=root;Password=;";

int passed = 0, failed = 0;

void Check(string name, bool condition)
{
    if (condition) { passed++; Console.WriteLine("  PASS  " + name); }
    else { failed++; Console.WriteLine("  FAIL  " + name); }
}

void Section(string title) => Console.WriteLine("\n" + title);

// ---------------------------------------------------------------------
// Create the test database from the project's own SQL scripts.
// ---------------------------------------------------------------------
string dbFolder = FindDatabaseFolder();
string schema = File.ReadAllText(Path.Combine(dbFolder, "01_schema.sql"))
    .Replace("DROP DATABASE IF EXISTS gsts;", "DROP DATABASE IF EXISTS " + TestDb + ";")
    .Replace("CREATE DATABASE gsts ", "CREATE DATABASE " + TestDb + " ")
    .Replace("USE gsts;", "USE " + TestDb + ";");
string seed = File.ReadAllText(Path.Combine(dbFolder, "02_seed.sql"))
    .Replace("USE gsts;", "USE " + TestDb + ";");

using (var setup = new MySqlConnection(server))
{
    setup.Open();
    foreach (string script in new[] { schema, seed })
    {
        using var cmd = new MySqlCommand(script, setup);
        cmd.ExecuteNonQuery();
    }
}
AppConfig.ConnectionString = server + "Database=" + TestDb + ";";
Console.WriteLine("Test database '" + TestDb + "' created from 01_schema.sql and 02_seed.sql");

// Officer ids from 02_seed.sql
const int Kamal = 1, Nimali = 2, Sunil = 3;

// ---------------------------------------------------------------------
Section("Sign-in (pre-registered officer accounts)");
Officer? kamal = AuthService.Login("kamal", "Officer@123");
Check("correct username and password signs in", kamal != null && kamal.OfficerId == Kamal);
Check("wrong password is refused", AuthService.Login("kamal", "wrong-password") == null);
Check("unknown username is refused", AuthService.Login("nobody", "Officer@123") == null);
Check("every seeded officer can sign in with Officer@123",
    new[] { "kamal", "nimali", "sunil", "ruwan", "anoma" }.All(u => AuthService.Login(u, "Officer@123") != null));

// ---------------------------------------------------------------------
Section("FR1, FR2, FR3 - Create and assign a ticket (Flowchart 1)");
Check("missing reason is refused",
    !TicketService.CreateTicket(Kamal, "Saman Kumara", "901234567V", "0712345678", " ", Kamal, "").Success);
Check("invalid ID number is refused",
    !TicketService.CreateTicket(Kamal, "Saman Kumara", "12345", "0712345678", "Birth certificate", Kamal, "").Success);
Check("invalid phone number is refused",
    !TicketService.CreateTicket(Kamal, "Saman Kumara", "901234567V", "12345", "Birth certificate", Kamal, "").Success);
Check("no officer selected: ticket creation is not completed",
    !TicketService.CreateTicket(Kamal, "Saman Kumara", "901234567V", "0712345678", "Birth certificate", null, "").Success);
Check("nothing was saved by the refused attempts",
    Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Tickets")) == 0);

Result created = TicketService.CreateTicket(Kamal, "Saman Kumara", "901234567v", "0712345678",
    "Copy of birth certificate", Nimali, "Citizen brought NIC");
Check("valid ticket is created", created.Success && created.Id != null);
int t1 = created.Id!.Value;
Ticket ticket1 = TicketService.GetTicket(t1)!;
Check("FR2 ticket ID has the form GST-YYYY-000001",
    ticket1.TicketNo == "GST-" + DateTime.Now.Year + "-" + t1.ToString().PadLeft(6, '0'));
Check("message shows the ticket ID", created.Message.Contains(ticket1.TicketNo));
Check("FR3 ticket is assigned to the selected officer", ticket1.AssignedOfficerId == Nimali);
Check("new ticket starts In Progress at stage 1", ticket1.Status == "In Progress" && ticket1.CurrentStage == 1);
Check("ID number is stored in upper case", ticket1.IdNumber == "901234567V");

DataTable log1 = TicketService.GetActivityLog(t1);
Check("creation and assignment are both logged",
    log1.Rows.Count == 2 && (string)log1.Rows[1]["Action"] == "CREATED" && (string)log1.Rows[0]["Action"] == "ASSIGNED");
Check("FR7 the creation note is kept in the log", Convert.ToString(log1.Rows[1]["Note"]) == "Citizen brought NIC");

Result created2 = TicketService.CreateTicket(Kamal, "Saman Kumara", "901234567V", "0779999999",
    "Business licence renewal", Kamal, "");
int t2 = created2.Id!.Value;
Check("FR2 a second ticket gets a different ticket ID", TicketService.GetTicket(t2)!.TicketNo != ticket1.TicketNo);
Check("a returning citizen is not duplicated", Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Persons")) == 1);
Check("a returning citizen's phone number is updated", TicketService.GetTicket(t2)!.PhoneNumber == "0779999999");

// ---------------------------------------------------------------------
Section("FR6 - Review before editing another officer's work (Flowchart 4)");
Check("Nimali (not the last actor) must review first", TicketService.NeedsReview(t1, Nimali));
Check("Kamal (the last actor) does not need to review", !TicketService.NeedsReview(t1, Kamal));
Result blocked = StepService.AddStep(Nimali, t1, "Original birth certificate", 1, true, "");
Check("an edit before the review is refused", !blocked.Success && blocked.Message.Contains("Approve or Flag/Change"));
Check("Nimali approves", ReviewService.ReviewTicket(Nimali, t1, approve: true, "Details look correct").Success);
Check("after approving, Nimali can edit", !TicketService.NeedsReview(t1, Nimali));
Check("FR7 the approval note is logged",
    Convert.ToString(TicketService.GetActivityLog(t1).Rows[0]["Note"]) == "Details look correct");

// ---------------------------------------------------------------------
Section("FR4 - Verify the requirement and define steps (Flowchart 2)");
Check("empty verified requirement is refused", !StepService.SaveVerifiedRequirement(Nimali, t1, "", "").Success);
Check("verified requirement is saved", StepService.SaveVerifiedRequirement(Nimali, t1, "Certified copy of birth certificate", "").Success);
Check("verified requirement is stored", TicketService.GetTicket(t1)!.VerifiedRequirement == "Certified copy of birth certificate");
Check("step without a description is refused", !StepService.AddStep(Nimali, t1, " ", 1, true, "").Success);
Check("compulsory step added to stage 1", StepService.AddStep(Nimali, t1, "Check NIC", 1, true, "").Success);
Check("optional step added to stage 1", StepService.AddStep(Nimali, t1, "Photocopy of old certificate", 1, false, "").Success);
Check("compulsory step added to stage 2", StepService.AddStep(Nimali, t1, "Registrar signature", 2, true, "").Success);
Check("originating officer of the workflow is recorded", TicketService.GetTicket(t1)!.WorkflowDefinedBy == Nimali);

DataTable steps = StepService.GetSteps(t1);
Check("steps are listed in stage order", steps.Rows.Count == 3 && Convert.ToInt32(steps.Rows[2]["Stage"]) == 2);
Check("compulsory steps are shown with *", ((string)steps.Rows[0]["Step / Document"]).EndsWith(" *"));
Check("optional steps are shown without *", !((string)steps.Rows[1]["Step / Document"]).EndsWith("*"));

int checkNic = Convert.ToInt32(steps.Rows[0]["step_id"]);
int photocopy = Convert.ToInt32(steps.Rows[1]["step_id"]);
int registrar = Convert.ToInt32(steps.Rows[2]["step_id"]);

Check("a step is edited", StepService.EditStep(Nimali, checkNic, "Check NIC and birth details", 1, true, "").Success);
Check("edited description is stored", StepService.GetStep(checkNic)!.Description == "Check NIC and birth details");
Check("moving the first step up is refused", !StepService.MoveStep(Nimali, checkNic, up: true, "").Success);
Check("a step is moved down", StepService.MoveStep(Nimali, checkNic, up: false, "").Success);
Check("the order has changed", Convert.ToInt32(StepService.GetSteps(t1).Rows[0]["step_id"]) == photocopy);
Check("the step is moved back up", StepService.MoveStep(Nimali, checkNic, up: true, "").Success);

// ---------------------------------------------------------------------
Section("FR5 - Complete steps and progress by stage (Flowchart 3)");
Result noSteps = StepService.ProgressTicket(Kamal, t2, "");
Check("progressing a ticket with no steps is refused", !noSteps.Success && noSteps.Message.Contains("define the steps"));
Check("a step in a later stage cannot be completed yet", !StepService.CompleteStep(Nimali, registrar, "").Success);
Check("a compulsory step cannot be skipped", !StepService.SkipStep(Nimali, checkNic, "").Success);
Result early = StepService.ProgressTicket(Nimali, t1, "");
Check("progression is blocked while a compulsory step is incomplete", !early.Success);
Check("the remaining compulsory steps are listed", early.Message.Contains("Check NIC and birth details"));
Check("the ticket stays in stage 1", TicketService.GetTicket(t1)!.CurrentStage == 1);
Check("the compulsory step is completed", StepService.CompleteStep(Nimali, checkNic, "NIC seen").Success);
Check("a completed step cannot be completed again", !StepService.CompleteStep(Nimali, checkNic, "").Success);
Check("a completed step cannot be edited", !StepService.EditStep(Nimali, checkNic, "x", 1, true, "").Success);
Result toStage2 = StepService.ProgressTicket(Nimali, t1, "");
Check("optional steps never block: the ticket moves to stage 2", toStage2.Success && TicketService.GetTicket(t1)!.CurrentStage == 2);
Check("a new step cannot be added to a stage already passed", !StepService.AddStep(Nimali, t1, "Late step", 1, false, "").Success);
Check("an optional step can be added to the current stage", StepService.AddStep(Nimali, t1, "Courier copy", 2, false, "").Success);
int courier = Convert.ToInt32(StepService.GetSteps(t1).Rows[3]["step_id"]);
Check("an optional step is skipped", StepService.SkipStep(Nimali, courier, "Citizen will collect").Success);
Check("the last compulsory step is completed", StepService.CompleteStep(Nimali, registrar, "").Success);
Result done = StepService.ProgressTicket(Nimali, t1, "All done");
Ticket finished = TicketService.GetTicket(t1)!;
Check("all required steps complete: the ticket is Completed", done.Success && finished.Status == "Completed");
Check("the completion time is recorded", finished.CompletedAt != null);
Check("FR10 the completion message shows the elapsed time", done.Message.StartsWith("Ticket completed in "));
Check("a completed ticket can no longer be changed", !StepService.AddStep(Nimali, t1, "x", 2, false, "").Success);
Check("a completed ticket needs no review", !TicketService.NeedsReview(t1, Kamal));

// ---------------------------------------------------------------------
Section("FR8 - Flag/Change notifies the originating officer");
Result t3r = TicketService.CreateTicket(Sunil, "Dilini Rathnayake", "199512345670", "0723456789",
    "Building permit", Sunil, "");
int t3 = t3r.Id!.Value;
StepService.AddStep(Sunil, t3, "Site plan", 1, true, "");
Check("Kamal must review Sunil's work", TicketService.NeedsReview(t3, Kamal));
Result flag = ReviewService.ReviewTicket(Kamal, t3, approve: false, "Site plan is missing the boundary");
Check("Kamal flags the workflow", flag.Success);
Check("the ticket status becomes Flagged", TicketService.GetTicket(t3)!.Status == "Flagged");
Check("the originating officer has 1 unread notification", NotificationService.UnreadCount(Sunil) == 1);
DataTable notes = NotificationService.ViewNotifications(Sunil);
Check("the notification includes the flagging note",
    notes.Rows.Count == 1 && ((string)notes.Rows[0]["Message"]).Contains("missing the boundary"));
NotificationService.MarkAsRead(Convert.ToInt32(notes.Rows[0]["notification_id"]), Kamal);
Check("an officer cannot mark someone else's notification as read", NotificationService.UnreadCount(Sunil) == 1);
NotificationService.MarkAsRead(Convert.ToInt32(notes.Rows[0]["notification_id"]), Sunil);
Check("the originating officer marks it as read", NotificationService.UnreadCount(Sunil) == 0);
Check("Sunil must now review Kamal's flag", TicketService.NeedsReview(t3, Sunil));
Check("Sunil approves after fixing: back to In Progress",
    ReviewService.ReviewTicket(Sunil, t3, true, "Boundary added").Success && TicketService.GetTicket(t3)!.Status == "In Progress");

// ---------------------------------------------------------------------
Section("FR3 - Reassignment");
Check("assigning to the current handler again is refused", !TicketService.AssignTicket(Sunil, t3, Sunil, "").Success);
Check("the ticket is reassigned to Kamal", TicketService.AssignTicket(Sunil, t3, Kamal, "On leave").Success
                                           && TicketService.GetTicket(t3)!.AssignedOfficerId == Kamal);

// ---------------------------------------------------------------------
Section("FR9 - The activity log is permanent");
bool updateRefused = false, deleteRefused = false;
try { Db.Execute("UPDATE TicketOfficerLog SET note = 'changed' WHERE ticket_id = @t", Db.P("@t", t1)); }
catch (MySqlException) { updateRefused = true; }
try { Db.Execute("DELETE FROM TicketOfficerLog WHERE ticket_id = @t", Db.P("@t", t1)); }
catch (MySqlException) { deleteRefused = true; }
Check("an UPDATE on the log is refused by the database", updateRefused);
Check("a DELETE on the log is refused by the database", deleteRefused);

// ---------------------------------------------------------------------
Section("FR10 - Elapsed time");
var start = new DateTime(2026, 1, 1, 8, 0, 0);
Check("completed ticket: completed_at - created_at",
    LogService.ElapsedTime(start, start.AddDays(2).AddHours(3).AddMinutes(15)) == new TimeSpan(2, 3, 15, 0));
Check("open ticket: current time - created_at", LogService.ElapsedTime(DateTime.Now.AddMinutes(-5), null) >= TimeSpan.FromMinutes(5));
Check("elapsed time is formatted as days, hours, minutes, seconds",
    LogService.FormatElapsed(new TimeSpan(2, 3, 15, 8)) == "2 d 03 h 15 min 08 s");

// ---------------------------------------------------------------------
Section("FR11 - Ticket status lookup (Flowchart 5)");
Check("found by its ticket ID", TicketService.FindTicket(ticket1.TicketNo.ToLower()) == t1);
Check("found by its number", TicketService.FindTicket(t1.ToString()) == t1);
Check("an unknown ticket ID is not found", TicketService.FindTicket("GST-1999-999999") == null);
DataTable handlers = TicketService.GetPreviousHandlers(t1);
Check("previous handlers: each officer once, with their first action",
    handlers.Rows.Count == 2 && (string)handlers.Rows[0]["Officer"] == "Kamal Perera"
    && (string)handlers.Rows[0]["First Action"] == "CREATED"
    && (string)handlers.Rows[1]["First Action"] == "APPROVED");
DataTable fullLog = TicketService.GetActivityLog(t1);
Check("activity log is newest first", (string)fullLog.Rows[0]["Action"] == "COMPLETED");
Check("lists show only open tickets for the officer",
    TicketService.GetMyTickets(Nimali).Rows.Count == 0 && TicketService.GetMyTickets(Kamal).Rows.Count == 2);

// ---------------------------------------------------------------------
Console.WriteLine("\n" + passed + " passed, " + failed + " failed");
return failed == 0 ? 0 : 1;

static string FindDatabaseFolder()
{
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "Database")))
    {
        dir = dir.Parent;
    }
    return dir == null
        ? throw new DirectoryNotFoundException("Could not find the Database folder")
        : Path.Combine(dir.FullName, "Database");
}
