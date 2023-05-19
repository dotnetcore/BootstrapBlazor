// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;

namespace BootstrapBlazor.Components;

class DefaultZipArchiveService : IZipArchiveService
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    /// <returns>归档数据流</returns>
    public async Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null)
    {
        var stream = new MemoryStream();
        options ??= new ArchiveOptions();
        options.LeaveOpen = true;
        await ArchiveFilesAsync(stream, files, options);
        stream.Position = 0;
        return stream;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFileName">归档文件</param>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    public async Task ArchiveAsync(string archiveFileName, IEnumerable<string> files, ArchiveOptions? options = null)
    {
        using var stream = File.OpenWrite(archiveFileName);
        await ArchiveFilesAsync(stream, files, options);
    }

    private static async Task ArchiveFilesAsync(Stream stream, IEnumerable<string> files, ArchiveOptions? options = null)
    {
        options ??= new ArchiveOptions();
        using var archive = new ZipArchive(stream, options.Mode, options.LeaveOpen, options.Encoding);
        foreach (var f in files)
        {
            var entry = archive.CreateEntry(Path.GetFileName(f), options.CompressionLevel);
            using var entryStream = entry.Open();
            using var content = await ReadAsync(f, options);
            content.CopyTo(entryStream);
        }
    }

    private static async Task<Stream> ReadAsync(string file, ArchiveOptions options)
    {
        Stream? content = null;
        if (options.ReadStreamAsync != null)
        {
            content = await options.ReadStreamAsync(file);
        }
        else
        {
            content = File.OpenRead(file);
        }
        return content;
    }
}
