// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Performance;

public class TaskTest
{
    [Fact]
    public async Task Cancel_Ok()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();
        await Assert.ThrowsAnyAsync<TaskCanceledException>(() => Task.Delay(10, cts.Token));
    }
}
