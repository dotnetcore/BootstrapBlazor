// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class PaginationTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnPageClick_Ok()
    {
        var pageClicked = false;
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 30);
            pb.Add(a => a.OnPageLinkClick, pageIndex =>
            {
                pageClicked = true;
                return Task.CompletedTask;
            });
        });
        var link = cut.Find(".page-link");
        cut.InvokeAsync(() => link.Click());
        Assert.True(pageClicked);
    }

    [Fact]
    public async Task MovePage_Ok()
    {
        var pageClicked = false;
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 30);
            pb.Add(a => a.OnPageLinkClick, pageIndex =>
            {
                pageClicked = true;
                return Task.CompletedTask;
            });
        });
        Assert.DoesNotContain("prev-link", cut.Markup);

        var item = cut.Find(".next-link .page-link");
        await cut.InvokeAsync(() => item.Click());
        Assert.True(pageClicked);

        pageClicked = false;
        item = cut.Find(".prev-link .page-link");
        await cut.InvokeAsync(() => item.Click());
        Assert.True(pageClicked);

        pageClicked = false;
        var items = cut.FindAll(".page-item .page-link");
        await cut.InvokeAsync(() => items[3].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        await cut.InvokeAsync(() => items[0].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        await cut.InvokeAsync(() => items[8].Click());
        Assert.True(pageClicked);
    }

    [Fact]
    public void GotoTemplate_Ok()
    {
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 10);
            pb.Add(a => a.ShowGotoNavigator, true);
            pb.Add(a => a.GotoTemplate, builder =>
            {
                builder.AddContent(0, "GotoTemplate");
            });
        });
        cut.Contains("GotoTemplate");
    }

    [Fact]
    public async Task GotoNavigator_Ok()
    {
        var index = 0;
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 20);
            pb.Add(a => a.ShowGotoNavigator, true);
            pb.Add(a => a.OnPageLinkClick, pageIndex =>
            {
                index = pageIndex;
                return Task.CompletedTask;
            });
        });

        var navigator = cut.FindComponent<GotoNavigator>();
        var input = navigator.Find("input");
        await cut.InvokeAsync(() =>
        {
            input.Change("5");
            input.KeyUp("Enter");
        });
        Assert.Equal(5, index);
    }

    [Fact]
    public void PageInfoTemplate_Ok()
    {
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 20);
            pb.Add(a => a.ShowPageInfo, true);
            pb.Add(a => a.PageInfoTemplate, build =>
            {
                build.AddContent(0, "PageInfoTemplate");
            });
        });
        cut.Contains("PageInfoTemplate");
    }

    [Fact]
    public void PageInfoText_Ok()
    {
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 20);
            pb.Add(a => a.ShowPageInfo, true);
            pb.Add(a => a.PageInfoText, "PageInfoText");
        });
        cut.Contains("PageInfoText");

        cut.Render(pb =>
        {
            pb.Add(a => a.ShowPageInfo, false);
        });
        cut.DoesNotContain("PageInfoText");
    }

    [Fact]
    public void Alignment_Ok()
    {
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.Alignment, Alignment.Left);
            pb.Add(a => a.PageCount, 20);
            pb.Add(a => a.MaxPageLinkCount, 5);
        });
        cut.Contains("justify-content-start");
    }

    [Fact]
    public async Task NextLink_Ok()
    {
        var index = 0;
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageCount, 8);
            pb.Add(a => a.PageIndex, 4);
            pb.Add(a => a.MaxPageLinkCount, 5);
            pb.Add(a => a.OnPageLinkClick, pageIndex =>
            {
                index = pageIndex;
                return Task.CompletedTask;
            });
        });
        var links = cut.FindAll(".page-link");
        var link = links[links.Count - 2];
        await cut.InvokeAsync(() => link.Click());
        Assert.Equal(8, index);

        link = links[links.Count - 1];
        await cut.InvokeAsync(() => link.Click());
        Assert.Equal(8, index);
    }

    [Fact]
    public void PageIndex_Ok()
    {
        var cut = Context.Render<Pagination>(pb =>
        {
            pb.Add(a => a.PageIndex, 2);
            pb.Add(a => a.PageCount, 8);
        });
        var link = cut.Find(".active");
        Assert.Equal("2", link.TextContent);
    }

    [Fact]
    public void PaginationItem_Ok()
    {
        var cut = Context.Render<PaginationItem>(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
            pb.Add(a => a.Index, 1);
        });
        Assert.Contains("disabled", cut.Markup);
    }
}
