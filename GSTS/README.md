# Government Service Ticketing System (GSTS)

ITE1943 – ICT Project. A Windows desktop application that records every government service
request as a ticket, routes it through ordered processing steps handled by officers, keeps a
full audit log and notifies citizens of every change.

| Item | Technology |
|---|---|
| Language / GUI | C# with Windows Forms (.NET 10) |
| Database | MariaDB (the MySQL that comes with XAMPP) |
| Data access | ADO.NET with the `MySqlConnector` package |
| IDE | Visual Studio Community 2026 |

---

## 1. Setup (first time)

1. **Start the database**: open the XAMPP Control Panel and click **Start** next to **MySQL**.
2. **Create the database**: run the two scripts in `Database\` in this order:
   - `01_schema.sql` creates the `gsts` database and its 6 tables.
     **It deletes any existing `gsts` database.**
   - `02_seed.sql` adds the sample administrator, officers, citizens and service types.

   Choose **one** of these ways to run them:
   - **phpMyAdmin**: open http://localhost/phpmyadmin, go to the **Import** tab, choose the file and click **Import**. Do this for each file.
   - **Command line** (run from the `GSTS` folder):
     ```
     C:\xampp\mysql\bin\mysql.exe -u root < Database\01_schema.sql
     C:\xampp\mysql\bin\mysql.exe -u root < Database\02_seed.sql
     ```
3. **Run the application**: open `GSTS.sln` in Visual Studio and press **F5**.
   Or, from the `GSTS` folder: `dotnet run --project GSTS`

If your MySQL `root` user has a password, or MySQL uses a port other than 3306, change
`GSTS\AppConfig.cs`.

## 2. Default logins

| Role | Username | Password |
|---|---|---|
| Administrator | `admin` | `Admin@123` |
| Officer – Document Verification | `198512345678` (Kamal Perera) | `Officer@123` |
| Officer – Document Verification | `199023456789` (Nimali Silva) | `Officer@123` |
| Officer – Registrar Approval | `198034567890` (Sunil Fernando) | `Officer@123` |
| Officer – Field Inspection | `198745678901` (Ruwan Jayasinghe) | `Officer@123` |
| Officer – Divisional Secretary Approval | `197956789012` (Anoma Wijesinghe) | `Officer@123` |
| Officer – Planning Committee Approval | `198267890123` (Chaminda Bandara) | `Officer@123` |
| Citizen | `901234567V` (Saman Kumara) | `Citizen@123` |
| Citizen | `199512345670` (Dilini Rathnayake) | `Citizen@123` |

Citizens and officers log in with their **NIC number** as the username.

The sample service types and their steps are:

| Service type | Steps |
|---|---|
| Birth Certificate Copy | 1 Document Verification → 2 Registrar Approval |
| Business Licence Renewal | 1 Document Verification → 2 Field Inspection → 3 Divisional Secretary Approval |
| Building Permit | 1 Document Verification → 2 Field Inspection → 3 Planning Committee Approval |

## 3. Project structure (NFR7 – interface, business logic and database access are separate)

```
GSTS/
  GSTS.sln
  Database/
    01_schema.sql          the 6 tables of Report §3.0, constraints, append-only log triggers
    02_seed.sql            sample data
  GSTS/                    the Windows Forms project
    Program.cs             start-up, database check, global error handler (NFR5)
    AppConfig.cs           connection string
    Session.cs             the logged-in user
    Data/Db.cs             the ONLY class that runs SQL – always with parameters (NFR3)
    Helpers/               PasswordHasher (PBKDF2), Validator, Messages, UiHelper
    Models/                Person, Officer, Ticket, Step
    Services/              business logic = the pseudocode functions of Report §3.4
    Forms/                 one screen per form (each has a .Designer.cs for the Form Designer)
  DEVIATIONS.md            where and why the code differs from the report
