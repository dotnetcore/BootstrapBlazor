// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using System.Text.Json;

namespace BootstrapBlazor.LLMsDocsGenerator;

internal sealed class DocsGenerator(GeneratorOptions options)
{
    public async Task<int> RunAsync()
    {
        var entries = LoadSkillIndex();
        if (!string.IsNullOrWhiteSpace(options.ComponentName))
        {
            entries = entries
                .Where(entry => string.Equals(entry.Name, options.ComponentName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (entries.Count == 0)
            {
                Console.Error.WriteLine($"Component was not found in skill-index.json: {options.ComponentName}");
                return 1;
            }
        }

        var analyzer = new ComponentAnalyzer(options.RepoRoot);
        var components = new List<ComponentDocument>();
        foreach (var entry in entries.Where(entry => !string.IsNullOrWhiteSpace(entry.Component)))
        {
            components.Add(await analyzer.AnalyzeAsync(entry));
        }

        components = components.OrderBy(component => component.Name, StringComparer.OrdinalIgnoreCase).ToList();

        var outputPath = Path.Combine(options.OutputRoot, "wwwroot", "llms");
        var componentOutputPath = Path.Combine(outputPath, "components");
        var generatedFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [Path.Combine(outputPath, "llms.txt")] = MarkdownBuilder.BuildIndexDoc(components)
        };

        if (!options.IndexOnly)
        {
            foreach (var component in components)
            {
                generatedFiles[Path.Combine(componentOutputPath, $"{component.Name}.txt")] = MarkdownBuilder.BuildComponentDoc(component);
            }
        }

        if (options.Check)
        {
            return CheckFiles(generatedFiles);
        }

        Directory.CreateDirectory(outputPath);
        Directory.CreateDirectory(componentOutputPath);
        foreach (var (file, content) in generatedFiles)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(file)!);
            await File.WriteAllTextAsync(file, content);
            Console.WriteLine($"Generated: {file}");
        }

        Console.WriteLine($"Generated {generatedFiles.Count} LLM documentation files.");
        return 0;
    }

    private List<SkillIndexEntry> LoadSkillIndex()
    {
        var indexPath = Path.Combine(options.RepoRoot, "skill-index.json");
        using var stream = File.OpenRead(indexPath);
        using var document = JsonDocument.Parse(stream);

        var entries = new List<SkillIndexEntry>();
        foreach (var property in document.RootElement.EnumerateObject())
        {
            var item = property.Value;
            entries.Add(new SkillIndexEntry(
                property.Name,
                GetString(item, "component"),
                GetString(item, "skill"),
                GetString(item, "sample")));
        }

        return entries;
    }

    private static string? GetString(JsonElement element, string name)
    {
        return element.TryGetProperty(name, out var property) && property.ValueKind == JsonValueKind.String
            ? property.GetString()
            : null;
    }

    private static int CheckFiles(Dictionary<string, string> generatedFiles)
    {
        var stale = false;
        foreach (var (file, content) in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Missing generated file: {file}");
                stale = true;
                continue;
            }

            var current = File.ReadAllText(file);
            if (!string.Equals(current, content, StringComparison.Ordinal))
            {
                Console.Error.WriteLine($"Generated file is out of date: {file}");
                stale = true;
            }
        }

        if (stale)
        {
            return 1;
        }

        Console.WriteLine("LLM documentation is up to date.");
        return 0;
    }
}
