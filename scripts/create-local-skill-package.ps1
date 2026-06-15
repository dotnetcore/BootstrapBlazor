# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the Apache 2.0 License
# See the LICENSE file in the project root for more information.

[CmdletBinding()]
param(
    [string]$PackageVersion = "10.7.2-beta03.local-skills",

    [string]$OutputPath = "artifacts/skill-pack-test/BootstrapBlazor.10.7.2-beta03.local-skills.nupkg",

    [string]$TargetFramework = "net8.0"
)

$ErrorActionPreference = "Stop"
$RepoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
Set-Location $RepoRoot

function Add-TextEntry {
    param(
        [Parameter(Mandatory = $true)][System.IO.Compression.ZipArchive]$Archive,
        [Parameter(Mandatory = $true)][string]$Name,
        [Parameter(Mandatory = $true)][string]$Text
    )

    $entry = $Archive.CreateEntry(($Name -replace "\\", "/"))
    $stream = $entry.Open()
    $writer = New-Object System.IO.StreamWriter($stream, [System.Text.UTF8Encoding]::new($false))
    $writer.Write($Text)
    $writer.Dispose()
    $stream.Dispose()
}

function Add-FileEntry {
    param(
        [Parameter(Mandatory = $true)][System.IO.Compression.ZipArchive]$Archive,
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$EntryName
    )

    if (-not (Test-Path -LiteralPath $Path)) {
        return
    }

    $entry = $Archive.CreateEntry(($EntryName -replace "\\", "/"))
    $input = [System.IO.File]::OpenRead((Resolve-Path $Path))
    $output = $entry.Open()
    $input.CopyTo($output)
    $output.Dispose()
    $input.Dispose()
}

$outputFullPath = [System.IO.Path]::GetFullPath((Join-Path $RepoRoot $OutputPath))
$outputDirectory = Split-Path $outputFullPath -Parent
New-Item -ItemType Directory -Force -Path $outputDirectory | Out-Null

if (Test-Path -LiteralPath $outputFullPath) {
    Remove-Item -LiteralPath $outputFullPath -Force
}

Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

$archive = [System.IO.Compression.ZipFile]::Open($outputFullPath, [System.IO.Compression.ZipArchiveMode]::Create)
try {
    Add-TextEntry -Archive $archive -Name "[Content_Types].xml" -Text '<?xml version="1.0" encoding="utf-8"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="nuspec" ContentType="application/octet"/><Default Extension="dll" ContentType="application/octet"/><Default Extension="xml" ContentType="application/xml"/><Default Extension="json" ContentType="application/json"/><Default Extension="md" ContentType="text/markdown"/><Default Extension="razor" ContentType="text/plain"/><Default Extension="cs" ContentType="text/plain"/><Default Extension="scss" ContentType="text/plain"/><Default Extension="js" ContentType="text/javascript"/><Default Extension="css" ContentType="text/css"/><Default Extension="targets" ContentType="application/xml"/><Default Extension="ps1" ContentType="text/plain"/></Types>'
    Add-TextEntry -Archive $archive -Name "_rels/.rels" -Text '<?xml version="1.0" encoding="utf-8"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"></Relationships>'
    Add-TextEntry -Archive $archive -Name "BootstrapBlazor.nuspec" -Text "<?xml version=`"1.0`" encoding=`"utf-8`"?><package xmlns=`"http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd`"><metadata><id>BootstrapBlazor</id><version>$PackageVersion</version><authors>BootstrapBlazor</authors><description>Local package layout for BootstrapBlazor AI Skill validation.</description></metadata></package>"

    Add-FileEntry -Archive $archive -Path "src/BootstrapBlazor/bin/Debug/$TargetFramework/BootstrapBlazor.dll" -EntryName "lib/$TargetFramework/BootstrapBlazor.dll"
    Add-FileEntry -Archive $archive -Path "src/BootstrapBlazor/bin/Debug/$TargetFramework/BootstrapBlazor.xml" -EntryName "lib/$TargetFramework/BootstrapBlazor.xml"

    foreach ($rootFile in @("skill-index.json")) {
        Add-FileEntry -Archive $archive -Path $rootFile -EntryName $rootFile
    }

    foreach ($packageFile in @(
        "src/BootstrapBlazor/agents/BootstrapBlazor.AGENTS.md",
        "src/BootstrapBlazor/buildTransitive/BootstrapBlazor.targets",
        "src/BootstrapBlazor/tools/install-bootstrapblazor-agents.ps1"
    )) {
        $entryName = $packageFile -replace "^src/BootstrapBlazor/", ""
        Add-FileEntry -Archive $archive -Path $packageFile -EntryName $entryName
    }

    $trackedFiles = @(& git ls-files src/BootstrapBlazor/Components src/BootstrapBlazor.Server/Components/Samples)
    foreach ($file in $trackedFiles) {
        Add-FileEntry -Archive $archive -Path $file -EntryName $file
    }
}
finally {
    $archive.Dispose()
}

Get-Item -LiteralPath $outputFullPath | Select-Object FullName, Length, LastWriteTime | Format-List
