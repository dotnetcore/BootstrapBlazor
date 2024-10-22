﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax服务类
/// </summary>
public class AjaxService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<AjaxOption, Task<JsonDocument?>> Callback)> Cache { get; } = [];

    /// <summary>
    /// 获得 跳转其他页面的回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<string, Task> Callback)> GotoCache { get; } = [];

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(IComponent key, Func<AjaxOption, Task<JsonDocument?>> callback) => Cache.Add((key, callback));

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
    public async Task<JsonDocument?> InvokeAsync(AjaxOption option)
    {
        var cb = Cache.FirstOrDefault().Callback;
        return cb == null ? default : await cb.Invoke(option);
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
