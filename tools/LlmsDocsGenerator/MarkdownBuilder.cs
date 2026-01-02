// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text;

namespace LlmsDocsGenerator;

/// <summary>
/// Builds Markdown documentation for components
/// </summary>
public class MarkdownBuilder
{
    private const string GitHubBaseUrl = "https://github.com/dotnetcore/BootstrapBlazor/blob/main/";
    private readonly StringBuilder _sb = new();

    /// <summary>
    /// Build the main llms.txt index file
    /// </summary>
    public string BuildIndex(List<ComponentInfo> components)
    {
        _sb.Clear();

        _sb.AppendLine("# BootstrapBlazor");
        _sb.AppendLine();
        _sb.AppendLine("> Enterprise-class Blazor UI component library based on Bootstrap 5");
        _sb.AppendLine();

        // Quick Start section
        _sb.AppendLine("## Quick Start");
        _sb.AppendLine();
        _sb.AppendLine("```bash");
        _sb.AppendLine("dotnet add package BootstrapBlazor");
        _sb.AppendLine("```");
        _sb.AppendLine();
        _sb.AppendLine("### Configuration");
        _sb.AppendLine();
        _sb.AppendLine("```csharp");
        _sb.AppendLine("// Program.cs");
        _sb.AppendLine("builder.Services.AddBootstrapBlazor();");
        _sb.AppendLine("```");
        _sb.AppendLine();
        _sb.AppendLine("```razor");
        _sb.AppendLine("@* _Imports.razor *@");
        _sb.AppendLine("@using BootstrapBlazor.Components");
        _sb.AppendLine("```");
        _sb.AppendLine();
        _sb.AppendLine("```html");
        _sb.AppendLine("<!-- App.razor or _Host.cshtml -->");
        _sb.AppendLine("<link href=\"_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css\" rel=\"stylesheet\" />");
        _sb.AppendLine("<script src=\"_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js\"></script>");
        _sb.AppendLine("```");
        _sb.AppendLine();

        // Component List - grouped by category for easy navigation
        _sb.AppendLine("## Components");
        _sb.AppendLine();
        _sb.AppendLine("Each component has its own documentation file in the `components/` directory.");
        _sb.AppendLine("Use `components/{ComponentName}.txt` to get detailed API information.");
        _sb.AppendLine();

        // Group components by category for the index
        var categorized = CategorizeComponents(components);

        var categoryDescriptions = new Dictionary<string, (string Title, string Description)>
        {
            ["table"] = ("Data Display - Table", "Complex data table with sorting, filtering, paging, editing"),
            ["input"] = ("Form Inputs", "Text input, number input, textarea, date picker"),
            ["select"] = ("Selection Components", "Select, multi-select, autocomplete, cascader, transfer"),
            ["button"] = ("Buttons", "Button, button group, dropdown button, split button"),
            ["dialog"] = ("Dialogs & Feedback", "Modal, drawer, dialog service, message, toast"),
            ["nav"] = ("Navigation", "Menu, tabs, breadcrumb, steps, pagination"),
            ["card"] = ("Containers", "Card, collapse, group box, split, layout"),
            ["treeview"] = ("Tree Components", "TreeView, tree select"),
            ["form"] = ("Form Validation", "ValidateForm, editor form, validation rules"),
            ["other"] = ("Other Components", "Miscellaneous components")
        };

        foreach (var (category, categoryComponents) in categorized.OrderBy(c => c.Key))
        {
            if (categoryComponents.Count == 0) continue;

            var (title, description) = categoryDescriptions.GetValueOrDefault(category, (category, ""));
            _sb.AppendLine($"### {title}");
            _sb.AppendLine();
            _sb.AppendLine($"{description}");
            _sb.AppendLine();

            // List components with links to their individual docs
            foreach (var component in categoryComponents.OrderBy(c => c.Name))
            {
                var summary = !string.IsNullOrEmpty(component.Summary)
                    ? $" - {TruncateSummary(component.Summary, 60)}"
                    : "";
                _sb.AppendLine($"- [{component.Name}](components/{component.Name}.txt){summary}");
            }
            _sb.AppendLine();
        }

        // Source Code Reference
        _sb.AppendLine("## Source Code Reference");
        _sb.AppendLine();
        _sb.AppendLine("GitHub Repository: https://github.com/dotnetcore/BootstrapBlazor");
        _sb.AppendLine();
        _sb.AppendLine("When documentation is insufficient, consult the source code:");
        _sb.AppendLine();
        _sb.AppendLine("### File Structure");
        _sb.AppendLine();
        _sb.AppendLine("```");
        _sb.AppendLine($"{GitHubBaseUrl}src/BootstrapBlazor/Components/{{ComponentName}}/");
        _sb.AppendLine("├── {Component}.razor          # Razor template");
        _sb.AppendLine("├── {Component}.razor.cs       # Component logic & parameters");
        _sb.AppendLine("├── {Component}Base.cs         # Base class (if exists)");
        _sb.AppendLine("├── {Component}Option.cs       # Configuration options");
        _sb.AppendLine("└── {Component}Service.cs      # Service class (Dialog, Toast, etc.)");
        _sb.AppendLine("```");
        _sb.AppendLine();
        _sb.AppendLine("### Examples");
        _sb.AppendLine();
        _sb.AppendLine("```");
        _sb.AppendLine($"{GitHubBaseUrl}src/BootstrapBlazor.Server/Components/Samples/{{ComponentName}}s.razor");
        _sb.AppendLine("```");
        _sb.AppendLine();
        _sb.AppendLine("### Reading Component Parameters");
        _sb.AppendLine();
        _sb.AppendLine("Look for properties with `[Parameter]` attribute:");
        _sb.AppendLine();
        _sb.AppendLine("```csharp");
        _sb.AppendLine("/// <summary>");
        _sb.AppendLine("/// Gets or sets whether to show the toolbar");
        _sb.AppendLine("/// </summary>");
        _sb.AppendLine("[Parameter]");
        _sb.AppendLine("public bool ShowToolbar { get; set; }");
        _sb.AppendLine("```");
        _sb.AppendLine();

        // Footer
        _sb.AppendLine("---");
        _sb.AppendLine($"Generated: {DateTime.UtcNow:yyyy-MM-dd}");
        _sb.AppendLine($"Total Components: {components.Count}");
        _sb.AppendLine($"Repository: {GitHubBaseUrl}");

        return _sb.ToString();
    }

