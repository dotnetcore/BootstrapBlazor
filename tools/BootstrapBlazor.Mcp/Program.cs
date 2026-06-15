using BootstrapBlazor.Mcp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

app.MapPost("/mcp", async (HttpRequest request, McpJsonRpcDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    JsonNode? message;
    try
    {
        message = await JsonNode.ParseAsync(request.Body, cancellationToken: cancellationToken);
    }
    catch
    {
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

    var response = await dispatcher.HandleMessageAsync(jsonObject, cancellationToken);
    return response is null
        ? Results.Accepted()
        : Results.Json(response);
});

app.Run();
