// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Rendering;
using System.Reflection;
using UnitTest.Misc;

namespace UnitTest.Components;

public class TabTest : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.ConfigureTabItemMenuBindOptions(options =>
        {
            options.Binders = new()
            {
                { "/Binder", new() { Text = "Index_Binder_Test" } }
            };
        });
    }

    [Fact]
    public async Task ContextMenu_Ok()
    {
        var cut = Context.RenderComponent<ContextMenuZone>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.ShowContextMenu, true);
                pb.Add(a => a.ShowContextMenuFullScreen, true);
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.IsDisabled, true);
                    pb.Add(a => a.Text, "Tab1");
                    pb.Add(a => a.Url, "/Index");
                    pb.Add(a => a.Closable, true);
                    pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                    pb.Add(a => a.ChildContent, "Tab1-Content");
                });
            });
        });

        var menuItem = cut.Find(".tabs-item");
        await cut.InvokeAsync(() => menuItem.ContextMenu());

        var item = cut.Find(".dropdown-menu .dropdown-item");
        Assert.NotNull(item);
    }

    [Fact]
    public void ToolbarTemplate_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Index");
                pb.Add(a => a.Closable, true);
                pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.Add(a => a.ToolbarTemplate, tab => builder => builder.AddContent(0, "test-toolbar-template"));
        });
        cut.DoesNotContain("test-toolbar-template");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowToolbar, true);
        });
        cut.Contains("test-toolbar-template");
    }

    [Fact]
    public void ToolbarTooltipText_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Index");
                pb.Add(a => a.Closable, true);
                pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.RefreshToolbarButtonIcon, "test-refresh-icon");
            pb.Add(a => a.RefreshToolbarTooltipText, "test-refresh-tooltip-text");
            pb.Add(a => a.FullscreenToolbarButtonIcon, "test-fullscreen-icon");
            pb.Add(a => a.FullscreenToolbarTooltipText, "test-fullscreen-tooltip-text");
        });
        cut.Contains("test-refresh-icon");
        cut.Contains("test-refresh-tooltip-text");
    }

    [Fact]
    public void TabItem_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Index");
                pb.Add(a => a.Closable, true);
                pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
        });
        Assert.Contains("Tab1-Content", cut.Markup);
    }

    [Fact]
    public void TabItem_Null()
    {
        var cut = Context.RenderComponent<TabItem>(pb =>
        {
            pb.Add(a => a.Text, "Test");
        });
    }

    [Fact]
    public void TabItemCreate_Ok()
    {
        TabItem.Create(new Dictionary<string, object?>()
        {
            ["Url"] = null
        });
        TabItem.Create(new Dictionary<string, object?>()
        {
            ["Url"] = new NullString()
        });
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
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
        TabItem? closedItem = null;
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowExtendButtons, true);
            pb.Add(a => a.Placement, Placement.Bottom);
            pb.Add(a => a.ShowClose, true);
            pb.Add(a => a.OnCloseTabItemAsync, item =>
            {
                closedItem = item;
                return Task.FromResult(true);
            });
            pb.Add(a => a.OnClickTabItemAsync, item =>
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

        // Click Prev
        var button = cut.Find(".nav-link-bar.left .nav-link-bar-button");
        button.Click();
        button.Click();
        button.Click();
        Assert.Equal("Tab1-Content", cut.Find(".tabs-body .d-none").InnerHtml);

        // Click Next
        button = cut.Find(".nav-link-bar.right .nav-link-bar-button");
        button.Click();
        button.Click();
        button.Click();
        Assert.Equal("Tab2-Content", cut.Find(".tabs-body .d-none").InnerHtml);

        // Close
        Assert.Null(closedItem);
        button = cut.Find(".tabs-item-close");
        button.Click();
        Assert.NotNull(closedItem);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnCloseTabItemAsync, item =>
            {
                return Task.FromResult(false);
            });
        });
        button = cut.Find(".tabs-item-close");
        button.Click();
        Assert.Contains("tabs-body-content", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnCloseTabItemAsync, item =>
            {
                return Task.FromResult(true);
            });
        });
        button = cut.Find(".tabs-item-close");
        button.Click();
        Assert.DoesNotContain("tabs-body-content", cut.Markup);
    }

    [Fact]
    public void ClickTabToNavigation_True()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowExtendButtons, true);
            pb.Add(a => a.Placement, Placement.Top);
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.Add(a => a.ShowClose, true);
            pb.Add(a => a.DefaultUrl, "/");
        });
        cut.InvokeAsync(() => cut.Instance.AddTab("/", "Index"));
        cut.InvokeAsync(() => cut.Instance.AddTab("/Cat", null!));

        // Click Prev
        var button = cut.Find(".nav-link-bar.left .nav-link-bar-button");
        button.Click();

        // Click Next
        button = cut.Find(".nav-link-bar.right .nav-link-bar-button");
        button.Click();

        button = cut.Find(".tabs-item-close");
        button.Click();

        // Close Current
        cut.InvokeAsync(() => cut.Instance.CloseAllTabs());

        button = cut.Find(".dropdown-item");
        button.Click();
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

        cut.InvokeAsync(() => tab.AddTab("/", "Tab2", "fa-solid fa-font-awesome", false, true));
        cut.InvokeAsync(() => tab.CloseOtherTabs());
        Assert.Equal("Tab1-Body", cut.Find(".tabs-body-content").InnerHtml);

        // NotFound
        cut.InvokeAsync(() => tab.AddTab("/Test", "Tab3", "fa-solid fa-font-awesome", false, true));
        Assert.Contains("NotFound-Tab", cut.Markup);
    }

    [Fact]
    public async Task AddTab_Index()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowClose, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
        });
        var tab = cut.Instance;
        await cut.InvokeAsync(() => tab.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Cat",
            ["Url"] = "Cat"
        }));
        await cut.InvokeAsync(() => tab.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Dog",
            ["Url"] = "Dog"
        }, 0));
        var tabItems = tab.Items.ToList();
        Assert.Equal("Dog", tabItems[0].Text);
        Assert.Equal("Cat", tabItems[1].Text);
    }

    [Fact]
    public async Task AddTabByUrl_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        navMan.NavigateTo("/");
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
        });

        navMan.NavigateTo("/");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, ["/"]);
        });

        navMan.NavigateTo("/");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, [""]);
        });

        navMan.NavigateTo("/Cat");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, ["/", "Cat"]);
        });

        navMan.NavigateTo("/");
        await cut.InvokeAsync(() => cut.Instance.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Cat",
            ["Url"] = "Cat"
        }));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, ["/Test"]);
        });
        await cut.InvokeAsync(() => cut.Instance.CloseCurrentTab());

        // AddTab
        await cut.InvokeAsync(() => cut.Instance.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Cat",
            ["Url"] = null,
            ["IsActive"] = true
        }));

        // Remove Tab
        var item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
        Assert.Equal("", item.Url);

        await cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
        Assert.Equal("Cat", item.Url);

        await cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.Null(item);

        await cut.InvokeAsync(() => cut.Instance.CloseCurrentTab());
    }

    [Fact]
    public void Menus_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.Add(a => a.Menus, new List<MenuItem>()
            {
                new()
                {
                    Text = "menu1",
                    Url = "/Binder",
                    Icon = "fa-solid fa-home"
                },
                new()
                {
                    Text = "menu1",
                    Url = "/Dog",
                    Icon = "fa-solid fa-home"
                }
            });
        });
        var nav = cut.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/Binder");
        cut.Contains("Binder");

        var items = cut.Instance.Items;
        Assert.Equal(2, items.Count());

        var item = items.Last();
        Assert.Equal("Binder", item.Url);
        Assert.Equal("Index_Binder_Test", item.Text);
    }

    [Fact]
    public void MenuItem_Menu()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.Add(a => a.Menus, new List<MenuItem>()
            {
                new(),
                new()
            });
        });

        // reflection test GetMenuItem
        cut.InvokeAsync(() =>
        {
            var instance = cut.Instance;
            var mi = instance.GetType().GetMethod("GetMenuItem", BindingFlags.Instance | BindingFlags.NonPublic)!;
            mi.Invoke(instance, ["/"]);
        });
    }

    [Fact]
    public async Task IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ClickTabToNavigation, false);

            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                pb.Add(a => a.Icon, "fa fa-fa");
                pb.Add(a => a.IsDisabled, true);
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text2");
                pb.AddChildContent<DisableTabItemButton>();
            });
        });
        var tabItems = cut.FindAll(".tabs-item");
        Assert.Contains("tabs-item disabled", tabItems[0].OuterHtml);
        Assert.DoesNotContain("tabs-item disabled", tabItems[1].OuterHtml);

        var button = cut.FindComponent<DisableTabItemButton>();
        Assert.NotNull(button);

        await cut.InvokeAsync(() => button.Instance.OnDisabledTabItem());
        tabItems = cut.FindAll(".tabs-item");
        Assert.Contains("tabs-item disabled", tabItems[1].OuterHtml);
    }

    [Fact]
    public void SetDisabled_Ok()
    {
        var cut = Context.RenderComponent<TabItem>();
        cut.Instance.SetDisabled(true);
    }

    [Fact]
    public async Task TabStyle_Chrome_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.TabStyle, TabStyle.Chrome);
            pb.Add(a => a.OnClickTabItemAsync, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                pb.Add(a => a.Icon, "fa fa-fa");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.IsActive, true);
                pb.Add(a => a.Text, "Text2");
                pb.AddChildContent<DisableTabItemButton>();
            });
        });
        cut.Contains("tabs tabs-top tabs-chrome");
        cut.Contains("tabs-item-wrap active");
        cut.Contains("<i class=\"tab-corner tab-corner-left\"></i>");
        cut.Contains("<i class=\"tab-corner tab-corner-right\"></i>");

        var button = cut.FindComponent<DisableTabItemButton>();
        Assert.NotNull(button);
        await cut.InvokeAsync(() => button.Instance.OnDisabledTabItem());

        // trigger click
        var link = cut.Find("a");
        await cut.InvokeAsync(() => link.Click());
        Assert.True(clicked);

        // placement top and chrome style
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Left);
        });
        Assert.Equal(TabStyle.Default, cut.Instance.TabStyle);
    }

    [Fact]
    public void TabStyle_Capsule_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.TabStyle, TabStyle.Capsule);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                pb.Add(a => a.Icon, "fa fa-fa");
            });
        });
        cut.Contains("tabs tabs-top tabs-capsule");
        cut.Contains("tabs-item-wrap active");
        cut.DoesNotContain("<i class=\"tab-corner tab-corner-left\"></i>");
        cut.DoesNotContain("<i class=\"tab-corner tab-corner-right\"></i>");
    }

    [Fact]
    public void MenuItem_Null()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
        });

        // reflection test GetMenuItem
        cut.InvokeAsync(() =>
        {
            var instance = cut.Instance;
            var mi = instance.GetType().GetMethod("GetMenuItem", BindingFlags.Instance | BindingFlags.NonPublic)!;
            mi.Invoke(instance, ["/"]);
        });
    }

    [Fact]
    public void DefaultUrl_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.DefaultUrl, "/");
        });
        var item = cut.Instance.GetActiveTab();
        Assert.Contains("Index", cut.Markup);
    }

    [Fact]
    public void IsOnlyRenderActiveTab_True()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.IsOnlyRenderActiveTab, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
                pb.Add(a => a.Closable, false);
                pb.Add(a => a.ChildContent, "Tab2-Content");
            });
        });
        Assert.Contains("Tab1-Content", cut.Markup);
        Assert.DoesNotContain("Tab2-Content", cut.Markup);

        // 提高代码覆盖率
        cut.InvokeAsync(cut.Instance.CloseOtherTabs);
    }

    [Fact]
    public void IsOnlyRenderActiveTab_False()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.IsOnlyRenderActiveTab, false);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
                pb.Add(a => a.Closable, false);
                pb.Add(a => a.ChildContent, "Tab2-Content");
            });
        });

        cut.InvokeAsync(() =>
        {
            var count = cut.FindAll("tabs-body-content").Count;
            Assert.Equal(2, count);
        });
    }

    [Fact]
    public void IsLazyLoadTabItem_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.IsLazyLoadTabItem, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
                pb.Add(a => a.ChildContent, "Tab2-Content");
            });
        });
        cut.Contains("Tab1-Content");
        cut.DoesNotContain("Tab2-Content");

        // 点击第二个 TabItem
        var items = cut.FindAll(".tabs-item");
        var item = items[items.Count - 1];
        cut.InvokeAsync(() => item.Click());
        cut.Contains("Tab1-Content");
        cut.Contains("Tab2-Content");

        // 再点击第一个 TabItem
        items = cut.FindAll(".tabs-item");
        item = items[0];
        cut.InvokeAsync(() => item.Click());
        cut.Contains("Tab1-Content");
        cut.Contains("Tab2-Content");
    }

    [Fact]
    public void AlwaysLoad_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.IsLazyLoadTabItem, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
                pb.Add(a => a.AlwaysLoad, true);
                pb.Add(a => a.ChildContent, "Tab2-Content");
            });
        });
        cut.Contains("Tab1-Content");
        cut.Contains("Tab2-Content");
    }

    [Fact]
    public async Task ActiveTab_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.DefaultUrl, "/");
        });
        await cut.InvokeAsync(() => cut.Instance.ActiveTab(0));

        var item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);

        await cut.InvokeAsync(() => cut.Instance.ActiveTab(item));

        // 移除标签导航到默认标签
        await cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
    }

    [Fact]
    public void NavigationActiveTab_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        navMan.NavigateTo("/");
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab2");
                pb.Add(a => a.Url, "/");
            });
        });
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowExtendButtons, true);
            pb.Add(a => a.ButtonTemplate, tab => builder =>
            {
                builder.AddContent(0, new MarkupString("<div>test-button</div>"));
            });
        });
        cut.Contains("<div>test-button</div>");
    }

    [Fact]
    public void ShowNavigatorButtons_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowNavigatorButtons, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
            });
        });

        var links = cut.FindAll(".nav-link-bar");
        Assert.Equal(2, links.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowNavigatorButtons, false);
        });
        links = cut.FindAll(".nav-link-bar");
        Assert.Empty(links);
    }

    [Fact]
    public void ShowActiveBar_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowActiveBar, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
            });
        });
        cut.Contains("<div class=\"tabs-active-bar\"></div>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowActiveBar, false);
        });
        cut.DoesNotContain("<div class=\"tabs-active-bar\"></div>");
    }

    [Fact]
    public void Text_Ok()
    {
        var text = "Tab1";
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, text);
                pb.Add(a => a.Url, "/Cat");
            });
        });
        cut.Contains("<span class=\"tabs-item-text\">Tab1</span>");

        text = "Text";
        cut.SetParametersAndRender(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, text);
                pb.Add(a => a.Url, "/Cat");
            });
        });
        cut.Contains("<span class=\"tabs-item-text\">Text</span>");
    }

    [Fact]
    public void SetText_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Tab1");
                pb.Add(a => a.Url, "/Cat");
            });
        });
        cut.Contains("<span class=\"tabs-item-text\">Tab1</span>");
        var item = cut.FindComponent<TabItem>();
        cut.InvokeAsync(() => item.Instance.SetHeader("Text", "fa fa-fa", true));
        item.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Url, "/Dog");
        });
        cut.Markup.Contains("<span class=\"tabs-item-text\">Text</span>");
    }

    [Fact]
    public void TabItem_HeaderTemplate()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.HeaderTemplate, tab => builder =>
                {
                    builder.AddContent(0, "test-HeaderTemplate");
                });
                pb.Add(a => a.Url, "/Cat");
            });
        });
        cut.Contains("test-HeaderTemplate");
    }

    [Fact]
    public void TabItemOptions_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowExtendButtons, true);
            pb.Add(a => a.Placement, Placement.Top);
            pb.Add(a => a.ClickTabToNavigation, true);
            pb.Add(a => a.ShowClose, true);
            pb.Add(a => a.DefaultUrl, "/Dog");
        });
        cut.InvokeAsync(() => cut.Instance.AddTab("/Dog", "Dog"));
        var tabItem = cut.FindAll(".tabs-item").First(i => i.InnerHtml.Contains("Dog"));
        Assert.DoesNotContain("tabs-item-close", tabItem.InnerHtml);
    }

    [Fact]
    public void TabItemMenuBinder_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.DefaultUrl, "/Binder");
        });
        cut.Contains("Index_Binder_Test");
    }

    [Fact]
    public void CloseAll_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.ShowExtendButtons, true);
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text2");
                pb.Add(a => a.Closable, false);
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test2"));
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text3");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test3"));
            });
        });

        var button = cut.FindAll(".dropdown-menu > .dropdown-item")[cut.FindAll(".dropdown-menu > .dropdown-item").Count - 1].Children.First();
        cut.InvokeAsync(() => button.Click());
        cut.Contains("Text2");
        cut.DoesNotContain("Text3");
    }

    [Fact]
    public void SetPlacement_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
            });
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Bottom);
        });
        cut.Contains("tabs-bottom");
    }

    [Fact]
    public void Drag_Ok()
    {
        var dragged = false;
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text1");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
            });
            pb.AddChildContent<TabItem>(pb =>
            {
                pb.Add(a => a.Text, "Text2");
                pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test2"));
            });
            pb.Add(a => a.AllowDrag, true);
            pb.Add(a => a.OnDragItemEndAsync, item =>
            {
                dragged = true;
                return Task.CompletedTask;
            });
        });

        cut.Contains("draggable=\"true\"");

        cut.InvokeAsync(() => cut.Instance.DragItemCallback(0, 1));
        Assert.True(dragged);

        dragged = false;
        cut.InvokeAsync(() => cut.Instance.DragItemCallback(10, 1));
        Assert.False(dragged);
    }

    [Fact]
    public async Task FullScreen_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.ShowFullScreen, true);
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.ShowFullScreen, true);
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                });
            });
        });

        var button = cut.Find(".btn-fs");
        await cut.InvokeAsync(() => button.Click());

        var tab = cut.FindComponent<Tab>();
        tab.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFullScreen, false);
        });
        cut.DoesNotContain("btn btn-fs");
    }

    [Fact]
    public void BeforeNavigatorTemplate_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.BeforeNavigatorTemplate, tab => builder => builder.AddContent(0, "before-navigator-template"));
                pb.Add(a => a.AfterNavigatorTemplate, tab => builder => builder.AddContent(0, "after-navigator-template"));
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.ShowFullScreen, true);
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                });
            });
        });
        cut.Contains("before-navigator-template");
        cut.Contains("after-navigator-template");
    }

    [Fact]
    public async Task ShowToolbar_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.ShowToolbar, false);
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.ShowFullScreen, true);
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                });
                pb.Add(a => a.OnToolbarRefreshCallback, () =>
                {
                    clicked = true;
                    return Task.CompletedTask;
                });
            });
        });
        cut.DoesNotContain("tabs-nav-toolbar");

        var tab = cut.FindComponent<Tab>();
        tab.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowToolbar, true);
        });
        cut.Contains("tabs-nav-toolbar");
        cut.Contains("tabs-nav-toolbar-refresh");
        cut.Contains("tabs-nav-toolbar-fs");

        // 点击刷新按钮
        var button = cut.Find(".tabs-nav-toolbar-refresh");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);

        clicked = false;
        var item = cut.FindComponent<TabItem>();
        await cut.InvokeAsync(() => tab.Instance.Refresh(item.Instance));
        Assert.True(clicked);

        tab.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowRefreshToolbarButton, false);
        });
        cut.DoesNotContain("tabs-nav-toolbar-refresh");

        tab.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFullscreenToolbarButton, false);
        });
        cut.DoesNotContain("tabs-nav-toolbar-fs");
    }

    [Fact]
    public void TabHeader_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTabHeader>();
            pb.AddChildContent<Tab>(pb =>
            {
                pb.Add(a => a.ShowToolbar, false);
                pb.AddChildContent<TabItem>(pb =>
                {
                    pb.Add(a => a.ShowFullScreen, true);
                    pb.Add(a => a.Text, "Text1");
                    pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "Test1"));
                });
            });
        });
        var header = cut.FindComponent<MockTabHeader>();
        var tab = cut.FindComponent<Tab>();
        var headerElement = cut.Find(".tabs-header");
        Assert.NotNull(headerElement);

        tab.Instance.SetTabHeader(header.Instance);
        tab.SetParametersAndRender();
        tab.DoesNotContain("tabs-header");
    }

    class DisableTabItemButton : ComponentBase
    {
        [CascadingParameter, NotNull]
        private TabItem? TabItem { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.OnClickWithoutRender), OnDisabledTabItem);
            builder.CloseComponent();
        }

        public Task OnDisabledTabItem()
        {
            TabItem.SetDisabled(true);
            return Task.CompletedTask;
        }
    }

    class MockTabHeader : ComponentBase, ITabHeader
    {
        public string GetId() => "MockTabHeader";

        private RenderFragment? _renderFragment;

        public void Render(RenderFragment renderFragment) => _renderFragment = renderFragment;

        protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, _renderFragment);
    }
}
