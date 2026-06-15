using BootstrapBlazor.Mcp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(McpServerOptions.Parse(args));
builder.Services.AddSingleton(serviceProvider =>
    BootstrapBlazorContextService.Create(serviceProvider.GetRequiredService<McpServerOptions>()));
builder.Services.AddSingleton<McpJsonRpcDispatcher>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    service = "bootstrapblazor-mcp"
}));

app.MapPost("/mcp", async (
    HttpRequest request,
    McpJsonRpcDispatcher dispatcher,
    McpServerOptions options,
    ILoggerFactory loggerFactory,
    CancellationToken cancellationToken) =>
{
    var logger = loggerFactory.CreateLogger("BootstrapBlazor.Mcp.Messages");
    string rawBody;
    using (var reader = new StreamReader(request.Body, Encoding.UTF8))
    {
        rawBody = await reader.ReadToEndAsync(cancellationToken);
    }

    JsonNode? message;
    try
    {
        message = JsonNode.Parse(rawBody);
    }
    catch
    {
        if (options.ShouldLogMessages(app.Environment.EnvironmentName, Debugger.IsAttached))
        {
            logger.LogWarning("MCP request parse failed. bodyPreview={BodyPreview}", rawBody);
        }

        return Results.Json(
            McpJsonRpcDispatcher.CreateErrorResponse(null, -32700, "Invalid JSON."),
            statusCode: StatusCodes.Status400BadRequest);
    }

    if (message is not JsonObject jsonObject)
    {
        return Results.Json(
            McpJsonRpcDispatcher.CreateErrorResponse(null, -32600, "Expected a JSON-RPC object."),
            statusCode: StatusCodes.Status400BadRequest);
    }

    var logMessages = options.ShouldLogMessages(app.Environment.EnvironmentName, Debugger.IsAttached);

    if (logMessages)
    {
        logger.LogInformation("MCP request: {Request}", McpDebugLogFormatter.FormatRequest(jsonObject, rawBody, options.LogPreviewChars));
    }

    var response = await dispatcher.HandleMessageAsync(jsonObject, cancellationToken);
    if (logMessages)
    {
        logger.LogInformation("MCP response: {Response}", McpDebugLogFormatter.FormatResponse(response, options.LogPreviewChars));
    }

    return response is null
        ? Results.Accepted()
        : Results.Json(response);
});

app.Run();
