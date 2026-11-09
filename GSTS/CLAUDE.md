# CLAUDE.md – Government Service Ticketing System (GSTS)

A student's ITE1943 ICT Project. The design report **E2510192 (Jayasuriya N.K.)** is already
submitted; the code must implement exactly what it describes, in simple C# the student can
explain in a viva. Read the relevant report section before changing any requirement.

## Rules
1. Implement only FR1–FR11 and the five NFRs of the submitted report. The app is used by
   officers only; officer accounts are pre-registered. No citizen login, admin screens,
   reports, SMS/email, payments or web front end.
2. Business methods follow the report's pseudocode (§3.4.1–§3.4.5). Put a comment above each one
   naming its requirement and flowchart, e.g. `// FR5 – Progress the ticket (Report §3.4.3, Flowchart 3)`.
3. Any decision the report does not settle goes in `DEVIATIONS.md`. If the report is unclear: ask, don't guess.
4. Only use APIs you are sure exist. Before saying a change is done:
   `dotnet build` (0 errors, 0 warnings) and `dotnet run --project Tests` (all checks pass).

## Technology (keep it simple)
- C# Windows Forms, `net10.0-windows`, Visual Studio 2026.
- MariaDB 10.4 from XAMPP (`root`, no password, port 3306), database `gsts`.
- Plain ADO.NET with **MySqlConnector**, the only NuGet package. No EF, Dapper, DI or MVVM.
- Passwords: `Rfc2898DeriveBytes.Pbkdf2`, SHA-256, 16-byte salt, 100,000 iterations, `base64salt:base64hash`.

## Structure
- `Forms/` – screens only, **no SQL**. Every form has a designer-compatible `.Designer.cs`.
- `Services/` – business logic, no Windows Forms code; return `Result.Ok(msg)` / `Result.Fail(msg)`.
  TicketService (F1, FR11), StepService (F2, F3), ReviewService (F4), LogService (helpers), NotificationService.
- `Data/Db.cs` – the only place that runs SQL; always `Db.P("@name", value)` parameters.
- A change and its log entry are saved in one `MySqlTransaction`, rolled back on failure.
- `Tests/` – console program that runs the services against a separate `gsts_test` database.

## Database (Database/01_schema.sql)
Persons (citizens), Officers (pre-registered), Tickets, Steps (stage, order, compulsory),
TicketOfficerLog (append-only via triggers), Notifications (officer to officer).
Ticket statuses: In Progress, Flagged, Completed. Step statuses: Pending, Completed, Skipped.
