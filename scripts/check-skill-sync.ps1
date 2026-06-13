# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the Apache 2.0 License
# See the LICENSE file in the project root for more information.

[CmdletBinding()]
param(
    [string]$BaseRef = "",
    [string]$ComponentRoot = "src/BootstrapBlazor/Components",
    [string[]]$SampleRoots = @(
        "src/BootstrapBlazor.Server/Components/Samples",
        "src/BootstrapBlazor.Server/Samples"
    ),
    [string]$SkillIndexPath = "skill-index.json",
    [switch]$WarnAllMissingSkills
)

$ErrorActionPreference = "Stop"
$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $RepoRoot

function ConvertTo-GitHubWarningValue {
    param([string]$Value)

    return $Value.Replace("%", "%25").Replace("`r", "%0D").Replace("`n", "%0A")
}

function Write-SkillWarning {
    param(
        [Parameter(Mandatory = $true)][string]$Message,
        [string]$File = ""
    )

    if ($env:GITHUB_ACTIONS -eq "true") {
        $escapedMessage = ConvertTo-GitHubWarningValue $Message
        if ([string]::IsNullOrWhiteSpace($File)) {
            Write-Host "::warning::$escapedMessage"
        }
        else {
            Write-Host "::warning file=$File::$escapedMessage"
        }
    }
    else {
        Write-Warning $Message
    }
}

function ConvertTo-SlashPath {
    param([string]$Path)

    return $Path -replace "\\", "/"
}

function Get-ChangedFiles {
    param([string]$CompareRef)

    if ([string]::IsNullOrWhiteSpace($CompareRef)) {
        $hasPreviousCommit = $false
        & git rev-parse --verify HEAD~1 *> $null
        if ($LASTEXITCODE -eq 0) {
            $hasPreviousCommit = $true
        }

        if ($hasPreviousCommit) {
            $CompareRef = "HEAD~1"
        }
        else {
            return @()
        }
    }

    $diffOutput = & git diff --name-status --diff-filter=ACMR "$CompareRef...HEAD" 2>$null
    if ($LASTEXITCODE -ne 0) {
        $diffOutput = & git diff --name-status --diff-filter=ACMR $CompareRef HEAD 2>$null
    }

    if ($LASTEXITCODE -ne 0) {
        Write-SkillWarning "Unable to inspect changed files for Skill synchronization."
        return @()
    }

    if ($null -eq $diffOutput) {
        return @()
    }

    return @($diffOutput | ForEach-Object {
        $columns = $_ -split "`t"
        [pscustomobject]@{
            Status = $columns[0]
            Path = ConvertTo-SlashPath $columns[-1]
        }
    })
}

function Test-PathInGitRef {
    param(
        [string]$CompareRef,
        [string]$Path
    )

    if ([string]::IsNullOrWhiteSpace($CompareRef)) {
        return $true
    }

    & git cat-file -e "${CompareRef}:$Path" 2>$null
    return $LASTEXITCODE -eq 0
}

function Get-ComponentNameFromPath {
    param([string]$Path)

    $prefix = (ConvertTo-SlashPath $ComponentRoot).TrimEnd("/") + "/"
    if (-not $Path.StartsWith($prefix, [System.StringComparison]::OrdinalIgnoreCase)) {
        return $null
    }

    $relativePath = $Path.Substring($prefix.Length)
    if ([string]::IsNullOrWhiteSpace($relativePath)) {
        return $null
    }

    return ($relativePath -split "/")[0]
}

function Test-SkillMetadata {
    param(
        [Parameter(Mandatory = $true)][string]$ComponentName,
        [Parameter(Mandatory = $true)][string]$SkillPath
    )

    $skillFullPath = Join-Path $RepoRoot $SkillPath
    if (-not (Test-Path $skillFullPath)) {
        return
    }

    $content = Get-Content -LiteralPath $skillFullPath -Raw
    $hasFrontMatter = $content -match "(?s)^---\s*\r?\n.*?\r?\n---"
    if (-not $hasFrontMatter) {
        Write-SkillWarning "$ComponentName.md is missing YAML metadata front matter." $SkillPath
        return
    }

    $checks = [ordered]@{
        component = "^component:\s*$([regex]::Escape($ComponentName))\s*$"
        namespace = "^namespace:\s*BootstrapBlazor\.Components\s*$"
        skillVersion = "^skillVersion:\s*\d+\s*$"
        lastUpdated = "^lastUpdated:\s*\d{4}-\d{2}-\d{2}\s*$"
    }

    foreach ($name in $checks.Keys) {
        if ($content -notmatch "(?m)$($checks[$name])") {
            Write-SkillWarning "$ComponentName.md metadata is missing or invalid: $name." $SkillPath
        }
    }
}

