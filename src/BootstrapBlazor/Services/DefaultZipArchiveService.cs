// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;
using System.Text;

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
        await using var stream = File.OpenWrite(archiveFileName);
        await ArchiveFilesAsync(stream, files, options);
    }

    private static async Task ArchiveFilesAsync(Stream stream, IEnumerable<string> files, ArchiveOptions? options = null)
    {
        options ??= new ArchiveOptions();
        using var archive = new ZipArchive(stream, options.Mode, options.LeaveOpen, options.Encoding);
        foreach (var f in files)
        {
            if (options.ReadStreamAsync != null)
            {
                var entry = archive.CreateEntry(Path.GetFileName(f), options.CompressionLevel);
                await using var entryStream = entry.Open();
                await using var content = await options.ReadStreamAsync(f);
                await content.CopyToAsync(entryStream);
            }
            else
            {
                archive.CreateEntryFromFile(f, Path.GetFileName(f), options.CompressionLevel);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="destinationDirectoryName">解压缩文件夹</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <returns></returns>
    public async Task ExtractToDirectory(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null)
    {
        if (!Directory.Exists(destinationDirectoryName))
        {
            Directory.CreateDirectory(destinationDirectoryName);
        }
        await using var stream = File.OpenRead(archiveFile);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read, overwriteFiles, encoding);
        archive.ExtractToDirectory(destinationDirectoryName, overwriteFiles);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entryFile">解压缩文件</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <returns></returns>
    public async Task<ZipArchiveEntry?> GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null)
    {
        await using var stream = File.OpenRead(archiveFile);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read, overwriteFiles, encoding);
        return archive.GetEntry(Path.GetFileName(entryFile));
    }
}
