// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
