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
    [string]$LastUpdated = "2026-06-13",
    [string[]]$SkipComponents = @(),
    [switch]$Force
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
    return [System.Uri]::UnescapeDataString($rootUri.MakeRelativeUri($pathUri).ToString()) -replace "\\", "/"
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

function Get-SampleMap {
    $samples = @{}

    foreach ($sampleRoot in $SampleRoots) {
        $sampleRootPath = Join-Path $RepoRoot $sampleRoot
        if (-not (Test-Path $sampleRootPath)) {
            continue
        }

        Get-ChildItem -LiteralPath $sampleRootPath -Directory |
            Sort-Object Name |
            ForEach-Object { Add-SampleEntry -Map $samples -Entry $_ -Name $_.Name }

        Get-ChildItem -LiteralPath $sampleRootPath -File -Filter "*.razor" |
            Sort-Object Name |
            ForEach-Object { Add-SampleEntry -Map $samples -Entry $_ -Name $_.BaseName }
    }

    return $samples
}

function Find-Sample {
    param(
        [Parameter(Mandatory = $true)][hashtable]$Samples,
        [Parameter(Mandatory = $true)][string]$ComponentName
    )

    foreach ($candidate in (Get-SampleNameCandidates $ComponentName)) {
        if ($Samples.ContainsKey($candidate)) {
            return $Samples[$candidate]
        }
    }

    return $null
}

function Read-AllText {
    param([System.IO.FileInfo[]]$Files)

    $parts = foreach ($file in $Files) {
        if ($file.Length -lt 1MB) {
            Get-Content -LiteralPath $file.FullName -Raw
        }
    }

    return ($parts -join "`n")
}

function Remove-NonAscii {
    param([string]$Text)

    if ($null -eq $Text) {
        return ""
    }

    return ($Text -replace '[^\u0009\u000A\u000D\u0020-\u007E]', '')
}

function Get-SourceSummary {
    param(
        [string]$Text,
        [string]$ComponentName
    )

    $classMatch = [regex]::Match($Text, "(?ms)<summary>\s*(?<summary>.*?)\s*</summary>\s*(?:\[[^\]]+\]\s*)*public\s+(?:partial\s+)?class\s+$([regex]::Escape($ComponentName))\b")
    if ($classMatch.Success) {
        $summary = $classMatch.Groups["summary"].Value
        $english = [regex]::Match($summary, '<para\s+lang="en">\s*(?<text>.*?)\s*</para>')
        if ($english.Success) {
            return (Remove-NonAscii (($english.Groups["text"].Value -replace "\s+", " ").Trim()))
        }

        return (Remove-NonAscii (($summary -replace "<.*?>", "" -replace "\s+", " ").Trim()))
    }

    $firstEnglish = [regex]::Match($Text, '<para\s+lang="en">\s*(?<text>.*?)\s*</para>')
    if ($firstEnglish.Success) {
        return (Remove-NonAscii (($firstEnglish.Groups["text"].Value -replace "\s+", " ").Trim()))
    }

    return ""
}

function ConvertFrom-XmlDocComment {
    param([string]$Text)

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return ""
    }

    $normalized = $Text -replace "(?m)^\s*///\s?", ""
    $english = [regex]::Match($normalized, '(?ms)<para\s+lang="en">\s*(?<text>.*?)\s*</para>')
    if ($english.Success) {
        $normalized = $english.Groups["text"].Value
    }

    return (Remove-NonAscii (($normalized -replace "<.*?>", "" -replace "\s+", " ").Trim()))
}

function Get-AttributedProperties {
    param(
        [string]$Text,
        [string]$AttributeName
    )

    $pattern = "(?m)(?<doc>(?:^\s*///[^\r\n]*(?:\r?\n))*)(?<attrs>(?:^\s*\[[^\r\n]+\]\s*(?:\r?\n))+)^\s*public\s+(?<type>[^\r\n{=;]+?)\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*\{\s*get;\s*(?:set|init);?\s*\}(?:\s*=\s*(?<default>[^;\r\n]+);)?"
    $matches = [regex]::Matches($Text, $pattern)
    $items = foreach ($match in $matches) {
        $attrs = $match.Groups["attrs"].Value
        if ($attrs -notmatch "\[$([regex]::Escape($AttributeName))(\(|\])") {
            continue
        }

        $doc = ConvertFrom-XmlDocComment $match.Groups["doc"].Value
        $type = ($match.Groups["type"].Value -replace "\s+", " ").Trim()
        $name = $match.Groups["name"].Value.Trim()
        $default = ""
        if ($match.Groups["default"].Success) {
            $default = ($match.Groups["default"].Value -replace "\s+", " ").Trim()
        }

        [pscustomobject]@{
            Name = $name
            Type = $type
            Summary = $doc
            Default = $default
            Required = $attrs -match "\[EditorRequired\]"
            Obsolete = $attrs -match "\[Obsolete"
            IsEvent = $type -match "EventCallback|Func\s*<|Action\s*<|Func\b|Action\b"
            IsTemplate = $type -match "RenderFragment"
        }
    }

    return @($items | Sort-Object Name -Unique)
}

