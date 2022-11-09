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
        var cut = Context.RenderComponent<Pagination>(pb =>
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
}
