# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the Apache 2.0 License
# See the LICENSE file in the project root for more information.

[CmdletBinding()]
param(
    [string]$ProjectDir = (Get-Location).Path,

    [string]$PackageRoot = "",

    [string]$OutputDirectory = ".bootstrapblazor",

    [switch]$Force
)

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($PackageRoot)) {
    $PackageRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
}
else {
    $PackageRoot = Resolve-Path $PackageRoot
}

$projectFullPath = [System.IO.Path]::GetFullPath($ProjectDir)
if (-not (Test-Path -LiteralPath $projectFullPath -PathType Container)) {
    throw "Project directory does not exist: $projectFullPath"
}

$templatePath = Join-Path $PackageRoot "agents\BootstrapBlazor.AGENTS.md"
if (-not (Test-Path -LiteralPath $templatePath)) {
    throw "BootstrapBlazor AGENTS template was not found: $templatePath"
}

$targetDirectory = Join-Path $projectFullPath $OutputDirectory
$targetPath = Join-Path $targetDirectory "BootstrapBlazor.AGENTS.md"

if ((Test-Path -LiteralPath $targetPath) -and -not $Force) {
    Write-Host "BootstrapBlazor Agent rules already exist: $targetPath"
    exit 0
}

New-Item -ItemType Directory -Force -Path $targetDirectory | Out-Null
Copy-Item -LiteralPath $templatePath -Destination $targetPath -Force:$Force
Write-Host "Installed BootstrapBlazor Agent rules: $targetPath"
Write-Host "Agent rules read BootstrapBlazor Skills from the installed NuGet package through obj/project.assets.json."