function Get-ObsoleteMembers {
    param([string]$Text)

    $pattern = "(?ms)\[Obsolete\((?<message>[^\)]*)\)\]\s*(?:public|protected|internal)\s+(?<signature>[^\r\n{;]+)"
    return @([regex]::Matches($Text, $pattern) | ForEach-Object {
        $message = (Remove-NonAscii ($_.Groups["message"].Value -replace "\s+", " ").Trim().Trim('"'))
        $signature = ($_.Groups["signature"].Value -replace "\s+", " ").Trim()
        if ($message.Length -gt 120) {
            $message = $message.Substring(0, 117) + "..."
        }
        "$signature - $message"
    } | Sort-Object -Unique)
}

function Get-LifecycleNotes {
    param([string]$Text)

    $names = @(
        "OnInitialized",
        "OnInitializedAsync",
        "OnParametersSet",
        "OnParametersSetAsync",
        "OnAfterRender",
        "OnAfterRenderAsync",
        "Dispose",
        "DisposeAsync"
    )

    return @($names | Where-Object { $Text -match "\b$([regex]::Escape($_))\s*\(" })
}

function Get-SampleFiles {
    param([System.IO.FileSystemInfo]$Sample)

    if ($null -eq $Sample) {
        return @()
    }

    if ($Sample.PSIsContainer) {
        return @(Get-ChildItem -LiteralPath $Sample.FullName -File -Recurse |
            Where-Object { $_.Extension -in @(".razor", ".cs") } |
            Sort-Object FullName)
    }

    $files = [System.Collections.Generic.List[System.IO.FileInfo]]::new()
    $files.Add([System.IO.FileInfo]$Sample)

    $codeBehind = "$($Sample.FullName).cs"
    if (Test-Path $codeBehind) {
        $files.Add([System.IO.FileInfo](Get-Item -LiteralPath $codeBehind))
    }

    return @($files | Sort-Object FullName)
}

function Get-SampleUsage {
    param(
        [string]$Text,
        [string]$ComponentName
    )

    $tagPattern = "(?ms)<\s*$([regex]::Escape($ComponentName))(?=[\s>/])(?<attrs>.{0,2000}?)(?:/?>)"
    $tags = @([regex]::Matches($Text, $tagPattern))
    $attrs = @()

    foreach ($tag in $tags) {
        $attrText = $tag.Groups["attrs"].Value
        $attrs += [regex]::Matches($attrText, "(?:^|\s)(?<name>@?[A-Za-z_][A-Za-z0-9_:\.-]*)\s*=") |
            ForEach-Object { $_.Groups["name"].Value }
    }

    $snippet = ""
    $snippetPattern = "(?ms)<\s*$([regex]::Escape($ComponentName))(?=[\s>/]).{0,1600}?(?:/>\s*|</\s*$([regex]::Escape($ComponentName))\s*>)"
    $snippetMatch = [regex]::Match($Text, $snippetPattern)
    if ($snippetMatch.Success) {
        $snippet = (Remove-NonAscii ($snippetMatch.Value -replace "\r\n", "`n")).Trim()
        $snippetLines = @($snippet -split "`n")
        if ($snippetLines.Count -gt 28) {
            $snippet = (($snippetLines | Select-Object -First 26) -join "`n") + "`n..."
        }
    }

    return [pscustomobject]@{
        Count = $tags.Count
        Attributes = @($attrs | Sort-Object -Unique)
        Snippet = $snippet
    }
}

function Join-CodeList {
    param([string[]]$Items)

    if ($Items.Count -eq 0) {
        return "None detected in current source."
    }

    return (($Items | ForEach-Object { "``$_``" }) -join ", ")
}

