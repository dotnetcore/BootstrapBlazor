// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class DispatchServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Dispatch_Ok()
    {
        var receiveName = "";
        var category = "";
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockDispatchItem>>();
        var invoker = new Func<DispatchEntry<MockDispatchItem>, Task>(async item =>
        {
            await Task.Yield();
            category = item.Name;
            receiveName = item.Entry?.Name;
        });
        await dispatchService.SubscribeAsync(invoker, Xunit.TestContext.Current.CancellationToken);
        await dispatchService.DispatchAsync(new DispatchEntry<MockDispatchItem>()
        {
            Entry = new MockDispatchItem() { Name = "test-dispatch" },
            Name = "test-category"
        }, Xunit.TestContext.Current.CancellationToken);

        Assert.Equal("test-dispatch", receiveName);
        Assert.Equal("test-category", category);
        await dispatchService.UnSubscribeAsync(invoker, Xunit.TestContext.Current.CancellationToken);
    }

    private class MockDispatchItem
    {
        public string? Name { get; set; }
    }
}
