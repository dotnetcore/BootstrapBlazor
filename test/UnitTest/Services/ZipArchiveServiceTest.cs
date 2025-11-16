// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        var stream = await archService.ArchiveAsync(files);
        Assert.NotNull(stream);

        stream = await archService.ArchiveAsync(files, new ArchiveOptions()
        {
            CompressionLevel = System.IO.Compression.CompressionLevel.Optimal,
            Encoding = System.Text.Encoding.UTF8,
            Mode = System.IO.Compression.ZipArchiveMode.Create,
            ReadStreamAsync = f => Task.FromResult<Stream>(new MemoryStream("A"u8.ToArray()))
        });
        Assert.NotNull(stream);

        var archiveFile = Path.Combine(root, "test.zip");
        await archService.ArchiveAsync(archiveFile, files);
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
        archService.ExtractToDirectory(archiveFile, destFolder);
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
        await archService.ArchiveDirectory(destFile, destFolder, includeBaseDirectory: true);
        Assert.True(File.Exists(destFile));
        File.Delete(destFile);

        await Assert.ThrowsAsync<ArgumentNullException>(() => archService.ArchiveDirectory(null!, destFolder, includeBaseDirectory: true));
    }
}