    private static Dictionary<string, List<ComponentInfo>> CategorizeComponents(List<ComponentInfo> components)
    {
        var categories = new Dictionary<string, List<ComponentInfo>>
        {
            ["table"] = [],
            ["input"] = [],
            ["select"] = [],
            ["button"] = [],
            ["dialog"] = [],
            ["nav"] = [],
            ["card"] = [],
            ["treeview"] = [],
            ["form"] = [],
            ["other"] = []
        };

        foreach (var component in components)
        {
            var category = GetComponentCategory(component.Name);
            if (categories.TryGetValue(category, out var list))
            {
                list.Add(component);
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

    private static string GetComponentCategory(string componentName)
    {
        return componentName.ToLowerInvariant() switch
        {
            var n when n.Contains("table") => "table",
            var n when n.Contains("input") || n.Contains("textarea") ||
                       n.Contains("password") || n == "otpinput" => "input",
            var n when n.Contains("select") || n.Contains("dropdown") ||
                       n.Contains("autocomplete") || n.Contains("cascader") ||
                       n.Contains("transfer") || n.Contains("multiselect") => "select",
            var n when n.Contains("button") || n == "gotop" ||
                       n.Contains("popconfirm") => "button",
            var n when n.Contains("dialog") || n.Contains("modal") ||
                       n.Contains("drawer") || n.Contains("swal") ||
                       n.Contains("toast") || n.Contains("message") => "dialog",
            var n when n.Contains("menu") || n.Contains("tab") ||
                       n.Contains("breadcrumb") || n.Contains("step") ||
                       n.Contains("anchor") || n.Contains("nav") => "nav",
            var n when n.Contains("card") || n.Contains("collapse") ||
                       n.Contains("groupbox") || n.Contains("panel") => "card",
            var n when n.Contains("tree") => "treeview",
            var n when n.Contains("validateform") || n.Contains("editorform") ||
                       n.Contains("validator") => "form",
            _ => "other"
        };
    }

    private static string TruncateSummary(string summary, int maxLength)
    {
        if (string.IsNullOrEmpty(summary)) return "";
        summary = summary.Replace("\n", " ").Replace("\r", "").Trim();
        return summary.Length <= maxLength ? summary : summary[..(maxLength - 3)] + "...";
    }

    /// <summary>
    /// Build documentation for a single component
    /// </summary>
    public string BuildComponentDoc(ComponentInfo component)
    {
        _sb.Clear();

        _sb.AppendLine($"# BootstrapBlazor {component.Name}");
        _sb.AppendLine();

        if (!string.IsNullOrEmpty(component.Summary))
        {
            _sb.AppendLine($"> {component.Summary}");
            _sb.AppendLine();
        }

        BuildComponentSection(component, includeHeader: false);

        // Footer
        _sb.AppendLine("---");
        _sb.AppendLine($"<!-- Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC -->");

        return _sb.ToString();
    }

    private void BuildComponentSection(ComponentInfo component, bool includeHeader = true)
    {
        if (includeHeader)
        {
            _sb.AppendLine($"## {component.Name}");
            _sb.AppendLine();

            if (!string.IsNullOrEmpty(component.Summary))
            {
                _sb.AppendLine(component.Summary);
                _sb.AppendLine();
            }
        }

        // Type parameters
        if (component.TypeParameters.Count > 0)
        {
            _sb.AppendLine("### Type Parameters");
            _sb.AppendLine();
            foreach (var tp in component.TypeParameters)
            {
                _sb.AppendLine($"- `{tp}` - Generic type parameter");
            }
            _sb.AppendLine();
        }

        // Base class info
        if (!string.IsNullOrEmpty(component.BaseClass))
        {
            _sb.AppendLine($"**Inherits from**: `{component.BaseClass}`");
            _sb.AppendLine();
        }

        // Parameters table
        if (component.Parameters.Count > 0)
        {
            _sb.AppendLine("### Parameters");
            _sb.AppendLine();
            _sb.AppendLine("<!-- AUTO-GENERATED-PARAMETERS-START -->");
            _sb.AppendLine();
            _sb.AppendLine("| Parameter | Type | Default | Description |");
            _sb.AppendLine("|-----------|------|---------|-------------|");

            // Sort: required first, then events, then alphabetically
            var sortedParams = component.Parameters
                .OrderByDescending(p => p.IsRequired)
                .ThenBy(p => p.IsEventCallback)
                .ThenBy(p => p.Name);

            foreach (var param in sortedParams)
            {
                var required = param.IsRequired ? " **[Required]**" : "";
                var description = EscapeMarkdownCell(param.Description ?? "") + required;
                var defaultVal = param.DefaultValue ?? "-";
                var type = EscapeMarkdownCell(param.Type);

                _sb.AppendLine($"| {param.Name} | `{type}` | {defaultVal} | {description} |");
            }

            _sb.AppendLine();
            _sb.AppendLine("<!-- AUTO-GENERATED-PARAMETERS-END -->");
            _sb.AppendLine();
        }

        // Event callbacks (separate section for clarity)
        var eventCallbacks = component.Parameters.Where(p => p.IsEventCallback).ToList();
        if (eventCallbacks.Count > 0)
        {
            _sb.AppendLine("### Event Callbacks");
            _sb.AppendLine();
            _sb.AppendLine("| Event | Type | Description |");
            _sb.AppendLine("|-------|------|-------------|");

            foreach (var evt in eventCallbacks.OrderBy(e => e.Name))
            {
                var description = EscapeMarkdownCell(evt.Description ?? "");
                var type = EscapeMarkdownCell(evt.Type);
                _sb.AppendLine($"| {evt.Name} | `{type}` | {description} |");
            }

            _sb.AppendLine();
        }

        // Public methods
        if (component.PublicMethods.Count > 0)
        {
            _sb.AppendLine("### Public Methods");
            _sb.AppendLine();

            foreach (var method in component.PublicMethods.OrderBy(m => m.Name))
            {
                var paramStr = string.Join(", ", method.Parameters.Select(p => $"{p.Item1} {p.Item2}"));
                _sb.AppendLine($"- `{method.ReturnType} {method.Name}({paramStr})`");
                if (!string.IsNullOrEmpty(method.Description))
                {
                    _sb.AppendLine($"  - {method.Description}");
                }
                if (method.IsJSInvokable)
                {
                    _sb.AppendLine("  - *[JSInvokable]*");
                }
            }

            _sb.AppendLine();
        }

        // Source reference with GitHub URLs
        if (!string.IsNullOrEmpty(component.SourcePath))
        {
            _sb.AppendLine("### Source");
            _sb.AppendLine();
            var sourceUrl = $"{GitHubBaseUrl}{component.SourcePath}";
            _sb.AppendLine($"- Component: [{component.SourcePath}]({sourceUrl})");
            if (!string.IsNullOrEmpty(component.SamplePath))
            {
                var sampleUrl = $"{GitHubBaseUrl}{component.SamplePath}";
                _sb.AppendLine($"- Examples: [{component.SamplePath}]({sampleUrl})");
            }
            _sb.AppendLine();
        }
    }

    /// <summary>
    /// Build a minimal parameter table for embedding in existing docs
    /// </summary>
    public string BuildParameterTable(List<ParameterInfo> parameters)
    {
        _sb.Clear();

        _sb.AppendLine("| Parameter | Type | Default | Description |");
        _sb.AppendLine("|-----------|------|---------|-------------|");

        var sortedParams = parameters
            .OrderByDescending(p => p.IsRequired)
            .ThenBy(p => p.IsEventCallback)
            .ThenBy(p => p.Name);

        foreach (var param in sortedParams)
        {
            var required = param.IsRequired ? " **[Required]**" : "";
            var description = EscapeMarkdownCell(param.Description ?? "") + required;
            var defaultVal = param.DefaultValue ?? "-";
            var type = EscapeMarkdownCell(param.Type);

            _sb.AppendLine($"| {param.Name} | `{type}` | {defaultVal} | {description} |");
        }

        return _sb.ToString();
    }

    private static string EscapeMarkdownCell(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";

        return text
            .Replace("|", "\\|")
            .Replace("\n", " ")
            .Replace("\r", "");
    }
}