if ([string]::IsNullOrWhiteSpace($BaseRef) -and -not [string]::IsNullOrWhiteSpace($env:GITHUB_BASE_REF)) {
    $BaseRef = "origin/$($env:GITHUB_BASE_REF)"
}

$changedFiles = Get-ChangedFiles -CompareRef $BaseRef
$changedPaths = @($changedFiles | ForEach-Object { $_.Path })

$componentChanges = @{}
$skillChanges = @{}

foreach ($change in $changedFiles) {
    $componentName = Get-ComponentNameFromPath -Path $change.Path
    if ($null -eq $componentName) {
        continue
    }

    $skillPath = (ConvertTo-SlashPath (Join-Path $ComponentRoot "$componentName/$componentName.md"))

    if ($change.Path.Equals($skillPath, [System.StringComparison]::OrdinalIgnoreCase)) {
        $skillChanges[$componentName] = $true
    }
    else {
        $componentChanges[$componentName] = $true
    }
}

foreach ($componentName in ($componentChanges.Keys | Sort-Object)) {
    $componentPath = (ConvertTo-SlashPath (Join-Path $ComponentRoot $componentName))
    $skillPath = (ConvertTo-SlashPath (Join-Path $componentPath "$componentName.md"))
    $skillFullPath = Join-Path $RepoRoot $skillPath

    $isNewComponent = -not (Test-PathInGitRef -CompareRef $BaseRef -Path $componentPath)

    if ($isNewComponent -and -not (Test-Path $skillFullPath)) {
        Write-SkillWarning "New component '$componentName' does not have $componentName.md." $componentPath
        continue
    }

    if ($isNewComponent) {
        Test-SkillMetadata -ComponentName $componentName -SkillPath $skillPath
    }

    if (-not $skillChanges.ContainsKey($componentName)) {
        Write-SkillWarning "Component '$componentName' changed without updating $componentName.md. Verify whether the Skill needs an update." $componentPath
    }
}

foreach ($componentName in ($skillChanges.Keys | Sort-Object)) {
    $skillPath = (ConvertTo-SlashPath (Join-Path $ComponentRoot "$componentName/$componentName.md"))
    Test-SkillMetadata -ComponentName $componentName -SkillPath $skillPath
}

if ($WarnAllMissingSkills) {
    $componentRootPath = Join-Path $RepoRoot $ComponentRoot
    if (Test-Path $componentRootPath) {
        Get-ChildItem -LiteralPath $componentRootPath -Directory |
            Sort-Object Name |
            ForEach-Object {
                $skillPath = Join-Path $_.FullName "$($_.Name).md"
                if (-not (Test-Path $skillPath)) {
                    Write-SkillWarning "Component '$($_.Name)' does not have $($_.Name).md." (ConvertTo-SlashPath (Join-Path $ComponentRoot $_.Name))
                }
                else {
                    Test-SkillMetadata -ComponentName $_.Name -SkillPath (ConvertTo-SlashPath (Join-Path $ComponentRoot "$($_.Name)/$($_.Name).md"))
                }
            }
    }
}

$sampleChanged = $false
foreach ($sampleRoot in $SampleRoots) {
    $samplePrefix = (ConvertTo-SlashPath $sampleRoot).TrimEnd("/") + "/"
    if ($changedPaths | Where-Object { $_.StartsWith($samplePrefix, [System.StringComparison]::OrdinalIgnoreCase) }) {
        $sampleChanged = $true
        break
    }
}

$indexDiff = & git diff --name-only -- $SkillIndexPath
if ($indexDiff) {
    Write-SkillWarning "$SkillIndexPath was updated by scripts/generate-skill-index.ps1. Commit the generated index so Agents can find new components or Samples." $SkillIndexPath
}
elseif ($sampleChanged -and -not ($changedPaths -contains $SkillIndexPath)) {
    Write-SkillWarning "Sample changes detected. Run scripts/generate-skill-index.ps1 and commit $SkillIndexPath if it changes." $SkillIndexPath
}

exit 0
