// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.CommandLine;
using LlmsDocsGenerator;

var rootCommand = new RootCommand("BootstrapBlazor LLMs Documentation Generator");

var componentOption = new Option<string?>(
    name: "--component",
    description: "Generate documentation for a specific component only");

var indexOnlyOption = new Option<bool>(
    name: "--index-only",
    description: "Generate only the index file (llms.txt)");

var checkOption = new Option<bool>(
    name: "--check",
    description: "Check if documentation is up-to-date (for CI/CD)");

var outputOption = new Option<string>(
    name: "--output",
    getDefaultValue: () => "src/BootstrapBlazor.Server/wwwroot/llmstxt",
    description: "Output directory for generated files (default: src/BootstrapBlazor.Server/wwwroot/llmstxt)");

rootCommand.AddOption(componentOption);
rootCommand.AddOption(indexOnlyOption);
rootCommand.AddOption(checkOption);
rootCommand.AddOption(outputOption);

rootCommand.SetHandler(async (component, indexOnly, check, output) =>
{
    var generator = new DocsGenerator(output);

    if (check)
    {
        var isUpToDate = await generator.CheckAsync();
        Environment.ExitCode = isUpToDate ? 0 : 1;
        return;
    }

    if (indexOnly)
    {
        await generator.GenerateIndexAsync();
        return;
    }

    if (!string.IsNullOrEmpty(component))
    {
        await generator.GenerateComponentAsync(component);
        return;
    }

    await generator.GenerateAllAsync();

}, componentOption, indexOnlyOption, checkOption, outputOption);

return await rootCommand.InvokeAsync(args);
