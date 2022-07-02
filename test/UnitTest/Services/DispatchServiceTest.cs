// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class DispatchServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Dispatch_Ok()
    {
        var receiveName = "";
        var category = "";
        var dispatchService = Context.Services.GetRequiredService<IDispatchService<MockDispatchItem>>();
        var invoker = new Func<DispatchEntry<MockDispatchItem>, Task>(item =>
        {
            category = item.Name;
            receiveName = item.Entry?.Name;
            return Task.CompletedTask;
        });
        dispatchService.Subscribe(invoker);
        dispatchService.Dispatch(new DispatchEntry<MockDispatchItem>()
        {
            Entry = new MockDispatchItem() { Name = "test-dispatch" },
            Name = "test-category"
        });

        Assert.Equal("test-dispatch", receiveName);
        Assert.Equal("test-category", category);
        dispatchService.UnSubscribe(invoker);
    }

    private class MockDispatchItem
    {
        public string? Name { get; set; }
    }
}
