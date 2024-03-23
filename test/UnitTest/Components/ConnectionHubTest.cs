// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class ConnectionHubTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task AddConnection_Ok()
    {
        var cut = Context.RenderComponent<ConnectionHub>();
        await cut.InvokeAsync(() =>
        {
            cut.Instance.Callback("test-key");
        });
        await Task.Delay(1000);
        await cut.InvokeAsync(() =>
        {
            cut.Instance.Callback("test-key");
        });
        await Task.Delay(5000);

        var service = Context.Services.GetRequiredService<IConnectionService>();
        Assert.True(service.TryGetValue("test-key", out var result));
        Assert.Equal(0, service.Count);
    }
}
