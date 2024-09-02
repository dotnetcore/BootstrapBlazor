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
    protected List<(ComponentBase Key, Func<WinBoxOption?, string, Task> Callback)> Cache { get; } = [];

    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="method"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    protected async Task Invoke(WinBoxOption? option, string method, ComponentBase? component = null)
    {
        var (Key, Callback) = component != null
            ? Cache.FirstOrDefault(k => k.Key == component)
            : Cache.FirstOrDefault();
        if (Callback == null)
        {
            throw new InvalidOperationException($"{GetType().Name} not registered. Please add <WinBox></WinBox>");
        }
        await Callback.Invoke(option, method);
    }

    /// <summary>
    /// 显示 WinBox 方法
    /// </summary>
    /// <param name="option">弹窗配置信息实体类</param>
    public Task Show(WinBoxOption option) => Invoke(option, "show");

    /// <summary>
    /// 关闭 WinBox 方法
    /// </summary>
    /// <param name="option">弹窗配置信息实体类</param>
    public Task Close(WinBoxOption option) => Invoke(option, "close");

    /// <summary>
    /// 最小化方法
    /// </summary>
    /// <returns></returns>
    public Task Minimize(WinBoxOption option) => Invoke(option, "minimize");

    /// <summary>
    /// 最大化方法
    /// </summary>
    /// <returns></returns>
    public Task Maximize(WinBoxOption option) => Invoke(option, "maximize");

    /// <summary>
    /// 最大化方法
    /// </summary>
    /// <returns></returns>
    public Task Restore(WinBoxOption option) => Invoke(option, "restore");

    /// <summary>
    /// 设置图标方法
    /// </summary>
    /// <returns></returns>
    public Task SetIcon(WinBoxOption option) => Invoke(option, "setIcon");

    /// <summary>
    /// 设置标题方法
    /// </summary>
    /// <returns></returns>
    public Task SetTitle(WinBoxOption option) => Invoke(option, "setTitle");

    /// <summary>
    /// 窗口排列方法
    /// </summary>
    /// <returns></returns>
    public Task Stack() => Invoke(null, "stack");

    /// <summary>
    /// 注册弹窗事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(ComponentBase key, Func<WinBoxOption?, string, Task> callback)
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
