// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapServiceBase 基类</para>
/// <para lang="en">BootstrapServiceBase Base Class</para>
/// </summary>
public abstract class BootstrapServiceBase<TOption>
{
    /// <summary>
    /// <para lang="zh">获得 回调委托缓存集合</para>
    /// <para lang="en">Get Callback Delegate Cache Collection</para>
    /// </summary>
    protected List<(ComponentBase Key, Func<TOption, Task> Callback)> Cache { get; } = [];

    /// <summary>
    /// <para lang="zh">异步回调方法</para>
    /// <para lang="en">Async Callback Method</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="component"></param>
    protected async Task Invoke(TOption option, ComponentBase? component = null)
    {
        var (_, callback) = component != null
            ? Cache.FirstOrDefault(k => k.Key == component)
            : Cache.FirstOrDefault();
        if (callback == null)
        {
#if NET8_0_OR_GREATER
            var message = $"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-webapp step 7 for BootstrapBlazorRoot; 未找到 BootstrapBlazorRoot 组件，无法完成当前操作，请根据 https://www.blazor.zone/install-webapp 第七步骤指引完成操作";
#else
            var message = $"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-server step 7 for BootstrapBlazorRoot; 未找到 BootstrapBlazorRoot 组件，无法完成当前操作，请根据 https://www.blazor.zone/install-server 第七步骤指引完成操作";
#endif
            throw new InvalidOperationException(message);
        }
        await callback(option);
    }

    /// <summary>
    /// <para lang="zh">注册弹窗事件</para>
    /// <para lang="en">Register Dialog Event</para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(ComponentBase key, Func<TOption, Task> callback)
    {
        Cache.Add((key, callback));
    }

    /// <summary>
    /// <para lang="zh">注销弹窗事件</para>
    /// <para lang="en">Unregister Dialog Event</para>
    /// </summary>
    internal void UnRegister(ComponentBase key)
    {
        var item = Cache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null) Cache.Remove(item);
    }
}
