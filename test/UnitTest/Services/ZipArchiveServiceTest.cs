// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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
    }
}
