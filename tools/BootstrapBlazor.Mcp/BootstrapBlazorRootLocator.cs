using System.Text.Json;

namespace BootstrapBlazor.Mcp;

public static class BootstrapBlazorRootLocator
{
    public static BootstrapBlazorRoot Locate(McpServerOptions options)
    {
        if (!string.IsNullOrWhiteSpace(options.PackageRoot))
        {
            return FromPackageRoot(options.PackageRoot);
        }

        if (!string.IsNullOrWhiteSpace(options.ProjectDir))
        {
            return FromProjectDir(options.ProjectDir);
        }

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

        throw new InvalidOperationException("Unable to locate skill-index.json. Use --repo-root, --package-root, or --project-dir.");
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

    private static BootstrapBlazorRoot FromPackageRoot(string packageRoot)
    {
        var root = Path.GetFullPath(packageRoot);
        var indexPath = Path.Combine(root, "skill-index.json");
        if (!File.Exists(indexPath))
        {
            throw new FileNotFoundException("Package skill-index.json was not found.", indexPath);
        }

        return new BootstrapBlazorRoot(BootstrapBlazorRootMode.Package, root, indexPath);
    }

    private static BootstrapBlazorRoot FromProjectDir(string projectDir)
    {
        var projectRoot = Path.GetFullPath(projectDir);
        var assetsPath = Path.Combine(projectRoot, "obj", "project.assets.json");
        if (!File.Exists(assetsPath))
        {
            throw new FileNotFoundException("project.assets.json was not found. Restore the project before starting the MCP server.", assetsPath);
        }

        using var document = JsonDocument.Parse(File.ReadAllText(assetsPath));
        var root = LocateBootstrapBlazorPackage(document.RootElement);
        var indexPath = Path.Combine(root, "skill-index.json");
        if (!File.Exists(indexPath))
        {
            throw new FileNotFoundException("BootstrapBlazor package skill-index.json was not found.", indexPath);
        }

        return new BootstrapBlazorRoot(
            BootstrapBlazorRootMode.ProjectPackage,
            root,
            indexPath,
            projectRoot);
    }

    private static string LocateBootstrapBlazorPackage(JsonElement assets)
    {
        if (!assets.TryGetProperty("libraries", out var libraries))
        {
            throw new InvalidOperationException("project.assets.json does not contain libraries.");
        }

        string? packageRelativePath = null;
        foreach (var library in libraries.EnumerateObject())
        {
            var separatorIndex = library.Name.IndexOf('/', StringComparison.Ordinal);
            var packageName = separatorIndex >= 0 ? library.Name[..separatorIndex] : library.Name;
            if (!packageName.Equals("BootstrapBlazor", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (library.Value.TryGetProperty("path", out var pathElement))
            {
                packageRelativePath = pathElement.GetString();
            }

            packageRelativePath ??= library.Name;
            break;
        }

        if (string.IsNullOrWhiteSpace(packageRelativePath))
        {
            throw new InvalidOperationException("BootstrapBlazor package was not found in project.assets.json.");
        }

        if (!assets.TryGetProperty("packageFolders", out var packageFolders))
        {
            throw new InvalidOperationException("project.assets.json does not contain packageFolders.");
        }

        foreach (var folder in packageFolders.EnumerateObject())
        {
            var fullPath = Path.GetFullPath(Path.Combine(folder.Name, packageRelativePath.Replace('/', Path.DirectorySeparatorChar)));
            if (Directory.Exists(fullPath))
            {
                return fullPath;
            }
        }

        throw new DirectoryNotFoundException($"BootstrapBlazor package directory was not found: {packageRelativePath}");
    }
}
