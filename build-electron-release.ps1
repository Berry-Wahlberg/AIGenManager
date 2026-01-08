<#
.SYNOPSIS
Builds and packages the BerryAIGen.Electron application for release with enhanced features.

.DESCRIPTION
This script builds the Electron.NET application with comprehensive environment checks,
robust error handling, and optimized output structure. It creates a ready-to-use desktop
application that can be launched by double-click.

.EXAMPLE
.uild-electron-release.ps1

.NOTES
Author: BerryAIGen Team
Date: 2026-01-09
Version: 2.0.0
#>

# Set script configuration
$ErrorActionPreference = "Stop"
$LogFilePath = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Path) "build.log"
$MaxRetryAttempts = 3
$buildStartTime = Get-Date

# Create log file with header
"=============================================" > $LogFilePath
"BerryAIGen Toolkit Electron Release Builder" >> $LogFilePath
"=============================================" >> $LogFilePath
"Build Started: $(Get-Date)" >> $LogFilePath
"=============================================" >> $LogFilePath

# Function to log messages
function Write-Log {
    param (
        [string]$Message,
        [string]$Level = "INFO"
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logMessage = "[{0}] [{1}] {2}" -f $timestamp, $Level, $Message
    $logMessage | Tee-Object -FilePath $LogFilePath -Append | Write-Host
}

# Function to check if a command exists
function Test-CommandExists {
    param (
        [string]$Command
    )
    try {
        Get-Command $Command -ErrorAction Stop | Out-Null
        return $true
    }
    catch {
        return $false
    }
}

# Function to get version from a JSON file
function Get-VersionFromJson {
    param (
        [string]$FilePath,
        [string]$JsonProperty
    )
    try {
        $jsonContent = Get-Content -Path $FilePath -Raw | ConvertFrom-Json
        $version = $jsonContent
        foreach ($prop in $JsonProperty.Split('.')) {
            $version = $version.$prop
        }
        return $version
    }
    catch {
        Write-Log ("Failed to extract version from {0}: {1}" -f $FilePath, ${_}) "ERROR"
        return $null
    }
}

# Function to get version from a version.txt file
function Get-VersionFromTxt {
    param (
        [string]$FilePath
    )
    try {
        $content = Get-Content -Path $FilePath -Raw
        # Extract version number (handles formats like "v2.0.0" or just "2.0.0")
        if ($content -match 'v?(\d+\.\d+\.\d+)') {
            return $Matches[1]
        }
        return $null
    }
    catch {
        Write-Log ("Failed to extract version from {0}: {1}" -f $FilePath, ${_}) "ERROR"
        return $null
    }
}

# Function to verify version consistency across all files
function Test-VersionConsistency {
    param (
        [string]$PrimaryVersion
    )
    Write-Log "Verifying version consistency across all files..."
    
    $RootPath = Split-Path -Parent $MyInvocation.MyCommand.Path
    $filesToCheck = @(
        @{ Path = "src/Presentation/Electron/electron.manifest.json"; Type = "json"; Property = "build.buildVersion" }
        @{ Path = "src/Presentation/Wpf/version.txt"; Type = "txt"; Property = "" }
        @{ Path = "version.txt"; Type = "txt"; Property = "" }
    )
    
    $allConsistent = $true
    
    foreach ($file in $filesToCheck) {
        $fullPath = Join-Path $RootPath $file.Path
        if (Test-Path $fullPath) {
            if ($file.Type -eq "json") {
                $fileVersion = Get-VersionFromJson $fullPath $file.Property
            } else {
                $fileVersion = Get-VersionFromTxt $fullPath
            }
            
            if ($fileVersion -ne $PrimaryVersion) {
                Write-Log "Version inconsistency detected in $($file.Path): expected $PrimaryVersion, found $fileVersion" "ERROR"
                $allConsistent = $false
            } else {
                Write-Log "Version $PrimaryVersion verified in $($file.Path)" "INFO"
            }
        } else {
            Write-Log "File not found: $($file.Path)" "WARNING"
        }
    }
    
    return $allConsistent
}

# Function to check dependencies
function Test-Dependencies {
    Write-Log "Checking build dependencies..."
    
    $dependencies = @(
        @{ Name = ".NET SDK"; Command = "dotnet"; MinVersion = "8.0.100"; InstallLink = "https://dotnet.microsoft.com/download/dotnet/8.0" }
        @{ Name = "Node.js"; Command = "node"; MinVersion = "18.0.0"; InstallLink = "https://nodejs.org/en/download/" }
        @{ Name = "electronize"; Command = "electronize"; MinVersion = "23.6.2"; InstallLink = "dotnet tool install ElectronNET.CLI -g" }
    )
    
    $allDependenciesMet = $true
    
    foreach ($dep in $dependencies) {
        if (Test-CommandExists $dep.Command) {
            if ($dep.Command -eq "electronize") {
                $versionOutput = & $dep.Command version
                if ($versionOutput -match "(\d+\.\d+\.\d+)") {
                    $version = $Matches[1]
                    if ([Version]$version -ge [Version]$dep.MinVersion) {
                        Write-Log "✓ $($dep.Name) $version (meets requirement of $($dep.MinVersion))" "INFO"
                    } else {
                        Write-Log "✗ $($dep.Name) $version (requires minimum $($dep.MinVersion))" "ERROR"
                        $allDependenciesMet = $false
                    }
                }
            } else {
                $versionOutput = & $dep.Command --version 2>&1
                if ($versionOutput -match "(\d+\.\d+\.\d+)") {
                    $version = $Matches[1]
                    if ([Version]$version -ge [Version]$dep.MinVersion) {
                        Write-Log "✓ $($dep.Name) $version (meets requirement of $($dep.MinVersion))" "INFO"
                    } else {
                        Write-Log "✗ $($dep.Name) $version (requires minimum $($dep.MinVersion))" "ERROR"
                        $allDependenciesMet = $false
                    }
                }
            }
        } else {
            Write-Log "✗ $($dep.Name) not found" "ERROR"
            Write-Log "  Installation instructions: $($dep.InstallLink)" "ERROR"
            $allDependenciesMet = $false
        }
    }
    
    return $allDependenciesMet
}

# Function to clean up temporary files
function Cleanup-TempFiles {
    param (
        [string]$RootPath
    )
    Write-Log "Cleaning up temporary files..."
    
    $tempDirs = @(
        "$RootPath/bin",
        "$RootPath/src/Presentation/Electron/bin",
        "$RootPath/src/Presentation/Electron/obj"
    )
    
    foreach ($dir in $tempDirs) {
        if (Test-Path $dir) {
            try {
                Remove-Item -Path $dir -Recurse -Force | Out-Null
                Write-Log "Cleaned up: $dir" "INFO"
            }
            catch {
                Write-Log ("Failed to clean up {0}: {1}" -f $dir, ${_}) "WARNING"
            }
        }
    }
}

# Function to manage flag files
function Manage-FlagFiles {
    param (
        [string]$RootPath,
        [string]$OutputPath
    )
    Write-Log "Managing flag files..."
    
    # Define flag files to copy
    $flagFiles = @(
        "Berry_ICO.ico",
        "Berry_LOGO.png"
    )
    
    foreach ($flagFile in $flagFiles) {
        $sourcePath = Join-Path $RootPath $flagFile
        $destPath = Join-Path $OutputPath $flagFile
        
        if (Test-Path $sourcePath) {
            try {
                Copy-Item -Path $sourcePath -Destination $destPath -Force
                Write-Log "Copied flag file: $flagFile" "INFO"
            }
            catch {
                Write-Log ("Failed to copy flag file {0}: {1}" -f $flagFile, ${_}) "WARNING"
            }
        } else {
            Write-Log "Flag file not found: $flagFile" "WARNING"
        }
    }
}

# Main build process

# Step 1: Check dependencies
Write-Log "Starting dependency check..."
if (-not (Test-Dependencies)) {
    Write-Log "Build failed due to missing or incompatible dependencies" "ERROR"
    exit 1
}
Write-Log "All dependencies are met" "INFO"

# Step 2: Define paths with user's home directory for Resources
Write-Log "Setting up build paths..."
$RootPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$ElectronProjectPath = Join-Path $RootPath "src\Presentation\Electron"
$UserHome = [Environment]::GetFolderPath([Environment+SpecialFolder]::UserProfile)
$OutputPath = Join-Path $UserHome "Resources\BerryAIGen"
$ResourcesPath = Join-Path $OutputPath "resources"
$ImagesPath = Join-Path $ResourcesPath "images"
$LibrariesPath = Join-Path $ResourcesPath "libraries"
$ConfigPath = Join-Path $ResourcesPath "config"
$LocalesPath = Join-Path $ResourcesPath "locales"
$LogsPath = Join-Path $OutputPath "logs"

# Log paths for debugging
Write-Log "Root Path: $RootPath" "DEBUG"
Write-Log "Electron Project Path: $ElectronProjectPath" "DEBUG"
Write-Log "Output Path: $OutputPath" "DEBUG"
Write-Log "Resources Path: $ResourcesPath" "DEBUG"

# Step 3: Create output directory structure
Write-Log "Creating output directory structure..."
foreach ($dir in @($OutputPath, $ResourcesPath, $ImagesPath, $LibrariesPath, $ConfigPath, $LocalesPath, $LogsPath)) {
    if (-not (Test-Path $dir)) {
        try {
            New-Item -ItemType Directory -Path $dir -Force | Out-Null
            Write-Log "Created directory: $dir" "INFO"
        }
        catch {
            Write-Log ("Failed to create directory {0}: {1}" -f $dir, ${_}) "ERROR"
            exit 1
        }
    }
}

# Step 4: Verify version consistency
Write-Log "Checking version consistency..."
$primaryVersionFile = Join-Path $RootPath "version.txt"
$primaryVersion = Get-VersionFromTxt $primaryVersionFile

if (-not $primaryVersion) {
    Write-Log "Failed to determine primary version from version.txt" "ERROR"
    exit 1
}

if (-not (Test-VersionConsistency $primaryVersion)) {
    Write-Log "Build failed due to version inconsistency across files" "ERROR"
    exit 1
}
Write-Log "Version consistency verified: $primaryVersion" "INFO"

# Step 5: Navigate to electron project directory
Write-Log "Navigating to Electron project directory..."
Set-Location $ElectronProjectPath

# Step 6: Restore NuGet packages with retry logic
Write-Log "Restoring NuGet packages..."
$retryCount = 0
$restoreSuccess = $false

while ($retryCount -lt $MaxRetryAttempts -and -not $restoreSuccess) {
    try {
        dotnet restore 2>&1 | Tee-Object -FilePath $LogFilePath -Append
        $restoreSuccess = $true
    }
    catch {
        $retryCount++
        if ($retryCount -lt $MaxRetryAttempts) {
            Write-Log "NuGet restore failed. Retrying in 5 seconds... (Attempt $retryCount/$MaxRetryAttempts)" "WARNING"
            Start-Sleep -Seconds 5
        } else {
            Write-Log ("NuGet restore failed after {0} attempts: {1}" -f $MaxRetryAttempts, ${_}) "ERROR"
            exit 1
        }
    }
}

# Step 7: Clean previous builds
Write-Log "Cleaning previous builds..."
try {
    dotnet clean -c Release 2>&1 | Tee-Object -FilePath $LogFilePath -Append
} catch {
    Write-Log "Clean failed: $_" "WARNING"
    # Continue anyway, as this is not critical
}

# Step 8: Build the application with retry logic
Write-Log "Building application in Release mode..."
$retryCount = 0
$buildSuccess = $false

while ($retryCount -lt $MaxRetryAttempts -and -not $buildSuccess) {
    try {
        dotnet build -c Release 2>&1 | Tee-Object -FilePath $LogFilePath -Append
        $buildSuccess = $true
    }
    catch {
        $retryCount++
        if ($retryCount -lt $MaxRetryAttempts) {
            Write-Log "Build failed. Retrying in 5 seconds... (Attempt $retryCount/$MaxRetryAttempts)" "WARNING"
            Start-Sleep -Seconds 5
        } else {
            Write-Log ("Build failed after {0} attempts: {1}" -f $MaxRetryAttempts, ${_}) "ERROR"
            exit 1
        }
    }
}

# Step 9: Publish the application
Write-Log "Publishing application..."
try {
    dotnet publish -c Release -r win-x64 --self-contained true --output "bin\Publish" 2>&1 | Tee-Object -FilePath $LogFilePath -Append
} catch {
    Write-Log ("Publish failed: {0}" -f ${_}) "ERROR"
    exit 1
}

# Step 10: Package with electronize
Write-Log "Packaging with electronize..."
try {
    electronize build /target win 2>&1 | Tee-Object -FilePath $LogFilePath -Append
} catch {
    Write-Log ("Electron packaging failed: {0}" -f ${_}) "ERROR"
    exit 1
}

# Step 11: Copy output to final destination
Write-Log "Copying output to final destination..."
$electronOutputDir = Join-Path $RootPath "bin/Desktop"

if (Test-Path $electronOutputDir) {
    try {
        # Copy all files from electron output to final output directory
        Copy-Item -Path "$electronOutputDir/*" -Destination $OutputPath -Force -Recurse 2>&1 | Tee-Object -FilePath $LogFilePath -Append
        Write-Log "Successfully copied output to $OutputPath" "INFO"
    }
    catch {
        Write-Log ("Failed to copy output files: {0}" -f ${_}) "ERROR"
        exit 1
    }
} else {
    Write-Log "Electron output directory not found: $electronOutputDir" "ERROR"
    exit 1
}

# Step 12: Verify the executable exists
$executablePath = Join-Path $OutputPath "BerryAIGen.Electron.exe"
if (Test-Path $executablePath) {
    Write-Log "✓ Executable verified: $executablePath" "INFO"
    # Get file information
    $fileInfo = Get-Item $executablePath
    Write-Log "  Size: $([math]::Round($fileInfo.Length / 1MB, 2)) MB" "INFO"
    Write-Log "  Created: $($fileInfo.CreationTime)" "INFO"
    Write-Log "  Modified: $($fileInfo.LastWriteTime)" "INFO"
} else {
    Write-Log "✗ Executable not found at $executablePath" "ERROR"
    exit 1
}

# Step 13: Create version.txt in output directory
$outputVersionPath = Join-Path $OutputPath "version.txt"
try {
    "v$primaryVersion" | Out-File -FilePath $outputVersionPath -Force
    Write-Log "Created version.txt at $outputVersionPath with version $primaryVersion" "INFO"
} catch {
    Write-Log "Failed to create version.txt: $_" "WARNING"
}

# Step 14: Manage flag files
Manage-FlagFiles $RootPath $OutputPath

# Step 15: Cleanup temporary build files
Cleanup-TempFiles $RootPath

# Final build completion message
Write-Log "=============================================" "INFO"
Write-Log "BUILD COMPLETE!" "SUCCESS"
Write-Log "=============================================" "INFO"
Write-Log "Output directory: $OutputPath" "INFO"
Write-Log "Executable: $executablePath" "INFO"
Write-Log "Version: $primaryVersion" "INFO"
Write-Log "Build Duration: $((Get-Date) - $buildStartTime)" "INFO"
Write-Log "=============================================" "INFO"
Write-Log "The application can now be launched by double-clicking the executable." "INFO"
Write-Log "Build log saved to: $LogFilePath" "INFO"
Write-Log "=============================================" "INFO"