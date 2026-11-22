-- =====================================================================
-- GSTS - upgrade an existing gsts database for flag replies (D15).
-- Only needed for a database created BEFORE this change; 01_schema.sql
-- already contains these columns. Existing data is kept.
-- Run it in phpMyAdmin (Import tab) or with:
--   C:\xampp\mysql\bin\mysql.exe -u root < 03_flag_replies.sql
-- =====================================================================

USE gsts;

ALTER TABLE Notifications
    ADD COLUMN IF NOT EXISTS needs_reply TINYINT(1) NOT NULL DEFAULT 0 AFTER is_read,
    ADD COLUMN IF NOT EXISTS replied_at  DATETIME   NULL AFTER needs_reply;

-- Earlier flag notifications on tickets that are still Flagged can now be answered.
UPDATE Notifications n
JOIN Tickets t ON t.ticket_id = n.ticket_id
SET n.needs_reply = 1
WHERE n.message LIKE 'Workflow flagged on ticket %'
  AND t.status = 'Flagged'
  AND n.replied_at IS NULL;
