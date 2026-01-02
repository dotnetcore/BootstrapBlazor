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
    private readonly string _sourcePath;
    private readonly ComponentAnalyzer _analyzer;
    private readonly MarkdownBuilder _markdownBuilder;

    public DocsGenerator(string outputPath)
    {
        _outputPath = Path.GetFullPath(outputPath);

        // Find the source directory (relative to tool location or current directory)
        _sourcePath = FindSourcePath();

        _analyzer = new ComponentAnalyzer(_sourcePath);
        _markdownBuilder = new MarkdownBuilder();
    }

    private string FindSourcePath()
    {
        // Try to find src/BootstrapBlazor from current directory or parent directories
        var current = Directory.GetCurrentDirectory();

        for (int i = 0; i < 5; i++)
        {
            var srcPath = Path.Combine(current, "src", "BootstrapBlazor");
            if (Directory.Exists(srcPath))
            {
                return srcPath;
            }

            var parent = Directory.GetParent(current);
            if (parent == null) break;
            current = parent.FullName;
        }

        throw new DirectoryNotFoundException(
            "Could not find src/BootstrapBlazor directory. " +
            "Please run this tool from the BootstrapBlazor repository root.");
    }

    /// <summary>
    /// Generate all documentation files
    /// </summary>
    public async Task GenerateAllAsync()
    {
        Console.WriteLine($"Source path: {_sourcePath}");
        Console.WriteLine($"Output path: {_outputPath}");
        Console.WriteLine();

        // Analyze all components
        Console.WriteLine("Analyzing components...");
        var components = await _analyzer.AnalyzeAllComponentsAsync();
        Console.WriteLine($"Found {components.Count} components");
        Console.WriteLine();

        // Group components by category
        var categorized = CategorizeComponents(components);

        // Generate index file
        await GenerateIndexAsync(categorized);

        // Generate component documentation files
        foreach (var category in categorized)
        {
            Console.WriteLine($"Generating {category.Key} documentation...");
            await GenerateCategoryDocAsync(category.Key, category.Value);
        }

        Console.WriteLine();
        Console.WriteLine("Documentation generation complete!");
    }

    /// <summary>
    /// Generate only the index file
    /// </summary>
    public async Task GenerateIndexAsync()
    {
        var components = await _analyzer.AnalyzeAllComponentsAsync();
        var categorized = CategorizeComponents(components);
        await GenerateIndexAsync(categorized);
    }

    private async Task GenerateIndexAsync(Dictionary<string, List<ComponentInfo>> categorized)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(_outputPath);

        var indexPath = Path.Combine(_outputPath, "llms.txt");
        var content = _markdownBuilder.BuildIndex(categorized);
        await File.WriteAllTextAsync(indexPath, content);
        Console.WriteLine($"Generated: {indexPath}");
    }

    /// <summary>
    /// Generate documentation for a specific component
    /// </summary>
    public async Task GenerateComponentAsync(string componentName)
    {
        var component = await _analyzer.AnalyzeComponentAsync(componentName);
        if (component == null)
        {
            Console.WriteLine($"Component not found: {componentName}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(_outputPath);

        var content = _markdownBuilder.BuildComponentDoc(component);
        var fileName = $"llms-{componentName.ToLowerInvariant()}.txt";
        var filePath = Path.Combine(_outputPath, fileName);
        await File.WriteAllTextAsync(filePath, content);
        Console.WriteLine($"Generated: {filePath}");
    }

    /// <summary>
    /// Check if documentation is up-to-date
    /// </summary>
    public async Task<bool> CheckAsync()
    {
        Console.WriteLine("Checking documentation freshness...");

        var components = await _analyzer.AnalyzeAllComponentsAsync();
        var categorized = CategorizeComponents(components);

        // Check index file
        var indexPath = Path.Combine(_outputPath, "llms.txt");
        if (!File.Exists(indexPath))
        {
            Console.WriteLine("OUTDATED: llms.txt does not exist");
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
            Console.WriteLine("Index file is stale relative to component sources. Please regenerate docs.");
            return false;
        }
        // Check each category file
        foreach (var category in categorized)
        {
            var fileName = GetCategoryFileName(category.Key);
            var filePath = Path.Combine(_outputPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"OUTDATED: {fileName} does not exist");
                return false;
            }

            // Check if any source file is newer than the doc file
            var docLastWrite = File.GetLastWriteTimeUtc(filePath);
            foreach (var component in category.Value)
            {
                if (component.LastModified > docLastWrite)
                {
                    Console.WriteLine($"OUTDATED: {component.Name} was modified after {fileName}");
                    return false;
                }
            }
        }

        Console.WriteLine("Documentation is up-to-date");
        return true;
    }

    private Dictionary<string, List<ComponentInfo>> CategorizeComponents(List<ComponentInfo> components)
    {
        var categories = new Dictionary<string, List<ComponentInfo>>
        {
            ["table"] = new(),
            ["input"] = new(),
            ["select"] = new(),
            ["button"] = new(),
            ["dialog"] = new(),
            ["nav"] = new(),
            ["card"] = new(),
            ["treeview"] = new(),
            ["form"] = new(),
            ["other"] = new()
        };

        foreach (var component in components)
        {
            var category = GetComponentCategory(component.Name);
            if (categories.ContainsKey(category))
            {
                categories[category].Add(component);
            }
            else
            {
                categories["other"].Add(component);
            }
        }

        // Remove empty categories
        return categories.Where(c => c.Value.Count > 0)
                        .ToDictionary(c => c.Key, c => c.Value);
    }

    private string GetComponentCategory(string componentName)
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

    private string GetCategoryFileName(string category)
    {
        return $"llms-{category}.txt";
    }

    private async Task GenerateCategoryDocAsync(string category, List<ComponentInfo> components)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(_outputPath);

        var fileName = GetCategoryFileName(category);
        var filePath = Path.Combine(_outputPath, fileName);
        var content = _markdownBuilder.BuildCategoryDoc(category, components);
        await File.WriteAllTextAsync(filePath, content);
        Console.WriteLine($"Generated: {filePath}");
    }
}
