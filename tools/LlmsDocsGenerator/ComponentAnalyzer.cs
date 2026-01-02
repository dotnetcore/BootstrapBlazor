// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace LlmsDocsGenerator;

/// <summary>
/// Analyzes Blazor component source files using Roslyn
/// </summary>
public partial class ComponentAnalyzer
{
    private readonly string _sourcePath;
    private readonly string _componentsPath;
    private readonly string _samplesPath;

    public ComponentAnalyzer(string sourcePath)
    {
        _sourcePath = sourcePath;
        _componentsPath = Path.Combine(sourcePath, "Components");
        _samplesPath = Path.Combine(Path.GetDirectoryName(sourcePath)!, "BootstrapBlazor.Server", "Components", "Samples");
    }

    /// <summary>
    /// Analyze all components in the source directory
    /// </summary>
    public async Task<List<ComponentInfo>> AnalyzeAllComponentsAsync()
    {
        var components = new List<ComponentInfo>();

        if (!Directory.Exists(_componentsPath))
        {
            Console.WriteLine($"Components directory not found: {_componentsPath}");
            return components;
        }

        // Find all .razor.cs files
        var files = Directory.GetFiles(_componentsPath, "*.razor.cs", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var component = await AnalyzeFileAsync(file);
            if (component != null && component.Parameters.Count > 0)
            {
                components.Add(component);
            }
        }

        // Also analyze .cs files that might be component base classes
        var csFiles = Directory.GetFiles(_componentsPath, "*Base.cs", SearchOption.AllDirectories);
        foreach (var file in csFiles)
        {
            var component = await AnalyzeFileAsync(file);
            if (component != null && component.Parameters.Count > 0)
            {
                components.Add(component);
            }
        }

        return components.OrderBy(c => c.Name).ToList();
    }

    /// <summary>
    /// Analyze a specific component by name
    /// </summary>
    public async Task<ComponentInfo?> AnalyzeComponentAsync(string componentName)
    {
        var pattern = $"{componentName}.razor.cs";
        var files = Directory.GetFiles(_componentsPath, pattern, SearchOption.AllDirectories);

        if (files.Length == 0)
        {
            // Try without .razor extension
            pattern = $"{componentName}.cs";
            files = Directory.GetFiles(_componentsPath, pattern, SearchOption.AllDirectories);
        }

        if (files.Length == 0)
        {
            return null;
        }

        return await AnalyzeFileAsync(files[0]);
    }

    private async Task<ComponentInfo?> AnalyzeFileAsync(string filePath)
    {
        try
        {
            var code = await File.ReadAllTextAsync(filePath);
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = await tree.GetRootAsync();

            // Find the class declaration
            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();

            if (classDeclaration == null)
            {
                return null;
            }

            var component = new ComponentInfo
            {
                Name = GetClassName(classDeclaration),
                FullName = GetFullClassName(classDeclaration, root),
                Summary = ExtractXmlSummary(classDeclaration),
                TypeParameters = GetTypeParameters(classDeclaration),
                BaseClass = GetBaseClass(classDeclaration),
                SourcePath = GetRelativePath(filePath),
                LastModified = File.GetLastWriteTimeUtc(filePath),
                SamplePath = FindSamplePath(GetClassName(classDeclaration))
            };

            // Extract parameters
            component.Parameters = ExtractParameters(classDeclaration);

            // Extract public methods
            component.PublicMethods = ExtractPublicMethods(classDeclaration);

            return component;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error analyzing {filePath}: {ex.Message}");
            return null;
        }
    }

    private string GetClassName(ClassDeclarationSyntax classDeclaration)
    {
        return classDeclaration.Identifier.Text;
    }

    private string GetFullClassName(ClassDeclarationSyntax classDeclaration, SyntaxNode root)
    {
        var namespaceName = root.DescendantNodes()
            .OfType<BaseNamespaceDeclarationSyntax>()
            .FirstOrDefault()?.Name.ToString() ?? "";

        var className = classDeclaration.Identifier.Text;

        if (classDeclaration.TypeParameterList != null)
        {
            className += classDeclaration.TypeParameterList.ToString();
        }

        return string.IsNullOrEmpty(namespaceName) ? className : $"{namespaceName}.{className}";
    }

    private List<string> GetTypeParameters(ClassDeclarationSyntax classDeclaration)
    {
        if (classDeclaration.TypeParameterList == null)
        {
            return new List<string>();
        }

        return classDeclaration.TypeParameterList.Parameters
            .Select(p => p.Identifier.Text)
            .ToList();
    }

    private string? GetBaseClass(ClassDeclarationSyntax classDeclaration)
    {
        var baseList = classDeclaration.BaseList;
        if (baseList == null) return null;

        var baseType = baseList.Types.FirstOrDefault();
        return baseType?.Type.ToString();
    }

    private List<ParameterInfo> ExtractParameters(ClassDeclarationSyntax classDeclaration)
    {
        var parameters = new List<ParameterInfo>();

        var properties = classDeclaration.DescendantNodes()
            .OfType<PropertyDeclarationSyntax>();

        foreach (var property in properties)
        {
            // Check if property has [Parameter] attribute
            var hasParameterAttr = property.AttributeLists
                .SelectMany(a => a.Attributes)
                .Any(a => a.Name.ToString() is "Parameter" or "ParameterAttribute");

            if (!hasParameterAttr) continue;

            var paramInfo = new ParameterInfo
            {
                Name = property.Identifier.Text,
                Type = SimplifyTypeName(property.Type?.ToString() ?? "unknown"),
                DefaultValue = GetDefaultValue(property),
                Description = ExtractXmlSummary(property),
                IsRequired = HasAttribute(property, "EditorRequired"),
                IsObsolete = HasAttribute(property, "Obsolete"),
                ObsoleteMessage = GetObsoleteMessage(property),
                IsEventCallback = property.Type?.ToString().Contains("EventCallback") ?? false
            };

            // Skip obsolete parameters
            if (!paramInfo.IsObsolete)
            {
                parameters.Add(paramInfo);
            }
        }

        return parameters.OrderBy(p => p.Name).ToList();
    }

