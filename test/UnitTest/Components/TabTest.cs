// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UnitTest.Components;

public class TabTest : BootstrapBlazorTestBase
{
    [Fact]
    public void TabItem_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Index");
                pb.Add(a => a.IsShow, true);
                pb.Add(a => a.Closable, true);
                pb.Add(a => a.Icon, "fa fa-fa");
                pb.Add(a => a.Key, "TabItem-Key");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
        });
        Assert.Contains("Tab1-Content", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowExtendButtons, true);
            pb.Add(a => a.Placement, Placement.Left);
            pb.Add(a => a.Height, 100);
        });
        Assert.Contains("100px;", cut.Markup);
    }

    [Fact]
    public void IsCard_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.IsCard, true);
            pb.Add(a => a.Placement, Placement.Bottom);
        });
        Assert.Contains("tabs-card", cut.Markup);
    }

    [Fact]
    public void IsBorderCard_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.IsBorderCard, true);
        });
        Assert.Contains("tabs-border-card", cut.Markup);
    }

    [Fact]
    public void ClickTab_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.OnClickTab, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Index");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
                pb.Add(a => a.ChildContent, "Tab2-Content");
            });
        });
        Assert.Equal("Tab2-Content", cut.Find(".tabs-body .d-none").InnerHtml);

        // Click TabItem
        cut.Find(".tabs-item").Click();
        Assert.True(clicked);

        var tab = cut.Instance;
        cut.InvokeAsync(() => tab.ClickNextTab());
        Assert.Equal("Tab1-Content", cut.Find(".tabs-body .d-none").InnerHtml);

        cut.InvokeAsync(() => tab.ClickPrevTab());
        cut.InvokeAsync(() => tab.CloseCurrentTab());
        cut.InvokeAsync(() => tab.CloseAllTabs());
    }

    [Fact]
    public void AddTab_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowClose, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.NotFoundTabText, "NotFound-Tab");
        });
        var tab = cut.Instance;
        cut.InvokeAsync(() => tab.AddTab(new Dictionary<string, object?>
        {
            ["Url"] = "/Index",
            ["IsActive"] = true,
            ["Text"] = "Tab1",
            ["ChildContent"] = new RenderFragment(builder => builder.AddContent(0, "Tab1-Body"))
        }));

        cut.InvokeAsync(() => tab.AddTab("/", "Tab2", "fa fa-fa", false, true));
        cut.InvokeAsync(() => tab.CloseOtherTabs());
        Assert.Equal("Tab1-Body", cut.Find(".tabs-body-content").InnerHtml);

        // NotFound
        cut.InvokeAsync(() => tab.AddTab("/Test", "Tab3", "fa fa-fa", false, true));
        Assert.Contains("NotFound-Tab", cut.Markup);
    }

    [Fact]
    public void AddTabByUrl_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
        });
        navMan.NavigateTo("/");
    }

    [Fact]
    public void ExcludeUrls_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.Add(a => a.ExcludeUrls, new String[] { "/Cat" });
        });
        navMan.NavigateTo("/Cat");
    }

    [Fact]
    public void DefaultUrl_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.DefaultUrl, "/");
        });
    }
}
