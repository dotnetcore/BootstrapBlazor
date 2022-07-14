// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace UnitTest.Components;

public class DownloadTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task CreateUrlAsync_NoService_Ok()
    {
        var fileName = "";
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<Button>(pb =>
        {
            pb.Add(a => a.OnClick, async () =>
            {
                await downloadService.CreateUrlAsync("test.txt", new byte[] { 0x01, 0x02 });
                fileName = "test.text";
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.Equal("test.text", fileName);
    }

    [Fact]
    public async Task CreateUrlAsync_Ok()
    {
        var fileName = "";
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.CreateUrlAsync("test.txt", new byte[] { 0x01, 0x02 });
                    fileName = "test.text";
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.Equal("test.text", fileName);
    }

    [Fact]
    public async Task CreateUrlAsync_Steam_Ok()
    {
        var fileName = "";
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    using var stream = new MemoryStream();
                    await downloadService.CreateUrlAsync("test.txt", stream);
                    fileName = "test.text";
                    stream.Close();
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.Equal("test.text", fileName);
    }

    [Fact]
    public async Task DownloadFile_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadAsync("test.txt", new byte[] { 0x01, 0x02 });
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFile_Stream_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    using var stream = new MemoryStream();
                    await downloadService.DownloadAsync("test.txt", stream);
                    stream.Close();
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    [Fact]
    public async Task DownloadFilePath_Ok()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Test.log");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadAsync("test.log", filePath);
                });
            });
        });
        var btn = cut.Find("button");
        await Assert.ThrowsAsync<FileNotFoundException>(() => cut.InvokeAsync(() => btn.Click()));

        using var fs = File.Create(filePath);
        fs.Close();
        btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task DownloadFolder_Ok()
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "Test");
        if (Directory.Exists(folder))
        {
            Directory.Delete(folder, true);
        }
        var fileName = Path.Combine(folder, "test.txt");
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
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
        using var fs = File.Create(fileName);
        fs.Close();
        btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public async Task Mock_CreateUrlAsync_Ok()
    {
        var fileName = "";
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<MockDownload>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.CreateUrlAsync("test.txt", new byte[] { 0x01, 0x02 });
                    fileName = "test.text";
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.Equal("test.text", fileName);
    }

    [Fact]
    public async Task Mock_DownloadFile_Ok()
    {
        var download = false;
        var downloadService = Context.Services.GetRequiredService<DownloadService>();
        var cut = Context.RenderComponent<MockDownload>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClick, async () =>
                {
                    await downloadService.DownloadAsync("test.txt", new byte[] { 0x01, 0x02 });
                    download = true;
                });
            });
        });
        var btn = cut.Find("button");
        await cut.InvokeAsync(() => btn.Click());
        Assert.True(download);
    }

    class MockDownload : Download
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            JSRuntime = new MockJSRuntime();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }

        class MockJSRuntime : IJSRuntime
        {
            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) => ValueTask.FromResult<TValue>(default!);

            public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) => ValueTask.FromResult<TValue>(default!);
        }
    }
}
