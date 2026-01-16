// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">文件下载服务类</para>
///  <para lang="en">Download Service Class</para>
/// </summary>
public class DownloadService
{
    /// <summary>
    ///  <para lang="zh">获得 回调委托缓存集合</para>
    ///  <para lang="en">Get Callback Delegate Cache Collection</para>
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> StreamCache { get; } = [];

    /// <summary>
    ///  <para lang="zh">获得 获取地址的回调委托缓存集合</para>
    ///  <para lang="en">Get Url Callback Delegate Cache Collection</para>
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> UrlCache { get; } = [];

    /// <summary>
    ///  <para lang="zh">注册服务</para>
    ///  <para lang="en">Register Service</para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    protected internal virtual void RegisterStream(IComponent key, Func<DownloadOption, Task> callback) => StreamCache.Add((key, callback));

    /// <summary>
    ///  <para lang="zh">注册获取Url服务</para>
    ///  <para lang="en">Register Url Service</para>
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    protected internal virtual void RegisterUrl(IComponent key, Func<DownloadOption, Task> callback) => UrlCache.Add((key, callback));

    /// <summary>
    ///  <para lang="zh">注销事件</para>
    ///  <para lang="en">Unregister Event</para>
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
    ///  <para lang="zh">注销获取Url事件</para>
    ///  <para lang="en">Unregister Url Event</para>
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
    ///  <para lang="zh">下载文件方法</para>
    ///  <para lang="en">Download File Method</para>
    /// </summary>
    /// <param name="option"><para lang="zh">文件下载选项</para><para lang="en">文件下载选项</para></param>
    public virtual async Task DownloadFromStreamAsync(DownloadOption option)
    {
        var cb = StreamCache.LastOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }

    /// <summary>
    ///  <para lang="zh">获取文件连接方法</para>
    ///  <para lang="en">Get File Url Method</para>
    /// </summary>
    /// <param name="option"><para lang="zh">文件下载选项</para><para lang="en">文件下载选项</para></param>
    public virtual async Task DownloadFromUrlAsync(DownloadOption option)
    {
        var cb = UrlCache.LastOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }
}
