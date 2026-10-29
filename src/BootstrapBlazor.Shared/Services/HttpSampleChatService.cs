// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.CompilerServices;
using System.Text.Json;

namespace BootstrapBlazor.Shared.Services;

/// <summary><para lang="zh">浏览器逐行读取后台聊天回复，不缓冲整个响应。</para><para lang="en">Reads streamed chat lines in the browser without buffering the full response.</para></summary>
public sealed class HttpSampleChatService(HttpClient client) : ISampleChatService
{
    private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    /// <inheritdoc />
    public Task<int> GetRemainingAsync(string code, CancellationToken cancellationToken = default) =>
        client.GetFromJsonAsync<int>($"api/chat/quota?code={Uri.EscapeDataString(code)}", cancellationToken);

    /// <inheritdoc />
    public async IAsyncEnumerable<SampleChatUpdate> StreamAsync(SampleChatRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, "api/chat") { Content = JsonContent.Create(request) };
        // 使用浏览器 HTTP 处理程序的流式选项，避免 Shared 引用 WebAssembly 宿主包。
        // Use the browser HTTP handler's streaming option without referencing the WebAssembly host package from Shared.
        message.Options.Set(new HttpRequestOptionsKey<bool>("WebAssemblyEnableStreamingResponse"), true);
        using var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        response.EnsureSuccessStatusCode();
        if (response.Content.Headers.ContentType?.MediaType != "application/x-ndjson")
        {
            throw new InvalidDataException("The server did not return a chat stream.");
        }
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new StreamReader(stream);
        while (await reader.ReadLineAsync(cancellationToken) is { } line)
        {
            var update = JsonSerializer.Deserialize<SampleChatUpdate>(line, _jsonOptions)
                ?? throw new InvalidDataException("An invalid chat event was received.");
            if (update.Error != null)
            {
                throw new InvalidOperationException(update.Error);
            }
            yield return update;
            if (update.Completed)
            {
                yield break;
            }
        }
        throw new EndOfStreamException("The chat stream ended before completion.");
    }
}
