// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LogoutLinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<LogoutLink>(builder => builder.Add(s => s.Icon, "fa-solid fa-key"));
        cut.Contains("fa-solid fa-key");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<LogoutLink>(builder => builder.Add(s => s.Text, "Logout"));
        Assert.Contains("Logout", cut.Markup);
    }

    [Fact]
    public void Url_Ok()
    {
        var cut = Context.Render<LogoutLink>(builder => builder.Add(s => s.Url, "/Account/Logout"));
        cut.Contains("href=\"/Account/Logout\"");
    }
}
