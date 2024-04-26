// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UnitTest.Misc;

namespace UnitTest.Components;

public class TabTest : TabTestBase
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
                pb.Add(a => a.Closable, true);
                pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                pb.Add(a => a.ChildContent, "Tab1-Content");
            });
        });
        Assert.Contains("Tab1-Content", cut.Markup);
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
        var button = cut.Find(".nav-link-bar.left");
        button.Click();
        button.Click();
        button.Click();
        Assert.Equal("Tab1-Content", cut.Find(".tabs-body .d-none").InnerHtml);

        // Click Next
        button = cut.Find(".nav-link-bar.right");
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
        var button = cut.Find(".nav-link-bar.left");
        button.Click();

        // Click Next
        button = cut.Find(".nav-link-bar.right");
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
    public void AddTabByUrl_Ok()
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
            pb.Add(a => a.ExcludeUrls, new String[] { "/" });
        });

        navMan.NavigateTo("/");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, new String[] { "" });
        });

        navMan.NavigateTo("/Cat");
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, new String[] { "/", "Cat" });
        });

        navMan.NavigateTo("/");
        cut.InvokeAsync(() => cut.Instance.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Cat",
            ["Url"] = "Cat"
        }));
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ExcludeUrls, new String[] { "/Test" });
        });
        cut.InvokeAsync(() => cut.Instance.CloseCurrentTab());

        // AddTab
        cut.InvokeAsync(() => cut.Instance.AddTab(new Dictionary<string, object?>
        {
            ["Text"] = "Cat",
            ["Url"] = null,
            ["IsActive"] = true
        }));

        // Remove Tab
        var item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
        Assert.Equal("", item.Url);

        cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
        Assert.Equal("Cat", item.Url);

        cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.Null(item);
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
        cut.Contains("<div class=\"tabs-body-content\">Binder</div>");

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
            mi.Invoke(instance, new object[] { "/" });
        });
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
            mi.Invoke(instance, new object[] { "/" });
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
        Assert.DoesNotContain("tabs-body-content", cut.Markup);

        // 提高代码覆盖率
        cut.InvokeAsync(() => cut.Instance.CloseOtherTabs());
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
        var item = cut.FindAll(".tabs-item").Last();
        cut.InvokeAsync(() => item.Click());
        cut.Contains("Tab1-Content");
        cut.Contains("Tab2-Content");

        // 再点击第一个 TabItem
        item = cut.FindAll(".tabs-item").First();
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
    public void ActiveTab_Ok()
    {
        var cut = Context.RenderComponent<Tab>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.DefaultUrl, "/");
        });
        cut.InvokeAsync(() => cut.Instance.ActiveTab(0));
        var item = cut.Instance.GetActiveTab();
        Assert.NotNull(item);
        cut.InvokeAsync(() =>
        {
            if (item != null)
            {
                cut.Instance.ActiveTab(item);
            }
        });
        cut.InvokeAsync(() => cut.Instance.RemoveTab(item!));
        item = cut.Instance.GetActiveTab();
        Assert.Null(item);
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
            pb.Add(a => a.ButtonTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, new MarkupString("<div>test-button</div>"));
            }));
        });
        cut.Contains("<div>test-button</div>");
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
    }
}
