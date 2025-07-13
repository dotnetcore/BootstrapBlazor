// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class DefaultNetworkMonitorServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetNetworkMonitorState_Ok()
    {
        Context.JSInterop.Setup<NetworkMonitorState>("getNetworkInfo", matcher => true).SetResult(new NetworkMonitorState
        {
            NetworkType = "4g",
            Downlink = 10,
            RTT = 100
        });
        var service = Context.Services.GetRequiredService<INetworkMonitorService>();
        var state = await service.GetNetworkMonitorState(CancellationToken.None);
        Assert.Equal("4g", state.NetworkType);
        Assert.Equal(10, state.Downlink);
        Assert.Equal(100, state.RTT);
    }
}