    private List<MethodInfo> ExtractPublicMethods(ClassDeclarationSyntax classDeclaration)
    {
        var methods = new List<MethodInfo>();

        var methodDeclarations = classDeclaration.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(m => m.Modifiers.Any(mod => mod.IsKind(SyntaxKind.PublicKeyword)));

        foreach (var method in methodDeclarations)
        {
            // Skip property accessors, overrides of base class methods
            if (method.Modifiers.Any(m => m.IsKind(SyntaxKind.OverrideKeyword)))
                continue;

            var methodInfo = new MethodInfo
            {
                Name = method.Identifier.Text,
                ReturnType = SimplifyTypeName(method.ReturnType.ToString()),
                Description = ExtractXmlSummary(method),
                IsJSInvokable = HasAttribute(method, "JSInvokable"),
                Parameters = method.ParameterList.Parameters
                    .Select(p => (SimplifyTypeName(p.Type?.ToString() ?? ""), p.Identifier.Text))
                    .ToList()
            };

            methods.Add(methodInfo);
        }

        return methods;
    }

    private string? ExtractXmlSummary(SyntaxNode node)
    {
        var trivia = node.GetLeadingTrivia();
        var xmlTrivia = trivia.FirstOrDefault(t =>
            t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
            t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia));

        if (xmlTrivia == default)
            return null;

        var xmlText = xmlTrivia.ToString();

        // Extract content from <summary> tags
        var match = SummaryRegex().Match(xmlText);
        if (match.Success)
        {
            var summary = match.Groups[1].Value;
            // Clean up the summary
            summary = CleanXmlComment(summary);
            return string.IsNullOrWhiteSpace(summary) ? null : summary;
        }

        return null;
    }

    private string CleanXmlComment(string comment)
    {
        // Remove /// prefixes and extra whitespace
        var lines = comment.Split('\n')
            .Select(l => l.Trim().TrimStart('/').Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l));

        return string.Join(" ", lines);
    }

    private string? GetDefaultValue(PropertyDeclarationSyntax property)
    {
        var initializer = property.Initializer;
        if (initializer != null)
        {
            return SimplifyDefaultValue(initializer.Value.ToString());
        }

        // Check for default in constructor or OnParametersSet
        return null;
    }

    private string SimplifyDefaultValue(string value)
    {
        // Simplify common patterns
        if (value == "false") return "false";
        if (value == "true") return "true";
        if (value == "null") return "null";
        if (value == "0") return "0";
        if (value == "string.Empty" || value == "\"\"") return "\"\"";
        if (value.StartsWith("new ")) return "new()";

        return value;
    }

    private string SimplifyTypeName(string typeName)
    {
        // Remove common namespace prefixes
        var result = typeName
            .Replace("System.", "")
            .Replace("Collections.Generic.", "")
            .Replace("Threading.Tasks.", "");

        // Handle Nullable<T> -> T? (must be done carefully to not break other generics)
        result = NullableRegex().Replace(result, "$1?");

        // Simplify primitive type names
        result = result
            .Replace("Int32", "int")
            .Replace("Int64", "long")
            .Replace("Boolean", "bool")
            .Replace("String", "string")
            .Replace("Object", "object");

        return result;
    }

    [GeneratedRegex(@"Nullable<([^>]+)>")]
    private static partial Regex NullableRegex();

    private bool HasAttribute(MemberDeclarationSyntax member, string attributeName)
    {
        return member.AttributeLists
            .SelectMany(a => a.Attributes)
            .Any(a => a.Name.ToString() == attributeName ||
                     a.Name.ToString() == attributeName + "Attribute");
    }

    private string? GetObsoleteMessage(PropertyDeclarationSyntax property)
    {
        var obsoleteAttr = property.AttributeLists
            .SelectMany(a => a.Attributes)
            .FirstOrDefault(a => a.Name.ToString() is "Obsolete" or "ObsoleteAttribute");

        if (obsoleteAttr?.ArgumentList?.Arguments.Count > 0)
        {
            return obsoleteAttr.ArgumentList.Arguments[0].ToString().Trim('"');
        }

        return null;
    }

    private string GetRelativePath(string fullPath)
    {
        var basePath = Path.GetDirectoryName(Path.GetDirectoryName(_sourcePath))!;
        return Path.GetRelativePath(basePath, fullPath).Replace('\\', '/');
    }

    private string? FindSamplePath(string componentName)
    {
        if (!Directory.Exists(_samplesPath))
            return null;

        // Try common sample file naming patterns
        var patterns = new[]
        {
            $"{componentName}s.razor",
            $"{componentName}.razor",
            $"{componentName}Demo.razor",
            $"{componentName}Sample.razor"
        };

        foreach (var pattern in patterns)
        {
            var files = Directory.GetFiles(_samplesPath, pattern, SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                return GetRelativePath(files[0]);
            }
        }

        return null;
    }

    [GeneratedRegex(@"<summary>\s*(.*?)\s*</summary>", RegexOptions.Singleline)]
    private static partial Regex SummaryRegex();
}
