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
    /// <inheritdoc cref="IZipArchiveService.ArchiveAsync(IEnumerable{string}, ArchiveOptions?)"/>
    /// </summary>
    public Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null) => ArchiveAsync(files.Select(f => new ArchiveEntry()
    {
        SourceFileName = f,
        EntryName = Path.GetFileName(f),
    }), options);

    /// <summary>
    /// <inheritdoc cref="IZipArchiveService.ArchiveAsync(string, IEnumerable{string}, ArchiveOptions?)"/>
    /// </summary>
    public Task ArchiveAsync(string archiveFile, IEnumerable<string> files, ArchiveOptions? options = null) => ArchiveAsync(archiveFile, files.Select(f => new ArchiveEntry()
    {
        SourceFileName = f,
        EntryName = Path.GetFileName(f),
    }), options);

    /// <summary>
    /// <inheritdoc cref="IZipArchiveService.ArchiveAsync(IEnumerable{ArchiveEntry}, ArchiveOptions?)"/>
    /// </summary>
    public async Task<Stream> ArchiveAsync(IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null)
    {
        var stream = new MemoryStream();
        options ??= new ArchiveOptions();
        options.LeaveOpen = true;
        await ArchiveFilesAsync(stream, entries, options);
        stream.Position = 0;
        return stream;
    }

    /// <summary>
    /// <inheritdoc cref="IZipArchiveService.ArchiveAsync(string, IEnumerable{ArchiveEntry}, ArchiveOptions?)"/>
    /// </summary>
    public async Task ArchiveAsync(string archiveFile, IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null)
    {
        await using var stream = File.OpenWrite(archiveFile);
        await ArchiveFilesAsync(stream, entries, options);
    }

    private static async Task ArchiveFilesAsync(Stream stream, IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null)
    {
        options ??= new ArchiveOptions();
        using var archive = new ZipArchive(stream, options.Mode, options.LeaveOpen, options.Encoding);
        foreach (var f in entries)
        {
            if (string.IsNullOrEmpty(f.EntryName))
            {
                continue;
            }

            if (options.ReadStreamAsync != null)
            {
                var entry = archive.CreateEntry(f.EntryName, options.CompressionLevel);
                await using var content = await options.ReadStreamAsync(f.SourceFileName);
                await using var entryStream = entry.Open();
                await content.CopyToAsync(entryStream);
            }
            else if (Directory.Exists(f.SourceFileName))
            {
                var entryName = f.EntryName.Replace("\\", "/");
                if (!entryName.EndsWith("/"))
                {
                    entryName = $"{entryName}/";
                }

                archive.CreateEntry(entryName, f.CompressionLevel ?? options.CompressionLevel);
            }
            else if (File.Exists(f.SourceFileName))
            {
                archive.CreateEntryFromFile(f.SourceFileName, f.EntryName, f.CompressionLevel ?? options.CompressionLevel);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task ArchiveDirectoryAsync(string archiveFile, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null, CancellationToken token = default)
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
    public ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null)
    {
        using var archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read, encoding);
        return archive.GetEntry(Path.GetFileName(entryFile));
    }
}
