-- =====================================================================
-- Government Service Ticketing System (GSTS) - Database schema
-- Database: MariaDB (XAMPP)
-- Design source: submitted design report E2510192 (Jayasuriya N.K.),
--   Chapter 3 and "Database design points to resolve before implementation".
--
-- Six tables: Persons, Officers, Tickets, Steps, TicketOfficerLog, Notifications
--
-- WARNING: this script DROPS and recreates the gsts database.
-- Run it in phpMyAdmin (Import tab) or with:
--   C:\xampp\mysql\bin\mysql.exe -u root < 01_schema.sql
-- =====================================================================

DROP DATABASE IF EXISTS gsts;
CREATE DATABASE gsts CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE gsts;

-- ---------------------------------------------------------------------
-- Persons: the citizen who brings a request (FR1).
-- Citizens do not use the system; an officer records their details.
-- ---------------------------------------------------------------------
CREATE TABLE Persons (
    person_id     INT AUTO_INCREMENT PRIMARY KEY,
    name          VARCHAR(100) NOT NULL,
    id_number     VARCHAR(12)  NOT NULL,
    phone_number  VARCHAR(10)  NOT NULL,
    CONSTRAINT uq_persons_id_number UNIQUE (id_number)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- Officers: the users of the system. Accounts are pre-registered
-- (Report 1.4) - they are created by 02_seed.sql, not by the application.
-- ---------------------------------------------------------------------
CREATE TABLE Officers (
    officer_id     INT AUTO_INCREMENT PRIMARY KEY,
    name           VARCHAR(100) NOT NULL,
    designation    VARCHAR(100) NOT NULL,
    username       VARCHAR(50)  NOT NULL,
    password_hash  VARCHAR(255) NOT NULL,      -- PBKDF2: base64salt:base64hash
    is_active      TINYINT(1)   NOT NULL DEFAULT 1,
    CONSTRAINT uq_officers_username UNIQUE (username)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- Tickets: one row per citizen request.
--   assigned_officer_id - the current handler (design point 1, FR3, FR11)
--   workflow_defined_by - the officer who first defined the steps;
--                         FR8 notifications go to this officer (design point 5)
--   current_stage       - the stage the ticket is in (design point 3, FR5)
-- ---------------------------------------------------------------------
CREATE TABLE Tickets (
    ticket_id             INT AUTO_INCREMENT PRIMARY KEY,
    ticket_no             VARCHAR(20)  NULL,           -- GST-YYYY-000001, set right after the insert (FR2)
    person_id             INT          NOT NULL,
    reason                VARCHAR(500) NOT NULL,       -- reason for visit, as first stated (FR1)
    verified_requirement  VARCHAR(500) NULL,           -- requirement verified with the citizen (Flowchart 2)
    status                VARCHAR(15)  NOT NULL DEFAULT 'In Progress',
    current_stage         INT          NOT NULL DEFAULT 1,
    assigned_officer_id   INT          NOT NULL,
    created_by            INT          NOT NULL,
    workflow_defined_by   INT          NULL,
    created_at            DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    completed_at          DATETIME     NULL,
    CONSTRAINT uq_tickets_no        UNIQUE (ticket_no),
    CONSTRAINT fk_tickets_person    FOREIGN KEY (person_id)           REFERENCES Persons (person_id),
    CONSTRAINT fk_tickets_assigned  FOREIGN KEY (assigned_officer_id) REFERENCES Officers (officer_id),
    CONSTRAINT fk_tickets_creator   FOREIGN KEY (created_by)          REFERENCES Officers (officer_id),
    CONSTRAINT fk_tickets_workflow  FOREIGN KEY (workflow_defined_by) REFERENCES Officers (officer_id),
    CONSTRAINT chk_tickets_status   CHECK (status IN ('In Progress', 'Flagged', 'Completed')),
    CONSTRAINT chk_tickets_stage    CHECK (current_stage > 0)
) ENGINE = InnoDB;

CREATE INDEX ix_tickets_assigned ON Tickets (assigned_officer_id, status);

-- ---------------------------------------------------------------------
-- Steps: the steps or documents needed for a ticket (FR4, FR5).
--   stage         - steps are grouped into stages (design point 3)
--   step_order    - the order of the steps inside the ticket
--   is_compulsory - compulsory steps are shown with * and block progression
--   status        - Pending, Completed, or Skipped (optional steps only)
--   defined_by    - the officer who added the step
-- ---------------------------------------------------------------------
CREATE TABLE Steps (
    step_id        INT AUTO_INCREMENT PRIMARY KEY,
    ticket_id      INT          NOT NULL,
    stage          INT          NOT NULL,
    step_order     INT          NOT NULL,
    description    VARCHAR(200) NOT NULL,
    is_compulsory  TINYINT(1)   NOT NULL,
    status         VARCHAR(10)  NOT NULL DEFAULT 'Pending',
    defined_by     INT          NOT NULL,
    completed_by   INT          NULL,
    completed_at   DATETIME     NULL,
    CONSTRAINT fk_steps_ticket     FOREIGN KEY (ticket_id)    REFERENCES Tickets (ticket_id),
    CONSTRAINT fk_steps_defined    FOREIGN KEY (defined_by)   REFERENCES Officers (officer_id),
    CONSTRAINT fk_steps_completed  FOREIGN KEY (completed_by) REFERENCES Officers (officer_id),
    CONSTRAINT chk_steps_stage     CHECK (stage > 0),
    CONSTRAINT chk_steps_status    CHECK (status IN ('Pending', 'Completed', 'Skipped'))
) ENGINE = InnoDB;

CREATE INDEX ix_steps_ticket ON Steps (ticket_id, stage, step_order);

-- ---------------------------------------------------------------------
-- TicketOfficerLog: every officer action on a ticket, with the officer,
-- an optional note and the time (FR7, FR9). Action names: see LogService.
-- ---------------------------------------------------------------------
CREATE TABLE TicketOfficerLog (
    log_id      INT AUTO_INCREMENT PRIMARY KEY,
    ticket_id   INT          NOT NULL,
    officer_id  INT          NOT NULL,
    action      VARCHAR(30)  NOT NULL,
    note        VARCHAR(1000) NULL,
    logged_at   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_log_ticket  FOREIGN KEY (ticket_id)  REFERENCES Tickets (ticket_id),
    CONSTRAINT fk_log_officer FOREIGN KEY (officer_id) REFERENCES Officers (officer_id)
) ENGINE = InnoDB;

CREATE INDEX ix_log_ticket ON TicketOfficerLog (ticket_id, log_id);

-- FR9 / NFR Data integrity: the log is append-only. Any UPDATE or DELETE is refused.
CREATE TRIGGER trg_log_no_update BEFORE UPDATE ON TicketOfficerLog
FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'TicketOfficerLog is append-only: entries cannot be changed';

CREATE TRIGGER trg_log_no_delete BEFORE DELETE ON TicketOfficerLog
FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'TicketOfficerLog is append-only: entries cannot be deleted';

-- ---------------------------------------------------------------------
-- Notifications: messages from one officer to another about a ticket (FR8).
--   needs_reply - 1 for a flag notification: the receiver accepts or denies
--                 the flag with a note (see DEVIATIONS.md D15)
--   replied_at  - when the receiver replied; a flag can be answered once
-- ---------------------------------------------------------------------
CREATE TABLE Notifications (
    notification_id  INT AUTO_INCREMENT PRIMARY KEY,
    ticket_id        INT           NOT NULL,
    to_officer_id    INT           NOT NULL,
    from_officer_id  INT           NOT NULL,
    message          VARCHAR(1200) NOT NULL,
    is_read          TINYINT(1)    NOT NULL DEFAULT 0,
    needs_reply      TINYINT(1)    NOT NULL DEFAULT 0,
    replied_at       DATETIME      NULL,
    created_at       DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_notif_ticket FOREIGN KEY (ticket_id)       REFERENCES Tickets (ticket_id),
    CONSTRAINT fk_notif_to     FOREIGN KEY (to_officer_id)   REFERENCES Officers (officer_id),
    CONSTRAINT fk_notif_from   FOREIGN KEY (from_officer_id) REFERENCES Officers (officer_id)
) ENGINE = InnoDB;

CREATE INDEX ix_notif_to ON Notifications (to_officer_id, is_read);
