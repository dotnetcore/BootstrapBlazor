// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;

namespace UnitTest.Components;

public class LayoutTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShowFooter_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.Footer, CreateFooter());
        });
        Assert.Contains("Footer", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowFooter, false));
        Assert.DoesNotContain("Footer", cut.Markup);
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
        Assert.DoesNotContain("is-fixed", cut.Markup);
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
        Assert.DoesNotContain("is-collapsed", cut.Markup);
    }

    [Fact]
    public void ShowCollapseBar_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.ShowCollapseBar, true);
            pb.Add(a => a.Side, CreateSide());
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

        cut.InvokeAsync(() =>
        {
            cut.Find("header > a").Click();
        });
        Assert.True(collapsed);

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowCollapseBar, false));
        Assert.DoesNotContain("<i class=\"fa-solid fa-bars\"></i>", cut.Markup);
    }

    [Fact]
    public void IsPage_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.IsPage, true);
        });
        Assert.Contains("is-page", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsPage, false));
        Assert.DoesNotContain("is-page", cut.Markup);
    }

    [Fact]
    public void IsFullSide_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.IsFullSide, true);
            pb.Add(a => a.Side, CreateSide());
            pb.Add(a => a.ShowFooter, true);
            pb.Add(a => a.ShowGotoTop, true);
            pb.Add(a => a.Menus, new MenuItem[] { new MenuItem { Url = "/" } });
        });
        Assert.Contains("layout has-sidebar", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.IsFullSide, false));
        Assert.DoesNotContain("layout has-sidebar", cut.Markup);
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
        Assert.Contains("10%", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.SideWidth, ""));
        Assert.DoesNotContain("width:", cut.Markup);
    }

    [Fact]
    public void UseTabSet_OK()
    {
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.UseTabSet, false);
            pb.Add(a => a.Main, CreateMain());
            pb.Add(a => a.ExcludeUrls, new String[] { "/Index" });
            pb.Add(a => a.TabDefaultUrl, "/Index");
            pb.Add(a => a.IsOnlyRenderActiveTab, true);
            pb.Add(a => a.NotFoundTabText, "Test");
            pb.Add(a => a.NotAuthorized, (RenderFragment?)null);
            pb.Add(a => a.NotFound, (RenderFragment?)null);
            pb.Add(a => a.AdditionalAssemblies, new Assembly[] { GetType().Assembly });
        });
        Assert.DoesNotContain("tabs", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.UseTabSet, true));
        Assert.Contains("tabs", cut.Markup);

        var nav = cut.Services.GetRequiredService<NavigationManager>();
        nav.NavigateTo("/Cat");
        cut.Contains(">Cat<");
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
        Assert.DoesNotContain("is-fixed", cut.Markup);
    }

    [Fact]
    public void IsSmallScreen_OK()
    {
        var collapsed = false;
        var cut = Context.RenderComponent<Layout>(pb =>
        {
            pb.Add(a => a.Side, CreateSide());
            pb.Add(a => a.Menus, new MenuItem[] { new MenuItem { Url = "/" } });
            pb.Add(a => a.IsCollapsedChanged, v => collapsed = v);
            pb.Add(a => a.OnClickMenu, item =>
            {
                collapsed = true;
                return Task.CompletedTask;
            });
        });

        cut.InvokeAsync(() =>
        {
            cut.Find("li").Click();
            Assert.True(collapsed);
        });

        cut.Instance.SetCollapsed(700);

        cut.InvokeAsync(() =>
        {
            cut.Find("li").Click();
            Assert.True(collapsed);
        });
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
        cut.InvokeAsync(() =>
        {
            navMan.NavigateTo("/");
        });
        Assert.Equal("http://localhost/", navMan.Uri);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnAuthorizing, url =>
            {
                return Task.FromResult(url == "http://localhost/Test");
            });
        });
        cut.InvokeAsync(() =>
        {
            navMan.NavigateTo("/");
        });
        Assert.Equal("http://localhost/Test", navMan.Uri);
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
    public void OnUpdateAsync_Ok()
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
        cut.InvokeAsync(() => cut.Instance.UpdateAsync("Test"));
        Assert.True(updated);
    }

    [Fact]
    public void HandlerException_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, true);
            pb.AddChildContent<Layout>(pb =>
            {
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
    }

    private static RenderFragment CreateHeader(string? content = "Header") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateFooter(string? content = "Footer") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateMain(string? content = "Main") => builder => builder.AddContent(0, content);

    private static RenderFragment CreateSide(string? content = "Side") => builder => builder.AddContent(0, content);
}

public class LayoutAuthorizationTest : AuthorizateViewTestBase
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
        cut.Contains("<section class=\"layout\"><header class=\"layout-header\"></header><main class=\"layout-main\"></main></section>");
    }
}
