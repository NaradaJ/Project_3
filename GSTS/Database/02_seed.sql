-- =====================================================================
-- GSTS - Sample data (run AFTER 01_schema.sql)
--   C:\xampp\mysql\bin\mysql.exe -u root < 02_seed.sql
--
-- Default logins (username / password):
--   Administrator : admin          / Admin@123
--   Officers      : <their NIC>    / Officer@123   (e.g. 198512345678)
--   Citizens      : <their NIC>    / Citizen@123   (e.g. 901234567V)
--
-- Passwords are stored as PBKDF2 hashes (base64salt:base64hash),
-- produced by GSTS.Helpers.PasswordHasher. Change them after first login.
-- =====================================================================

USE gsts;

-- ---------------------------------------------------------------------
-- Administrator (person_id 1)
-- ---------------------------------------------------------------------
INSERT INTO Persons (person_id, name, nic, contact, address, username, password_hash, role, is_active) VALUES
(1, 'System Administrator', '200000000001', '0112000000', NULL, 'admin',
 'VfoSfRurSBL8aSIlMhR9hQ==:wGEITlk6UZC507fhGc7L7VbBDoSSWXg/u17lcUxkJ/4=', 'Admin', 1);

-- ---------------------------------------------------------------------
-- Officers (person_id 2-7). Username = NIC.
-- ---------------------------------------------------------------------
INSERT INTO Persons (person_id, name, nic, contact, address, username, password_hash, role, is_active) VALUES
(2, 'Kamal Perera',      '198512345678', '0771000001', NULL, '198512345678',
 'WephJgiAaT81hVXTaledcg==:x8/dcc/+0GGneIhige7PBupuy22+eteYc8OOzibGIK8=', 'Officer', 1),
(3, 'Nimali Silva',      '199023456789', '0771000002', NULL, '199023456789',
 'tq5cjIkYS5+5FwOtonVeJQ==:oYrdz6Hv/gKMViue1kloDIChZQBxPMvBChJt6cun6FY=', 'Officer', 1),
(4, 'Sunil Fernando',    '198034567890', '0771000003', NULL, '198034567890',
 'x9F+2A+Dnye5wdVsrbBalg==:XPzXLr3FERBDWoUfX8lHPYw/+1/9T7wx/re4heOLA5U=', 'Officer', 1),
(5, 'Ruwan Jayasinghe',  '198745678901', '0771000004', NULL, '198745678901',
 'Z/K726sWDHeMDV579DpXEQ==:gb6owNl0IUHnkk5/I97Npw8voyx6d1OYSty8eSWgaf0=', 'Officer', 1),
(6, 'Anoma Wijesinghe',  '197956789012', '0771000005', NULL, '197956789012',
 'FaV58Xh8mox9aRY6VOYw9w==:sRyIjPUqfAlA0I7Hd0m2iBTWc91vs/0FxKdwvp32ifE=', 'Officer', 1),
(7, 'Chaminda Bandara',  '198267890123', '0771000006', NULL, '198267890123',
 'OWQAZENueztWH4kv07SJig==:iruMyweL5zIabEoOFtH4MvdaSt6NllYJnKXh+HhiKRI=', 'Officer', 1);

INSERT INTO Officers (officer_id, person_id, designation, department, handles_step, is_active) VALUES
(1, 2, 'Development Officer',  'Divisional Secretariat', 'Document Verification',          1),
(2, 3, 'Development Officer',  'Divisional Secretariat', 'Document Verification',          1),
(3, 4, 'Registrar',            'Registrar Office',       'Registrar Approval',             1),
(4, 5, 'Technical Officer',    'Field Services',         'Field Inspection',               1),
(5, 6, 'Divisional Secretary', 'Divisional Secretariat', 'Divisional Secretary Approval',  1),
(6, 7, 'Planning Officer',     'Planning Division',      'Planning Committee Approval',    1);

-- ---------------------------------------------------------------------
-- Citizens (person_id 8-9). Username = NIC.
-- ---------------------------------------------------------------------
INSERT INTO Persons (person_id, name, nic, contact, address, username, password_hash, role, is_active) VALUES
(8, 'Saman Kumara',      '901234567V',   '0712345678', '12 Temple Road, Kandy', '901234567V',
 '78uBXAQBojv+Xx3N48yrNw==:s0y3ap/w5llsW1F3Vaa8lFukVPoFQtKvrp+lPaweJYY=', 'Citizen', 1),
(9, 'Dilini Rathnayake', '199512345670', '0723456789', '45 Lake View, Colombo 05', '199512345670',
 'Fq5jU7uzc6PIOlwlHfo5gA==:9IWT0bVIglF2kDfLCmAJ39HlkPAoKdxDjJcHXVQ0mHQ=', 'Citizen', 1);

-- ---------------------------------------------------------------------
-- Service types and their standard steps = TEMPLATE rows in Steps
-- (ticket_id IS NULL, status = 'Template') - Report Section 3.0
-- ---------------------------------------------------------------------
INSERT INTO Steps (ticket_id, service_type, step_order, step_name, status) VALUES
(NULL, 'Birth Certificate Copy',   1, 'Document Verification',         'Template'),
(NULL, 'Birth Certificate Copy',   2, 'Registrar Approval',            'Template'),

(NULL, 'Business Licence Renewal', 1, 'Document Verification',         'Template'),
(NULL, 'Business Licence Renewal', 2, 'Field Inspection',              'Template'),
(NULL, 'Business Licence Renewal', 3, 'Divisional Secretary Approval', 'Template'),

(NULL, 'Building Permit',          1, 'Document Verification',         'Template'),
(NULL, 'Building Permit',          2, 'Field Inspection',              'Template'),
(NULL, 'Building Permit',          3, 'Planning Committee Approval',   'Template');
