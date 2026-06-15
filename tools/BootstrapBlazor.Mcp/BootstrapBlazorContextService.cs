using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace BootstrapBlazor.Mcp;

public sealed class BootstrapBlazorContextService
{
    private static readonly string[] SourceExtensions = [".razor", ".cs", ".scss", ".js", ".css"];
    private static readonly string[] SampleExtensions = [".razor", ".cs", ".md"];
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    private readonly BootstrapBlazorRoot _root;
    private readonly Dictionary<string, SkillIndexEntry> _index;

    private BootstrapBlazorContextService(BootstrapBlazorRoot root, Dictionary<string, SkillIndexEntry> index)
    {
        _root = root;
        _index = index;
    }

    public static BootstrapBlazorContextService Create(McpServerOptions options)
    {
        var root = BootstrapBlazorRootLocator.Locate(options);
        return new BootstrapBlazorContextService(root, LoadIndex(root.SkillIndexPath));
    }

    public IReadOnlyList<ComponentSummary> ListComponents(string? query = null)
    {
        IEnumerable<SkillIndexEntry> items = _index.Values;
        if (!string.IsNullOrWhiteSpace(query))
        {
            items = items.Where(entry =>
                entry.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                (entry.Component?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (entry.Sample?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (entry.Skill?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        return items
            .OrderBy(entry => entry.Name, StringComparer.OrdinalIgnoreCase)
            .Select(entry => new ComponentSummary(
                entry.Name,
                !string.IsNullOrWhiteSpace(entry.Component),
                !string.IsNullOrWhiteSpace(entry.Sample),
                !string.IsNullOrWhiteSpace(entry.Skill),
                entry.Component,
                entry.Sample,
                entry.Skill))
            .ToArray();
    }

    public IReadOnlyList<ComponentSummary> SearchComponents(string query, int limit = 20)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return [];
        }

        return ListComponents(query)
            .OrderBy(summary => summary.Name.StartsWith(query, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(summary => summary.Name, StringComparer.OrdinalIgnoreCase)
            .Take(Math.Clamp(limit, 1, 100))
            .ToArray();
    }

    public ComponentContext GetComponentContext(
        string componentName,
        bool includeSource = true,
        bool includeSample = true,
        bool includeSkill = true,
        int maxFileBytes = 128 * 1024,
        int maxFiles = 40)
    {
        var entry = GetEntry(componentName);
        var warnings = new List<string>();
        var indexedPaths = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            ["component"] = entry.Component,
            ["sample"] = entry.Sample,
            ["skill"] = entry.Skill
        };

        var sourceFiles = includeSource && !string.IsNullOrWhiteSpace(entry.Component)
            ? ReadPath(entry.Component, SourceExtensions, maxFileBytes, maxFiles, warnings)
            : [];

        var sampleFiles = includeSample && !string.IsNullOrWhiteSpace(entry.Sample)
            ? ReadPath(entry.Sample, SampleExtensions, maxFileBytes, maxFiles, warnings)
            : [];

        FileContent? skillFile = null;
        if (includeSkill && !string.IsNullOrWhiteSpace(entry.Skill))
        {
            skillFile = ReadSingleFile(entry.Skill, maxFileBytes, warnings);
        }

        if (entry.Component is null)
        {
            warnings.Add("No component source path exists in skill-index.json for this entry.");
        }

        if (entry.Sample is null)
        {
            warnings.Add("No official sample path exists in skill-index.json for this entry.");
        }

        if (entry.Skill is null)
        {
            warnings.Add("No component Skill path exists in skill-index.json for this entry.");
        }

        return new ComponentContext(
            entry.Name,
            _root.Mode.ToString(),
            _root.RootPath,
            [
                "Current component source",
                "Official Sample",
                "Component Skill"
            ],
            indexedPaths,
            warnings,
            sourceFiles,
            sampleFiles,
            skillFile);
    }

    public ComponentContext GetComponentSource(string componentName, int maxFileBytes = 128 * 1024, int maxFiles = 40)
    {
        return GetComponentContext(
            componentName,
            includeSource: true,
            includeSample: false,
            includeSkill: false,
            maxFileBytes,
            maxFiles);
    }

    public ComponentContext GetComponentSample(string componentName, int maxFileBytes = 128 * 1024, int maxFiles = 40)
    {
        return GetComponentContext(
            componentName,
            includeSource: false,
            includeSample: true,
            includeSkill: false,
            maxFileBytes,
            maxFiles);
    }

    public ComponentContext GetComponentSkill(string componentName, int maxFileBytes = 128 * 1024)
    {
        return GetComponentContext(
            componentName,
            includeSource: false,
            includeSample: false,
            includeSkill: true,
            maxFileBytes);
    }

    public async Task<ScriptResult> ValidateSkillIndexAsync(CancellationToken cancellationToken)
    {
        EnsureRepositoryMode();
        return await RunPowerShellScriptAsync("generate-skill-index.ps1", ["-Check"], cancellationToken);
    }

    public async Task<ScriptResult> CheckSkillSyncAsync(string? baseRef, bool warnAllMissingSkills, CancellationToken cancellationToken)
    {
        EnsureRepositoryMode();
        var args = new List<string>();
        if (!string.IsNullOrWhiteSpace(baseRef))
        {
            args.Add("-BaseRef");
            args.Add(baseRef);
        }

        if (warnAllMissingSkills)
        {
            args.Add("-WarnAllMissingSkills");
        }

        return await RunPowerShellScriptAsync("check-skill-sync.ps1", args, cancellationToken);
    }

    public async Task<object> GenerateSkillIndexAsync(bool dryRun, CancellationToken cancellationToken)
    {
        EnsureRepositoryMode();
        var result = dryRun
            ? await RunPowerShellScriptAsync("generate-skill-index.ps1", ["-Check"], cancellationToken)
            : await RunPowerShellScriptAsync("generate-skill-index.ps1", [], cancellationToken);

        return new
        {
            dryRun,
            changed = dryRun ? result.ExitCode != 0 : (bool?)null,
            result
        };
    }

    public string SerializeToolResult(object value)
    {
        return JsonSerializer.Serialize(value, JsonOptions);
    }

    private SkillIndexEntry GetEntry(string componentName)
    {
        if (string.IsNullOrWhiteSpace(componentName))
        {
            throw new ArgumentException("Component name is required.", nameof(componentName));
        }

        if (_index.TryGetValue(componentName, out var entry))
        {
            return entry;
        }

        var match = _index.Values.FirstOrDefault(item => item.Name.Equals(componentName, StringComparison.OrdinalIgnoreCase));
        if (match is not null)
        {
            return match;
        }

        throw new KeyNotFoundException($"Component was not found in skill-index.json: {componentName}");
    }

    private static Dictionary<string, SkillIndexEntry> LoadIndex(string indexPath)
    {
        using var document = JsonDocument.Parse(File.ReadAllText(indexPath));
        var entries = new Dictionary<string, SkillIndexEntry>(StringComparer.OrdinalIgnoreCase);

        foreach (var property in document.RootElement.EnumerateObject())
        {
            string? component = null;
            string? skill = null;
            string? sample = null;

            if (property.Value.TryGetProperty("component", out var componentElement))
            {
                component = componentElement.GetString();
            }

            if (property.Value.TryGetProperty("skill", out var skillElement))
            {
                skill = skillElement.GetString();
            }

            if (property.Value.TryGetProperty("sample", out var sampleElement))
            {
                sample = sampleElement.GetString();
            }

            entries[property.Name] = new SkillIndexEntry(property.Name, component, skill, sample);
        }

        return entries;
    }

    private IReadOnlyList<FileContent> ReadPath(
        string relativePath,
        IReadOnlyCollection<string> allowedExtensions,
        int maxFileBytes,
        int maxFiles,
        List<string> warnings)
    {
        var fullPath = ResolveIndexedPath(relativePath);
        if (File.Exists(fullPath))
        {
            return [ReadFile(fullPath, maxFileBytes, warnings)];
        }

        if (!Directory.Exists(fullPath))
        {
            warnings.Add($"Indexed path does not exist: {relativePath}");
            return [];
        }

        var files = Directory
            .EnumerateFiles(fullPath, "*", SearchOption.AllDirectories)
            .Where(path => allowedExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase))
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .Take(Math.Clamp(maxFiles, 1, 200) + 1)
            .ToArray();

        if (files.Length > maxFiles)
        {
            warnings.Add($"File list was truncated to {maxFiles} files for {relativePath}.");
            files = files.Take(maxFiles).ToArray();
        }

        return files.Select(path => ReadFile(path, maxFileBytes, warnings)).ToArray();
    }

    private FileContent? ReadSingleFile(string relativePath, int maxFileBytes, List<string> warnings)
    {
        var fullPath = ResolveIndexedPath(relativePath);
        if (!File.Exists(fullPath))
        {
            warnings.Add($"Indexed file does not exist: {relativePath}");
            return null;
        }

        return ReadFile(fullPath, maxFileBytes, warnings);
    }

    private FileContent ReadFile(string fullPath, int maxFileBytes, List<string> warnings)
    {
        var info = new FileInfo(fullPath);
        var limit = Math.Clamp(maxFileBytes, 4 * 1024, 1024 * 1024);
        var truncated = info.Length > limit;
        var bytesToRead = (int)Math.Min(info.Length, limit);
        var buffer = new byte[bytesToRead];

        using (var stream = File.OpenRead(fullPath))
        {
            var read = stream.Read(buffer, 0, bytesToRead);
            if (read != bytesToRead)
            {
                Array.Resize(ref buffer, read);
            }
        }

        var content = Encoding.UTF8.GetString(buffer);
        if (truncated)
        {
            warnings.Add($"File content was truncated to {limit} bytes: {ToRepoPath(fullPath)}");
        }

        return new FileContent(
            ToRepoPath(fullPath),
            fullPath,
            content,
            truncated,
            info.Length);
    }

    private string ResolveIndexedPath(string relativePath)
    {
        var normalized = relativePath.Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.GetFullPath(Path.Combine(_root.RootPath, normalized));
        var rootPath = Path.GetFullPath(_root.RootPath);

        if (!rootPath.EndsWith(Path.DirectorySeparatorChar))
        {
            rootPath += Path.DirectorySeparatorChar;
        }

        if (!fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Indexed path escapes the BootstrapBlazor root: {relativePath}");
        }

        return fullPath;
    }

    private string ToRepoPath(string fullPath)
    {
        return Path.GetRelativePath(_root.RootPath, fullPath).Replace('\\', '/');
    }

    private void EnsureRepositoryMode()
    {
        if (_root.Mode != BootstrapBlazorRootMode.Repository)
        {
            throw new InvalidOperationException("This tool is available only when the MCP server is running in repository mode.");
        }
    }

    private async Task<ScriptResult> RunPowerShellScriptAsync(
        string scriptName,
        IReadOnlyList<string> scriptArgs,
        CancellationToken cancellationToken)
    {
        var scriptPath = Path.Combine(_root.RootPath, "scripts", scriptName);
        if (!File.Exists(scriptPath))
        {
            throw new FileNotFoundException("Script was not found.", scriptPath);
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "powershell",
            WorkingDirectory = _root.RootPath,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        startInfo.ArgumentList.Add("-NoProfile");
        startInfo.ArgumentList.Add("-ExecutionPolicy");
        startInfo.ArgumentList.Add("Bypass");
        startInfo.ArgumentList.Add("-File");
        startInfo.ArgumentList.Add(scriptPath);

        foreach (var arg in scriptArgs)
        {
            startInfo.ArgumentList.Add(arg);
        }

        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException("Unable to start PowerShell.");
        var stdoutTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var stderrTask = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);
        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        return new ScriptResult(process.ExitCode, stdout, stderr);
    }
}
