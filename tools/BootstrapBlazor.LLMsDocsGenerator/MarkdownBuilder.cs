// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using System.Text;

namespace BootstrapBlazor.LLMsDocsGenerator;

internal static class MarkdownBuilder
{
    private const string GitHubBaseUrl = "https://github.com/dotnetcore/BootstrapBlazor/blob/main/";

    public static string BuildIndexDoc(IReadOnlyList<ComponentDocument> components)
    {
        var builder = new StringBuilder();
        builder.AppendLine("# BootstrapBlazor");
        builder.AppendLine();
        builder.AppendLine("> LLM-friendly component reference generated from the current repository source, official Samples, and skill-index.json.");
        builder.AppendLine();
        builder.AppendLine("## Quick Start");
        builder.AppendLine();
        builder.AppendLine("```bash");
        builder.AppendLine("dotnet add package BootstrapBlazor");
        builder.AppendLine("```");
        builder.AppendLine();
        builder.AppendLine("```csharp");
        builder.AppendLine("builder.Services.AddBootstrapBlazor();");
        builder.AppendLine("```");
        builder.AppendLine();
        builder.AppendLine("```razor");
        builder.AppendLine("@using BootstrapBlazor.Components");
        builder.AppendLine("```");
        builder.AppendLine();
        builder.AppendLine("## Agent Rules");
        builder.AppendLine();
        builder.AppendLine("1. Load only the component files needed for the task.");
        builder.AppendLine("2. Prefer current source over official Samples, and Samples over Skill guidance.");
        builder.AppendLine("3. Do not invent parameters, events, template context types, or obsolete APIs.");
        builder.AppendLine();
        builder.AppendLine("## Components");
        builder.AppendLine();

        foreach (var group in components.GroupBy(component => GetCategory(component.Name)).OrderBy(group => group.Key))
        {
            builder.AppendLine($"### {GetCategoryTitle(group.Key)}");
            builder.AppendLine();
            foreach (var component in group.OrderBy(component => component.Name, StringComparer.OrdinalIgnoreCase))
            {
                var summary = component.Types.FirstOrDefault(type => string.Equals(type.Name, component.Name, StringComparison.Ordinal))?.Summary
                    ?? component.Types.FirstOrDefault()?.Summary;
                var suffix = string.IsNullOrWhiteSpace(summary) ? "" : $" - {TextHelpers.Truncate(summary, 80)}";
                builder.AppendLine($"- [{component.Name}](components/{component.Name}.txt){suffix}");
            }
            builder.AppendLine();
        }

        builder.AppendLine("## Source Code Reference");
        builder.AppendLine();
        builder.AppendLine($"Repository: {GitHubBaseUrl}");
        builder.AppendLine($"Total Components: {components.Count}");
        return builder.ToString();
    }

