// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AutoRedirectTest : BootstrapBlazorTestBase
{
    [Fact]
    public void LogoutUrl_Ok()
    {
        var cut = Context.RenderComponent<AutoRedirect>(pb =>
        {
            pb.Add(a => a.RedirectUrl, "/Account/Logout");
        });

        Assert.Equal("/Account/Logout", cut.Instance.RedirectUrl);
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
    public async Task OnBeforeRedirectAsync_Ok()
    {
        var cut = Context.RenderComponent<AutoRedirect>(pb =>
        {
            pb.Add(a => a.RedirectUrl, "/Account/Logout");
            pb.Add(a => a.OnBeforeRedirectAsync, () =>
            {
                return Task.FromResult(true);
            });
        });
        await cut.InvokeAsync(() => cut.Instance.Lock());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsForceLoad, false);
            pb.Add(a => a.OnBeforeRedirectAsync, () =>
            {
                return Task.FromResult(false);
            });
        });
        await cut.InvokeAsync(() => cut.Instance.Lock());
    }
}
