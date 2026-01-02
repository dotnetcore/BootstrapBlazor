// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using LlmsDocsGenerator;
using System.CommandLine;

var componentOption = new Option<string?>("--component") { Description = "Generate documentation for a specific component only" };
var indexOnlyOption = new Option<bool>("--index-only") { Description = "Generate only the index file (llms.txt)" };
var checkOption = new Option<bool>("--check") { Description = "Check if documentation is up-to-date (for CI/CD)" };
var rootFolderOption = new Option<string?>("--root") { Description = "Set the root folder of project" };
var debugOption = new Option<bool>("--debug") { Description = "Set the environment to development and display debugging information." };

var rootCommand = new RootCommand("BootstrapBlazor LLMs Documentation Generator")
{
    componentOption,
    indexOnlyOption,
    checkOption,
    rootFolderOption,
    debugOption
};

rootCommand.SetAction(async result =>
{
    var debug = result.GetValue(debugOption);
    var rootFolder = result.GetValue(rootFolderOption);
    var generator = new DocsGenerator(rootFolder, debug);

    var check = result.GetValue(checkOption);
    if (check)
    {
        var isUpToDate = await generator.CheckAsync();
        Environment.ExitCode = isUpToDate ? 0 : 1;
        return;
    }

    var indexOnly = result.GetValue(indexOnlyOption);
    if (indexOnly)
    {
        await generator.GenerateIndexAsync();
        return;
    }

    var component = result.GetValue(componentOption);
    if (!string.IsNullOrEmpty(component))
    {
        await generator.GenerateComponentAsync(component);
        return;
    }

    await generator.GenerateAllAsync();
});

return await rootCommand.Parse(args).InvokeAsync();
