// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TitleTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task SetTitle_Ok()
    {
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTitle>();
        });
        var mockTitle = cut.FindComponent<MockTitle>();
        await mockTitle.Instance.SetTitle("test");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<Title>(pb =>
        {
            pb.Add(a => a.Text, "Text");
        });
        var text = cut.Instance.Text;
        Assert.Equal("Text", text);

        cut.Render();
    }

    private class MockTitle : ComponentBase
    {
        [Inject]
        [NotNull]
        public TitleService? TitleService { get; set; }

        public Task SetTitle(string title) => TitleService.SetTitle(title);
    }
}
