namespace BootstrapBlazor.Mcp;

public sealed class McpServerOptions
{
    public string? RepoRoot { get; init; }

    public string? PackageRoot { get; init; }

    public string? ProjectDir { get; init; }

    public static McpServerOptions Parse(string[] args)
    {
        var options = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        for (var index = 0; index < args.Length; index++)
        {
            var arg = args[index];
            if (!arg.StartsWith("--", StringComparison.Ordinal))
            {
                continue;
            }

            var name = arg[2..];
            if (string.IsNullOrWhiteSpace(name) || index + 1 >= args.Length)
            {
                continue;
            }

            options[name] = args[++index];
        }

        return new McpServerOptions
        {
            RepoRoot = GetOption(options, "repo-root"),
            PackageRoot = GetOption(options, "package-root"),
            ProjectDir = GetOption(options, "project-dir")
        };
    }

    private static string? GetOption(Dictionary<string, string> options, string name)
    {
        return options.TryGetValue(name, out var value) && !string.IsNullOrWhiteSpace(value)
            ? value
            : null;
    }
}
