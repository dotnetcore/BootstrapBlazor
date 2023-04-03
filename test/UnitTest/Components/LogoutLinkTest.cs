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
        var cut = Context.RenderComponent<LogoutLink>(builder => builder.Add(s => s.Icon, "fa-solid fa-key"));
        cut.Contains("fa-solid fa-key");
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
        cut.Contains("href=\"/Account/Logout\"");
    }
}
