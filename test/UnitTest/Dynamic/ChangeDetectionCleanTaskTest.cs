// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

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

        var methodInfo = type.GetMethod("Register", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(null, new object[] { table });

        // 多次调用 Rent，确保计数正确增加
        methodInfo.Invoke(null, new object[] { table });

        // 同一个 Table 多次调用 Rent，只有一个键值
        Assert.Single(v.Keys);

        methodInfo = type.GetMethod("UnRegister", BindingFlags.Public | BindingFlags.Static);
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
    public void Clean_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        // 先停止并清空 _cleanTask，确保 Run 会重新创建 PeriodicTimer，保证测试隔离
        var stopMethodInfo = type.GetMethod("Stop", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(stopMethodInfo);
        stopMethodInfo.Invoke(null, null);

        var cleanTaskFieldInfo = type.GetField("_cleanTask", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(cleanTaskFieldInfo);
        cleanTaskFieldInfo.SetValue(null, null);

        // 调用 Run 在锁内同步创建 PeriodicTimer 并开启 Clean 任务
        var runMethodInfo = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(runMethodInfo);
        runMethodInfo.Invoke(null, null);

        // 反射获得 _timer 值
        var fieldInfo = type.GetField("_timer", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(fieldInfo);

        var timer = fieldInfo.GetValue(null) as PeriodicTimer;
        Assert.NotNull(timer);

        // 调用 Stop 后 _timer 应该被释放并置空
        stopMethodInfo.Invoke(null, null);

        timer = fieldInfo.GetValue(null) as PeriodicTimer;
        Assert.Null(timer);
    }

    [Fact]
    public async Task CleanLoop_Ok()
    {
        var type = typeof(Table<>).Assembly.GetType("BootstrapBlazor.Components.ChangeDetectionCleanTask");
        Assert.NotNull(type);

        var cacheFieldInfo = type.GetField("_cache", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(cacheFieldInfo);

        var cleanMethodInfo = type.GetMethod("Clean", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(cleanMethodInfo);

        // 构造一个隶属于动态程序集 (DataTableDynamicContext.DynamicAssemblyName) 的类型，DoTask 执行后应将其移除
        var dynamicType = CreateDynamicType();
        var cache = new ConcurrentDictionary<Type, bool>();
        cache[dynamicType] = true;

        // 临时替换 _cache，避免污染 ASP.NET Core 共享缓存；测试结束后还原
        var originalCache = cacheFieldInfo.GetValue(null);
        cacheFieldInfo.SetValue(null, cache);
        try
        {
            // 使用自建的高频定时器直接驱动 Clean 循环，确定性地触发 DoTask
            using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(50));
            var task = (Task)cleanMethodInfo.Invoke(null, new object[] { timer })!;

            // 轮询等待至少一次 DoTask 执行，将动态程序集类型移除
            var removed = false;
            for (var i = 0; i < 100 && !removed; i++)
            {
                await Task.Delay(50);
                removed = !cache.ContainsKey(dynamicType);
            }
            Assert.True(removed);

            // 释放定时器后 WaitForNextTickAsync 返回 false，Clean 循环正常退出
            timer.Dispose();
            await task;
            Assert.True(task.IsCompletedSuccessfully);
        }
        finally
        {
            cacheFieldInfo.SetValue(null, originalCache);
        }
    }

    /// <summary>
    /// 构造一个隶属于动态程序集 BootstrapBlazor_DynamicAssembly 的类型
    /// 程序集名称需与 DataTableDynamicContext.DynamicAssemblyName 常量保持一致
    /// </summary>
    private static Type CreateDynamicType()
    {
        var assemblyName = new AssemblyName("BootstrapBlazor_DynamicAssembly");
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        var typeBuilder = moduleBuilder.DefineType("TestDynamicType", TypeAttributes.Public);
        return typeBuilder.CreateType();
    }

    class MockTable : ITable
    {
        public Dictionary<string, IFilterAction> Filters { get; } = [];

        public Func<Task>? OnFilterAsync { get; }

        public List<ITableColumn> Columns { get; } = [];

        public List<ITableColumn> GetVisibleColumns() => [];
    }
}
