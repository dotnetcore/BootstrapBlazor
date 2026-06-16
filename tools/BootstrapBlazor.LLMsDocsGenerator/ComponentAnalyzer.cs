// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace BootstrapBlazor.LLMsDocsGenerator;

internal sealed partial class ComponentAnalyzer(string repoRoot)
{
    public async Task<ComponentDocument> AnalyzeAsync(SkillIndexEntry entry)
    {
        var document = new ComponentDocument
        {
            Name = entry.Name,
            ComponentPath = entry.Component,
            SkillPath = entry.Skill,
            SamplePath = entry.Sample
        };

        var componentDirectory = entry.Component is null ? null : RepositoryPaths.FromRepoPath(repoRoot, entry.Component);
        if (!string.IsNullOrWhiteSpace(componentDirectory) && Directory.Exists(componentDirectory))
        {
            var sourceFiles = Directory.GetFiles(componentDirectory, "*", SearchOption.AllDirectories)
                .Where(IsSourceFile)
                .OrderBy(file => file, StringComparer.OrdinalIgnoreCase)
                .ToList();

            document.SourceFiles.AddRange(sourceFiles.Select(file => RepositoryPaths.ToRepoPath(repoRoot, file)));

            var typeMap = new Dictionary<string, ComponentTypeInfo>(StringComparer.Ordinal);
            foreach (var file in sourceFiles.Where(file => Path.GetExtension(file).Equals(".cs", StringComparison.OrdinalIgnoreCase)))
            {
                await AnalyzeCSharpFileAsync(file, typeMap);
            }

            document.Types.AddRange(typeMap.Values
                .Where(type => ShouldIncludeType(document.Name, type))
                .OrderByDescending(type => string.Equals(type.Name, document.Name, StringComparison.Ordinal))
                .ThenBy(type => type.Name, StringComparer.Ordinal));
        }

        document.SampleUsage = await AnalyzeSampleAsync(entry);
        return document;
    }

    private static bool IsSourceFile(string file)
    {
        var extension = Path.GetExtension(file);
        return extension is ".razor" or ".cs" or ".js";
    }

    private static bool ShouldIncludeType(string componentName, ComponentTypeInfo type)
    {
        return string.Equals(type.Name, componentName, StringComparison.Ordinal)
            || type.Parameters.Count > 0
            || type.CascadingParameters.Count > 0
            || type.PublicMethods.Count > 0
            || type.ObsoleteMembers.Count > 0;
    }

