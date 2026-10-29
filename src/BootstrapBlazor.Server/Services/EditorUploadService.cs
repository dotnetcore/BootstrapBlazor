// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// IEditorUploadService 接口 Server 端实现类
/// </summary>
/// <remarks>直接保存上传的文件到服务器指定目录</remarks>
sealed class EditorUploadService(IWebHostEnvironment environment) : IEditorUploadService
{
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

    private (string Path, string Url) CreateUploadPath(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        var folder = Path.Combine(environment.WebRootPath, "images", "uploader");
        Directory.CreateDirectory(folder);
        var name = $"{Guid.NewGuid():N}{extension}";
        return (Path.Combine(folder, name), $"images/uploader/{name}");
    }

    private static void ValidateSize(long size)
    {
        if (size <= 0 || size > IEditorUploadService.MaxFileSize)
        {
            throw new InvalidDataException("Attachments must be nonempty and must not exceed 5 MB.");
        }
    }
}
