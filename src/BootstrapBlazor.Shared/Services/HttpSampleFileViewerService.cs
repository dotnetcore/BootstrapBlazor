// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">通过同源 HTTP 获取服务器转换的预览内容。</para>
/// <para lang="en">Gets server-converted preview content over same-origin HTTP.</para>
/// </summary>
public sealed class HttpSampleFileViewerService(HttpClient client) : ISampleFileViewerService
{
    /// <inheritdoc />
    public Task<string> PreviewAsync(string fileName, CancellationToken cancellationToken = default) =>
        client.GetStringAsync($"api/file-viewer/preview?fileName={Uri.EscapeDataString(fileName)}", cancellationToken);

    /// <inheritdoc />
    public async Task<List<AttributeItem>> GetAttributesAsync(CancellationToken cancellationToken = default) =>
        await client.GetFromJsonAsync<List<AttributeItem>>("api/file-viewer/attributes", cancellationToken)
        ?? throw new InvalidDataException("FileViewer parameter documentation is missing.");
}
