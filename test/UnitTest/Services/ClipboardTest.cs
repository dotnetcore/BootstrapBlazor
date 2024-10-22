// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
