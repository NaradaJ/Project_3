-- =====================================================================
-- GSTS - Pre-registered officer accounts (run AFTER 01_schema.sql)
--   C:\xampp\mysql\bin\mysql.exe -u root < 02_seed.sql
--
-- The report assumes officer accounts are pre-registered (Report 1.4),
-- so they are created here rather than in the application.
--
-- Every officer's password is: Officer@123
-- Passwords are stored as PBKDF2 hashes (base64salt:base64hash),
-- produced by GSTS.Helpers.PasswordHasher.
-- =====================================================================

USE gsts;

INSERT INTO Officers (officer_id, name, designation, username, password_hash, is_active) VALUES
(1, 'Kamal Perera',     'Development Officer',  'kamal',
 'WephJgiAaT81hVXTaledcg==:x8/dcc/+0GGneIhige7PBupuy22+eteYc8OOzibGIK8=', 1),
(2, 'Nimali Silva',     'Development Officer',  'nimali',
 'tq5cjIkYS5+5FwOtonVeJQ==:oYrdz6Hv/gKMViue1kloDIChZQBxPMvBChJt6cun6FY=', 1),
(3, 'Sunil Fernando',   'Registrar',            'sunil',
 'x9F+2A+Dnye5wdVsrbBalg==:XPzXLr3FERBDWoUfX8lHPYw/+1/9T7wx/re4heOLA5U=', 1),
(4, 'Ruwan Jayasinghe', 'Technical Officer',    'ruwan',
 'Z/K726sWDHeMDV579DpXEQ==:gb6owNl0IUHnkk5/I97Npw8voyx6d1OYSty8eSWgaf0=', 1),
(5, 'Anoma Wijesinghe', 'Divisional Secretary', 'anoma',
 'FaV58Xh8mox9aRY6VOYw9w==:sRyIjPUqfAlA0I7Hd0m2iBTWc91vs/0FxKdwvp32ifE=', 1);
