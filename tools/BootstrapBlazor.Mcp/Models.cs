using System.Text.Json.Serialization;

namespace BootstrapBlazor.Mcp;

public enum BootstrapBlazorRootMode
{
    Repository
}

public sealed record BootstrapBlazorRoot(
    BootstrapBlazorRootMode Mode,
    string RootPath,
    string SkillIndexPath);

public sealed record SkillIndexEntry(
    string Name,
    string? Component,
    string? Skill,
    string? Sample);

public sealed record ComponentSummary(
    string Name,
    bool HasComponent,
    bool HasSample,
    bool HasSkill,
    string? Component,
    string? Sample,
    string? Skill);

public sealed record FileContent(
    string Path,
    string FullPath,
    string Content,
    bool Truncated,
    long Length);

public sealed record ComponentContext(
    string Component,
    string Mode,
    string RootPath,
    IReadOnlyList<string> Priority,
    IReadOnlyDictionary<string, string?> IndexedPaths,
    IReadOnlyList<string> Warnings,
    IReadOnlyList<FileContent> SourceFiles,
    IReadOnlyList<FileContent> SampleFiles,
    FileContent? SkillFile);

public sealed record ScriptResult(
    int ExitCode,
    string StandardOutput,
    string StandardError);

public sealed record ToolContent(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text);

public sealed record ToolCallResult(
    [property: JsonPropertyName("content")] IReadOnlyList<ToolContent> Content,
    [property: JsonPropertyName("isError")] bool IsError = false);
