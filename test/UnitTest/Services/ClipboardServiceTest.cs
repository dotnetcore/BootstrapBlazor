// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ClipboardServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Clipboard_Ok()
    {
        var service = Context.Services.GetRequiredService<ClipboardService>();
        await service.Copy(null, () => Task.CompletedTask);
        await service.Copy("Test", () => Task.CompletedTask);
    }

    [Fact]
    public async Task GetText()
    {
        Context.JSInterop.Setup<string?>("getTextFromClipboard").SetResult("test123");
        var service = Context.Services.GetRequiredService<ClipboardService>();

        var text = await service.GetText();
        Assert.Equal("test123", text);
        await service.GetText();
    }

    [Fact]
    public async Task Get()
    {
        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new() { Data = [0x31, 0x32, 0x33], MimeType = "text/text" }]);
        var service = Context.Services.GetRequiredService<ClipboardService>();
        var items = await service.Get();
        var item = items[0];

        Assert.Equal("123", item.Text);
        Assert.Equal("text/text", item.MimeType);

        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new() { Data = [0x31, 0x32, 0x33] }]);
        items = await service.Get();
        item = items[0];
        Assert.Empty(item.Text);

        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new()]);
        items = await service.Get();
        item = items[0];
        Assert.Empty(item.Text);

        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult(null);
        items = await service.Get();
        Assert.Empty(items);
    }
}
