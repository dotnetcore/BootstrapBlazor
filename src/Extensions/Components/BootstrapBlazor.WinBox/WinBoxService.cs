// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// WinBox 弹窗服务
/// </summary>
public class WinBoxService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    protected List<(ComponentBase Key, Func<WinBoxOption, Task> Callback)> Cache { get; } = [];

    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    protected async Task Invoke(WinBoxOption option, ComponentBase? component = null)
    {
        var (Key, Callback) = component != null
            ? Cache.FirstOrDefault(k => k.Key == component)
            : Cache.FirstOrDefault();
        if (Callback == null)
        {
            throw new InvalidOperationException($"{GetType().Name} not registered. Please add <WinBox></WinBox>");
        }
        await Callback.Invoke(option);
    }

    /// <summary>
    /// 显示 WinBox 方法
    /// </summary>
    /// <param name="option">弹窗配置信息实体类</param>
    /// <returns></returns>
    public Task Show(WinBoxOption option) => Invoke(option);

    /// <summary>
    /// 注册弹窗事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(ComponentBase key, Func<WinBoxOption, Task> callback)
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
