﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapServiceBase 基类
/// </summary>
public abstract class BootstrapServiceBase<TOption>
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    protected List<(ComponentBase Key, Func<TOption, Task> Callback)> Cache { get; } = [];

    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    protected async Task Invoke(TOption option, ComponentBase? component = null)
    {
        var (Key, Callback) = component != null
            ? Cache.FirstOrDefault(k => k.Key == component)
            : Cache.FirstOrDefault();
        if (Callback == null)
        {
#if NET8_0_OR_GREATER
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-webapp step 7 for BootstrapBlazorRoot");
#else
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-server step 7 for BootstrapBlazorRoot");
#endif
        }
        await Callback.Invoke(option);
    }

    /// <summary>
    /// 注册弹窗事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(ComponentBase key, Func<TOption, Task> callback)
    {
        Cache.Add((key, callback));
    }

    /// <summary>
    /// 注销弹窗事件
    /// </summary>
    internal void UnRegister(ComponentBase key)
    {
        var item = Cache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null) Cache.Remove(item);
    }
}
