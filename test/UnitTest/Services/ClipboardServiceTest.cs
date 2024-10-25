// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using UnitTest.Pages;

namespace UnitTest.Components;

public class ClipboardServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Clipboard_Ok()
    {
        var cut = Context.RenderComponent<MockClipboard>();
        await cut.Instance.Copy(null, () => Task.CompletedTask);
        await cut.Instance.Copy("Test", () => Task.CompletedTask);
    }

    [Fact]
    public async Task GetText()
    {
        Context.JSInterop.Setup<string?>("getTextFromClipboard").SetResult("test123");
        var cut = Context.RenderComponent<MockClipboard>();

        var text = await cut.Instance.GetText();
        Assert.Equal("test123", text);
        await cut.Instance.GetText();
    }

    [Fact]
    public async Task Get()
    {
        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new() { Data = [0x31, 0x32, 0x33], MimeType = "text/text" }]);
        var cut = Context.RenderComponent<MockClipboard>();
        var items = await cut.Instance.Get();
        Assert.NotNull(items);
        var item = items[0];

        Assert.Equal("123", item.Text);
        Assert.Equal("text/text", item.MimeType);

        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new() { Data = [0x31, 0x32, 0x33] }]);
        items = await cut.Instance.Get();
        Assert.NotNull(items);
        item = items[0];
        Assert.Empty(item.Text);

        Context.JSInterop.Setup<List<ClipboardItem>?>("getAllClipboardContents").SetResult([new()]);
        items = await cut.Instance.Get();
        Assert.NotNull(items);
        item = items[0];
        Assert.Empty(item.Text);
    }

    private class MockClipboard : ComponentBase
    {
        [Inject]
        [NotNull]
        public ClipboardService? ClipboardService { get; set; }

        public Task Copy(string? text, Func<Task> callback) => ClipboardService.Copy(text, callback, CancellationToken.None);

        public Task<string?> GetText() => ClipboardService.GetText(CancellationToken.None);

        public Task<List<ClipboardItem>?> Get() => ClipboardService.Get(CancellationToken.None);
    }
}
