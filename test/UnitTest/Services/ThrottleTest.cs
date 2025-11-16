// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class ThrottleTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Throttle_Ok()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate("test", 200);

        var count = 0;
        dispatcher.Throttle(() => count++);
        Assert.Equal(1, count);

        dispatcher.Throttle(() => count++);
        Assert.Equal(1, count);

        await Task.Delay(250);
        dispatcher.Throttle(() => count++);
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task ThrottleAsync_Ok()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate("test-async", new ThrottleOptions() { Interval = TimeSpan.FromMilliseconds(200) });

        var count = 0;
        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(1, count);

        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(1, count);

        await Task.Delay(250);
        await dispatcher.ThrottleAsync(Count);
        Assert.Equal(2, count);

        Task Count()
        {
            count++;
            return Task.CompletedTask;
        }
    }

    [Theory]
    [InlineData(false, 2)]
    [InlineData(true, 1)]
    public async Task DelayAfterExecution_Ok(bool delayAfterExecution, int expected)
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate($"DelayAfterExecution-{expected}", new ThrottleOptions() { Interval = TimeSpan.FromMilliseconds(200), DelayAfterExecution = delayAfterExecution });

        // 开始执行时计时 100ms 后可再次执行
        var count = 0;
        await dispatcher.ThrottleAsync(async () =>
        {
            await Task.Delay(100);
            count++;
        });
        await Task.Delay(150);

        // 如果 DelayAfterExecution = false 再次执行时计数 已经超出 200ms
        // 如果 DelayAfterExecution = true 再次执行时不会计数 invokeTime 重置 未到达 200ms
        dispatcher.Throttle(() =>
        {
            count++;
        });
    }

    [Fact]
    public async Task ResetIntervalOnException_Ok()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        var dispatcher = factory.GetOrCreate("Error", new ThrottleOptions() { ResetIntervalOnException = true });

        var count = 0;
        await dispatcher.ThrottleAsync(() =>
        {
            count++;
            throw new Exception();
        });
        Assert.Equal(1, count);

        dispatcher.Throttle(() => throw new InvalidOperationException());

        // 发生错误后可以立即执行下一次任务，不限流
        dispatcher.Throttle(() =>
        {
            count++;
        });
        Assert.Equal(2, count);
    }

    [Fact]
    public void Clear()
    {
        var factory = Context.Services.GetRequiredService<IThrottleDispatcherFactory>();
        _ = factory.GetOrCreate("Clear");
        factory.Clear();
        factory.Clear("Clear");
    }

    [Fact]
    public void ShouldWait_Ok()
    {
        var dispatch = new ThrottleDispatcher(new ThrottleOptions());
        var count = 0;
        dispatch.Throttle(() => count++);
        Assert.Equal(1, count);
        dispatch.Throttle(() => count++);
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task MultipleThread_ThrottleAsync_Ok()
    {
        var count = 0;
        var dispatch = new ThrottleDispatcher(new ThrottleOptions()
        {
            Interval = TimeSpan.FromMilliseconds(100),
            DelayAfterExecution = true
        });
        var tasks = Enumerable.Range(1, 2).Select(i => dispatch.ThrottleAsync(() =>
        {
            count++;
            return Task.CompletedTask;
        })).ToList();
        tasks.Add(dispatch.ThrottleAsync(async () =>
        {
            await Task.Delay(120);
            count++;
        }));
        await Task.WhenAll(tasks);
        Assert.Equal(1, count);
    }
}
