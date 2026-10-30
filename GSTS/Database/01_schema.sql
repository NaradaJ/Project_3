-- =====================================================================
-- Government Service Ticketing System (GSTS) - Database schema
-- Database: MariaDB (XAMPP)            Report reference: Section 3.0
--
-- Creates the six tables described in the report:
--   Persons, Officers, Tickets, Steps, TicketOfficerLog, Notifications
--
-- WARNING: this script DROPS and recreates the gsts database.
-- Run it in phpMyAdmin (Import tab) or with:
--   C:\xampp\mysql\bin\mysql.exe -u root < 01_schema.sql
-- =====================================================================

DROP DATABASE IF EXISTS gsts;
CREATE DATABASE gsts CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE gsts;

-- ---------------------------------------------------------------------
-- Persons: basic details and login information of every user
-- (citizens, officers and administrators).
-- Citizens and officers log in with their NIC as the username.
-- ---------------------------------------------------------------------
CREATE TABLE Persons (
    person_id      INT AUTO_INCREMENT PRIMARY KEY,
    name           VARCHAR(100) NOT NULL,
    nic            VARCHAR(12)  NOT NULL,
    contact        VARCHAR(10)  NOT NULL,
    address        VARCHAR(255) NULL,          -- officers/admins may have no address
    username       VARCHAR(50)  NOT NULL,
    password_hash  VARCHAR(255) NOT NULL,      -- PBKDF2: base64salt:base64hash
    role           VARCHAR(10)  NOT NULL,
    is_active      TINYINT(1)   NOT NULL DEFAULT 1,
    created_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT uq_persons_nic      UNIQUE (nic),
    CONSTRAINT uq_persons_username UNIQUE (username),
    CONSTRAINT chk_persons_role    CHECK (role IN ('Citizen', 'Officer', 'Admin'))
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- Officers: officer-specific details and the person record they belong to.
-- handles_step = the name of the processing step this officer handles.
-- ---------------------------------------------------------------------
CREATE TABLE Officers (
    officer_id    INT AUTO_INCREMENT PRIMARY KEY,
    person_id     INT          NOT NULL,
    designation   VARCHAR(100) NOT NULL,
    department    VARCHAR(100) NOT NULL,
    handles_step  VARCHAR(100) NOT NULL,
    is_active     TINYINT(1)   NOT NULL DEFAULT 1,
    CONSTRAINT uq_officers_person UNIQUE (person_id),
    CONSTRAINT fk_officers_person FOREIGN KEY (person_id) REFERENCES Persons (person_id)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- Tickets: one row per service request.
-- ---------------------------------------------------------------------
CREATE TABLE Tickets (
    ticket_id     INT AUTO_INCREMENT PRIMARY KEY,
    ticket_no     VARCHAR(20)  NOT NULL,          -- format GST-YYYY-000001
    citizen_id    INT          NOT NULL,
    service_type  VARCHAR(100) NOT NULL,
    details       TEXT         NOT NULL,
    status        VARCHAR(15)  NOT NULL DEFAULT 'Open',
    current_step  INT          NULL,
    created_at    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    closed_at     DATETIME     NULL,
    CONSTRAINT uq_tickets_no      UNIQUE (ticket_no),
    CONSTRAINT fk_tickets_citizen FOREIGN KEY (citizen_id) REFERENCES Persons (person_id),
    CONSTRAINT chk_tickets_status CHECK (status IN ('Open', 'In progress', 'Returned', 'Completed', 'Rejected'))
) ENGINE = InnoDB;

CREATE INDEX ix_tickets_status  ON Tickets (status);
CREATE INDEX ix_tickets_created ON Tickets (created_at);

-- ---------------------------------------------------------------------
-- Steps: the ordered processing steps of each ticket.
-- Template rows (Report 3.0): ticket_id IS NULL and status = 'Template'.
-- They hold the standard step list of each service type.
-- ---------------------------------------------------------------------
CREATE TABLE Steps (
    step_id       INT AUTO_INCREMENT PRIMARY KEY,
    ticket_id     INT          NULL,               -- NULL = template row
    service_type  VARCHAR(100) NOT NULL,
    step_order    INT          NOT NULL,
    step_name     VARCHAR(100) NOT NULL,
    officer_id    INT          NULL,
    status        VARCHAR(10)  NOT NULL,
    remarks       TEXT         NULL,
    started_at    DATETIME     NULL,
    completed_at  DATETIME     NULL,
    CONSTRAINT uq_steps_ticket_order UNIQUE (ticket_id, step_order),
    CONSTRAINT fk_steps_ticket  FOREIGN KEY (ticket_id)  REFERENCES Tickets (ticket_id),
    CONSTRAINT fk_steps_officer FOREIGN KEY (officer_id) REFERENCES Officers (officer_id),
    CONSTRAINT chk_steps_order  CHECK (step_order > 0),
    CONSTRAINT chk_steps_status CHECK (status IN ('Template', 'Pending', 'Waiting', 'Active',
                                                  'Approved', 'Returned', 'Rejected'))
) ENGINE = InnoDB;

CREATE INDEX ix_steps_officer_status ON Steps (officer_id, status);
CREATE INDEX ix_steps_service        ON Steps (service_type);

-- ---------------------------------------------------------------------
-- TicketOfficerLog: an entry for every action taken on a ticket (audit trail).
-- performed_by = the person who did the action (see DEVIATIONS.md, D1).
-- ---------------------------------------------------------------------
CREATE TABLE TicketOfficerLog (
    log_id        INT AUTO_INCREMENT PRIMARY KEY,
    ticket_id     INT          NULL,
    step_id       INT          NULL,
    officer_id    INT          NULL,
    performed_by  INT          NOT NULL,
    action        VARCHAR(30)  NOT NULL,
    remarks       TEXT         NULL,
    logged_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_log_ticket    FOREIGN KEY (ticket_id)    REFERENCES Tickets (ticket_id),
    CONSTRAINT fk_log_step      FOREIGN KEY (step_id)      REFERENCES Steps (step_id),
    CONSTRAINT fk_log_officer   FOREIGN KEY (officer_id)   REFERENCES Officers (officer_id),
    CONSTRAINT fk_log_performer FOREIGN KEY (performed_by) REFERENCES Persons (person_id)
) ENGINE = InnoDB;

-- FR7 / NFR6: the log is append-only. Any UPDATE or DELETE is refused.
CREATE TRIGGER trg_log_no_update BEFORE UPDATE ON TicketOfficerLog
FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'TicketOfficerLog is append-only: entries cannot be changed';

CREATE TRIGGER trg_log_no_delete BEFORE DELETE ON TicketOfficerLog
FOR EACH ROW
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'TicketOfficerLog is append-only: entries cannot be deleted';

-- ---------------------------------------------------------------------
-- Notifications: messages sent to users when a ticket changes.
-- ---------------------------------------------------------------------
CREATE TABLE Notifications (
    notification_id  INT AUTO_INCREMENT PRIMARY KEY,
    person_id        INT          NOT NULL,
    ticket_id        INT          NULL,
    message          VARCHAR(500) NOT NULL,
    is_read          TINYINT(1)   NOT NULL DEFAULT 0,
    created_at       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_notif_person FOREIGN KEY (person_id) REFERENCES Persons (person_id),
    CONSTRAINT fk_notif_ticket FOREIGN KEY (ticket_id) REFERENCES Tickets (ticket_id)
) ENGINE = InnoDB;

CREATE INDEX ix_notif_person ON Notifications (person_id, is_read);
