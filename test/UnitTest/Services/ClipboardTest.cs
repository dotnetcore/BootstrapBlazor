// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

public class ClipboardServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetText_Ok()
    {
        var service = Context.Services.GetRequiredService<ClipboardService>();
        var cut = Context.RenderComponent<Clipboard>();
        var copied = false;
        await service.Copy("Test", () =>
        {
            copied = true;
            return Task.CompletedTask;
        });
        Assert.True(copied);

        var text = await service.GetText();
        await cut.Instance.DisposeAsync();
    }

    [Fact]
    public async Task Get_Ok()
    {
        var service = Context.Services.GetRequiredService<ClipboardService>();
        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new() { MimeType = "text/txt", Data = [0x31] }, new() { MimeType = null, Data = [0x32] }, new() { MimeType = null, Data = null }]);
        Context.RenderComponent<Clipboard>();

        var items = await service.Get();
        Assert.NotNull(items);
        Assert.Equal(3, items.Count);

        Assert.Equal("text/txt", items[0].MimeType);
        Assert.Equal([0x31], items[0].Data);
        Assert.Equal("1", items[0].Text);

        Assert.Null(items[1].MimeType);
        Assert.Equal([0x32], items[1].Data);
        Assert.Equal(string.Empty, items[1].Text);

        Assert.Null(items[2].MimeType);
        Assert.Null(items[2].Data);
        Assert.Equal(string.Empty, items[2].Text);
    }
}
