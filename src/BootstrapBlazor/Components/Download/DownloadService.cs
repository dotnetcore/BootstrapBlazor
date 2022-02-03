// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 文件下载服务类
/// </summary>
public class DownloadService
{
    /// <summary>
    /// 获得 回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<DownloadOption, Task> Callback)> Cache { get; set; } = new();

    /// <summary>
    /// 获得 获取地址的回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<DownloadOption, Task<string>> Callback)> CacheUrl { get; set; } = new();

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void Register(IComponent key, Func<DownloadOption, Task> callback) => Cache.Add((key, callback));

    /// <summary>
    /// 注册获取Url服务
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    internal void RegisterUrl(IComponent key, Func<DownloadOption, Task<string>> callback) => CacheUrl.Add((key, callback));

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
    /// 注销获取Url事件
    /// </summary>
    internal void UnRegisterUrl(IComponent key)
    {
        var item = CacheUrl.FirstOrDefault(i => i.Key == key);
        if (item.Key != null)
        {
            Cache.Remove(item);
        }
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task DownloadAsync(string fileName, Stream stream, string mime = "application/octet-stream")
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        await DownloadAsync(new DownloadOption() { FileName = fileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="fileContent">文件内容 byte[] 数组</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public Task DownloadAsync(string fileName, byte[] fileContent, string mime = "application/octet-stream") => DownloadAsync(new DownloadOption() { FileName = fileName, FileContent = fileContent, Mime = mime });

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task<string> CreateUrlAsync(string fileName, Stream stream, string mime = "application/octet-stream")
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        return await CreateUrlAsync(new DownloadOption() { FileName = fileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="fileContent">文件内容 byte[] 数组</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public Task<string> CreateUrlAsync(string fileName, byte[] fileContent, string mime = "application/octet-stream") => CreateUrlAsync(new DownloadOption() { FileName = fileName, FileContent = fileContent, Mime = mime });

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    public async Task DownloadAsync(DownloadOption option)
    {
        var cb = Cache.FirstOrDefault().Callback;
        if (cb != null)
        {
            await cb.Invoke(option);
        }
    }

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    public async Task<string> CreateUrlAsync(DownloadOption option)
    {
        var cb = CacheUrl.FirstOrDefault().Callback;
        return cb == null ? "" : await cb.Invoke(option);
    }
}