    public static string BuildComponentDoc(ComponentDocument component)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"# BootstrapBlazor {component.Name}");
        builder.AppendLine();
        builder.AppendLine("This file is generated from repository source. It is an API reference for agents, not a replacement for source validation.");
        builder.AppendLine();
        AppendSourceSection(builder, component);
        AppendSampleSection(builder, component);

        builder.AppendLine("## Component Types");
        builder.AppendLine();
        if (component.Types.Count == 0)
        {
            builder.AppendLine("No public component type with parameters or public methods was detected in the current source scan.");
            builder.AppendLine();
        }

        foreach (var type in component.Types)
        {
            AppendTypeSection(builder, type);
        }

        builder.AppendLine("## Generation Notes");
        builder.AppendLine();
        builder.AppendLine("- Parameter tables include `[Parameter]` properties detected in current C# source.");
        builder.AppendLine("- Cascading parameters are listed for implementation context and usually should not be set by consumers.");
        builder.AppendLine("- Official Sample snippets are extracted mechanically; inspect the Sample before copying advanced usage.");
        builder.AppendLine("- Existing component Skill files remain the manual guidance layer for usage nuance.");

        return builder.ToString();
    }

    private static void AppendSourceSection(StringBuilder builder, ComponentDocument component)
    {
        builder.AppendLine("## Source");
        builder.AppendLine();
        if (!string.IsNullOrWhiteSpace(component.ComponentPath))
        {
            builder.AppendLine($"- Component directory: [{component.ComponentPath}]({GitHubBaseUrl}{component.ComponentPath})");
        }
        if (!string.IsNullOrWhiteSpace(component.SamplePath))
        {
            builder.AppendLine($"- Official Sample: [{component.SamplePath}]({GitHubBaseUrl}{component.SamplePath})");
        }
        if (!string.IsNullOrWhiteSpace(component.SkillPath))
        {
            builder.AppendLine($"- Component Skill: [{component.SkillPath}]({GitHubBaseUrl}{component.SkillPath})");
        }
        builder.AppendLine();
        builder.AppendLine("### Files Reviewed");
        builder.AppendLine();
        foreach (var file in component.SourceFiles.Take(24))
        {
            builder.AppendLine($"- [{file}]({GitHubBaseUrl}{file})");
        }
        if (component.SourceFiles.Count > 24)
        {
            builder.AppendLine($"- ... {component.SourceFiles.Count - 24} more files in the component directory");
        }
        builder.AppendLine();
    }

    private static void AppendSampleSection(StringBuilder builder, ComponentDocument component)
    {
        builder.AppendLine("## Official Sample Usage");
        builder.AppendLine();
        if (string.IsNullOrWhiteSpace(component.SamplePath))
        {
            builder.AppendLine("No official Sample is listed in skill-index.json.");
            builder.AppendLine();
            return;
        }

        builder.AppendLine($"Direct `<{component.Name}>` tag usages detected: {component.SampleUsage.DirectTagCount}");
        builder.AppendLine();
        if (component.SampleUsage.Attributes.Count > 0)
        {
            builder.AppendLine("Observed attributes:");
            builder.AppendLine();
            builder.AppendLine(string.Join(", ", component.SampleUsage.Attributes.Select(attribute => $"`{attribute}`")));
            builder.AppendLine();
        }
        if (!string.IsNullOrWhiteSpace(component.SampleUsage.Snippet))
        {
            builder.AppendLine("Sample-derived snippet:");
            builder.AppendLine();
            builder.AppendLine("```razor");
            builder.AppendLine(component.SampleUsage.Snippet);
            builder.AppendLine("```");
            builder.AppendLine();
        }
    }

    private static void AppendTypeSection(StringBuilder builder, ComponentTypeInfo type)
    {
        builder.AppendLine($"### {type.FullName}");
        builder.AppendLine();
        if (!string.IsNullOrWhiteSpace(type.Summary))
        {
            builder.AppendLine(type.Summary);
            builder.AppendLine();
        }
        if (!string.IsNullOrWhiteSpace(type.BaseClass))
        {
            builder.AppendLine($"Inherits from: `{type.BaseClass}`");
            builder.AppendLine();
        }
        if (type.TypeParameters.Count > 0)
        {
            builder.AppendLine("Type parameters: " + string.Join(", ", type.TypeParameters.Select(parameter => $"`{parameter}`")));
            builder.AppendLine();
        }
        if (type.SourceFiles.Count > 0)
        {
            builder.AppendLine("Source files: " + string.Join(", ", type.SourceFiles.Select(file => $"[{file}]({GitHubBaseUrl}{file})")));
            builder.AppendLine();
        }

        AppendParameterTable(builder, "Parameters", type.Parameters);
        AppendParameterList(builder, "Event Callbacks", type.Parameters.Where(parameter => parameter.IsEventCallback));
        AppendParameterList(builder, "Templates And Child Content", type.Parameters.Where(parameter => parameter.IsTemplate));
        AppendParameterTable(builder, "Cascading Parameters", type.CascadingParameters);
        AppendMethods(builder, type);
        AppendObsoleteMembers(builder, type);
    }

    private static void AppendParameterTable(StringBuilder builder, string title, IEnumerable<ParameterInfo> parameters)
    {
        var list = parameters
            .OrderByDescending(parameter => parameter.IsRequired)
            .ThenBy(parameter => parameter.IsEventCallback)
            .ThenBy(parameter => parameter.IsTemplate)
            .ThenBy(parameter => parameter.Name, StringComparer.Ordinal)
            .ToList();

        builder.AppendLine($"#### {title}");
        builder.AppendLine();
        if (list.Count == 0)
        {
            builder.AppendLine("None detected.");
            builder.AppendLine();
            return;
        }

        builder.AppendLine("| Name | Type | Default | Notes | Source |");
        builder.AppendLine("| --- | --- | --- | --- | --- |");
        foreach (var parameter in list)
        {
            var notes = new List<string>();
            if (parameter.IsRequired)
            {
                notes.Add("Required");
            }
            if (parameter.IsEventCallback)
            {
                notes.Add("Callback/event parameter");
            }
            if (parameter.IsTemplate)
            {
                notes.Add("Template parameter");
            }
            if (parameter.IsObsolete)
            {
                notes.Add("Obsolete: " + (parameter.ObsoleteMessage ?? "do not use"));
            }
            if (!string.IsNullOrWhiteSpace(parameter.Description))
            {
                notes.Add(TextHelpers.Truncate(parameter.Description, 160));
            }

            var description = notes.Count == 0 ? "" : string.Join("; ", notes);
            builder.AppendLine($"| `{parameter.Name}` | `{TextHelpers.EscapeMarkdownCell(parameter.Type)}` | {parameter.DefaultValue ?? "-"} | {TextHelpers.EscapeMarkdownCell(description)} | [{parameter.SourcePath}]({GitHubBaseUrl}{parameter.SourcePath}) |");
        }
        builder.AppendLine();
    }

    private static void AppendParameterList(StringBuilder builder, string title, IEnumerable<ParameterInfo> parameters)
    {
        var list = parameters.OrderBy(parameter => parameter.Name, StringComparer.Ordinal).ToList();
        builder.AppendLine($"#### {title}");
        builder.AppendLine();
        if (list.Count == 0)
        {
            builder.AppendLine("None detected.");
        }
        else
        {
            builder.AppendLine(string.Join(", ", list.Select(parameter => $"`{parameter.Name}: {parameter.Type}`")));
        }
        builder.AppendLine();
    }

    private static void AppendMethods(StringBuilder builder, ComponentTypeInfo type)
    {
        builder.AppendLine("#### Public Methods");
        builder.AppendLine();
        if (type.PublicMethods.Count == 0)
        {
            builder.AppendLine("None detected.");
            builder.AppendLine();
            return;
        }

        foreach (var method in type.PublicMethods.OrderBy(method => method.Name, StringComparer.Ordinal))
        {
            var parameters = string.Join(", ", method.Parameters.Select(parameter => $"{parameter.Type} {parameter.Name}"));
            var jsInvokable = method.IsJSInvokable ? " [JSInvokable]" : "";
            builder.AppendLine($"- `{method.ReturnType} {method.Name}({parameters})`{jsInvokable}");
            if (!string.IsNullOrWhiteSpace(method.Description))
            {
                builder.AppendLine($"  - {method.Description}");
            }
        }
        builder.AppendLine();
    }

    private static void AppendObsoleteMembers(StringBuilder builder, ComponentTypeInfo type)
    {
        builder.AppendLine("#### Obsolete Members");
        builder.AppendLine();
        var obsoleteParameters = type.Parameters
            .Concat(type.CascadingParameters)
            .Where(parameter => parameter.IsObsolete)
            .Select(parameter => $"{parameter.Type} {parameter.Name} - {parameter.ObsoleteMessage ?? "do not use"}");
        var obsolete = obsoleteParameters.Concat(type.ObsoleteMembers).Distinct(StringComparer.Ordinal).ToList();
        if (obsolete.Count == 0)
        {
            builder.AppendLine("None detected.");
        }
        else
        {
            foreach (var item in obsolete)
            {
                builder.AppendLine($"- {item}");
            }
        }
        builder.AppendLine();
    }

    private static string GetCategory(string componentName)
    {
        var name = componentName.ToLowerInvariant();
        if (name.Contains("table", StringComparison.Ordinal)) return "table";
        if (name.Contains("input", StringComparison.Ordinal) || name.Contains("textarea", StringComparison.Ordinal) || name.Contains("password", StringComparison.Ordinal)) return "input";
        if (name.Contains("select", StringComparison.Ordinal) || name.Contains("dropdown", StringComparison.Ordinal) || name.Contains("autocomplete", StringComparison.Ordinal) || name.Contains("cascader", StringComparison.Ordinal) || name.Contains("transfer", StringComparison.Ordinal)) return "select";
        if (name.Contains("button", StringComparison.Ordinal) || name.Contains("popconfirm", StringComparison.Ordinal)) return "button";
        if (name.Contains("dialog", StringComparison.Ordinal) || name.Contains("modal", StringComparison.Ordinal) || name.Contains("drawer", StringComparison.Ordinal) || name.Contains("toast", StringComparison.Ordinal) || name.Contains("message", StringComparison.Ordinal)) return "dialog";
        if (name.Contains("menu", StringComparison.Ordinal) || name.Contains("tab", StringComparison.Ordinal) || name.Contains("breadcrumb", StringComparison.Ordinal) || name.Contains("nav", StringComparison.Ordinal)) return "nav";
        if (name.Contains("card", StringComparison.Ordinal) || name.Contains("collapse", StringComparison.Ordinal) || name.Contains("panel", StringComparison.Ordinal)) return "container";
        if (name.Contains("tree", StringComparison.Ordinal)) return "tree";
        if (name.Contains("form", StringComparison.Ordinal) || name.Contains("validate", StringComparison.Ordinal)) return "form";
        return "other";
    }

    private static string GetCategoryTitle(string category)
    {
        return category switch
        {
            "button" => "Buttons",
            "container" => "Containers",
            "dialog" => "Dialogs And Feedback",
            "form" => "Forms And Validation",
            "input" => "Inputs",
            "nav" => "Navigation",
            "select" => "Selection",
            "table" => "Tables",
            "tree" => "Trees",
            _ => "Other Components"
        };
    }
}
