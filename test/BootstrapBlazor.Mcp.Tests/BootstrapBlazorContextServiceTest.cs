using BootstrapBlazor.Mcp;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BootstrapBlazor.Mcp.Tests;

public class BootstrapBlazorContextServiceTest : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "BootstrapBlazor.Mcp.Tests", Guid.NewGuid().ToString("N"));

    [Fact]
    public void ListComponents_Returns_IndexEntries()
    {
        CreateRepository();
        var service = CreateService();

        var components = service.ListComponents();

        var button = Assert.Single(components, item => item.Name == "Button");
        Assert.True(button.HasComponent);
        Assert.True(button.HasSample);
        Assert.True(button.HasSkill);
    }

    [Fact]
    public void GetComponentContext_Reads_Source_Sample_And_Skill()
    {
        CreateRepository(sampleAsDirectory: true);
        var service = CreateService();

        var context = service.GetComponentContext("Button");

        Assert.Equal("Button", context.Component);
        Assert.Equal(["Current component source", "Official Sample", "Component Skill"], context.Priority);
        Assert.Contains(context.SourceFiles, file => file.Path.EndsWith("Button.razor", StringComparison.Ordinal));
        Assert.Contains(context.SampleFiles, file => file.Path.EndsWith("Basic.razor", StringComparison.Ordinal));
        Assert.NotNull(context.SkillFile);
    }

    [Fact]
    public void GetComponentContext_Rejects_IndexPath_OutsideRoot()
    {
        CreateRepository(componentPath: "../outside");
        var service = CreateService();

        var exception = Assert.Throws<InvalidOperationException>(() => service.GetComponentContext("Button"));
        Assert.Contains("escapes", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Dispatcher_Initialize_Reports_HttpTransport()
    {
        CreateRepository();
        var dispatcher = new McpJsonRpcDispatcher(CreateService());
        var message = new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = 1,
            ["method"] = "initialize",
            ["params"] = new JsonObject()
        };

        var response = await dispatcher.HandleMessageAsync(message, CancellationToken.None);

        Assert.Equal("http", response?["result"]?["serverInfo"]?["transport"]?.GetValue<string>());
    }

    [Fact]
    public void Options_Parse_LogMessages_Flag()
    {
        var options = McpServerOptions.Parse([
            "--repo-root",
            _root,
            "--urls",
            "http://127.0.0.1:5178",
            "--log-messages",
            "--log-preview-chars",
            "2000"
        ]);

        Assert.Equal(_root, options.RepoRoot);
        Assert.True(options.LogMessages);
        Assert.Equal(2000, options.LogPreviewChars);
        Assert.True(options.ShouldLogMessages("Development"));
        Assert.True(options.ShouldLogMessages("Production", debuggerAttached: true));
        Assert.False(options.ShouldLogMessages("Production"));
    }

    [Fact]
    public void DebugLogFormatter_Summarizes_ComponentContext_Response()
    {
        CreateRepository();
        var context = CreateService().GetComponentContext("Button");
        var toolResult = new ToolCallResult([
            new ToolContent("text", JsonSerializer.Serialize(context, new JsonSerializerOptions(JsonSerializerDefaults.Web)))
        ]);
        var response = new JsonObject
        {
            ["jsonrpc"] = "2.0",
            ["id"] = 1,
            ["result"] = JsonSerializer.SerializeToNode(toolResult)
        };

        var log = McpDebugLogFormatter.FormatResponse(response, 2000);

        Assert.Contains("component=Button", log);
        Assert.Contains("sourceFiles=", log);
        Assert.Contains("sampleFiles=", log);
        Assert.Contains("skillFile=", log);
    }

    public void Dispose()
    {
        if (Directory.Exists(_root))
        {
            Directory.Delete(_root, recursive: true);
        }
    }

    private BootstrapBlazorContextService CreateService()
    {
        return BootstrapBlazorContextService.Create(new McpServerOptions
        {
            RepoRoot = _root
        });
    }

    private void CreateRepository(bool sampleAsDirectory = false, string? componentPath = null)
    {
        componentPath ??= "src/BootstrapBlazor/Components/Button";
        var componentFullPath = Path.Combine(_root, componentPath.Replace('/', Path.DirectorySeparatorChar));
        if (!componentPath.StartsWith("..", StringComparison.Ordinal))
        {
            Directory.CreateDirectory(componentFullPath);
            File.WriteAllText(Path.Combine(componentFullPath, "Button.razor"), "<button>@ChildContent</button>");
            File.WriteAllText(Path.Combine(componentFullPath, "Button.razor.cs"), "namespace BootstrapBlazor.Components; public partial class Button { }");
            File.WriteAllText(Path.Combine(componentFullPath, "Button.md"), "# Button Skill");
        }

        var samplePath = sampleAsDirectory
            ? "src/BootstrapBlazor.Server/Components/Samples/Buttons"
            : "src/BootstrapBlazor.Server/Components/Samples/Buttons.razor";

        if (sampleAsDirectory)
        {
            var sampleFullPath = Path.Combine(_root, samplePath.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(sampleFullPath);
            File.WriteAllText(Path.Combine(sampleFullPath, "Basic.razor"), "<Button>OK</Button>");
        }
        else
        {
            var sampleFullPath = Path.Combine(_root, samplePath.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(sampleFullPath)!);
            File.WriteAllText(sampleFullPath, "<Button>OK</Button>");
        }

        var index = new
        {
            Button = new
            {
                component = componentPath,
                skill = $"{componentPath}/Button.md",
                sample = samplePath
            }
        };

        File.WriteAllText(Path.Combine(_root, "skill-index.json"), JsonSerializer.Serialize(index));
    }
}
