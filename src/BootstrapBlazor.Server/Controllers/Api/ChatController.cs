// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace BootstrapBlazor.Controllers.Api;

/// <summary><para lang="zh">为 WebAssembly 提供额度查询及逐段聊天回复。</para><para lang="en">Provides quota queries and streamed chat responses for WebAssembly.</para></summary>
[ApiController]
public sealed class ChatController(ISampleChatService chat, ILogger<ChatController> logger) : ControllerBase
{
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    /// <summary><para lang="zh">查询后台额度。</para><para lang="en">Queries server-side quota.</para></summary>
    [HttpGet("api/chat/quota")]
    public Task<int> GetRemaining([Required, StringLength(256)] string code, CancellationToken cancellationToken)
    {
        Response.Headers.CacheControl = "no-store";
        return chat.GetRemainingAsync(code, cancellationToken);
    }

    /// <summary><para lang="zh">逐行发送回复，完成及失败均返回明确事件。</para><para lang="en">Sends reply lines with explicit completion and failure events.</para></summary>
    [HttpPost("api/chat")]
    [Consumes("application/json")]
    [RequestSizeLimit(64 * 1024)]
    public async Task Stream(SampleChatRequest request, CancellationToken cancellationToken)
    {
        Response.ContentType = "application/x-ndjson";
        Response.Headers.CacheControl = "no-store";
        Response.Headers["X-Accel-Buffering"] = "no";
        HttpContext.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();
        try
        {
            await foreach (var update in chat.StreamAsync(request, cancellationToken))
            {
                await WriteAsync(update, cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogDebug("Chat request was cancelled by the client.");
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogError(ex, "Chat stream failed.");
            await WriteAsync(new() { Error = "Chat generation failed. Please retry." }, cancellationToken);
        }
    }

    private async Task WriteAsync(SampleChatUpdate update, CancellationToken cancellationToken)
    {
        await Response.WriteAsync(JsonSerializer.Serialize(update, _jsonOptions) + "\n", cancellationToken);
        await Response.Body.FlushAsync(cancellationToken);
    }
}