function New-ParameterTable {
    param([object[]]$Parameters)

    if ($Parameters.Count -eq 0) {
        return "No `[Parameter]` properties were detected in the current source."
    }

    $selected = @($Parameters | Sort-Object @{ Expression = { if ($_.Required) { 0 } elseif ($_.IsEvent) { 1 } elseif ($_.IsTemplate) { 2 } else { 3 } } }, Name | Select-Object -First 40)
    $lines = [System.Collections.Generic.List[string]]::new()
    $lines.Add("| Name | Type | Notes |")
    $lines.Add("| --- | --- | --- |")

    foreach ($parameter in $selected) {
        $notes = [System.Collections.Generic.List[string]]::new()
        if ($parameter.Required) { $notes.Add("Required") }
        if ($parameter.IsEvent) { $notes.Add("Callback/event parameter") }
        if ($parameter.IsTemplate) { $notes.Add("Template parameter; verify context type") }
        if ($parameter.Obsolete) { $notes.Add("Obsolete; do not use") }
        if (-not [string]::IsNullOrWhiteSpace($parameter.Default)) { $notes.Add("Default: ``$($parameter.Default)``") }
        if (-not [string]::IsNullOrWhiteSpace($parameter.Summary)) {
            $summary = $parameter.Summary
            if ($summary.Length -gt 120) {
                $summary = $summary.Substring(0, 117) + "..."
            }
            $notes.Add($summary)
        }
        if ($notes.Count -eq 0) { $notes.Add("Verify current source before use") }

        $lines.Add("| ``$($parameter.Name)`` | ``$($parameter.Type)`` | $($notes -join "; ") |")
    }

    if ($Parameters.Count -gt $selected.Count) {
        $lines.Add("")
        $lines.Add("Only the first $($selected.Count) important parameters are listed. Inspect the current source for the remaining $($Parameters.Count - $selected.Count) parameters before generating code.")
    }

    return ($lines -join "`n")
}

function New-SkillContent {
    param(
        [System.IO.DirectoryInfo]$Component,
        [System.IO.FileInfo[]]$SourceFiles,
        [System.IO.FileSystemInfo]$Sample,
        [System.IO.FileInfo[]]$SampleFiles
    )

    $name = $Component.Name
    $sourceText = Read-AllText $SourceFiles
    $sampleText = Read-AllText $SampleFiles
    $codeFence = '```'
    $parameters = Get-AttributedProperties -Text $sourceText -AttributeName "Parameter"
    $cascading = Get-AttributedProperties -Text $sourceText -AttributeName "CascadingParameter"
    $events = @($parameters | Where-Object { $_.IsEvent })
    $templates = @($parameters | Where-Object { $_.IsTemplate })
    $obsolete = Get-ObsoleteMembers $sourceText
    $lifecycle = Get-LifecycleNotes $sourceText
    $summary = Get-SourceSummary -Text $sourceText -ComponentName $name
    $usage = Get-SampleUsage -Text $sampleText -ComponentName $name
    $hasRazor = @($SourceFiles | Where-Object { $_.Extension -eq ".razor" }).Count -gt 0
    $hasNamedRazor = @($SourceFiles | Where-Object { $_.Name -eq "$name.razor" }).Count -gt 0
    $hasJs = @($SourceFiles | Where-Object { $_.Name -like "*.js" }).Count -gt 0
    $hasAutoLoader = $sourceText -match "BootstrapModuleAutoLoader"
    $hasJsInterop = $hasJs -or $hasAutoLoader -or $sourceText -match "IJSRuntime|IJSObjectReference|JSObjectReference|InvokeVoidAsync|InvokeAsync"
    $repoComponentPath = ConvertTo-RepoPath $Component.FullName
    $sourceList = @($SourceFiles | Select-Object -First 12 | ForEach-Object { "- ``$(ConvertTo-RepoPath $_.FullName)``" })
    $samplePath = if ($null -eq $Sample) { "" } else { ConvertTo-RepoPath $Sample.FullName }
    $sampleList = @($SampleFiles |
        Where-Object { $null -ne $_ -and -not [string]::IsNullOrWhiteSpace($_.FullName) } |
        ForEach-Object { ConvertTo-RepoPath $_.FullName } |
        Sort-Object -Unique |
        Select-Object -First 12 |
        ForEach-Object { "- ``$_``" })
    $sampleFilesReviewed = @($sampleList | Where-Object { $_ -ne "- ``$samplePath``" })
    $sampleFilesReviewedSection = if ($sampleFilesReviewed.Count -gt 0) {
        @"

Sample files reviewed:

$($sampleFilesReviewed -join "`n")
"@
    }
    else {
        ""
    }

    if ([string]::IsNullOrWhiteSpace($summary)) {
        if ($hasRazor) {
            $summary = "The ``$name`` component is implemented in this directory."
        }
        else {
            $summary = "The ``$name`` directory contains component-related infrastructure or support types."
        }
    }

    $sampleSection = if ($null -ne $Sample) {
        $attributeLine = if ($usage.Attributes.Count -gt 0) {
            "Observed attributes in official Sample: " + (Join-CodeList $usage.Attributes)
        }
        else {
            "No direct ``<$name>`` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code."
        }

        $snippetBlock = if (-not [string]::IsNullOrWhiteSpace($usage.Snippet)) {
            @"

Sample-derived snippet:

${codeFence}razor
$($usage.Snippet)
$codeFence
"@
        }
        else {
            ""
        }

        @"
Official Sample mapping:

- ``$samplePath``
$sampleFilesReviewedSection

Sample analysis:

- Direct ``<$name>`` tag usages detected: $($usage.Count)
- $attributeLine$snippetBlock

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.
"@
    }
    else {
        @"
No official Sample was matched for ``$name``.

Use examples in this Skill only after validating the current source. If the directory contains infrastructure rather than a public Razor component, do not generate direct Razor usage.
"@
    }

    $exampleSection = if ($null -ne $Sample -and -not [string]::IsNullOrWhiteSpace($usage.Snippet)) {
        @"
Use the official Sample-derived snippet above as the starting point.
"@
    }
    elseif ($hasNamedRazor) {
        @"
Source-validated skeleton:

${codeFence}razor
<$name>
</$name>
$codeFence

This skeleton is not a substitute for source validation. Add only parameters that exist in the current source.
"@
    }
    else {
        @"
No direct Razor example is generated because this directory does not expose an authoritative ``$name.razor`` usage pattern in the current repository.
"@
    }

    $obsoleteSection = if ($obsolete.Count -gt 0) {
        (($obsolete | Select-Object -First 12 | ForEach-Object { "- $_" }) -join "`n")
    }
    else {
        "No obsolete members were detected in the current source scan."
    }

    $content = @"
---
component: $name
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: $LastUpdated
---

# $name Skill

## Component Purpose

$summary

Primary source directory: ``$repoComponentPath``.

Source files reviewed:

$($sourceList -join "`n")

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

$(New-ParameterTable $parameters)

## Events And Callbacks

$(if ($events.Count -gt 0) { Join-CodeList @($events | ForEach-Object { "$($_.Name): $($_.Type)" }) } else { "No callback/event parameters were detected in the current source scan." })

## Templates And Child Content

$(if ($templates.Count -gt 0) { Join-CodeList @($templates | ForEach-Object { "$($_.Name): $($_.Type)" }) } else { "No RenderFragment parameters were detected in the current source scan." })

## Cascading Parameters

$(if ($cascading.Count -gt 0) { Join-CodeList @($cascading | ForEach-Object { "$($_.Name): $($_.Type)" }) } else { "No CascadingParameter properties were detected in the current source scan." })

## Implementation Notes

- Lifecycle methods detected: $(Join-CodeList $lifecycle)
- JS interop or module dependency detected: ``$hasJsInterop``
- Razor component file detected: ``$hasRazor``
- Keep generated code aligned with the files listed above.

## Sample Mapping

$sampleSection

## Examples

$exampleSection

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

$obsoleteSection

## Agent Rules

1. Read ``$repoComponentPath`` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
"@

    return $content
}

