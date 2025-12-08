// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// <param name="archiveFile">归档文件</param>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    public async Task ArchiveAsync(string archiveFile, IEnumerable<string> files, ArchiveOptions? options = null)
    {
        using var stream = File.OpenWrite(archiveFile);
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
                using var entryStream = entry.Open();
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
    /// <param name="archiveFile"></param>
    /// <param name="directoryName"></param>
    /// <param name="compressionLevel"></param>
    /// <param name="includeBaseDirectory"></param>
    /// <param name="encoding"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ArchiveDirectory(string archiveFile, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null, CancellationToken token = default)
    {
        if (Directory.Exists(directoryName))
        {
            var folder = Path.GetDirectoryName(archiveFile);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
#if NET10_0_OR_GREATER
            await ZipFile.CreateFromDirectoryAsync(directoryName, archiveFile, compressionLevel, includeBaseDirectory, encoding, token);
#else
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();
                ZipFile.CreateFromDirectory(directoryName, archiveFile, compressionLevel, includeBaseDirectory, encoding);
            }, token);
#endif
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entries"></param>
    /// <param name="compressionLevel"></param>
    /// <param name="encoding"></param>
    /// <param name="skipEmptyFolder"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ArchiveDirectory(string archiveFile, IEnumerable<string> entries, CompressionLevel compressionLevel = CompressionLevel.Optimal, Encoding? encoding = null, bool skipEmptyFolder = false, CancellationToken token = default)
    {
        using var archive = ZipFile.Open(archiveFile, ZipArchiveMode.Create, encoding);

        foreach (var entry in entries)
        {
            if (Directory.Exists(entry))
            {
                AddFolderToZip(archive, entry, Path.GetFileName(entry), compressionLevel);
            }
            else if (File.Exists(entry))
            {
                archive.CreateEntryFromFile(entry, Path.GetFileName(entry), compressionLevel);
            }
        }
    }

    private static void AddFolderToZip(ZipArchive archive, string folderPath, string relativePath, CompressionLevel compressionLevel = CompressionLevel.Optimal)
    {
        archive.CreateEntry($"{relativePath}/", compressionLevel);

        // 添加当前文件夹中的所有文件
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string entryName = Path.Combine(relativePath, Path.GetFileName(filePath));
            archive.CreateEntryFromFile(filePath, entryName, compressionLevel);
        }

        // 递归添加所有子文件夹
        foreach (string subfolderPath in Directory.GetDirectories(folderPath))
        {
            string newRelativePath = Path.Combine(relativePath, Path.GetFileName(subfolderPath));
            AddFolderToZip(archive, subfolderPath, newRelativePath, compressionLevel);
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
    public bool ExtractToDirectory(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null)
    {
        if (!Directory.Exists(destinationDirectoryName))
        {
            Directory.CreateDirectory(destinationDirectoryName);
        }
        ZipFile.ExtractToDirectory(archiveFile, destinationDirectoryName, encoding, overwriteFiles);
        return true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFile"></param>
    /// <param name="destinationDirectoryName"></param>
    /// <param name="overwriteFiles"></param>
    /// <param name="encoding"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> ExtractToDirectoryAsync(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null, CancellationToken token = default)
    {
        if (!Directory.Exists(destinationDirectoryName))
        {
            Directory.CreateDirectory(destinationDirectoryName);
        }

#if NET10_0_OR_GREATER
        await ZipFile.ExtractToDirectoryAsync(archiveFile, destinationDirectoryName, encoding, overwriteFiles, token);
#else
        await Task.Run(() =>
        {
            token.ThrowIfCancellationRequested();
            ZipFile.ExtractToDirectory(archiveFile, destinationDirectoryName, encoding, overwriteFiles);
        }, token);
#endif
        return true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entryFile">解压缩文件</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <returns></returns>
    public ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null)
    {
        using var archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read, encoding);
        return archive.GetEntry(Path.GetFileName(entryFile));
    }
}
