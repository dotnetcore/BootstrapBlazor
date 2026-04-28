// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh-cn">组件变更检测清理后台服务</para>
/// <para lang="en">Component change detection clean background service</para>
/// </summary>
static class ChangeDetectionCleanTask
{
    private static ConcurrentDictionary<Type, bool> _cache = new ConcurrentDictionary<Type, bool>();
    private static CancellationTokenSource? _cancellationTokenSource;

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
    /// <para lang="zh-cn">运行组件变更检测清理方法</para>
    /// <para lang="en">Run component change detection clean method</para>
    /// </summary>
    public static void Run()
    {
        // 运行组件变更检测清理方法
        DoTask();
    }

    private static void DoTask()
    {
        if (_cancellationTokenSource != null)
        {
            return;
        }

        Task.Run(Clean);
    }

    private static async Task Clean()
    {
        _cancellationTokenSource ??= new();
        while (_cancellationTokenSource is { IsCancellationRequested: false })
        {
            try
            {
                await Task.Delay(5000, _cancellationTokenSource.Token);

                RemoveCache();
            }
            catch
            {

            }
        }
    }

    private static void RemoveCache()
    {
        var keys = _cache.Keys.Where(i => i.Assembly.GetName().Name == "BootstrapBlazor_DynamicAssembly").ToList();

        foreach (var key in keys)
        {
            _cache.TryRemove(key, out _);
        }
    }
}
