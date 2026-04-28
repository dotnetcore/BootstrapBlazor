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
    private static readonly ConcurrentDictionary<ITable, byte> _tables = new();

#if NET9_0_OR_GREATER
    private static Lock _locker = new();
#else
    private static readonly object _locker = new();
#endif

    private static ConcurrentDictionary<Type, bool> _cache = new();
    private static CancellationTokenSource? _cancellationTokenSource;
    private static Task? _cleanTask;

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
    /// <para lang="zh">添加表格实例</para>
    /// <para lang="en">Add table instance</para>
    /// </summary>
    /// <param name="table">表格实例</param>
    public static void Add(ITable table)
    {
        _tables.TryAdd(table, 0);

        Run();
    }

    /// <summary>
    /// <para lang="zh">移除表格实例</para>
    /// <para lang="en">Remove table instance</para>
    /// </summary>
    /// <param name="table">表格实例</param>
    public static void Remove(ITable table)
    {
        _tables.TryRemove(table, out _);

        if (_tables.IsEmpty)
        {
            Stop();
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

            _cleanTask = Task.Run(Clean);
        }
    }

    private static void Stop()
    {
        lock (_locker)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private static async Task Clean()
    {
        _cancellationTokenSource ??= new();
        while (_cancellationTokenSource is { IsCancellationRequested: false })
        {
            try
            {
                // 每隔 5 秒钟执行一次清理方法
                await Task.Delay(5000, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {

            }
            finally
            {
                DoTask();
            }
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