$componentRootPath = Join-Path $RepoRoot $ComponentRoot
if (-not (Test-Path $componentRootPath)) {
    throw "Component root not found: $ComponentRoot"
}

$samples = Get-SampleMap
$skillRootPath = Join-Path $RepoRoot $SkillRoot
New-Item -ItemType Directory -Force -Path $skillRootPath | Out-Null
$generated = 0
$skipped = 0
$skipLookup = @{}
foreach ($skipComponent in $SkipComponents) {
    foreach ($skipName in ($skipComponent -split "[,;]")) {
        $trimmed = $skipName.Trim()
        if (-not [string]::IsNullOrWhiteSpace($trimmed)) {
            $skipLookup[$trimmed] = $true
        }
    }
}

Get-ChildItem -LiteralPath $componentRootPath -Directory |
    Sort-Object Name |
    ForEach-Object {
        $component = $_
        $skillPath = Join-Path $skillRootPath "$($component.Name).md"

        if ($skipLookup.ContainsKey($component.Name)) {
            $script:skipped++
            return
        }

        if ((Test-Path $skillPath) -and -not $Force) {
            $script:skipped++
            return
        }

        $sourceFiles = @(Get-ChildItem -LiteralPath $component.FullName -File -Recurse |
            Where-Object { $_.Extension -in @(".razor", ".cs", ".js") } |
            Sort-Object FullName)
        $sample = Find-Sample -Samples $samples -ComponentName $component.Name
        $sampleFiles = Get-SampleFiles -Sample $sample
        $content = New-SkillContent -Component $component -SourceFiles $sourceFiles -Sample $sample -SampleFiles $sampleFiles

        $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllText($skillPath, $content, $utf8NoBom)
        $script:generated++
    }

Write-Host "Generated $generated Skill files. Skipped $skipped existing Skill files."
