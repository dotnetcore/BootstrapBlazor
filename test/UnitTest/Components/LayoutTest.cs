﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using System.Security.Claims;

namespace UnitTest.Components;

public class LayoutTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task TabStyle_Ok()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.TabStyle, TabStyle.Default);
            pb.Add(a => a.RefreshToolbarButtonIcon, "test-refresh-icon");
            pb.Add(a => a.FullscreenToolbarButtonIcon, "test-fullscreen-icon");
            pb.Add(a => a.OnToolbarRefreshCallback, () => Task.CompletedTask);
            pb.Add(a => a.RefreshToolbarTooltipText, "test-refresh-tooltip");
            pb.Add(a => a.FullscreenToolbarTooltipText, "test-fullscreen-tooltip");
        });
        Assert.DoesNotContain("tabs-chrome", cut.Markup);
        Assert.DoesNotContain("tabs-capsule", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.TabStyle, TabStyle.Capsule));
        Assert.Contains("tabs-capsule", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.TabStyle, TabStyle.Chrome));
        Assert.Contains("tabs-chrome", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowToolbar, true));
        Assert.Contains("tabs-nav-toolbar-refresh", cut.Markup);
        Assert.Contains("tabs-nav-toolbar-fs", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowRefreshToolbarButton, false));
        Assert.DoesNotContain("tabs-nav-toolbar-refresh", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowFullscreenToolbarButton, false));
        Assert.DoesNotContain("tabs-nav-toolbar-fs", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ToolbarTemplate, tab => builder => builder.AddContent(0, "test-toolbar-template")));
        Assert.Contains("test-toolbar-template", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowTabContextMenu, true));
        cut.Contains("bb-cm-zone");

        cut.SetParametersAndRender(pb => pb.Add(a => a.BeforeTabContextMenuTemplate, tab => b => b.AddContent(0, "test-before-tab-context-menu")));
        cut.Contains("test-before-tab-context-menu");

        cut.SetParametersAndRender(pb => pb.Add(a => a.TabContextMenuTemplate, tab => b => b.AddContent(0, "test-tab-context-menu")));
        cut.Contains("test-tab-context-menu");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.TabContextMenuRefreshIcon, "test-tab-refresh-icon");
            pb.Add(a => a.TabContextMenuCloseIcon, "test-tab-close-icon");
            pb.Add(a => a.TabContextMenuCloseAllIcon, "test-tab-close-all-icon");
            pb.Add(a => a.TabContextMenuCloseOtherIcon, "test-tab-close-other-icon");
        });

        // test context menu onclick event handler
        var tab = cut.Find(".tabs-item");
        await cut.InvokeAsync(() => tab.ContextMenu());

        var buttons = cut.FindAll(".bb-cm-zone > .dropdown-menu .dropdown-item");
        foreach (var button in buttons)
        {
            await cut.InvokeAsync(() => button.Click());
        }
        cut.Contains("test-tab-refresh-icon");
        cut.Contains("test-tab-close-icon");
        cut.Contains("test-tab-close-all-icon");
        cut.Contains("test-tab-close-other-icon");

        var show = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnBeforeShowContextMenu, item =>
            {
                show = true;
                return Task.FromResult(true);
            });
        });
        await cut.InvokeAsync(() => tab.ContextMenu());
        Assert.True(show);
    }

    [Fact]
    public async Task ShowTabInHeader_Ok()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Id, "LayoutId");
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.ShowTabInHeader, false);
            pb.Add(a => a.Header, CreateHeader());
        });
        await cut.InvokeAsync(() => cut.Instance.Render(bulder => bulder.AddContent(0, "")));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowTabInHeader, true);
        });
        cut.Contains("data-bb-header-id=\"LayoutId_tab_header\"");
        cut.Contains("tabs tabs-chrome");
        await cut.InvokeAsync(() => cut.Instance.Render(bulder => bulder.AddContent(0, "")));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowTabInHeader, false);
        });
        await cut.InvokeAsync(() => cut.Instance.Render(bulder => bulder.AddContent(0, "")));
    }

    [Fact]
    public void ShowFooter_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.Footer, CreateFooter());
            pb.Add(a => a.ShowGotoTop, true);
        });
        Assert.Contains("Footer", cut.Markup);
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsFixedTabHeader, true);
        });
        cut.Contains("data-bb-target=\".tabs-body\"");

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowFooter, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("Footer", cut.Markup));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.Footer, (RenderFragment?)null);
        });
        cut.Contains("--bb-layout-footer-height: 0px;");
    }

    [Fact]
    public void IsFixedFooter_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.IsFixedFooter, true);
            pb.Add(a => a.Footer, CreateFooter());
        });
        Assert.Contains("layout-footer is-fixed", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsFixedFooter, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("is-fixed", cut.Markup));
    }

    [Fact]
    public void IsFixedTabHeader_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Side, new RenderFragment(builder =>
            {
                builder.AddContent(0, "test");
            }));
            pb.Add(a => a.Menus, new MenuItem[] { new() { Url = "/" } });

        });
        Assert.DoesNotContain("is-fixed-tab", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsFixedTabHeader, true));
        Assert.DoesNotContain("is-fixed-tab", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.UseTabSet, true));
        Assert.Contains("is-fixed-tab", cut.Markup);
    }

    [Fact]
    public void IsPage_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.IsPage, true);
        });
        Assert.Contains("is-page", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPage, false);
        });
        Assert.DoesNotContain("is-page", cut.Markup);
    }

    [Fact]
    public void IsCollapsed_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.IsCollapsed, true);
        });
        Assert.Contains("is-collapsed", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsCollapsed, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("is-collapsed", cut.Markup));
    }

    [Fact]
    public async Task ShowCollapseBar_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.ShowCollapseBar, true);
            pb.Add(a => a.Side, CreateSide());
        });
        cut.DoesNotContain("<i class=\"fa-solid fa-bars\"></i>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Header, CreateHeader());
        });
        Assert.Contains("<i class=\"fa-solid fa-bars\"></i>", cut.Markup);

        var collapsed = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnCollapsed, state =>
            {
                collapsed = state;
                return Task.CompletedTask;
            });
            pb.Add(a => a.IsCollapsedChanged, v => collapsed = v);
        });

        var bar = cut.Find(".layout-header-bar");
        await cut.InvokeAsync(() =>
        {
            bar.Click();
        });
        Assert.True(collapsed);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowCollapseBar, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("<i class=\"fa-solid fa-bars\"></i>", cut.Markup));
    }

    [Fact]
    public void IsFullSide_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.IsFullSide, true);
            pb.Add(a => a.Side, CreateSide());
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.Footer, CreateFooter());
            pb.Add(a => a.ShowGotoTop, true);
            pb.Add(a => a.Menus, new MenuItem[] { new() { Url = "/" } });
        });
        Assert.Contains("layout has-sidebar", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsFullSide, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("layout has-sidebar", cut.Markup));
    }

    [Fact]
    public void SideWidth_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.SideWidth, "300px");
            pb.Add(a => a.Side, CreateSide());
        });
        Assert.Contains("300px", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.SideWidth, "10%"));
        cut.WaitForAssertion(() => Assert.Contains("10%", cut.Markup));

        cut.SetParametersAndRender(pb => pb.Add(a => a.SideWidth, ""));
        cut.WaitForAssertion(() => Assert.DoesNotContain("width:", cut.Markup));
    }

    [Fact]
    public void UseTabSet_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, false);
            pb.Add(a => a.Main, CreateMain());
            pb.Add(a => a.ExcludeUrls, new string[] { "/Index" });
            pb.Add(a => a.TabDefaultUrl, "/Index");
            pb.Add(a => a.IsOnlyRenderActiveTab, true);
            pb.Add(a => a.AllowDragTab, true);
            pb.Add(a => a.NotFoundTabText, "Test");
            pb.Add(a => a.NotAuthorized, (RenderFragment?)null);
            pb.Add(a => a.NotFound, (RenderFragment?)null);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
        });
        Assert.DoesNotContain("tabs", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.UseTabSet, true));
        cut.WaitForAssertion(() => Assert.Contains("tabs", cut.Markup));

        var nav = cut.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/Cat");
        cut.WaitForAssertion(() => cut.Contains(">Cat<"));

        var items = cut.FindComponent<Tab>().Instance.Items;
        Assert.Equal(2, items.Count());
        var item = items.Last();
        Assert.Equal("Cat", item.Text);
    }

    [Fact]
    public void UseTabSet_Layout()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
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
    }

    [Fact]
    public void ShowLayoutSidebar_Ok()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.IsFullSide, true);
            pb.Add(a => a.ShowSplitBar, true);
            pb.Add(a => a.SidebarMinWidth, 100);
            pb.Add(a => a.SidebarMaxWidth, 300);
            pb.Add(a => a.Side, new RenderFragment(builder =>
            {
                builder.AddContent(0, "test");
            }));
        });
        cut.Contains("layout-split-bar");
        cut.Contains("data-bb-min=\"100\"");
        cut.Contains("data-bb-max=\"300\"");
    }

    [Fact]
    public void UseTabSet_Menus()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
        });
        var nav = cut.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/Binder");
        cut.Contains("Binder");
    }

    [Fact]
    public void UseTabSet_ShowTabExtendButtons()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowTabExtendButtons, false);
        });
        cut.DoesNotContain("<div class=\"nav-link-bar dropdown dropdown-toggle\" data-bs-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">");
    }

    [Fact]
    public void UseTabSet_ShowCloseButton()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ShowTabItemClose, false);
        });
        cut.DoesNotContain("<span class=\"tabs-item-close\"");
    }

    [Fact]
    public void UseTabSet_ClickTabToNavigation()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, true);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.ClickTabToNavigation, false);
        });
        var tab = cut.FindComponent<Tab>();
        Assert.False(tab.Instance.ClickTabToNavigation);
    }

    [Fact]
    public void IsFixedHeader_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Header, CreateHeader());
            pb.Add(a => a.IsFixedHeader, true);
            pb.Add(a => a.IsAccordion, false);
        });
        Assert.Contains("layout-header is-fixed", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsFixedHeader, false));
        cut.WaitForAssertion(() => Assert.DoesNotContain("is-fixed", cut.Markup));
    }

    [Fact]
    public void IsSmallScreen_OK()
    {
        var collapsed = false;
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Side, CreateSide());
            pb.Add(a => a.Menus, new MenuItem[] { new() { Url = "/" } });
            pb.Add(a => a.IsCollapsedChanged, v => collapsed = v);
            pb.Add(a => a.OnClickMenu, item =>
            {
                collapsed = true;
                return Task.CompletedTask;
            });
        });

        cut.Find("li").Click();
        cut.WaitForAssertion(() => Assert.True(collapsed));

        cut.Instance.SetCollapsed(700);
        cut.Find("li").Click();
        cut.WaitForAssertion(() => Assert.True(collapsed));
    }

    [Fact]
    public void OnAuthorizing_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Resource, null);
            pb.Add(a => a.NotAuthorizeUrl, "/Test");
            pb.Add(a => a.OnAuthorizing, url =>
            {
                return Task.FromResult(true);
            });
        });
        navMan.NavigateTo("/");
        cut.WaitForAssertion(() => Assert.Equal("http://localhost/", navMan.Uri));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnAuthorizing, url =>
            {
                return Task.FromResult(url == "http://localhost/Test");
            });
        });
        navMan.NavigateTo("/");
        cut.WaitForAssertion(() => Assert.Equal("http://localhost/Test", navMan.Uri));
    }

    [Fact]
    public void Main_Ok()
    {
        var cut = Context.RenderComponent<CascadingValue<Task<AuthenticationState>>>(pb =>
        {
            pb.Add(a => a.Value, Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
            pb.AddChildContent<Layout>(pb =>
            {
                pb.Add(a => a.Main, builder => builder.AddContent(0, "Main"));
            });
        });
        Assert.Equal("<main class=\"layout-main\">Main</main>", cut.Markup);
    }

    [Fact]
    public void NotAuthorized_Ok()
    {
        var cut = Context.RenderComponent<CascadingValue<Task<AuthenticationState>>>(pb =>
        {
            pb.Add(a => a.Value, Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
            pb.AddChildContent<Layout>(pb =>
            {
                pb.Add(a => a.NotAuthorized, new RenderFragment(builder =>
                {
                    builder.AddContent(0, "NotAuth");
                }));
            });
        });
        Assert.Equal("NotAuth", cut.Markup);
    }

    [Fact]
    public async Task OnUpdateAsync_Ok()
    {
        var updated = false;
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.OnUpdateAsync, name =>
            {
                updated = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.UpdateAsync("Test"));
        Assert.True(updated);
    }

    [Fact]
    public void IHandlerException_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.AddChildContent<Layout>(pb =>
            {
                // 按钮触发异常
                pb.Add(a => a.Main, new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.AddAttribute(1, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                    {
                        var a = 1;
                        var b = 0;
                        var c = a / b;
                        return Task.CompletedTask;
                    }));
                    builder.CloseComponent();
                }));
            });
        });
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        cut.Contains("<div class=\"error-stack\">");
        cut.Contains("class=\"layout\"");
        Context.DisposeComponents();
    }

    [Fact]
    public void ErrorLogger_LifeCycle()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.AddChildContent<Layout>(pb =>
            {
                pb.Add(a => a.UseTabSet, false);
                pb.Add(a => a.EnableErrorLogger, true);
                pb.Add(a => a.ShowErrorLoggerToast, false);
                pb.Add(a => a.ErrorLoggerToastTitle, "Title");
                // 按钮触发异常
                pb.Add(a => a.Main, new RenderFragment(builder =>
                {
                    builder.OpenComponent<MockPage>(0);
                    builder.CloseComponent();
                }));
            });
        });
        cut.Contains("<div class=\"error-stack\">");
        cut.Contains("class=\"layout\"");
    }

    [Fact]
    public void ErrorLogger_OnErrorHandleAsync_Page()
    {
        // 页面生命周期内报错调用自定义处理方法
        Exception? ex1 = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.AddChildContent<Layout>(pb =>
            {
                pb.Add(a => a.UseTabSet, false);
                pb.Add(a => a.OnErrorHandleAsync, (logger, ex) =>
                {
                    ex1 = ex;
                    return Task.CompletedTask;
                });
                // 按钮触发异常
                pb.Add(a => a.Main, new RenderFragment(builder =>
                {
                    builder.OpenComponent<MockPage>(0);
                    builder.CloseComponent();
                }));
            });
        });
        Assert.NotNull(ex1);
    }

    [Fact]
    public void ErrorLogger_OnErrorHandleAsync_Button()
    {
        // 页面生命周期内报错调用自定义处理方法
        Exception? ex1 = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.AddChildContent<Layout>(pb =>
            {
                pb.Add(a => a.OnErrorHandleAsync, (logger, ex) =>
                {
                    ex1 = ex;
                    return Task.CompletedTask;
                });
                // 按钮触发异常
                pb.Add(a => a.Main, new RenderFragment(builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.AddAttribute(1, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
                    {
                        var a = 1;
                        var b = 0;
                        var c = a / b;
                        return Task.CompletedTask;
                    }));
                    builder.CloseComponent();
                }));
            });
        });
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        Assert.NotNull(ex1);

        // 移除自定义逻辑使用内部异常处理逻辑
        var layout = cut.FindComponent<Layout>();
        layout.SetParametersAndRender(pb =>
        {
           pb.Add(a => a.OnErrorHandleAsync, null);
        });
        button = cut.Find("button");

        ex1 = null;
        cut.InvokeAsync(() => button.Click());
        Assert.Null(ex1);
    }

    [Fact]
    public void CollapseBarTemplate_Ok()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Side, CreateSide());
            pb.Add(a => a.Header, CreateHeader());
            pb.Add(a => a.IsFullSide, true);
            pb.Add(a => a.ShowCollapseBar, true);
            pb.Add(a => a.CollapseBarTemplate, builder =>
            {
                builder.AddContent(0, "CollapseBarTemplate-Content");
            });
        });
        Assert.Contains("CollapseBarTemplate-Content", cut.Markup);
    }

    private static RenderFragment CreateHeader(string? content = "Header") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateFooter(string? content = "Footer") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateMain(string? content = "Main") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateSide(string? content = "Side") => builder => builder.AddContent(0, content);
}

public class LayoutAuthorizationTest : AuthorizationViewTestBase
{
    [Fact]
    public void Authorized_Ok()
    {
        AuthorizationContext.SetAuthorized("Admin");

        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        navMan.NavigateTo("Dog");

        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
            pb.Add(a => a.OnAuthorizing, url => Task.FromResult(true));
        });
        cut.MarkupMatches("<section id:ignore class=\"layout\" style=\"--bb-layout-header-height: 0px; --bb-layout-footer-height: 0px;\"><main class=\"layout-main\"></main></section>");
        Context.DisposeComponents();
    }
}

class MockPage : ComponentBase
{
    protected override void OnInitialized()
    {
        var a = 1;
        var b = 0;

        // 触发生命周期内异常
        var c = a / b; 
    }
}
