// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;

namespace BootstrapBlazor.Components;

/// <summary>
/// 文件下载服务类
/// </summary>
public class DownloadService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> StreamCache { get; } = new();

    /// <summary>
    /// 获得 获取地址的回调委托缓存集合
    /// </summary>
    protected List<(IComponent Key, Func<DownloadOption, Task> Callback)> UrlCache { get; } = new();

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
    /// <param name="downloadFileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <returns></returns>
    public virtual Task DownloadFromStreamAsync(string downloadFileName, Stream stream) => DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="data">Byte[] 数组</param>
    /// <returns></returns>
    public virtual Task DownloadFromByteArrayAsync(string downloadFileName, byte[] data) => DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = new MemoryStream(data) });

    /// <summary>
    /// 下载文件夹方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="folder">文件夹路径</param>
    /// <returns></returns>
    public virtual async Task DownloadFolderAsync(string downloadFileName, string folder)
    {
        if (!Directory.Exists(folder))
        {
            throw new DirectoryNotFoundException($"Couldn't be not found {folder}");
        }

        // 打包文件
        var directoryName = folder.TrimEnd('\\', '\r', '\n');
        var destZipFile = $"{directoryName}.zip";
        ZipFile.CreateFromDirectory(folder, destZipFile);

        using var stream = new FileStream(destZipFile, FileMode.Open);
        await DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    protected virtual async Task DownloadFromStreamAsync(DownloadOption option)
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
    /// <param name="downloadFileName">文件名</param>
    /// <param name="url">文件地址</param>
    /// <returns></returns>
    public virtual Task DownloadFromUrlAsync(string downloadFileName, string url) => DownloadFromUrlAsync(new DownloadOption() { FileName = downloadFileName, Url = url });

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    protected virtual async Task DownloadFromUrlAsync(DownloadOption option)
    {
        var cb = UrlCache.LastOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }
}
