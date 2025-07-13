// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class NetworkMonitorIndicatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void NetworkMonitorIndicator_Ok()
    {
        var cut = Context.RenderComponent<NetworkMonitorIndicator>(pb =>
        {
            pb.Add(a => a.PopoverPlacement, Placement.Top);
        });

        Assert.DoesNotContain("offline", cut.Markup);
    }

    [Fact]
    public async Task RegisterStateChangedCallback_Ok()
    {
        var service = Context.Services.GetRequiredService<INetworkMonitorService>();
        var cut = Context.RenderComponent<NetworkMonitorIndicator>(pb =>
        {
            pb.Add(a => a.PopoverPlacement, Placement.Top);
        });

        // 测试 TriggerNetworkStateChanged 方法
        var innerServicePropertyInfo = cut.Instance.GetType().GetProperty("NetworkMonitorService", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(innerServicePropertyInfo);

        var innerService = innerServicePropertyInfo.GetValue(cut.Instance);
        Assert.NotNull(innerService);

        var methodInfo = innerService.GetType().GetMethod("TriggerNetworkStateChanged");
        Assert.NotNull(methodInfo);

        var result = methodInfo.Invoke(service, [new NetworkMonitorState() { Downlink = 10, RTT = 100, NetworkType = "4g" }]);
        if (result is Task task)
        {
            await task;
        }

        // 测试重复注册回调
        var callback = new Func<NetworkMonitorState, Task>(state =>
        {
            return Task.CompletedTask;
        });
        await service.RegisterStateChangedCallback(cut.Instance, callback);
        await service.RegisterStateChangedCallback(cut.Instance, callback);
    }
}
