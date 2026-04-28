// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;

namespace UnitTest.Dynamic;

public class ChangeDetectionCleanTaskTest
{
    [Fact]
    public void Release_Ok()
    {
        var table = new MockTable();
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var fieldInfo = type.GetField("_tableCache", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(fieldInfo);
        var v = Assert.IsType<ConcurrentDictionary<ITable, byte>>(fieldInfo.GetValue(null));

        var methodInfo = type.GetMethod("Rent", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(null, new object[] { table });

        // 多次调用 Rent，确保计数正确增加
        methodInfo.Invoke(null, new object[] { table });

        // 同一个 Table 多次调用 Rent，只有一个键值
        Assert.Single(v.Keys);

        methodInfo = type.GetMethod("Release", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);

        methodInfo.Invoke(null, new object[] { table });
        Assert.Empty(v.Keys);

        // 继续调用 Release，确保不会出现负数
        methodInfo.Invoke(null, new object[] { table });
        Assert.Empty(v.Keys);
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

    class MockTable : ITable
    {
        public Dictionary<string, IFilterAction> Filters { get; } = [];

        public Func<Task>? OnFilterAsync { get; }

        public List<ITableColumn> Columns { get; } = [];

        public IEnumerable<ITableColumn> GetVisibleColumns() => [];
    }
}
