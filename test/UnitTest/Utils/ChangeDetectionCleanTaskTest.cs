// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace UnitTest.Utils;

public class ChangeDetectionCleanTaskTest
{
    [Fact]
    public void Release_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var fieldInfo = type.GetField("_tableCount", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(fieldInfo);
        var v = Assert.IsType<long>(fieldInfo.GetValue(null));

        var methodInfo = type.GetMethod("Rent", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(null, null);

        var v1 = Assert.IsType<long>(fieldInfo.GetValue(null));

        Assert.Equal(v + 1, v1);

        // 多次调用 Rent，确保计数正确增加
        methodInfo.Invoke(null, null);
        v1 = Assert.IsType<long>(fieldInfo.GetValue(null));
        Assert.Equal(v + 2, v1);

        methodInfo = type.GetMethod("Release", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        methodInfo.Invoke(null, null);
        v1 = Assert.IsType<long>(fieldInfo.GetValue(null));
        Assert.Equal(v + 1, v1);

        // 继续调用 Release，确保不会出现负数
        methodInfo.Invoke(null, null);
        v1 = Assert.IsType<long>(fieldInfo.GetValue(null));
        Assert.Equal(v, v1);

        fieldInfo.SetValue(null, 0L);

        // 继续调用 Release，确保不会出现负数
        methodInfo.Invoke(null, null);
        v1 = Assert.IsType<long>(fieldInfo.GetValue(null));
        Assert.Equal(0L, v1);
    }

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

        // 反射获得 Stop 方法
        methodInfo = type.GetMethod("Stop", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        methodInfo.Invoke(null, null);

        // _cancellationTokenSource 应该被取消
        cancellationTokenSource = fieldInfo.GetValue(null) as CancellationTokenSource;
        Assert.Null(cancellationTokenSource);
    }
}
