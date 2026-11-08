; =====================================================================
; Government Service Ticketing System (GSTS) - Windows installer
; Built with Inno Setup 6 (free): https://jrsoftware.org/isinfo.php
;
; Build it with Installer\build-installer.ps1, which first publishes the
; application into Installer\publish and then compiles this script.
;
; The installer:
;   1. installs GSTS (self-contained: no separate .NET install needed),
;   2. asks for the database connection and writes gsts.config,
;   3. optionally creates the gsts database with the sample data,
;   4. adds Start menu / desktop shortcuts and an uninstaller.
; =====================================================================

#define AppName      "Government Service Ticketing System"
#define AppShortName "GSTS"
#define AppVersion   "1.0.0"
#define AppExe       "GSTS.exe"

[Setup]
AppId={{6F2B8C1E-4A7D-4E2B-9C51-3D8A0F7E4B21}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher=ITE1943 ICT Project
DefaultDirName={autopf}\{#AppShortName}
DefaultGroupName={#AppShortName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=GSTS-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#AppExe}

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Shortcuts:"
Name: "createdb"; Description: "Create the GSTS database with the sample data (an existing gsts database is DELETED)"; GroupDescription: "Database:"

[Files]
Source: "publish\*"; DestDir: "{app}"; Excludes: "*.pdb"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\Database\*.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "..\README.md"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#AppShortName}"; Filename: "{app}\{#AppExe}"
Name: "{group}\Uninstall {#AppShortName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppShortName}"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "Start {#AppShortName} now"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: "{app}\gsts.config"

[Code]
var
  DbPage: TInputQueryWizardPage;
  MySqlPage: TInputFileWizardPage;

// Page 1: database connection. Page 2: where mysql.exe is (used only to create the database).
procedure InitializeWizard;
begin
  DbPage := CreateInputQueryPage(wpSelectTasks,
    'Database connection',
    'Where is the GSTS database?',
    'GSTS stores its data in MariaDB/MySQL (XAMPP). Keep the defaults if XAMPP runs on ' +
    'this computer with user "root" and no password. Start MySQL in the XAMPP Control ' +
    'Panel before you continue.');
  DbPage.Add('Server:', False);
  DbPage.Add('Port:', False);
  DbPage.Add('User name:', False);
  DbPage.Add('Password (leave empty if none):', True);
  DbPage.Values[0] := 'localhost';
  DbPage.Values[1] := '3306';
  DbPage.Values[2] := 'root';
  DbPage.Values[3] := '';

  MySqlPage := CreateInputFilePage(DbPage.ID,
    'MySQL client',
    'Where is mysql.exe?',
    'Setup uses the MySQL command-line client from XAMPP to create the gsts database.');
  MySqlPage.Add('mysql.exe:', 'mysql.exe|mysql.exe', '.exe');
  MySqlPage.Values[0] := 'C:\xampp\mysql\bin\mysql.exe';
end;

// The mysql.exe page is only needed when the "create database" task is ticked.
function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = MySqlPage.ID) and not WizardIsTaskSelected('createdb');
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if CurPageID = DbPage.ID then
  begin
    if (Trim(DbPage.Values[0]) = '') or (StrToIntDef(Trim(DbPage.Values[1]), 0) <= 0) or
       (Trim(DbPage.Values[2]) = '') then
    begin
      MsgBox('Please enter the server, a valid port number and the user name.', mbError, MB_OK);
      Result := False;
    end;
  end
  else if CurPageID = MySqlPage.ID then
  begin
    if not FileExists(MySqlPage.Values[0]) then
    begin
      MsgBox('mysql.exe was not found at:' + #13#10 + MySqlPage.Values[0] + #13#10#13#10 +
             'Choose the file inside your XAMPP folder (mysql\bin\mysql.exe).', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

// Writes the connection string that GSTS reads at start-up (AppConfig.cs).
procedure WriteConfigFile;
var
  Lines: TArrayOfString;
begin
  SetArrayLength(Lines, 2);
  Lines[0] := '# GSTS database connection (written by the installer). Edit and restart GSTS to change it.';
  Lines[1] := 'Server=' + Trim(DbPage.Values[0]) + ';Port=' + Trim(DbPage.Values[1]) +
              ';Database=gsts;User ID=' + Trim(DbPage.Values[2]) +
              ';Password=' + DbPage.Values[3] + ';';
  SaveStringsToFile(ExpandConstant('{app}\gsts.config'), Lines, False);
end;

// Runs one .sql file with mysql.exe. Returns True when mysql.exe exits with code 0.
function RunSqlFile(SqlFile: String): Boolean;
var
  Params: String;
  ResultCode: Integer;
begin
  // mysql treats "\" as the start of a command, so the path must use "/".
  StringChangeEx(SqlFile, '\', '/', True);
  Params := '--host=' + Trim(DbPage.Values[0]) + ' --port=' + Trim(DbPage.Values[1]) +
            ' --user=' + Trim(DbPage.Values[2]);
  if DbPage.Values[3] <> '' then
    Params := Params + ' "--password=' + DbPage.Values[3] + '"';
  // "source" takes the rest of the line as the file name, so spaces in the path are fine.
  Params := Params + ' "--execute=source ' + SqlFile + '"';

  Result := Exec(MySqlPage.Values[0], Params, '', SW_HIDE, ewWaitUntilTerminated, ResultCode)
            and (ResultCode = 0);
end;

procedure CreateDatabase;
var
  Folder: String;
begin
  Folder := ExpandConstant('{app}\Database\');
  WizardForm.StatusLabel.Caption := 'Creating the GSTS database...';
  if RunSqlFile(Folder + '01_schema.sql') and RunSqlFile(Folder + '02_seed.sql') then
    Exit;

  MsgBox('The database could not be created.' + #13#10#13#10 +
         'Check that MySQL is started in the XAMPP Control Panel and that the user name and ' +
         'password are correct. You can create it later by importing these two files in ' +
         'phpMyAdmin (http://localhost/phpmyadmin):' + #13#10#13#10 +
         Folder + '01_schema.sql' + #13#10 + Folder + '02_seed.sql', mbError, MB_OK);
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    WriteConfigFile;
    if WizardIsTaskSelected('createdb') then
      CreateDatabase;
  end;
end;
