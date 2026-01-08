#!/usr/bin/env pwsh

<#
.SYNOPSIS
Build script for AIGenManager with warning monitoring

.DESCRIPTION
This script builds the AIGenManager solution with warning monitoring, 
enabling developers to catch critical issues early during development.

.PARAMETER Configuration
Build configuration (Debug or Release). Default is Debug.

.PARAMETER WarningsAsErrors
Treat warnings as errors. Default is $true for Release, $false for Debug.

.PARAMETER WarningLevel
Warning level (0-4). Default is 4 (all warnings).

.EXAMPLE
.uild.ps1 -Configuration Release -WarningsAsErrors $true
Builds in Release mode with warnings as errors

.EXAMPLE
.uild.ps1 -Configuration Debug -WarningsAsErrors $false
Builds in Debug mode with warnings not as errors
#>

param(
    [Parameter(Mandatory=$false)]
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug",
    
    [Parameter(Mandatory=$false)]
    [bool]$WarningsAsErrors = $null,
    
    [Parameter(Mandatory=$false)]
    [ValidateRange(0, 4)]
    [int]$WarningLevel = 4
)

# Set default WarningsAsErrors if not specified
if ($WarningsAsErrors -eq $null) {
    $WarningsAsErrors = $Configuration -eq "Release"
}

# Define solution path and name
$SolutionName = "BerryAIGen.sln"
$SolutionPath = Join-Path -Path $PSScriptRoot -ChildPath $SolutionName

# Validate solution file exists
if (-not (Test-Path $SolutionPath)) {
    Write-Error "Solution file not found: $SolutionPath"
    exit 1
}

Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "AIGenManager Build Script with Warning Monitoring" -ForegroundColor Cyan
Write-Host "=================================================" -ForegroundColor Cyan
Write-Host "Configuration: $Configuration" -ForegroundColor Yellow
Write-Host "Warnings as Errors: $WarningsAsErrors" -ForegroundColor Yellow
Write-Host "Warning Level: $WarningLevel" -ForegroundColor Yellow
Write-Host "Solution: $SolutionPath" -ForegroundColor Yellow
Write-Host "=================================================" -ForegroundColor Cyan

# Build solution with warning monitoring
try {
    Write-Host "Restoring dependencies..." -ForegroundColor Green
    dotnet restore $SolutionPath
    
    Write-Host "Building solution..." -ForegroundColor Green
    
    $buildArgs = @(
        "build",
        $SolutionPath,
        "--configuration", $Configuration,
        "--warning-level", $WarningLevel,
        "--verbosity", "normal"
    )
    
    if ($WarningsAsErrors) {
        $buildArgs += "--warnaserror"
    }
    
    $buildOutput = dotnet @buildArgs 2>&1
    
    # Display build output
    Write-Host $buildOutput
    
    # Check for build errors
    if ($LASTEXITCODE -ne 0) {
        Write-Host "=================================================" -ForegroundColor Red
        Write-Host "BUILD FAILED with exit code: $LASTEXITCODE" -ForegroundColor Red
        Write-Host "=================================================" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    
    Write-Host "=================================================" -ForegroundColor Green
    Write-Host "BUILD SUCCEEDED!" -ForegroundColor Green
    
    # Count warnings
    $warningCount = ($buildOutput | Where-Object { $_ -match "warning\s+[A-Z0-9]+:" }).Count
    $errorCount = ($buildOutput | Where-Object { $_ -match "error\s+[A-Z0-9]+:" }).Count
    
    Write-Host "Warnings: $warningCount" -ForegroundColor Yellow
    Write-Host "Errors: $errorCount" -ForegroundColor Red
    Write-Host "=================================================" -ForegroundColor Green
    
    # If there are warnings, display them
    if ($warningCount -gt 0) {
        Write-Host "Warnings Summary:" -ForegroundColor Yellow
        Write-Host "-------------------------------------------------" -ForegroundColor Yellow
        $buildOutput | Where-Object { $_ -match "warning\s+[A-Z0-9]+:" } | ForEach-Object {
            Write-Host $_ -ForegroundColor Yellow
        }
        Write-Host "-------------------------------------------------" -ForegroundColor Yellow
    }
    
    exit 0
}
catch {
    Write-Host "=================================================" -ForegroundColor Red
    Write-Host "BUILD FAILED with exception: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "=================================================" -ForegroundColor Red
    exit 1
}