# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the Apache 2.0 License
# See the LICENSE file in the project root for more information.

[CmdletBinding()]
param(
    [string]$ComponentRoot = "src/BootstrapBlazor/Components",
    [string]$SkillRoot = "docs/skills/components",
    [string[]]$SampleRoots = @(
        "src/BootstrapBlazor.Server/Components/Samples",
        "src/BootstrapBlazor.Server/Samples"
    ),
    [string]$OutputPath = "skill-index.json",
    [switch]$Check
)

$ErrorActionPreference = "Stop"
$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $RepoRoot

function ConvertTo-RepoPath {
    param([Parameter(Mandatory = $true)][string]$Path)

    $fullPath = [System.IO.Path]::GetFullPath($Path)
    $rootPath = [System.IO.Path]::GetFullPath($RepoRoot)

    if (-not $rootPath.EndsWith([System.IO.Path]::DirectorySeparatorChar)) {
        $rootPath += [System.IO.Path]::DirectorySeparatorChar
    }

    $rootUri = New-Object System.Uri($rootPath)
    $pathUri = New-Object System.Uri($fullPath)
    $relativePath = [System.Uri]::UnescapeDataString($rootUri.MakeRelativeUri($pathUri).ToString())

    return $relativePath -replace "\\", "/"
}

function Add-DirectoryEntry {
    param(
        [Parameter(Mandatory = $true)][hashtable]$Map,
        [Parameter(Mandatory = $true)][System.IO.DirectoryInfo]$Directory
    )

    if (-not $Map.ContainsKey($Directory.Name)) {
        $Map[$Directory.Name] = $Directory
    }
}

function Add-SampleEntry {
    param(
        [Parameter(Mandatory = $true)][hashtable]$Map,
        [Parameter(Mandatory = $true)][System.IO.FileSystemInfo]$Entry,
        [Parameter(Mandatory = $true)][string]$Name
    )

    if (-not $Map.ContainsKey($Name)) {
        $Map[$Name] = $Entry
    }
}

function Get-SampleNameCandidates {
    param([Parameter(Mandatory = $true)][string]$Name)

    $candidates = [System.Collections.Generic.List[string]]::new()
    $candidates.Add($Name)
    $candidates.Add("$($Name)s")
    $candidates.Add("$($Name)es")

    if ($Name.EndsWith("y", [System.StringComparison]::OrdinalIgnoreCase)) {
        $candidates.Add($Name.Substring(0, $Name.Length - 1) + "ies")
    }

    return $candidates | Sort-Object -Unique
}

$components = @{}
$componentRootPath = Join-Path $RepoRoot $ComponentRoot
if (Test-Path $componentRootPath) {
    Get-ChildItem -LiteralPath $componentRootPath -Directory |
        Sort-Object Name |
        ForEach-Object { Add-DirectoryEntry -Map $components -Directory $_ }
}

$samples = @{}
$sampleIndexNames = @{}
foreach ($sampleRoot in $SampleRoots) {
    $sampleRootPath = Join-Path $RepoRoot $sampleRoot
    if (Test-Path $sampleRootPath) {
        Get-ChildItem -LiteralPath $sampleRootPath -Directory |
            Sort-Object Name |
            ForEach-Object {
                Add-SampleEntry -Map $samples -Entry $_ -Name $_.Name
                $sampleIndexNames[$_.Name] = $true
            }

        Get-ChildItem -LiteralPath $sampleRootPath -File -Filter "*.razor" |
            Sort-Object Name |
            ForEach-Object { Add-SampleEntry -Map $samples -Entry $_ -Name $_.BaseName }
    }
}

$names = @($components.Keys + $sampleIndexNames.Keys) | Sort-Object -Unique
$index = [ordered]@{}

foreach ($name in $names) {
    $entry = [ordered]@{}

    if ($components.ContainsKey($name)) {
        $componentDirectory = $components[$name]

        $entry["component"] = ConvertTo-RepoPath $componentDirectory.FullName
    }

    $skillPath = Join-Path (Join-Path $RepoRoot $SkillRoot) "$name.md"
    if (Test-Path $skillPath) {
        $entry["skill"] = ConvertTo-RepoPath $skillPath
    }

    $sample = $null
    foreach ($sampleName in (Get-SampleNameCandidates $name)) {
        if ($samples.ContainsKey($sampleName)) {
            $sample = $samples[$sampleName]
            break
        }
    }

    if ($null -ne $sample) {
        $entry["sample"] = ConvertTo-RepoPath $sample.FullName
    }

    $index[$name] = $entry
}

$json = $index | ConvertTo-Json -Depth 5
if (-not $json.EndsWith("`n")) {
    $json += "`n"
}

$outputFullPath = Join-Path $RepoRoot $OutputPath

if ($Check) {
    if (-not (Test-Path $outputFullPath)) {
        Write-Warning "$OutputPath does not exist. Run scripts/generate-skill-index.ps1."
        exit 1
    }

    $current = Get-Content -LiteralPath $outputFullPath -Raw
    if ($current -ne $json) {
        Write-Warning "$OutputPath is out of date. Run scripts/generate-skill-index.ps1."
        exit 1
    }

    Write-Host "$OutputPath is up to date."
    exit 0
}

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($outputFullPath, $json, $utf8NoBom)
Write-Host "Generated $OutputPath"
