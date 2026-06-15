using System.Text.Json;
using System.Text.Json.Nodes;

namespace BootstrapBlazor.Mcp;

public sealed class McpJsonRpcDispatcher
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = false
    };

    private readonly BootstrapBlazorContextService _service;

    public McpJsonRpcDispatcher(BootstrapBlazorContextService service)
    {
        _service = service;
    }

    public async Task<JsonNode?> HandleMessageAsync(JsonObject message, CancellationToken cancellationToken)
    {
        var method = message["method"]?.GetValue<string>();
        if (string.IsNullOrWhiteSpace(method))
        {
            return CreateErrorResponse(GetId(message), -32600, "Missing method.");
        }

        var id = GetId(message);
        return method switch
        {
            "initialize" => CreateResponse(id, new
            {
                protocolVersion = "2024-11-05",
                capabilities = new
                {
                    tools = new { }
                },
                serverInfo = new
                {
                    name = "bootstrapblazor-mcp",
                    version = typeof(McpJsonRpcDispatcher).Assembly.GetName().Version?.ToString() ?? "0.0.0",
                    transport = "http"
                }
            }),
            "notifications/initialized" => null,
            "ping" => CreateResponse(id, new { }),
            "tools/list" => CreateResponse(id, new { tools = GetTools() }),
            "tools/call" => await HandleToolCallAsync(id, message["params"] as JsonObject, cancellationToken),
            _ => id is null ? null : CreateErrorResponse(id, -32601, $"Unknown method: {method}")
        };
    }

    private async Task<JsonNode> HandleToolCallAsync(JsonNode? id, JsonObject? parameters, CancellationToken cancellationToken)
    {
        var name = parameters?["name"]?.GetValue<string>();
        if (string.IsNullOrWhiteSpace(name))
        {
            return CreateErrorResponse(id, -32602, "Tool name is required.");
        }

        try
        {
            var arguments = parameters?["arguments"] as JsonObject ?? [];
            var result = name switch
            {
                "list_components" => _service.ListComponents(GetString(arguments, "query")),
                "search_components" => _service.SearchComponents(
                    GetRequiredString(arguments, "query"),
                    GetInt(arguments, "limit", 20)),
                "get_component_context" => _service.GetComponentContext(
                    GetRequiredString(arguments, "component"),
                    GetBool(arguments, "includeSource", true),
                    GetBool(arguments, "includeSample", true),
                    GetBool(arguments, "includeSkill", true),
                    GetInt(arguments, "maxFileBytes", 128 * 1024),
                    GetInt(arguments, "maxFiles", 40)),
                "get_component_source" => _service.GetComponentSource(
                    GetRequiredString(arguments, "component"),
                    GetInt(arguments, "maxFileBytes", 128 * 1024),
                    GetInt(arguments, "maxFiles", 40)),
                "get_component_sample" => _service.GetComponentSample(
                    GetRequiredString(arguments, "component"),
                    GetInt(arguments, "maxFileBytes", 128 * 1024),
                    GetInt(arguments, "maxFiles", 40)),
                "get_component_skill" => _service.GetComponentSkill(
                    GetRequiredString(arguments, "component"),
                    GetInt(arguments, "maxFileBytes", 128 * 1024)),
                "validate_skill_index" => await _service.ValidateSkillIndexAsync(cancellationToken),
                "check_skill_sync" => await _service.CheckSkillSyncAsync(
                    GetString(arguments, "baseRef"),
                    GetBool(arguments, "warnAllMissingSkills", false),
                    cancellationToken),
                "generate_skill_index" => await _service.GenerateSkillIndexAsync(
                    GetBool(arguments, "dryRun", true),
                    cancellationToken),
                _ => throw new InvalidOperationException($"Unknown tool: {name}")
            };

            return CreateResponse(id, CreateToolResult(result));
        }
        catch (Exception ex)
        {
            return CreateResponse(id, CreateToolResult(new { error = ex.Message }, isError: true));
        }
    }

    public static JsonObject CreateErrorResponse(JsonNode? id, int code, string message)
    {
        return new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = id,
            ["error"] = new JsonObject
            {
                ["code"] = code,
                ["message"] = message
            }
        };
    }

    private static object[] GetTools()
    {
        return
        [
            Tool("list_components", "List BootstrapBlazor components from skill-index.json.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        query = StringProperty("Optional name or path filter.")
                    }
                }),
            Tool("search_components", "Search BootstrapBlazor components by name or indexed path.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        query = StringProperty("Search term."),
                        limit = IntegerProperty("Maximum results. Default: 20.")
                    },
                    required = new[] { "query" }
                }),
            Tool("get_component_context", "Read source, official Sample, and Skill for a component in the required priority order.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        component = StringProperty("Component name, for example Button."),
                        includeSource = BooleanProperty("Include current component source. Default: true."),
                        includeSample = BooleanProperty("Include official Sample. Default: true."),
                        includeSkill = BooleanProperty("Include component Skill. Default: true."),
                        maxFileBytes = IntegerProperty("Maximum bytes per file. Default: 131072."),
                        maxFiles = IntegerProperty("Maximum files per directory. Default: 40.")
                    },
                    required = new[] { "component" }
                }),
            Tool("get_component_source", "Read only the current component source files.",
                ComponentReadSchema()),
            Tool("get_component_sample", "Read only the official Sample files from skill-index.json.",
                ComponentReadSchema()),
            Tool("get_component_skill", "Read only the component Skill file.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        component = StringProperty("Component name."),
                        maxFileBytes = IntegerProperty("Maximum bytes per file. Default: 131072.")
                    },
                    required = new[] { "component" }
                }),
            Tool("validate_skill_index", "Run scripts/generate-skill-index.ps1 -Check. Repository mode only.",
                new { type = "object", properties = new { } }),
            Tool("check_skill_sync", "Run scripts/check-skill-sync.ps1. Repository mode only.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        baseRef = StringProperty("Optional git base ref."),
                        warnAllMissingSkills = BooleanProperty("Warn for every component missing a Skill.")
                    }
                }),
            Tool("generate_skill_index", "Generate or dry-run-check skill-index.json. Repository mode only; dryRun defaults to true.",
                new
                {
                    type = "object",
                    properties = new
                    {
                        dryRun = BooleanProperty("Run check only when true. Default: true.")
                    }
                })
        ];
    }

    private ToolCallResult CreateToolResult(object result, bool isError = false)
    {
        return new ToolCallResult(
            [new ToolContent("text", _service.SerializeToolResult(result))],
            isError);
    }

    private static object ComponentReadSchema()
    {
        return new
        {
            type = "object",
            properties = new
            {
                component = StringProperty("Component name."),
                maxFileBytes = IntegerProperty("Maximum bytes per file. Default: 131072."),
                maxFiles = IntegerProperty("Maximum files per directory. Default: 40.")
            },
            required = new[] { "component" }
        };
    }

    private static object Tool(string name, string description, object inputSchema)
    {
        return new
        {
            name,
            description,
            inputSchema
        };
    }

    private static object StringProperty(string description)
    {
        return new { type = "string", description };
    }

    private static object IntegerProperty(string description)
    {
        return new { type = "integer", description };
    }

    private static object BooleanProperty(string description)
    {
        return new { type = "boolean", description };
    }

    private static JsonNode? GetId(JsonObject message)
    {
        return message["id"]?.DeepClone();
    }

    private static JsonObject CreateResponse(JsonNode? id, object result)
    {
        return new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = id,
            ["result"] = JsonSerializer.SerializeToNode(result, JsonOptions)
        };
    }

    private static string? GetString(JsonObject arguments, string name)
    {
        return arguments.TryGetPropertyValue(name, out var value) && value is not null
            ? value.GetValue<string>()
            : null;
    }

    private static string GetRequiredString(JsonObject arguments, string name)
    {
        return GetString(arguments, name) ?? throw new ArgumentException($"Argument is required: {name}");
    }

    private static int GetInt(JsonObject arguments, string name, int defaultValue)
    {
        return arguments.TryGetPropertyValue(name, out var value) && value is not null
            ? value.GetValue<int>()
            : defaultValue;
    }

    private static bool GetBool(JsonObject arguments, string name, bool defaultValue)
    {
        return arguments.TryGetPropertyValue(name, out var value) && value is not null
            ? value.GetValue<bool>()
            : defaultValue;
    }
}
