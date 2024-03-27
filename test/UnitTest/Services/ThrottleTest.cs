// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class ThrottleTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Throttle_Ok()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate("test", 100);

        var count = 0;
        dispatcher.Throttle(() => count++);
        Assert.Equal(1, count);

        dispatcher.Throttle(() => count++);
        Assert.Equal(1, count);

        await Task.Delay(100);
        dispatcher.Throttle(() => count++);
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task ThrottleAsync_Ok()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate("test-async", new ThrottleOptions() { Interval = 100 });

        var count = 0;
        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(1, count);

        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(1, count);

        await Task.Delay(100);
        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(2, count);

        Task Count()
        {
            count++;
            return Task.CompletedTask;
        }
    }
}
