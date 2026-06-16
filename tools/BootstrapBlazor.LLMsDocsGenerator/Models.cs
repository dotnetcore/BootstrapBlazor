// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.LLMsDocsGenerator;

internal sealed record GeneratorOptions(
    string RepoRoot,
    string OutputRoot,
    string? ComponentName,
    bool IndexOnly,
    bool Check);

internal sealed record SkillIndexEntry(
    string Name,
    string? Component,
    string? Skill,
    string? Sample);

internal sealed class ComponentDocument
{
    public string Name { get; init; } = "";

    public string? ComponentPath { get; init; }

    public string? SkillPath { get; init; }

    public string? SamplePath { get; init; }

    public List<string> SourceFiles { get; } = [];

    public List<ComponentTypeInfo> Types { get; } = [];

    public SampleUsage SampleUsage { get; set; } = new();
}

internal sealed class ComponentTypeInfo
{
    public string Name { get; init; } = "";

    public string FullName { get; set; } = "";

    public string? Summary { get; set; }

    public string? BaseClass { get; set; }

    public List<string> TypeParameters { get; } = [];

    public List<string> SourceFiles { get; } = [];

    public List<ParameterInfo> Parameters { get; } = [];

    public List<ParameterInfo> CascadingParameters { get; } = [];

    public List<MethodInfo> PublicMethods { get; } = [];

    public List<string> ObsoleteMembers { get; } = [];

    public bool HasJsInvokable { get; set; }
}

internal sealed class ParameterInfo
{
    public string Name { get; init; } = "";

    public string Type { get; init; } = "";

    public string? DefaultValue { get; init; }

    public string? Description { get; init; }

    public bool IsRequired { get; init; }

    public bool IsObsolete { get; init; }

    public string? ObsoleteMessage { get; init; }

    public bool IsEventCallback { get; init; }

    public bool IsTemplate { get; init; }

    public string SourcePath { get; init; } = "";
}

internal sealed class MethodInfo
{
    public string Name { get; init; } = "";

    public string ReturnType { get; init; } = "";

    public List<(string Type, string Name)> Parameters { get; } = [];

    public string? Description { get; init; }

    public bool IsJSInvokable { get; init; }

    public string SourcePath { get; init; } = "";
}

internal sealed class SampleUsage
{
    public int DirectTagCount { get; set; }

    public List<string> Attributes { get; } = [];

    public string? Snippet { get; set; }

    public List<string> Files { get; } = [];
}
