// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AutoRedirectTest : BootstrapBlazorTestBase
{
    [Fact]
    public void LogoutUrl_Ok()
    {
        var cut = Context.Render<AutoRedirect>(pb =>
        {
            pb.Add(a => a.RedirectUrl, "/Account/Logout");
        });

        Assert.Equal("/Account/Logout", cut.Instance.RedirectUrl);
    }

    [Fact]
    public void Interval_Ok()
    {
        var cut = Context.Render<AutoRedirect>(pb =>
        {
            pb.Add(a => a.Interval, 50);
        });

        Assert.Equal(50, cut.Instance.Interval);
    }

    [Fact]
    public async Task OnBeforeRedirectAsync_Ok()
    {
        var cut = Context.Render<AutoRedirect>(pb =>
        {
            pb.Add(a => a.RedirectUrl, "/Account/Logout");
            pb.Add(a => a.OnBeforeRedirectAsync, () =>
            {
                return Task.FromResult(true);
            });
        });
        await cut.InvokeAsync(() => cut.Instance.Lock());

        cut.Render(pb =>
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
