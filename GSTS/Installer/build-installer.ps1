# Builds Installer\Output\GSTS-Setup.exe
#   1. Publishes GSTS as a self-contained Windows x64 app (no .NET install needed on the target PC)
#   2. Compiles GSTS.iss with Inno Setup 6
# Run from any folder:  powershell -ExecutionPolicy Bypass -File Installer\build-installer.ps1

$ErrorActionPreference = 'Stop'
$installerDir = $PSScriptRoot
$projectFile  = Join-Path $installerDir '..\GSTS\GSTS.csproj'
$publishDir   = Join-Path $installerDir 'publish'

Write-Host 'Publishing GSTS (Release, win-x64, self-contained)...'
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
dotnet publish $projectFile -c Release -r win-x64 --self-contained true -o $publishDir `
    -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
if ($LASTEXITCODE -ne 0) { throw 'dotnet publish failed.' }

$iscc = @(
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe",
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1

if (-not $iscc) {
    throw 'Inno Setup 6 was not found. Install it from https://jrsoftware.org/isdl.php (or: winget install JRSoftware.InnoSetup) and run this script again.'
}

Write-Host 'Compiling the installer...'
& $iscc (Join-Path $installerDir 'GSTS.iss')
if ($LASTEXITCODE -ne 0) { throw 'Inno Setup compilation failed.' }

Write-Host ''
Write-Host "Done: $(Join-Path $installerDir 'Output\GSTS-Setup.exe')"