    private async Task AnalyzeCSharpFileAsync(string file, Dictionary<string, ComponentTypeInfo> typeMap)
    {
        var code = await File.ReadAllTextAsync(file);
        var root = await CSharpSyntaxTree.ParseText(code).GetRootAsync();
        var repoPath = RepositoryPaths.ToRepoPath(repoRoot, file);

        foreach (var classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
        {
            var type = GetOrCreateType(typeMap, classDeclaration, root);
            AddUnique(type.SourceFiles, repoPath);

            type.Summary ??= ExtractXmlSummary(classDeclaration);
            type.BaseClass ??= classDeclaration.BaseList?.Types.FirstOrDefault()?.Type.ToString();
            type.HasJsInvokable |= HasAttribute(classDeclaration, "JSInvokable");

            foreach (var parameter in classDeclaration.TypeParameterList?.Parameters ?? [])
            {
                AddUnique(type.TypeParameters, parameter.Identifier.Text);
            }

            foreach (var property in classDeclaration.Members.OfType<PropertyDeclarationSyntax>())
            {
                var parameter = CreateParameter(property, repoPath);
                if (parameter is null)
                {
                    if (HasAttribute(property, "Obsolete"))
                    {
                        AddUnique(type.ObsoleteMembers, $"{property.Type} {property.Identifier.Text} - {GetObsoleteMessage(property)}");
                    }
                    continue;
                }

                if (HasAttribute(property, "CascadingParameter"))
                {
                    AddUnique(type.CascadingParameters, parameter, static item => item.Name);
                }
                else
                {
                    AddUnique(type.Parameters, parameter, static item => item.Name);
                }
            }

            foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
            {
                if (!method.Modifiers.Any(SyntaxKind.PublicKeyword) || method.Modifiers.Any(SyntaxKind.OverrideKeyword))
                {
                    continue;
                }

                var methodInfo = new MethodInfo
                {
                    Name = method.Identifier.Text,
                    ReturnType = SimplifyTypeName(method.ReturnType.ToString()),
                    Description = ExtractXmlSummary(method),
                    IsJSInvokable = HasAttribute(method, "JSInvokable"),
                    SourcePath = repoPath
                };

                foreach (var parameter in method.ParameterList.Parameters)
                {
                    methodInfo.Parameters.Add((SimplifyTypeName(parameter.Type?.ToString() ?? ""), parameter.Identifier.Text));
                }

                AddUnique(type.PublicMethods, methodInfo, static item => item.Name);
                type.HasJsInvokable |= methodInfo.IsJSInvokable;
            }
        }
    }

    private static ComponentTypeInfo GetOrCreateType(Dictionary<string, ComponentTypeInfo> typeMap, ClassDeclarationSyntax classDeclaration, SyntaxNode root)
    {
        var name = classDeclaration.Identifier.Text;
        if (typeMap.TryGetValue(name, out var type))
        {
            return type;
        }

        var namespaceName = root.DescendantNodes()
            .OfType<BaseNamespaceDeclarationSyntax>()
            .FirstOrDefault()?.Name.ToString();

        var fullName = string.IsNullOrWhiteSpace(namespaceName) ? name : $"{namespaceName}.{name}";
        if (classDeclaration.TypeParameterList is not null)
        {
            fullName += classDeclaration.TypeParameterList;
        }

        type = new ComponentTypeInfo
        {
            Name = name,
            FullName = fullName
        };
        typeMap[name] = type;
        return type;
    }

    private static ParameterInfo? CreateParameter(PropertyDeclarationSyntax property, string repoPath)
    {
        var isParameter = HasAttribute(property, "Parameter");
        var isCascadingParameter = HasAttribute(property, "CascadingParameter");
        if (!isParameter && !isCascadingParameter)
        {
            return null;
        }

        var type = SimplifyTypeName(property.Type.ToString());
        return new ParameterInfo
        {
            Name = property.Identifier.Text,
            Type = type,
            DefaultValue = property.Initializer is null ? null : SimplifyDefaultValue(property.Initializer.Value.ToString()),
            Description = ExtractXmlSummary(property),
            IsRequired = HasAttribute(property, "EditorRequired"),
            IsObsolete = HasAttribute(property, "Obsolete"),
            ObsoleteMessage = GetObsoleteMessage(property),
            IsEventCallback = IsEventType(type),
            IsTemplate = type.Contains("RenderFragment", StringComparison.Ordinal),
            SourcePath = repoPath
        };
    }

    private async Task<SampleUsage> AnalyzeSampleAsync(SkillIndexEntry entry)
    {
        var usage = new SampleUsage();
        if (entry.Sample is null)
        {
            return usage;
        }

        var samplePath = RepositoryPaths.FromRepoPath(repoRoot, entry.Sample);
        var files = Directory.Exists(samplePath)
            ? Directory.GetFiles(samplePath, "*", SearchOption.AllDirectories)
                .Where(file => Path.GetExtension(file) is ".razor" or ".cs")
                .OrderBy(file => file, StringComparer.OrdinalIgnoreCase)
                .ToArray()
            : File.Exists(samplePath)
                ? GetSampleFileSet(samplePath)
                : [];

        usage.Files.AddRange(files.Select(file => RepositoryPaths.ToRepoPath(repoRoot, file)));
        var text = string.Join(Environment.NewLine, await Task.WhenAll(files.Select(file => File.ReadAllTextAsync(file))));

        var tagPattern = $@"(?ms)<\s*{Regex.Escape(entry.Name)}(?=[\s>/])(?<attrs>.{{0,2000}}?)(?:/?>)";
        var tags = TagRegex(tagPattern).Matches(text);
        usage.DirectTagCount = tags.Count;

        foreach (Match tag in tags)
        {
            foreach (Match attribute in AttributeRegex().Matches(tag.Groups["attrs"].Value))
            {
                AddUnique(usage.Attributes, attribute.Groups["name"].Value);
            }
        }

        var snippetPattern = $@"(?ms)<\s*{Regex.Escape(entry.Name)}(?=[\s>/]).{{0,1600}}?(?:/>\s*|</\s*{Regex.Escape(entry.Name)}\s*>)";
        var snippetMatch = TagRegex(snippetPattern).Match(text);
        if (snippetMatch.Success)
        {
            var snippet = TextHelpers.RemoveNonAscii(snippetMatch.Value.Replace("\r\n", "\n", StringComparison.Ordinal)).Trim();
            var lines = snippet.Split('\n');
            usage.Snippet = lines.Length > 28
                ? string.Join('\n', lines.Take(26)) + "\n..."
                : snippet;
        }

        usage.Attributes.Sort(StringComparer.Ordinal);
        return usage;
    }

    private static string[] GetSampleFileSet(string samplePath)
    {
        var files = new List<string> { samplePath };
        var codeBehind = samplePath + ".cs";
        if (File.Exists(codeBehind))
        {
            files.Add(codeBehind);
        }

        return files.ToArray();
    }

    private static string? ExtractXmlSummary(SyntaxNode node)
    {
        var xmlText = node.GetLeadingTrivia().ToFullString();
        if (string.IsNullOrWhiteSpace(xmlText))
        {
            return null;
        }

        var match = SummaryRegex().Match(xmlText);
        if (!match.Success)
        {
            return null;
        }

        var summary = match.Groups["summary"].Value;
        var english = EnglishParaRegex().Match(summary);
        if (english.Success)
        {
            summary = english.Groups["text"].Value;
        }

        summary = LineCommentRegex().Replace(summary, "");
        summary = XmlTagRegex().Replace(summary, "");
        summary = TextHelpers.RemoveNonAscii(TextHelpers.NormalizeWhitespace(summary));
        return string.IsNullOrWhiteSpace(summary) ? null : summary;
    }

    private static bool HasAttribute(MemberDeclarationSyntax member, string name)
    {
        return member.AttributeLists
            .SelectMany(list => list.Attributes)
            .Any(attribute => IsAttributeName(attribute.Name.ToString(), name));
    }

    private static bool IsAttributeName(string actual, string expected)
    {
        var shortName = actual.Split('.').Last();
        return string.Equals(shortName, expected, StringComparison.Ordinal)
            || string.Equals(shortName, expected + "Attribute", StringComparison.Ordinal);
    }

    private static string? GetObsoleteMessage(MemberDeclarationSyntax member)
    {
        var obsolete = member.AttributeLists
            .SelectMany(list => list.Attributes)
            .FirstOrDefault(attribute => IsAttributeName(attribute.Name.ToString(), "Obsolete"));

        return obsolete?.ArgumentList?.Arguments.Count > 0
            ? obsolete.ArgumentList.Arguments[0].ToString().Trim('"')
            : null;
    }

    private static bool IsEventType(string type)
    {
        return type.Contains("EventCallback", StringComparison.Ordinal)
            || type.StartsWith("Func<", StringComparison.Ordinal)
            || type.StartsWith("Action<", StringComparison.Ordinal)
            || type is "Func" or "Action"
            || type.StartsWith("Func<", StringComparison.Ordinal)
            || type.StartsWith("Action<", StringComparison.Ordinal);
    }

    private static string SimplifyDefaultValue(string value)
    {
        return value switch
        {
            "string.Empty" or "\"\"" => "\"\"",
            "true" or "false" or "null" or "0" => value,
            _ when value.StartsWith("new ", StringComparison.Ordinal) => "new()",
            _ => value
        };
    }

    private static string SimplifyTypeName(string typeName)
    {
        var result = typeName
            .Replace("System.", "", StringComparison.Ordinal)
            .Replace("Collections.Generic.", "", StringComparison.Ordinal)
            .Replace("Threading.Tasks.", "", StringComparison.Ordinal);

        result = NullableRegex().Replace(result, "$1?");

        return result
            .Replace("Int32", "int", StringComparison.Ordinal)
            .Replace("Int64", "long", StringComparison.Ordinal)
            .Replace("Boolean", "bool", StringComparison.Ordinal)
            .Replace("String", "string", StringComparison.Ordinal)
            .Replace("Object", "object", StringComparison.Ordinal);
    }

    private static void AddUnique(List<string> list, string item)
    {
        if (!string.IsNullOrWhiteSpace(item) && !list.Contains(item, StringComparer.Ordinal))
        {
            list.Add(item);
        }
    }

    private static void AddUnique<T>(List<T> list, T item, Func<T, string> keySelector)
    {
        var key = keySelector(item);
        if (!list.Any(existing => string.Equals(keySelector(existing), key, StringComparison.Ordinal)))
        {
            list.Add(item);
        }
    }

    [GeneratedRegex(@"Nullable<([^>]+)>")]
    private static partial Regex NullableRegex();

    [GeneratedRegex(@"<summary>\s*(?<summary>.*?)\s*</summary>", RegexOptions.Singleline)]
    private static partial Regex SummaryRegex();

    [GeneratedRegex(@"<para\s+lang=""en"">\s*(?<text>.*?)\s*</para>", RegexOptions.Singleline)]
    private static partial Regex EnglishParaRegex();

    [GeneratedRegex(@"(?m)^\s*///\s?")]
    private static partial Regex LineCommentRegex();

    [GeneratedRegex(@"<.*?>", RegexOptions.Singleline)]
    private static partial Regex XmlTagRegex();

    [GeneratedRegex(@"(?:^|\s)(?<name>@?[A-Za-z_][A-Za-z0-9_:\.-]*)\s*=")]
    private static partial Regex AttributeRegex();

    private static Regex TagRegex(string pattern) => new(pattern, RegexOptions.Compiled);
}
