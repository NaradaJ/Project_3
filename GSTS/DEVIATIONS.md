# Deviations from the submitted report

The implementation follows the requirements (FR1–FR16), the pseudocode (§3.4) and the
6-table design (§3.0) of the report. This file lists every place where the code differs,
with the reason. Most are small fixes for cases the pseudocode does not cover.

| ID | Report says | Implementation | Why |
|---|---|---|---|
| **T1** | §1.5 / NFR1 / NFR8: Python with Tkinter | C# with Windows Forms (.NET 10). The database is still MariaDB on XAMPP, as in the report | Visual Studio's form designer is also a visual programming environment; same requirements, flowcharts, pseudocode and tables |
| **T2** | NFR8: runs on any Windows PC with XAMPP; the connection settings are fixed (localhost, root, no password) | A Windows installer (`Installer/GSTS.iss`, Inno Setup) installs a self-contained build, asks for the database connection, writes it to `gsts.config` next to `GSTS.exe`, and can create the database from the two SQL scripts. `AppConfig` reads `gsts.config` and falls back to the XAMPP defaults when the file is missing | So the evaluators can install the app without Visual Studio or the .NET SDK, and point it at a MySQL server that has a password or runs on another port. No requirement or behaviour changes |
| **D1** | `InsertLog(ticket, step, officer_id, …)`. ManageOfficer and ReassignTicket pass the admin's id as `officer_id` | `TicketOfficerLog` has an extra column `performed_by` (FK → Persons) recording who did the action. `officer_id` holds the officer concerned | Administrators are not in the Officers table, so their id in `officer_id` would break the foreign key |
| **D2** | AssignOfficer sets the step to `Waiting` when no officer is available | `current_step` is still updated. When an officer is **added or edited**, waiting steps for their step name are assigned automatically (`TicketService.AssignWaitingSteps`) | Without this, a Waiting ticket would never move |
| **D3** | §3.0: template rows are held in Steps | Steps has a `service_type` column; template rows have `ticket_id = NULL` and `status = 'Template'` | Needed to tell which service type a template row belongs to |
| **D4** | GenerateTicketNo = COUNT + 1 | Same formula, plus a UNIQUE constraint on `ticket_no` | If two tickets are saved at exactly the same moment, one gets the pseudocode's message "The ticket could not be saved. Please try again" instead of a duplicate number |
| **D5** | ManageOfficer ADD inputs: name, nic, contact, designation, department, handles_step | Also asks for an **initial password**. The officer's username is their NIC (as for citizens) | Without a password the officer could not log in |
| **D6** | DEACTIVATE sets `Officers.is_active = FALSE` | Also sets `Persons.is_active = FALSE` | A deactivated officer should not be able to log in |
| **D7** | CreateTicket copies template steps with their `step_order` | Ticket steps are numbered 1, 2, 3 … in template order | If an admin removes a template step (e.g. order 2), there would be a gap and "next step = order + 1" would fail |
| **D8** | EDIT: update the officer | Changing the step an officer handles is refused while they hold active tickets ("Reassign this officer's tickets first") | Otherwise an officer could hold a ticket for a step they no longer handle |
| **D9** | FR7 lists "view"; the pseudocode does not log it | Opening a ticket in ProcessStepForm logs `VIEWED` | FR7 requires it |
| **D10** | `created_at BETWEEN from_date AND to_date` | `created_at >= from AND created_at < to + 1 day` | `BETWEEN` on a date-time would leave out most of the last day |
| **D11** | Not covered | CreateTicket refuses a service type with no steps; ReassignTicket says "This ticket has no active step to reassign" when the ticket is Waiting/Returned | Prevent errors in cases the pseudocode does not mention |
| **D12** | ProcessStep reads the step, then saves | The step is read again inside the transaction (`FOR UPDATE`) | The same step cannot be processed twice (e.g. two clicks) |
| **D13** | Login: after 3 failures "EXIT" | The login button is disabled for the rest of the session (the app stays open showing the message) | Same effect as the flowchart: login disabled for this session |
