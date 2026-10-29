// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// IEditorUploadService 接口 wasm 模式实现类
/// </summary>
public sealed class HttpEditorUploadService(HttpClient client) : IEditorUploadService
{
    /// <inheritdoc />
    public Task<string> UploadAsync(EditorUploadFile file, CancellationToken cancellationToken = default) =>
        UploadAsync(file.Stream, file.FileName, cancellationToken);

    /// <inheritdoc />
    public Task<string> UploadAsync(CherryMarkdownUploadFile file, CancellationToken cancellationToken = default) =>
        UploadAsync(file.UploadStream ?? throw new InvalidDataException("The attachment stream is missing."),
            file.FileName ?? throw new InvalidDataException("The attachment name is missing."), cancellationToken);

    /// <inheritdoc />
    public async Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        using var content = new StreamContent(stream);
        content.Headers.ContentType = new("application/octet-stream");
        using var response = await client.PostAsync($"api/editor-upload?fileName={Uri.EscapeDataString(fileName)}", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return an attachment URL.");
    }
}
