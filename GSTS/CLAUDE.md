# CLAUDE.md – Government Service Ticketing System (GSTS)

A student's ITE1943 ICT Project. The design report is **already submitted**; the code must
implement exactly what it describes, in simple C# the student can explain in a viva.
Source of truth: `docs/GSTS_Report.txt` (copy it here from the starter zip). Read the
relevant section before changing any requirement.

## Rules
1. Implement only FR1–FR16 / NFR1–NFR10. No email/SMS, payments, web front end, uploads or new roles.
2. Business methods keep the pseudocode names and messages (`RegisterUser`, `CreateTicket`,
   `AssignOfficer`, `ProcessStep`, `ResubmitTicket`, `SearchTickets`, `ReassignTicket`, `GenerateReport` …).
   Put a comment above each one naming its requirement, e.g. `// FR6 – … (Report §3.4, Figure 4)`.
3. Any change from the report goes in `DEVIATIONS.md` (what the report says, what the code does, why).
   If the report is unclear: ask, don't guess.
4. Only use APIs you are sure exist. Run `dotnet build` (0 errors, 0 warnings) before saying a change is done.

## Technology (keep it simple)
- C# Windows Forms, `net10.0-windows`, Visual Studio 2026.
- MariaDB 10.4 from XAMPP (`root`, no password, port 3306), database `gsts`.
- Plain ADO.NET with **MySqlConnector**, the only NuGet package. No EF, Dapper, DI or MVVM.
- Passwords: `Rfc2898DeriveBytes.Pbkdf2`, SHA-256, 16-byte salt, 100,000 iterations, `base64salt:base64hash`.

## Structure
- `Forms/` – screens only, **no SQL**. Every form has a designer-compatible `.Designer.cs`.
- `Services/` – business logic; return `Result.Ok(msg)` / `Result.Fail(msg)`.
- `Data/Db.cs` – the only place that runs SQL; always `Db.P("@name", value)` parameters.
- Multi-table changes: one `MySqlTransaction`, rollback in `catch`, as in the pseudocode.
- Errors: `Program.cs` has a global handler (`Messages.ShowException`) so the app never crashes (NFR5).

## Database (Database/01_schema.sql – 6 tables of Report §3.0)
Persons, Officers, Tickets, Steps (template rows: `ticket_id IS NULL`, status 'Template'),
TicketOfficerLog (append-only via triggers, `performed_by` column – D1), Notifications.
Ticket statuses: Open, In progress, Returned, Completed, Rejected.
Step statuses: Template, Pending, Waiting, Active, Approved, Returned, Rejected.
MariaDB date syntax: `DATEDIFF(end, start)`.
