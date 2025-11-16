// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DownloadTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task DownloadFromByteArrayAsync_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadFromByteArrayAsync("test.txt", [0x01, 0x02]);
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFromFileAsync_Ok()
    {
        var fileName = Path.Combine(AppContext.BaseDirectory, "down.log");
        using var fs = File.OpenWrite(fileName);
        fs.Write([0x01, 0x02], 0, 2);
        fs.Close();

        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadFromFileAsync("test.txt", fileName);
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFromStreamAsync_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    using var stream = new MemoryStream([0x01, 0x02]);
                    await downloadService.DownloadFromStreamAsync("test.txt", stream);
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFromStreamAsync_Null()
    {
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    Stream? stream = null;
                    await downloadService.DownloadFromStreamAsync("test.txt", stream!);
                });
            });
        });
        var btn = cut.Find("button");
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => btn.Click()));

        var trigger = cut.FindComponent<Button>();
        trigger.Render(pb =>
        {
            pb.Add(a => a.OnClick, async () =>
            {
                var stream = new MemoryStream();
                await downloadService.DownloadFromStreamAsync("", stream);
            });
        });
        btn = cut.Find("button");
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => btn.Click()));
    }

    [Fact]
    public async Task DownloadFolderAsync_Ok()
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "Test");
        if (Directory.Exists(folder))
        {
            Directory.Delete(folder, true);
        }
        var fileName = Path.Combine(folder, "test.txt");
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadFolderAsync(fileName, folder);
                });
            });
        });
        var btn = cut.Find("button");
        await Assert.ThrowsAsync<DirectoryNotFoundException>(() => cut.InvokeAsync(() => btn.Click()));

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        var zipFile = Path.Combine(Directory.GetCurrentDirectory(), "Test.zip");
        if (File.Exists(zipFile))
        {
            File.Delete(zipFile);
        }
        await using var fs = File.Create(fileName);
        fs.Close();
        btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task DownloadFromUrlAsync_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadFromUrlAsync("test.txt", "./favicon.png");
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFromUrlAsync_Null()
    {
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadFromUrlAsync("test.txt", "");
                });
            });
        });
        var btn = cut.Find("button");
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => btn.Click()));

        var trigger = cut.FindComponent<Button>();
        trigger.Render(pb =>
        {
            pb.Add(a => a.OnClick, async () =>
            {
                await downloadService.DownloadFromUrlAsync("", "./favicon.png");
            });
        });
        btn = cut.Find("button");
        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => btn.Click()));
    }
}
