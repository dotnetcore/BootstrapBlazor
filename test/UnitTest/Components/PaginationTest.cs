// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class PaginationTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnPageClick_Ok()
    {
        var pageClicked = false;
        var cut = Context.RenderComponent<Pagination>(pb =>
        {
            pb.Add(a => a.PageItems, 2);
            pb.Add(a => a.TotalCount, 30);
            pb.Add(a => a.OnPageClick, (pageIndex, pageItems) =>
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
    public void OnPageItemsChanged_Ok()
    {
        int count = 0;
        var cut = Context.RenderComponent<Pagination>(pb =>
        {
            pb.Add(a => a.PageItems, 2);
            pb.Add(a => a.TotalCount, 300);
            pb.Add(a => a.OnPageItemsChanged, pageItems =>
            {
                count = pageItems;
                return Task.CompletedTask;
            });
        });
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        Assert.Equal(50, count);
    }

    [Fact]
    public void MovePage_Ok()
    {
        var pageClicked = false;
        var cut = Context.RenderComponent<Pagination>(pb =>
        {
            pb.Add(a => a.PageItems, 2);
            pb.Add(a => a.TotalCount, 300);
            pb.Add(a => a.OnPageClick, (pageIndex, pageItems) =>
            {
                pageClicked = true;
                return Task.CompletedTask;
            });
        });
        var items = cut.FindAll(".page-link-prev, .page-link-next");
        cut.InvokeAsync(() => items[0].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        cut.InvokeAsync(() => items[1].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        items = cut.FindAll(".page-item .page-link");
        cut.InvokeAsync(() => items[3].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        cut.InvokeAsync(() => items[0].Click());
        Assert.True(pageClicked);

        pageClicked = false;
        cut.InvokeAsync(() => items[8].Click());
        Assert.True(pageClicked);
    }

    [Fact]
    public void ShowPaginationInfo_Ok()
    {
        var source = new int[] { 1, 2, 4, 8, 10, 20 };
        var cut = Context.RenderComponent<Pagination>(pb =>
        {
            pb.Add(a => a.PageItems, 2);
            pb.Add(a => a.TotalCount, 10);
            pb.Add(a => a.PageItemsSource, source);
        });
        cut.Contains("pagination-bar");

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowPaginationInfo, false));
        cut.Contains("pagination-bar d-none");

        var items = cut.FindAll(".dropdown-item");
        Assert.Equal(string.Join(",", source[..5]), string.Join(",", items.Select(i => i.TextContent)));
        Assert.DoesNotContain("20", string.Join(",", items.Select(i => i.TextContent)));
    }
}
