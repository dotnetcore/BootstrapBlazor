// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">组件变更检测清理后台服务</para>
/// <para lang="en">Component change detection clean background service</para>
/// </summary>
static class ChangeDetectionCleanTask
{
#if NET9_0_OR_GREATER
    private static Lock _locker = new();
#else
    private static readonly object _locker = new();
#endif

    private static ConcurrentDictionary<Type, bool> _cache = new();
    private static PeriodicTimer? _timer;
    private static Task? _cleanTask;
    private static ConcurrentDictionary<ITable, byte> _tableCache = new();

    static ChangeDetectionCleanTask()
    {
        var type = typeof(ComponentBase).Assembly.GetType("Microsoft.AspNetCore.Components.ChangeDetection");
        if (type != null)
        {
            var fieldInfo = type.GetField("_immutableObjectTypesCache", BindingFlags.NonPublic | BindingFlags.Static);

            if (fieldInfo != null)
            {
                if (fieldInfo.GetValue(null) is ConcurrentDictionary<Type, bool> cache)
                {
                    _cache = cache;
                }
            }
        }
    }

    /// <summary>
    /// <para lang="zh">注册表格引用</para>
    /// <para lang="en">Register table reference</para>
    /// </summary>
    public static void Register(ITable table)
    {
        if (_tableCache.TryAdd(table, 0))
        {
            Run();
        }
    }

    /// <summary>
    /// <para lang="zh">注销表格引用</para>
    /// <para lang="en">Unregister table reference</para>
    /// </summary>
    public static void UnRegister(ITable table)
    {
        if (_tableCache.TryRemove(table, out _))
        {
            if (_tableCache.IsEmpty)
            {
                Stop();
            }
        }
    }

    /// <summary>
    /// <para lang="zh">运行组件变更检测清理方法</para>
    /// <para lang="en">Run component change detection clean method</para>
    /// </summary>
    public static void Run()
    {
        lock (_locker)
        {
            if (_cleanTask is { IsCompleted: false })
            {
                return;
            }

            // 组件变更检测清理方法执行间隔，默认 5000 毫秒，最小 500 毫秒
            var interval = 5000;
            if (CacheManager.Options != null)
            {
                interval = Math.Max(500, CacheManager.Options.ChangeDetectionTaskInterval);
            }

            // 在锁内创建 PeriodicTimer，与 Stop 互斥，确保其创建与销毁不会发生竞态
            var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(interval));
            _timer = timer;
            _cleanTask = Task.Run(() => Clean(timer));
        }
    }

    private static void Stop()
    {
        lock (_locker)
        {
            // 释放定时器后 WaitForNextTickAsync 返回 false，Clean 循环正常退出
            _timer?.Dispose();
            _timer = null;
        }
    }

    private static async Task Clean(PeriodicTimer timer)
    {
        // 每隔 interval 毫秒执行一次清理方法；调用 Stop 释放定时器后 WaitForNextTickAsync 返回 false 退出循环
        while (await timer.WaitForNextTickAsync())
        {
            DoTask();
        }
    }

    private static void DoTask()
    {
        var keys = _cache.Keys.Where(i => i.Assembly.GetName().Name == DataTableDynamicContext.DynamicAssemblyName).ToList();

        foreach (var key in keys)
        {
            _cache.TryRemove(key, out _);
        }
    }
}
