// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax服务类
/// </summary>
public class AjaxService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<AjaxOption, Task<string?>> Callback)> Cache { get; } = new();

    /// <summary>
    /// 获得 跳转其他页面的回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<string, Task> Callback)> GotoCache { get; } = new();

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(IComponent key, Func<AjaxOption, Task<string?>> callback) => Cache.Add((key, callback));

    /// <summary>
    /// 注销事件
    /// </summary>
    internal void UnRegister(IComponent key)
    {
        var item = Cache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            Cache.Remove(item);
        }
    }

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void RegisterGoto(IComponent key, Func<string, Task> callback) => GotoCache.Add((key, callback));

    /// <summary>
    /// 注销事件
    /// </summary>
    internal void UnRegisterGoto(IComponent key)
    {
        var item = GotoCache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            GotoCache.Remove(item);
        }
    }

    /// <summary>
    /// 调用Ajax方法发送请求
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public async Task<string?> GetMessage(AjaxOption option)
    {
        var cb = Cache.FirstOrDefault().Callback;
        return cb == null ? null : await cb.Invoke(option);
    }

    /// <summary>
    /// 调用 Goto 方法跳转其他页面
    /// </summary>
    /// <param name="url"></param>
    public async Task Goto(string url)
    {
        var cb = GotoCache.FirstOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(url);
        }
    }
}
