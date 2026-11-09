# Implementation decisions

The code implements the submitted design report **E2510192 (Jayasuriya N.K.)**:
FR1–FR11, the five non-functional requirements, Flowcharts 1–5 and the pseudocode in §3.4.

The report deliberately leaves some details open. Its last section, *"Database design points to
resolve before implementation"*, lists the gaps in the proposed data structure. This file records
how each open point was settled, and every other place where the code had to choose something
the report does not state.

## Points the report asked to be resolved

| ID | Report | Implementation |
|---|---|---|
| **R1** | Design point 1: represent the currently assigned officer | `Tickets.assigned_officer_id` holds the current handler (FR3, FR11) |
| **R2** | Design point 2: record assignment history, or at least log every assignment | Every assignment is logged as `ASSIGNED` with the new officer's name (FR3, FR9) |
| **R3** | Design point 3 and review finding 1: represent stages | Each step has a `stage` number and the ticket has a `current_stage`. A ticket moves to the next stage that has steps; after the last stage it is Completed (FR5, Flowchart 3) |
| **R4** | Design point 4 and review finding 3: log every auditable action | Log actions: `CREATED`, `ASSIGNED`, `REQUIREMENT_VERIFIED`, `STEP_ADDED`, `STEP_EDITED`, `STEP_REORDERED`, `STEP_COMPLETED`, `STEP_SKIPPED`, `STAGE_PROGRESSED`, `COMPLETED`, `APPROVED`, `FLAGGED` |
| **R5** | Design point 5: preserve the officer who defined the workflow | `Tickets.workflow_defined_by` = the first officer who adds a step. FR8 notifications go to this officer. If no step has been defined yet, the officer who created the ticket is notified |
| **R6** | Review finding 6: optional-step skipping | A skipped optional step has status `Skipped` and the skip is logged |
| **R7** | §1.4: GUI language and framework not finalised | C# Windows Forms (.NET 10) in Visual Studio, whose form designer is a visual programming environment. MariaDB through XAMPP, as the report states |

## Other decisions

| ID | Topic | Decision | Why |
|---|---|---|---|
| **D1** | Officer identity | Officers sign in with a username and password. Accounts are pre-registered by `02_seed.sql` (Report §1.4); the app has no registration screen | Every action must be logged with the officer's identity (FR9), so the app must know who is using it |
| **D2** | Ticket ID format (FR2) | `GST-YYYY-NNNNNN`, built from the AUTO_INCREMENT id, with a UNIQUE constraint | The ID can never repeat |
| **D3** | Citizen details (FR1) | A citizen is matched by ID number; a returning citizen's name and phone are updated. ID number and phone are checked for format only | Avoids duplicate citizens; real ID verification is out of scope (§1.4) |
| **D4** | "Available officer" (FR3) | Any active officer account | The report does not define availability further |
| **D5** | Review trigger (FR6) | Viewing a ticket is not logged, so opening a ticket or the status report does not change "the last officer who acted" | Otherwise simply looking at a ticket would force the next officer to review the viewer |
| **D6** | Flag/Change (FR6, FR8) | One choice, "Flag/Change", sets the status to `Flagged` and notifies the originating officer. The officer can then change the steps | The report presents Flag/Change as one option |
| **D7** | Clearing a flag | When the next officer chooses Approve, a `Flagged` ticket returns to `In Progress` | The report does not say how a flagged ticket continues |
| **D8** | Review rule is enforced twice | The screens become view-only until the review is done, and every service method also refuses edits | The rule holds even if a screen is changed later |
| **D9** | Stage rules (FR5) | Only steps of the current stage can be completed or skipped. A new or edited step cannot be placed in a stage the ticket has already passed. Only pending steps can be edited | Keeps the stage order meaningful |
| **D10** | Progressing with no steps | Refused: "Please define the steps for this ticket first" | Otherwise a ticket with no steps would be completed at once |
| **D11** | Completed tickets | Read-only; no review is asked | No further work happens on a completed ticket |
| **D12** | Status lookup (FR11) | One search box accepts the full ticket ID (`GST-2026-000001`) or just its number. If no ticket has that ID, it searches by the citizen's name or NIC. An empty search lists the completed tickets. When several tickets match, the officer picks one from a list | Completed tickets leave the dashboard lists, and a citizen may not have their ticket ID at hand |
| **D13** | Elapsed time (FR10) | Shown as days, hours, minutes and seconds and refreshed every second while a ticket or report is open | "Updating automatically while the ticket is open" |
| **D14** | Text limits | Reason and verified requirement 500 characters, step 200, note 1000. The notification column holds the full note | Values are never cut off silently |
