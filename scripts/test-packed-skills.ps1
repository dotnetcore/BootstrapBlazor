# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the Apache 2.0 License
# See the LICENSE file in the project root for more information.

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$PackagePath,

    [string]$ExtractPath = "artifacts/skill-package-extract",

    [switch]$KeepExtracted
)

$ErrorActionPreference = "Stop"
$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $RepoRoot

function ConvertTo-LocalPath {
    param([Parameter(Mandatory = $true)][string]$Path)

    return $Path -replace "/", [System.IO.Path]::DirectorySeparatorChar
}

function Assert-WorkspacePath {
    param([Parameter(Mandatory = $true)][string]$Path)

    $fullPath = [System.IO.Path]::GetFullPath($Path)
    $rootPath = [System.IO.Path]::GetFullPath($RepoRoot)

    if (-not $rootPath.EndsWith([System.IO.Path]::DirectorySeparatorChar)) {
        $rootPath += [System.IO.Path]::DirectorySeparatorChar
    }

    if (-not $fullPath.StartsWith($rootPath, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to use path outside the repository: $fullPath"
    }

    return $fullPath
}

$packageFullPath = Resolve-Path $PackagePath
$extractFullPath = Assert-WorkspacePath $ExtractPath

if (Test-Path -LiteralPath $extractFullPath) {
    if (-not $KeepExtracted) {
        Remove-Item -LiteralPath $extractFullPath -Recurse -Force
    }
    else {
        throw "Extract path already exists: $extractFullPath"
    }
}

New-Item -ItemType Directory -Path $extractFullPath | Out-Null

Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::ExtractToDirectory($packageFullPath.Path, $extractFullPath)

$requiredPackageFiles = @(
    "skill-index.json",
    "agents/BootstrapBlazor.AGENTS.md",
    "buildTransitive/BootstrapBlazor.targets",
    "tools/install-bootstrapblazor-agents.ps1"
)

foreach ($requiredPackageFile in $requiredPackageFiles) {
    $requiredFullPath = Join-Path $extractFullPath (ConvertTo-LocalPath $requiredPackageFile)
    if (-not (Test-Path -LiteralPath $requiredFullPath)) {
        throw "Package does not contain required file: $requiredPackageFile"
    }
}

foreach ($unexpectedPackageFile in @("AGENTS.md", "CLAUDE.md")) {
    $unexpectedFullPath = Join-Path $extractFullPath (ConvertTo-LocalPath $unexpectedPackageFile)
    if (Test-Path -LiteralPath $unexpectedFullPath) {
        throw "Package should not contain root file: $unexpectedPackageFile"
    }
}

$indexPath = Join-Path $extractFullPath "skill-index.json"
if (-not (Test-Path -LiteralPath $indexPath)) {
    throw "Package does not contain skill-index.json"
}

$index = Get-Content -LiteralPath $indexPath -Raw | ConvertFrom-Json
$entries = @($index.PSObject.Properties)
$missing = [System.Collections.Generic.List[object]]::new()
$skillCount = 0
$sampleCount = 0

foreach ($entry in $entries) {
    $name = $entry.Name
    $value = $entry.Value

    foreach ($propertyName in @("component", "skill", "sample")) {
        $relativePath = $value.$propertyName
        if ([string]::IsNullOrWhiteSpace($relativePath)) {
            continue
        }

        if ($propertyName -eq "skill") {
            $skillCount++
        }

        if ($propertyName -eq "sample") {
            $sampleCount++
        }

        $fullPath = Join-Path $extractFullPath (ConvertTo-LocalPath $relativePath)
        if (-not (Test-Path -LiteralPath $fullPath)) {
            $missing.Add([pscustomobject]@{
                Component = $name
                Field = $propertyName
                Path = $relativePath
            })
        }
    }
}

if ($missing.Count -gt 0) {
    $missing | Format-Table -AutoSize
    throw "Packed Skill validation failed. Missing paths: $($missing.Count)"
}

$manualProjectPath = Assert-WorkspacePath "artifacts/skill-manual-install-test"
if (Test-Path -LiteralPath $manualProjectPath) {
    Remove-Item -LiteralPath $manualProjectPath -Recurse -Force
}

New-Item -ItemType Directory -Path $manualProjectPath | Out-Null

$manualScript = Join-Path $extractFullPath "tools\install-bootstrapblazor-agents.ps1"
& powershell -NoProfile -ExecutionPolicy Bypass -File $manualScript -ProjectDir $manualProjectPath | Out-Host

$manualAgentsPath = Join-Path $manualProjectPath ".bootstrapblazor\BootstrapBlazor.AGENTS.md"
if (-not (Test-Path -LiteralPath $manualAgentsPath)) {
    throw "Manual BootstrapBlazor.AGENTS.md installation failed."
}

if (Test-Path -LiteralPath (Join-Path $manualProjectPath "AGENTS.md")) {
    throw "Manual installation should not create root AGENTS.md."
}

$autoProjectPath = Assert-WorkspacePath "artifacts/skill-auto-install-test"
if (Test-Path -LiteralPath $autoProjectPath) {
    Remove-Item -LiteralPath $autoProjectPath -Recurse -Force
}

New-Item -ItemType Directory -Path $autoProjectPath | Out-Null

$autoProjectFile = Join-Path $autoProjectPath "BootstrapBlazorSkillAutoInstall.proj"
$targetsPath = Join-Path $extractFullPath "buildTransitive\BootstrapBlazor.targets"
$escapedTargetsPath = [System.Security.SecurityElement]::Escape($targetsPath)
$autoProjectXml = @"
<Project>
  <Import Project="$escapedTargetsPath" />
  <Target Name="PrepareForBuild" />
</Project>
"@

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($autoProjectFile, $autoProjectXml, $utf8NoBom)

& dotnet msbuild $autoProjectFile -nologo -t:PrepareForBuild -v:minimal | Out-Host
if ($LASTEXITCODE -ne 0) {
    throw "Automatic AGENTS.md generation target failed."
}

$autoAgentsPath = Join-Path $autoProjectPath ".bootstrapblazor\BootstrapBlazor.AGENTS.md"
if (-not (Test-Path -LiteralPath $autoAgentsPath)) {
    throw "Automatic BootstrapBlazor.AGENTS.md generation failed."
}

if (Test-Path -LiteralPath (Join-Path $autoProjectPath "AGENTS.md")) {
    throw "Automatic installation should not create root AGENTS.md."
}

[pscustomobject]@{
    Package = $packageFullPath.Path
    ExtractedTo = $extractFullPath
    IndexEntries = $entries.Count
    SkillEntries = $skillCount
    SampleEntries = $sampleCount
    ManualInstall = $manualAgentsPath
    AutomaticInstall = $autoAgentsPath
} | Format-List

Write-Host "Packed Skill validation passed."