```

**Forms contain no SQL.** A form reads the inputs, calls a service method and shows the
`Result` message. Services contain the logic, and `Db` talks to the database.

## 4. Traceability: requirement → code

| Req. | Pseudocode function | Code | Screen |
|---|---|---|---|
| FR1 | RegisterUser | `AuthService.RegisterUser` | RegisterForm |
| FR2 | Login | `AuthService.Login` (3 attempts counted in `LoginForm`) | LoginForm |
| FR3 | CreateTicket | `TicketService.CreateTicket` | NewTicketForm |
| FR4 | AssignOfficer | `TicketService.AssignOfficer` | (automatic) |
| FR5 | ViewAssignedTickets | `TicketService.ViewAssignedTickets` | OfficerDashboardForm |
| FR6 | ProcessStep | `TicketService.ProcessStep` | ProcessStepForm |
| FR7 | InsertLog | `CommonService.InsertLog` + DB triggers | (automatic) |
| FR8 | CreateNotification, ViewNotifications | `CommonService.CreateNotification`, `NotificationService` | NotificationsForm |
| FR9 | TrackTicket | `TicketService.GetTicket / GetCurrentStepText / GetCompletedSteps` | TicketDetailsForm |
| FR10 | SearchTickets | `TicketService.SearchTickets` | SearchTicketsForm |
| FR11 | ResubmitTicket | `TicketService.ResubmitTicket` | TicketDetailsForm |
| FR12 | ManageOfficer | `AdminService.AddOfficer / EditOfficer / DeactivateOfficer` | ManageOfficersForm |
| FR13 | ManageServiceSteps | `AdminService.AddTemplateStep / EditTemplateStep / RemoveTemplateStep` | ManageServiceStepsForm |
| FR14 | ReassignTicket | `AdminService.ReassignTicket` | ReassignTicketForm |
| FR15 | GenerateReport | `AdminService.GenerateReport` | ReportsForm |
| FR16 | ChangePassword, Logout | `AuthService.ChangePassword`, `Session.Clear` | ChangePasswordForm, dashboards |

## 5. How the requirements are met (non-functional)

| NFR | How |
|---|---|
| NFR1 Usability | Same layout on every screen, a label on every input, hints for NIC/phone/password formats, the report's messages |
| NFR3 Security | PBKDF2-SHA256 password hashes with a random salt; every query uses `@parameters`; menus depend on role; citizens can only open their own tickets |
| NFR4 Data integrity | Foreign keys, UNIQUE and CHECK constraints; every multi-table change runs in a `MySqlTransaction` with rollback |
| NFR5 Reliability | Database check at start-up and a global error handler – a stopped MySQL shows a clear message instead of crashing |
| NFR6 Auditability | `TicketOfficerLog` has BEFORE UPDATE / DELETE triggers that refuse changes; every entry has `logged_at` |
| NFR7 Maintainability | Forms / Services / Data folders; a comment above each method names its requirement |
| NFR9 Scalability | Indexes on ticket status, created date, step officer/status and notification person |
| NFR10 Cost | .NET, Visual Studio Community, XAMPP/MariaDB and MySqlConnector are all free |

## 6. Manual test walkthrough (follows Figures 1–8)

1. **Register (Fig. 1)**: register a new citizen. Try an empty field, the NIC `12345`, and an existing NIC (`901234567V`); each one shows a message.
2. **Login (Fig. 2)**: enter a wrong password 3 times. Login becomes disabled until the app is restarted.
3. **Create ticket (Fig. 3)**: as `901234567V`, create a *Business Licence Renewal* request. Note the reference number `GST-YYYY-000001`.
4. **Process (Fig. 4)**: log in as the Document Verification officer with that ticket (`198512345678`) and **Approve**. The ticket moves to Field Inspection.
5. **Return (Fig. 4)**: as `198745678901`, choose **Return for correction** with remarks.
6. **Resubmit (FR11)**: as the citizen, open the ticket, edit the details and click **Resubmit**. It goes back to the same officer.
7. Approve the remaining steps. The ticket becomes **Completed** and the citizen sees all the notifications.
8. **Search (Fig. 5)**: check that each role sees only its own tickets.
9. **Reassign (Fig. 6)**: as admin, reassign an open ticket. A wrong-step officer is refused.
10. **Reports (Fig. 7)**: try each report, an invalid date range, and an empty period.
11. **Officers and steps (Fig. 8)**: add, edit and deactivate an officer (an officer with active tickets cannot be deactivated). Add a step to a service type.
12. **NFR5**: stop MySQL in XAMPP and use the app. A clear message appears.

## 7. Troubleshooting

| Problem | Fix |
|---|---|
| "Cannot connect to the database" | Start MySQL in the XAMPP Control Panel |
| "Unknown database 'gsts'" | Run `Database\01_schema.sql` and `02_seed.sql` |
| "Access denied for user 'root'" | Put your MySQL password in `AppConfig.cs` |
| Visual Studio cannot open the project | Install the **.NET desktop development** workload (.NET 10) |
