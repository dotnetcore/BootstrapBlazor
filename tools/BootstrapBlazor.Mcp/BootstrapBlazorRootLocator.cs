namespace BootstrapBlazor.Mcp;

public static class BootstrapBlazorRootLocator
{
    public static BootstrapBlazorRoot Locate(McpServerOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.RepoRoot))
        {
            return FromRepositoryRoot(options.RepoRoot);
        }

        return FromCurrentDirectory();
    }

    private static BootstrapBlazorRoot FromCurrentDirectory()
    {
        var current = new DirectoryInfo(Environment.CurrentDirectory);
        while (current is not null)
        {
            var indexPath = Path.Combine(current.FullName, "skill-index.json");
            if (File.Exists(indexPath))
            {
                return new BootstrapBlazorRoot(
                    BootstrapBlazorRootMode.Repository,
                    current.FullName,
                    indexPath);
            }

            current = current.Parent;
        }

        throw new InvalidOperationException("Unable to locate skill-index.json. Use --repo-root.");
    }

    private static BootstrapBlazorRoot FromRepositoryRoot(string repoRoot)
    {
        var root = Path.GetFullPath(repoRoot);
        var indexPath = Path.Combine(root, "skill-index.json");
        if (!File.Exists(indexPath))
        {
            throw new FileNotFoundException("Repository skill-index.json was not found.", indexPath);
        }

        return new BootstrapBlazorRoot(BootstrapBlazorRootMode.Repository, root, indexPath);
    }
}
