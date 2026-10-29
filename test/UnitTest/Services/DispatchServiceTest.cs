// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class DispatchServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task DispatchAsync_Ok()
    {
        var token = TestContext.Current.CancellationToken;
        var receiveName = "";
        var category = "";
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockDispatchItem>>();
        var invoker = new Func<DispatchEntry<MockDispatchItem>, Task>(item =>
        {
            category = item.Name;
            receiveName = item.Entry?.Name;
            return Task.CompletedTask;
        });
        await dispatchService.SubscribeAsync(invoker, token);
        await dispatchService.DispatchAsync(new DispatchEntry<MockDispatchItem>()
        {
            Entry = new MockDispatchItem() { Name = "test-dispatch" },
            Name = "test-category"
        }, token);

        Assert.Equal("test-dispatch", receiveName);
        Assert.Equal("test-category", category);

        await dispatchService.UnSubscribeAsync(invoker, token);
        receiveName = "";
        await dispatchService.DispatchAsync(new DispatchEntry<MockDispatchItem>()
        {
            Entry = new MockDispatchItem() { Name = "test-dispatch" }
        }, token);
        Assert.Equal("", receiveName);
    }

    [Fact]
    public async Task DispatchAsync_SubscriberException()
    {
        var token = TestContext.Current.CancellationToken;
        var invoked = false;
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockExceptionItem>>();
        var failed = new Func<DispatchEntry<MockExceptionItem>, Task>(_ => throw new InvalidOperationException("dispatch-error"));
        var invoker = new Func<DispatchEntry<MockExceptionItem>, Task>(_ =>
        {
            invoked = true;
            return Task.CompletedTask;
        });
        await dispatchService.SubscribeAsync(failed, token);
        await dispatchService.SubscribeAsync(invoker, token);

        // subscriber exception is logged and remaining subscribers still run
        await dispatchService.DispatchAsync(new DispatchEntry<MockExceptionItem>(), token);
        Assert.True(invoked);

        await dispatchService.UnSubscribeAsync(failed, token);
        await dispatchService.UnSubscribeAsync(invoker, token);
    }

    [Fact]
    public async Task DispatchAsync_Cancel()
    {
        var token = TestContext.Current.CancellationToken;
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockCancelItem>>();

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => dispatchService.DispatchAsync(new DispatchEntry<MockCancelItem>(), cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => dispatchService.SubscribeAsync(_ => Task.CompletedTask, cts.Token));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => dispatchService.UnSubscribeAsync(_ => Task.CompletedTask, cts.Token));

        // cancel during dispatch loop
        using var loopCts = new CancellationTokenSource();
        var invoked = false;
        var cancel = new Func<DispatchEntry<MockCancelItem>, Task>(_ => loopCts.CancelAsync());
        var invoker = new Func<DispatchEntry<MockCancelItem>, Task>(_ =>
        {
            invoked = true;
            return Task.CompletedTask;
        });
        await dispatchService.SubscribeAsync(cancel, token);
        await dispatchService.SubscribeAsync(invoker, token);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => dispatchService.DispatchAsync(new DispatchEntry<MockCancelItem>(), loopCts.Token));
        Assert.False(invoked);
        await dispatchService.UnSubscribeAsync(cancel, token);
        await dispatchService.UnSubscribeAsync(invoker, token);

        // subscriber exception after cancellation is not swallowed
        using var throwCts = new CancellationTokenSource();
        var failed = new Func<DispatchEntry<MockCancelItem>, Task>(async _ =>
        {
            await throwCts.CancelAsync();
            throw new InvalidOperationException("dispatch-cancel");
        });
        await dispatchService.SubscribeAsync(failed, token);
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => dispatchService.DispatchAsync(new DispatchEntry<MockCancelItem>(), throwCts.Token));
        Assert.Equal("dispatch-cancel", ex.Message);
        await dispatchService.UnSubscribeAsync(failed, token);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task DispatchAsync_UnSubscribeDuringDispatch(bool resubscribe)
    {
        var token = TestContext.Current.CancellationToken;
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockDispatchItem>>();
        var pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var first = new Func<DispatchEntry<MockDispatchItem>, Task>(_ => pending.Task);
        var invoked = 0;
        var next = new Func<DispatchEntry<MockDispatchItem>, Task>(_ =>
        {
            invoked++;
            return Task.CompletedTask;
        });
        await dispatchService.SubscribeAsync(first, token);
        await dispatchService.SubscribeAsync(next, token);

        var dispatch = dispatchService.DispatchAsync(new DispatchEntry<MockDispatchItem>(), token);
        try
        {
            await dispatchService.UnSubscribeAsync(next, token);
            if (resubscribe)
            {
                await dispatchService.SubscribeAsync(next, token);
            }
        }
        finally
        {
            pending.TrySetResult();
        }

        await dispatch.WaitAsync(TimeSpan.FromSeconds(5), token);
        Assert.Equal(0, invoked);

        await dispatchService.UnSubscribeAsync(first, token);
        await dispatchService.DispatchAsync(new DispatchEntry<MockDispatchItem>(), token);
        Assert.Equal(resubscribe ? 1 : 0, invoked);
        await dispatchService.UnSubscribeAsync(next, token);
    }

    [Fact]
    public async Task DispatchAsync_CancelPendingCallback()
    {
        var token = TestContext.Current.CancellationToken;
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockCancelItem>>();
        var pending = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        await dispatchService.SubscribeAsync(_ => pending.Task, token);

        using var cts = new CancellationTokenSource();
        var dispatch = dispatchService.DispatchAsync(new DispatchEntry<MockCancelItem>(), cts.Token);
        try
        {
            await cts.CancelAsync();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => dispatch.WaitAsync(TimeSpan.FromSeconds(5), token));
            Assert.False(pending.Task.IsCompleted);
        }
        finally
        {
            pending.TrySetResult();
        }
    }

    [Fact]
    public void LegacyMethods_Ok()
    {
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockDispatchItem>>();
        var serviceType = typeof(IDispatchService<MockDispatchItem>);
        var invoked = 0;
        var callback = new Func<DispatchEntry<MockDispatchItem>, Task>(_ =>
        {
            invoked++;
            return Task.CompletedTask;
        });
        var entry = new DispatchEntry<MockDispatchItem>();

        serviceType.GetMethod("Subscribe")!.Invoke(dispatchService, [callback]);
        serviceType.GetMethod("Dispatch")!.Invoke(dispatchService, [entry]);
        Assert.Equal(1, invoked);

        serviceType.GetMethod("UnSubscribe")!.Invoke(dispatchService, [callback]);
        serviceType.GetMethod("Dispatch")!.Invoke(dispatchService, [entry]);
        Assert.Equal(1, invoked);
    }

    private class MockDispatchItem
    {
        public string? Name { get; set; }
    }

    private class MockExceptionItem;

    private class MockCancelItem;
}
