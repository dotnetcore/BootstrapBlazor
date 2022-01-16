// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class AutoRedirectTest : TestBase
{
    [Fact]
    public void LogoutUrl_Ok()
    {
        var cut = Context.RenderComponent<AutoRedirect>(pb =>
        {
            pb.Add(a => a.LogoutUrl, "/Account/Logout");
        });

        Assert.Equal("/Account/Logout", cut.Instance.LogoutUrl);
    }

    [Fact]
    public void Interval_Ok()
    {
        var cut = Context.RenderComponent<AutoRedirect>(pb =>
        {
            pb.Add(a => a.Interval, 50);
        });

        Assert.Equal(50, cut.Instance.Interval);
    }

    [Fact]
    public void Lock_Ok()
    {
        var cut = Context.RenderComponent<AutoRedirect>(pb =>
        {
            pb.Add(a => a.LogoutUrl, "/Account/Logout");
        });
        cut.Instance.Lock();
    }
}
