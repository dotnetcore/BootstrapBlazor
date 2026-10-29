// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// <para lang="zh">IEditorUploadService 接口 Server 端实现类</para>
/// <para lang="en">Server implementation of IEditorUploadService.</para>
/// </summary>
/// <remarks>
/// <para lang="zh">组件回调使用原有 SaveToFile 方法保存；HTTP 请求体按实际读取字节数限制大小。</para>
/// <para lang="en">Component callbacks use the existing SaveToFile methods; HTTP request bodies are bounded by the bytes actually read.</para>
/// </remarks>
sealed class EditorUploadService(IWebHostEnvironment environment) : IEditorUploadService
{
    private static readonly HashSet<string> _extensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".png", ".jpg", ".jpeg", ".gif", ".webp", ".pdf", ".txt", ".zip"
    };

    /// <inheritdoc />
    public async Task<string> UploadAsync(UploadFile file, CancellationToken cancellationToken = default)
    {
        var browserFile = file.File ?? throw new InvalidDataException("The attachment is missing.");
        ValidateSize(browserFile.Size);
        var (path, url) = CreateUploadPath(browserFile.Name);
        try
        {
            if (!await file.SaveToFileAsync(path, IEditorUploadService.MaxFileSize, token: cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new IOException(file.Error ?? "Unable to save the attachment.");
            }
        }
        catch
        {
            File.Delete(path);
            throw;
        }
        return url;
    }

    /// <inheritdoc />
    public async Task<string> UploadAsync(EditorUploadFile file, CancellationToken cancellationToken = default)
    {
        ValidateSize(file.FileSize);
        var (path, url) = CreateUploadPath(file.FileName);
        try
        {
            if (!await file.SaveToFileAsync(path, token: cancellationToken))
            {
                throw new IOException("Unable to save the attachment.", file.Error);
            }
        }
        catch
        {
            File.Delete(path);
            throw;
        }
        return url;
    }

    /// <inheritdoc />
    public async Task<string> UploadAsync(CherryMarkdownUploadFile file, CancellationToken cancellationToken = default)
    {
        ValidateSize(file.FileSize);
        var (path, url) = CreateUploadPath(file.FileName ?? "");
        try
        {
            if (!await file.SaveToFile(path, cancellationToken))
            {
                throw new IOException(file.Error ?? "Unable to save the attachment.");
            }
        }
        catch
        {
            File.Delete(path);
            throw;
        }
        return url;
    }

    /// <inheritdoc />
    public async Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        var (path, url) = CreateUploadPath(fileName);
        try
        {
            await using var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true);
            var buffer = new byte[81920];
            long total = 0;
            int read;
            while ((read = await stream.ReadAsync(buffer, cancellationToken)) != 0)
            {
                total += read;
                if (total > IEditorUploadService.MaxFileSize)
                {
                    throw new InvalidDataException("Attachments must not exceed 5 MB.");
                }
                await target.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            }
            if (total == 0)
            {
                throw new InvalidDataException("The attachment is empty.");
            }
        }
        catch
        {
            File.Delete(path);
            throw;
        }
        return url;
    }

    /// <summary>
    /// <para lang="zh">校验扩展名并生成随机存储路径及应用相对 URL。</para>
    /// <para lang="en">Validates the extension and generates a random storage path and application-relative URL.</para>
    /// </summary>
    private (string Path, string Url) CreateUploadPath(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (!_extensions.Contains(extension))
        {
            throw new InvalidDataException("This attachment type is not allowed.");
        }

        var folder = Path.Combine(environment.WebRootPath, "images", "uploader");
        Directory.CreateDirectory(folder);
        var name = $"{Guid.NewGuid():N}{extension}";
        return (Path.Combine(folder, name), $"images/uploader/{name}");
    }

    /// <summary>
    /// <para lang="zh">校验组件提供的附件大小。</para>
    /// <para lang="en">Validates the attachment size supplied by the component.</para>
    /// </summary>
    private static void ValidateSize(long size)
    {
        if (size <= 0 || size > IEditorUploadService.MaxFileSize)
        {
            throw new InvalidDataException("Attachments must be nonempty and must not exceed 5 MB.");
        }
    }
}
