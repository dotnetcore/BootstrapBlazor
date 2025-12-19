// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;

namespace UnitTest.Services;

public class ZipArchiveServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Archive_Ok()
    {
        var archService = Context.Services.GetRequiredService<IZipArchiveService>();
        var root = AppContext.BaseDirectory;
        var files = new string[]
        {
            Path.Combine(root, "1.txt"),
            Path.Combine(root, "2.txt")
        };
        files.ToList().ForEach(f =>
        {
            using var fs = File.OpenWrite(f);
            fs.WriteByte(65);
        });
        var items = files.Select(i => new ArchiveEntry()
        {
            SourceFileName = i,
            EntryName = Path.GetFileName(i)
        });
        var stream = await archService.ArchiveAsync(items);
        Assert.NotNull(stream);

        stream = await archService.ArchiveAsync(items, new ArchiveOptions()
        {
            CompressionLevel = System.IO.Compression.CompressionLevel.Optimal,
            Encoding = System.Text.Encoding.UTF8,
            Mode = System.IO.Compression.ZipArchiveMode.Create,
            ReadStreamAsync = f => Task.FromResult<Stream>(new MemoryStream("A"u8.ToArray()))
        });
        Assert.NotNull(stream);

        var archiveFile = Path.Combine(root, "test.zip");
        await archService.ArchiveAsync(archiveFile, items);
        Assert.True(File.Exists(archiveFile));

        // GetEntry
        var entry = archService.GetEntry(archiveFile, files[0]);
        Assert.NotNull(entry);

        // ExtractToDirectory
        var destFolder = Path.Combine(root, "test");
        if (Directory.Exists(destFolder))
        {
            Directory.Delete(destFolder, true);
        }
        await archService.ExtractToDirectoryAsync(archiveFile, destFolder);
        Assert.True(Directory.Exists(destFolder));

        // 删除文件夹
        Directory.Delete(destFolder, true);
        Assert.False(Directory.Exists(destFolder));

        // 异步解压缩单元测试
        await archService.ExtractToDirectoryAsync(archiveFile, destFolder);
        Assert.True(Directory.Exists(destFolder));

        // 打包文件夹单元测试
        var tempFolder = Path.Combine(root, "test_temp");
        if (Directory.Exists(tempFolder))
        {
            Directory.Delete(tempFolder, true);
        }
        var destFile = Path.Combine(tempFolder, "folder.zip");
        if (File.Exists(destFile))
        {
            File.Delete(destFile);
        }
        await archService.ArchiveDirectoryAsync(destFile, destFolder, includeBaseDirectory: true);
        Assert.True(File.Exists(destFile));
        File.Delete(destFile);

        await Assert.ThrowsAsync<ArgumentNullException>(() => archService.ArchiveDirectoryAsync(null!, destFolder, includeBaseDirectory: true));
    }

    [Fact]
    public async Task ZipArchive_Ok()
    {
        var fileName = Path.Combine(AppContext.BaseDirectory, "test", "3.zip");
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        using var fs = File.OpenWrite(fileName);
        using var zip = new ZipArchive(fs, ZipArchiveMode.Create);

        var item = Path.Combine(AppContext.BaseDirectory, "test", "1.txt");
        zip.CreateEntry("text/");
        await zip.CreateEntryFromFileAsync(item, "text/1.txt");
    }

    [Fact]
    public async Task ArchiveAsync_Ok()
    {
        var fileName = Path.Combine(AppContext.BaseDirectory, "archive_test", "test.zip");
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        var root = AppContext.BaseDirectory;
        var files = new string[]
        {
            Path.Combine(root, "archive_test", "test1", "1.txt"),
            Path.Combine(root, "archive_test", "test2", "2.txt")
        };
        files.ToList().ForEach(f =>
        {
            var folder = Path.GetDirectoryName(f);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            using var fs = File.OpenWrite(f);
            fs.WriteByte(65);
        });

        var archService = Context.Services.GetRequiredService<IZipArchiveService>();
        await archService.ArchiveAsync(fileName, new List<ArchiveEntry>()
        {
            new ArchiveEntry()
            {
                SourceFileName = files[0],
                EntryName = "test1/test.log"
            },
            new ArchiveEntry()
            {
                SourceFileName = files[1],
                EntryName = "test2/test.log",
                CompressionLevel = CompressionLevel.Optimal
            },
            new ArchiveEntry()
            {
                SourceFileName = Path.Combine(AppContext.BaseDirectory, "archive_test", "test1"),
                EntryName = "test1",
            },
            new ArchiveEntry()
            {
                SourceFileName = Path.Combine(AppContext.BaseDirectory, "archive_test", "test1"),
                EntryName = "test2",
                CompressionLevel = CompressionLevel.Optimal
            }
        });

        Assert.True(File.Exists(fileName));
    }
}
