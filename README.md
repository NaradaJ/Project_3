# GSTS – Government Service Ticketing System

A desktop app that turns walk-in government office requests into trackable tickets, so every request has an ID, an owner, a defined set of steps and a full history.

Built as my first-year ICT project (ITE 1943) for the BIT programme at the **University of Moratuwa**.

## ✨ Features
- **Unique ticket IDs.** Every citizen request gets an ID like `GST-2026-000003`
- **Assignment and reassignment.** Tickets are assigned to an officer and can be handed over
- **Stage-based steps.** Officers define the required steps and documents in stages, marked compulsory or optional
- **Enforced progression.** A ticket can't move to the next stage while a compulsory step is still open
- **Peer review.** If another officer acted last, the next officer must **Approve** or **Flag/Change** first, and a flag notifies the officer who planned the work
- **Append-only activity log.** Every action and note is timestamped, and database triggers block edits and deletes
- **Live elapsed time.** The time since ticket creation is always on screen
- **Status lookup.** Search by ticket ID, citizen name or NIC to see status, handlers, steps and history

## 🛠 Tech stack
| Area | Choice |
|---|---|
| Language / UI | C# · Windows Forms · .NET 10 |
| Database | MariaDB (XAMPP) |
| Data access | ADO.NET with MySqlConnector |
| Security | PBKDF2 (SHA-256) password hashing, parameterised SQL |
| Architecture | Three layers: Forms → Services → Data |

## 📁 Project structure
```
GSTS/
├── Database/      01_schema.sql (6 tables + log triggers), 02_seed.sql (demo officers)
├── GSTS/          The application
│   ├── Data/      Db.cs: the only class that runs SQL
│   ├── Services/  Ticket, Step, Review, Log, Notification, Auth
│   ├── Models/    Person, Officer, Ticket, Step
│   ├── Helpers/   PasswordHasher, Validator, Messages, UiHelper
│   └── Forms/     Nine screens
└── Tests/         Console app with 88 automated checks
```

## 🚀 Getting started
**Requirements:** Windows 10 or 11, the .NET 10 SDK (or Visual Studio 2026 with .NET desktop development), and XAMPP.

1. Clone the repo and open the `GSTS` folder:
   ```bash
   git clone https://github.com/NaradaJ/Project_3.git
   ```
2. Start **MySQL** in the XAMPP Control Panel.
3. Import `Database/01_schema.sql`, then `Database/02_seed.sql`, in phpMyAdmin.
   ⚠️ `01_schema.sql` drops any existing `gsts` database.
4. If your MySQL root user has a password, or MySQL uses a port other than 3306, update the connection string in `GSTS/AppConfig.cs`.
5. Run the app: open `GSTS.sln` and press **F5**, or run:
   ```bash
   dotnet run --project GSTS
   ```

### Demo accounts
All seeded officers use the password `Officer@123`.

| Username | Officer | Designation |
|---|---|---|
| kamal | Kamal Perera | Development Officer |
| nimali | Nimali Silva | Development Officer |
| sunil | Sunil Fernando | Registrar |
| ruwan | Ruwan Jayasinghe | Technical Officer |
| anoma | Anoma Wijesinghe | Divisional Secretary |

## 🧪 Tests
With MySQL running:
```bash
dotnet run --project Tests
```
The tests build their own `gsts_test` database, so your data is never touched. Expected result: **88 passed, 0 failed**.

## 🔭 Limitations and future work
- Add a server-side API so PCs don't connect straight to the database
- Raise PBKDF2 iterations to OWASP's recommended 600,000
- Add admin screens for officer accounts and password changes
- Pause the clock while waiting on the citizen, and add target times and overdue alerts
- Add a citizen web portal or SMS updates

## 🎥 Demo
See the demo video on [LinkedIn](https://www.linkedin.com/feed/update/urn:li:ugcPost:7529235782184919040).

---
Built by **Narada Kasun** · [LinkedIn](https://www.linkedin.com/in/narada-kasun-27a407327)
