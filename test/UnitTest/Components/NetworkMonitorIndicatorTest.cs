// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class NetworkMonitorIndicatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task NetworkMonitorIndicator_Ok()
    {
        var cut = Context.RenderComponent<NetworkMonitorIndicator>(pb =>
        {
            pb.Add(a => a.PopoverPlacement, Placement.Top);
        });

        var com = cut.FindComponent<NetworkMonitor>();
        await cut.InvokeAsync(() => com.Instance.TriggerNetworkStateChanged(new NetworkMonitorState
        {
            IsOnline = false,
            NetworkType = "4g",
            Downlink = 10.0,
            RTT = 50
        }));
        Assert.DoesNotContain("offline", cut.Markup);

        await cut.InvokeAsync(() => com.Instance.TriggerOnlineStateChanged(true));
        Assert.DoesNotContain("offline", cut.Markup);
    }

    [Fact]
    public async Task NetworkMonitor_Ok()
    {
        NetworkMonitorState? state = null;
        var cut = Context.RenderComponent<NetworkMonitor>(pb =>
        {
            pb.Add(a => a.OnNetworkStateChanged, v =>
            {
                state = v;
                return Task.CompletedTask;
            });
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerOnlineStateChanged(false));
        Assert.NotNull(state);
        Assert.False(state.IsOnline);
    }
}
