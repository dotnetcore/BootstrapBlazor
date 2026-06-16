// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using BootstrapBlazor.LLMsDocsGenerator;

var options = CommandLineOptions.Parse(args);
if (options is null)
{
    return 1;
}

var generator = new DocsGenerator(options);
return await generator.RunAsync();

internal static class CommandLineOptions
{
    public static GeneratorOptions? Parse(string[] args)
    {
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        var flags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var index = 0; index < args.Length; index++)
        {
            var arg = args[index];
            if (arg is "--help" or "-h")
            {
                PrintHelp();
                return null;
            }

            if (!arg.StartsWith("--", StringComparison.Ordinal))
            {
                continue;
            }

            var key = arg;
            string? value = null;
            var equalsIndex = arg.IndexOf('=');
            if (equalsIndex > 0)
            {
                key = arg[..equalsIndex];
                value = arg[(equalsIndex + 1)..];
            }
            else if (index + 1 < args.Length && !args[index + 1].StartsWith("--", StringComparison.Ordinal))
            {
                value = args[++index];
            }

            if (value is null)
            {
                flags.Add(key);
            }
            else
            {
                values[key] = value;
            }
        }

        var probeRoot = values.GetValueOrDefault("--repo-root")
            ?? values.GetValueOrDefault("--root")
            ?? Directory.GetCurrentDirectory();
        var repoRoot = RepositoryPaths.FindRepoRoot(probeRoot);
        if (repoRoot is null)
        {
            Console.Error.WriteLine("Unable to locate repository root. Pass --repo-root or run inside the BootstrapBlazor repository.");
            return null;
        }

        var outputRoot = values.GetValueOrDefault("--output")
            ?? Path.Combine(repoRoot, "artifacts", "llms-docs");

        return new GeneratorOptions(
            RepoRoot: Path.GetFullPath(repoRoot),
            OutputRoot: Path.GetFullPath(outputRoot),
            ComponentName: values.GetValueOrDefault("--component"),
            IndexOnly: flags.Contains("--index-only"),
            Check: flags.Contains("--check"));
    }

    private static void PrintHelp()
    {
        Console.WriteLine("""
            BootstrapBlazor LLMs documentation generator

            Options:
              --repo-root <path>    Repository root. Defaults to current directory lookup.
              --root <path>         Compatibility alias used by the publish target.
              --output <path>       Publish/output root. Generated files go to wwwroot/llms.
              --component <name>    Generate one component.
              --index-only          Generate llms.txt only.
              --check               Compare generated output with existing files.
              --help                Show help.
            """);
    }
}
