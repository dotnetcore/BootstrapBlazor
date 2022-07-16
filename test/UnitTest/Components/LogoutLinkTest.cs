// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class LogoutLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<LogoutLink>(builder => builder.Add(s => s.Icon, "fa fa-key"));
        var ele = cut.Find(".fa-key");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<LogoutLink>(builder => builder.Add(s => s.Text, "Logout"));
        Assert.Contains("Logout", cut.Markup);
    }

    [Fact]
    public void Url_Ok()
    {
        var cut = Context.RenderComponent<LogoutLink>(builder => builder.Add(s => s.Url, "/Account/Logout"));
        cut.Contains("href=\"#\"");
    }

    [Fact]
    public void ForceLoad_Ok()
    {
        var cut = Context.RenderComponent<LogoutLink>(builder => builder.Add(s => s.ForceLoad, true));
        cut.Contains("href=\"#\"");
    }

    [Fact]
    public async Task OnLogout_Ok()
    {
        var navMan = Context.Services.GetRequiredService<FakeNavigationManager>();
        var cut = Context.RenderComponent<LogoutLink>();
        await cut.InvokeAsync(() => cut.Find("a").Click());

        // 由于其他 Test 更改为 "/Test"
        Assert.NotEqual("/", navMan.Uri);
    }
}
