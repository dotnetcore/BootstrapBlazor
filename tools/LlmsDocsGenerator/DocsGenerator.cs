// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace LlmsDocsGenerator;

/// <summary>
/// Main documentation generator class
/// </summary>
public class DocsGenerator
{
    private readonly string _outputPath;
    private readonly string _componentsOutputPath;
    private readonly string _sourcePath;
    private readonly ComponentAnalyzer _analyzer;
    private readonly MarkdownBuilder _markdownBuilder;
    private readonly bool _debug;

    public DocsGenerator(string? rootFolder, bool debug)
    {
        _debug = debug;

        // Find the source directory (relative to tool location or current directory)
        var root = FindSourcePath(rootFolder);

        _sourcePath = Path.Combine(root, "src", "BootstrapBlazor");
        _outputPath = Path.Combine(root, "src", "BootstrapBlazor.Server", "wwwroot", "llms");
        _componentsOutputPath = Path.Combine(_outputPath, "components");
        _analyzer = new ComponentAnalyzer(_sourcePath);
        _markdownBuilder = new MarkdownBuilder();
    }

    private string FindSourcePath(string? rootFolder)
    {
        // Try to find src/BootstrapBlazor from current directory or parent directories
        var current = rootFolder ?? AppContext.BaseDirectory;
        Logger($"Root path: {current}");

        while (!string.IsNullOrEmpty(current))
        {
            var parent = Directory.GetParent(current);
            if (parent == null)
            {
                break;
            }
            if (parent.Name.Equals("BootstrapBlazor", StringComparison.OrdinalIgnoreCase))
            {
                return parent.FullName;
            }
            current = parent.FullName;
        }

        throw new DirectoryNotFoundException("Could not find src directory. Please run this tool from the BootstrapBlazor repository root.");
    }

    /// <summary>
    /// Generate all documentation files
    /// </summary>
    public async Task GenerateAllAsync()
    {
        Logger($"Source path: {_sourcePath}");
        Logger($"Output path: {_outputPath}");
        Logger($"Components path: {_componentsOutputPath}");

        // Ensure output directories exist
        Directory.CreateDirectory(_outputPath);
        Directory.CreateDirectory(_componentsOutputPath);

        // Analyze all components
        Logger("Analyzing components...");
        var components = await _analyzer.AnalyzeAllComponentsAsync();
        Logger($"Found {components.Count} components");

        // Generate index file
        await GenerateIndexAsync(components);

        // Generate individual component documentation files
        Logger("Generating individual component documentation...");
        foreach (var component in components)
        {
            await GenerateComponentDocAsync(component);
        }

        Logger("Documentation generation complete!");
    }

    /// <summary>
    /// Generate only the index file
    /// </summary>
    public async Task GenerateIndexAsync()
    {
        var components = await _analyzer.AnalyzeAllComponentsAsync();
        await GenerateIndexAsync(components);
    }

    private async Task GenerateIndexAsync(List<ComponentInfo> components)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(_outputPath);

        var indexPath = Path.Combine(_outputPath, "llms.txt");
        var content = _markdownBuilder.BuildIndex(components);
        await File.WriteAllTextAsync(indexPath, content);
        Logger($"Generated: {indexPath}");
    }

    /// <summary>
    /// Generate documentation for a specific component
    /// </summary>
    public async Task GenerateComponentAsync(string componentName)
    {
        var component = await _analyzer.AnalyzeComponentAsync(componentName);
        if (component == null)
        {
            Logger($"Component not found: {componentName}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(_componentsOutputPath);

        await GenerateComponentDocAsync(component);
    }

    private async Task GenerateComponentDocAsync(ComponentInfo component)
    {
        var content = _markdownBuilder.BuildComponentDoc(component);
        var fileName = $"{component.Name}.txt";
        var filePath = Path.Combine(_componentsOutputPath, fileName);
        await File.WriteAllTextAsync(filePath, content);
        Logger($"Generated: {filePath}");
    }

    /// <summary>
    /// Check if documentation is up-to-date
    /// </summary>
    public async Task<bool> CheckAsync()
    {
        Logger("Checking documentation freshness...");

        var components = await _analyzer.AnalyzeAllComponentsAsync();

        // Check index file
        var indexPath = Path.Combine(_outputPath, "llms.txt");
        if (!File.Exists(indexPath))
        {
            Logger("OUTDATED: llms.txt does not exist");
            return false;
        }

        var indexLastWrite = File.GetLastWriteTimeUtc(indexPath);

        // compute the most recent component source timestamp:
        var newestComponentWrite = components
            .Select(c => c.LastModified)
            .DefaultIfEmpty(indexLastWrite)
            .Max();

        if (indexLastWrite < newestComponentWrite)
        {
            Logger("Index file is stale relative to component sources. Please regenerate docs.");
            return false;
        }

        // Check each component file
        foreach (var component in components)
        {
            var fileName = $"{component.Name}.txt";
            var filePath = Path.Combine(_componentsOutputPath, fileName);

            if (!File.Exists(filePath))
            {
                Logger($"OUTDATED: {fileName} does not exist");
                return false;
            }

            // Check if source file is newer than the doc file
            var docLastWrite = File.GetLastWriteTimeUtc(filePath);
            if (component.LastModified > docLastWrite)
            {
                Logger($"OUTDATED: {component.Name} was modified after {fileName}");
                return false;
            }
        }

        Logger("Documentation is up-to-date");
        return true;
    }

    private static string GetComponentCategory(string componentName)
    {
        return componentName.ToLowerInvariant() switch
        {
            // Table family
            var n when n.Contains("table") => "table",

            // Input family
            var n when n.Contains("input") || n.Contains("textarea") ||
                       n.Contains("password") || n == "otpinput" => "input",

            // Select family
            var n when n.Contains("select") || n.Contains("dropdown") ||
                       n.Contains("autocomplete") || n.Contains("cascader") ||
                       n.Contains("transfer") || n.Contains("multiselect") => "select",

            // Button family
            var n when n.Contains("button") || n == "gotop" ||
                       n.Contains("popconfirm") => "button",

            // Dialog family
            var n when n.Contains("dialog") || n.Contains("modal") ||
                       n.Contains("drawer") || n.Contains("swal") ||
                       n.Contains("toast") || n.Contains("message") => "dialog",

            // Navigation family
            var n when n.Contains("menu") || n.Contains("tab") ||
                       n.Contains("breadcrumb") || n.Contains("step") ||
                       n.Contains("anchor") || n.Contains("nav") => "nav",

            // Card/Container family
            var n when n.Contains("card") || n.Contains("collapse") ||
                       n.Contains("groupbox") || n.Contains("panel") => "card",

            // TreeView
            var n when n.Contains("tree") => "treeview",

            // Form
            var n when n.Contains("validateform") || n.Contains("editorform") ||
                       n.Contains("validator") => "form",

            _ => "other"
        };
    }

    private void Logger(string message)
    {
        if (_debug)
        {
            Console.WriteLine(message);
        }
    }
}
