// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace UnitTest.Utils;

public class ChangeDetectionCleanTaskTest
{
    [Fact]
    public void Stop_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("Stop", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        methodInfo.Invoke(null, null);
    }

    [Fact]
    public async Task Clean_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("Clean", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        // 开启 Clean 任务
        methodInfo.Invoke(null, null);

        // 反射获得 _cancellationTokenSource 值
        var fieldInfo = type.GetField("_cancellationTokenSource", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(fieldInfo);

        var cancellationTokenSource = fieldInfo.GetValue(null) as CancellationTokenSource;
        Assert.NotNull(cancellationTokenSource);

        cancellationTokenSource.Cancel();
        // 等待 Clean 任务完成
        await Task.Delay(100);
    }
}
