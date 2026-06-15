using System.Text.Json;
using System.Text.Json.Nodes;

namespace BootstrapBlazor.Mcp;

public static class McpDebugLogFormatter
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = false
    };

    public static string FormatRequest(JsonObject message, string rawBody, int previewChars)
    {
        var method = GetString(message, "method") ?? "<missing>";
        var id = message["id"]?.ToJsonString(JsonOptions) ?? "<notification>";
        var toolName = message["params"]?["name"]?.GetValue<string>();
        var arguments = message["params"]?["arguments"]?.ToJsonString(JsonOptions);

        var parts = new List<string>
        {
            $"id={id}",
            $"method={method}"
        };

        if (!string.IsNullOrWhiteSpace(toolName))
        {
            parts.Add($"tool={toolName}");
        }

        if (!string.IsNullOrWhiteSpace(arguments))
        {
            parts.Add($"arguments={Preview(arguments, previewChars)}");
        }

        parts.Add($"bodyPreview={Preview(rawBody, previewChars)}");
        return string.Join("; ", parts);
    }

    public static string FormatResponse(JsonNode? response, int previewChars)
    {
        if (response is null)
        {
            return "notification accepted; response=<none>";
        }

        var id = response["id"]?.ToJsonString(JsonOptions) ?? "<none>";
        if (response["error"] is JsonObject error)
        {
            return $"id={id}; error={Preview(error.ToJsonString(JsonOptions), previewChars)}";
        }

        var result = response["result"];
        var isToolError = result?["isError"]?.GetValue<bool>();
        var text = result?["content"]?[0]?["text"]?.GetValue<string>();
        if (!string.IsNullOrWhiteSpace(text))
        {
            return $"id={id}; isToolError={isToolError}; toolResult={FormatToolResultText(text, previewChars)}";
        }

        return $"id={id}; resultPreview={Preview(result?.ToJsonString(JsonOptions) ?? "null", previewChars)}";
    }

    private static string FormatToolResultText(string text, int previewChars)
    {
        try
        {
            var json = JsonNode.Parse(text);
            if (json is JsonArray array)
            {
                return $"arrayCount={array.Count}; preview={Preview(text, previewChars)}";
            }

            if (json is JsonObject obj)
            {
                if (obj["component"] is not null)
                {
                    return FormatComponentContext(obj, previewChars);
                }

                if (obj["exitCode"] is not null)
                {
                    return $"scriptExitCode={obj["exitCode"]}; stdoutPreview={Preview(obj["standardOutput"]?.GetValue<string>() ?? "", previewChars)}; stderrPreview={Preview(obj["standardError"]?.GetValue<string>() ?? "", previewChars)}";
                }

                if (obj["error"] is not null)
                {
                    return $"error={Preview(obj["error"]?.GetValue<string>() ?? "", previewChars)}";
                }

                return $"objectKeys={string.Join(",", obj.Select(property => property.Key))}; preview={Preview(text, previewChars)}";
            }
        }
        catch (JsonException)
        {
            // Fall back to plain preview below.
        }

        return $"textLength={text.Length}; preview={Preview(text, previewChars)}";
    }

    private static string FormatComponentContext(JsonObject obj, int previewChars)
    {
        var sourceFiles = obj["sourceFiles"] as JsonArray;
        var sampleFiles = obj["sampleFiles"] as JsonArray;
        var skillFile = obj["skillFile"] as JsonObject;
        var warnings = obj["warnings"] as JsonArray;

        return string.Join("; ", new[]
        {
            $"component={obj["component"]}",
            $"mode={obj["mode"]}",
            $"warnings={warnings?.Count ?? 0}",
            $"sourceFiles={sourceFiles?.Count ?? 0}:{FormatPaths(sourceFiles, previewChars)}",
            $"sampleFiles={sampleFiles?.Count ?? 0}:{FormatPaths(sampleFiles, previewChars)}",
            $"skillFile={skillFile?["path"]?.GetValue<string>() ?? "<none>"}"
        });
    }

    private static string FormatPaths(JsonArray? files, int previewChars)
    {
        if (files is null || files.Count == 0)
        {
            return "[]";
        }

        var paths = files
            .OfType<JsonObject>()
            .Select(file => file["path"]?.GetValue<string>())
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Take(8);

        return Preview("[" + string.Join(", ", paths) + "]", previewChars);
    }

    private static string Preview(string text, int previewChars)
    {
        var normalized = text
            .Replace("\r", "\\r", StringComparison.Ordinal)
            .Replace("\n", "\\n", StringComparison.Ordinal);

        return normalized.Length <= previewChars
            ? normalized
            : normalized[..previewChars] + "...";
    }

    private static string? GetString(JsonObject obj, string name)
    {
        return obj.TryGetPropertyValue(name, out var value) && value is not null
            ? value.GetValue<string>()
            : null;
    }
}
