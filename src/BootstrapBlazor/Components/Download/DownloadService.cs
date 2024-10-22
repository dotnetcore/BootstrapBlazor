// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 文件下载服务类
/// </summary>
public class DownloadService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> StreamCache { get; } = [];

    /// <summary>
    /// 获得 获取地址的回调委托缓存集合
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> UrlCache { get; } = [];

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    protected internal virtual void RegisterStream(IComponent key, Func<DownloadOption, Task> callback) => StreamCache.Add((key, callback));

    /// <summary>
    /// 注册获取Url服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    protected internal virtual void RegisterUrl(IComponent key, Func<DownloadOption, Task> callback) => UrlCache.Add((key, callback));

    /// <summary>
    /// 注销事件
    /// </summary>
    protected internal virtual void UnRegisterStream(IComponent key)
    {
        var item = StreamCache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            StreamCache.Remove(item);
        }
    }

    /// <summary>
    /// 注销获取Url事件
    /// </summary>
    protected internal virtual void UnRegisterUrl(IComponent key)
    {
        var item = UrlCache.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            UrlCache.Remove(item);
        }
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    public virtual async Task DownloadFromStreamAsync(DownloadOption option)
    {
        var cb = StreamCache.LastOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    public virtual async Task DownloadFromUrlAsync(DownloadOption option)
    {
        var cb = UrlCache.LastOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }
}
