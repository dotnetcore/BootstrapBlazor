// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">IEditorUploadService 接口的 WebAssembly 实现，通过 HTTP 上传附件。</para>
/// <para lang="en">WebAssembly implementation of IEditorUploadService that uploads attachments over HTTP.</para>
/// </summary>
public sealed class HttpEditorUploadService(HttpClient client) : IEditorUploadService
{
    /// <inheritdoc />
    public async Task<string> UploadAsync(UploadFile file, CancellationToken cancellationToken = default)
    {
        var browserFile = file.File ?? throw new InvalidDataException("The attachment is missing.");
        if (browserFile.Size <= 0 || browserFile.Size > IEditorUploadService.MaxFileSize)
        {
            throw new InvalidDataException("Attachments must be nonempty and must not exceed 5 MB.");
        }
        using var content = new UploadFileContent(file);
        return await UploadAsync(content, browserFile.Name, cancellationToken);
    }

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
        return await UploadAsync(content, fileName, cancellationToken);
    }

    private async Task<string> UploadAsync(HttpContent content, string fileName, CancellationToken cancellationToken)
    {
        content.Headers.ContentType = new("application/octet-stream");
        using var response = await client.PostAsync($"api/editor-upload?fileName={Uri.EscapeDataString(fileName)}", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return an attachment URL.");
    }

    /// <summary>
    /// <para lang="zh">读取浏览器文件时更新组件进度，不将整个文件复制到内存缓冲区。</para>
    /// <para lang="en">Updates component progress while reading the browser file without an additional whole-file buffer.</para>
    /// </summary>
    private sealed class UploadFileContent(UploadFile file) : HttpContent
    {
        protected override bool TryComputeLength(out long length)
        {
            length = file.File!.Size;
            return true;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) =>
            SerializeToStreamAsync(stream, context, CancellationToken.None);

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context, CancellationToken cancellationToken)
        {
            var browserFile = file.File!;
            await using var source = browserFile.OpenReadStream(IEditorUploadService.MaxFileSize, cancellationToken);
            var buffer = new byte[64 * 1024];
            long total = 0;
            int read;
            while ((read = await source.ReadAsync(buffer, cancellationToken)) != 0)
            {
                await stream.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                total += read;
                file.UpdateProgress((int)(total * 100 / browserFile.Size));
            }
        }
    }
}
