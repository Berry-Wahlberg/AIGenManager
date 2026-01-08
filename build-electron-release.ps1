<#
.SYNOPSIS
Builds and packages the BerryAIGen.Electron application for release.

.DESCRIPTION
This script builds the Electron.NET application, packages it with electron-builder,
and creates a ready-to-use desktop application that can be launched by double-click.

.EXAMPLE
.uild-electron-release.ps1

.NOTES
Author: BerryAIGen Team
Date: 2026-01-09
#>

# Set script to exit on error
$ErrorActionPreference = "Stop"

# Define paths
$RootPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$ElectronProjectPath = Join-Path $RootPath "src\Presentation\Electron"
$OutputPath = Join-Path $RootPath "bin\Desktop"

# Display build information
Write-Host "============================================="
Write-Host "BerryAIGen Toolkit Electron Release Builder"
Write-Host "============================================="
Write-Host "Root Path: $RootPath"
Write-Host "Electron Project Path: $ElectronProjectPath"
Write-Host "Output Path: $OutputPath"
Write-Host "Timestamp: $(Get-Date)"
Write-Host "============================================="

# Create output directory if it doesn't exist
if (-not (Test-Path $OutputPath)) {
    Write-Host "Creating output directory: $OutputPath"
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
}

# Navigate to electron project directory
Write-Host "Navigating to Electron project directory..."
Set-Location $ElectronProjectPath

# Restore NuGet packages
Write-Host "Restoring NuGet packages..."
dotnet restore

# Clean previous builds
Write-Host "Cleaning previous builds..."
dotnet clean -c Release

# Build the application
Write-Host "Building application in Release mode..."
dotnet build -c Release

# Publish the application
Write-Host "Publishing application..."
dotnet publish -c Release -r win-x64 --self-contained true --output "bin\Publish"

# Package with electronize
Write-Host "Packaging with electronize..."
electronize build /target win

# Copy output to desktop directory
Write-Host "Copying output to desktop directory..."
if (Test-Path "../../../bin/Desktop") {
    Copy-Item -Path "../../../bin/Desktop/*" -Destination $OutputPath -Force -Recurse
}

# Display build completion
Write-Host "============================================="
Write-Host "BUILD COMPLETE!"
Write-Host "Output directory: $OutputPath"
Write-Host "Timestamp: $(Get-Date)"
Write-Host "============================================="
Write-Host "The application can now be launched by double-clicking the executable."