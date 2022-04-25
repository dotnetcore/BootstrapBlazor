// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

public class BootstrapServiceBaseTest
{
    [Fact]
    public async Task Invoke_Ok()
    {
        var called = false;
        var service = new MockBootstrapService<DialogOption>();
        await service.Test1(new DialogOption(), op =>
        {
            called = true;
            return Task.CompletedTask;
        });
        Assert.True(called);

        called = false;
        await service.Test2(new DialogOption());
        Assert.True(called);
    }

    private class MockBootstrapService<TOption> : BootstrapServiceBase<TOption>
    {
        private Alert Alert { get; } = new Alert();

        public async Task Test1(TOption option, Func<TOption, Task> callback)
        {
            Cache.Add((Alert, callback));
            await Invoke(option);
        }

        public async Task Test2(TOption option)
        {
            await Invoke(option, Alert);
        }
    }
}
