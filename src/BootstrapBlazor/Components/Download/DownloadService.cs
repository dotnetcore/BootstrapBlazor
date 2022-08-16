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
    private List<(IComponent Key, Func<DownloadOption, Task> Callback)> Cache { get; } = new();

    /// <summary>
    /// 获得 获取地址的回调委托缓存集合
    /// </summary>
    private List<(IComponent Key, Func<DownloadOption, Task<string>> Callback)> CacheUrl { get; } = new();

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
    /// <param name="downloadFileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task DownloadAsync(string downloadFileName, Stream stream, string mime = "application/octet-stream")
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        await DownloadAsync(new DownloadOption() { FileName = downloadFileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="physicalFilePath">文件物理路径</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task DownloadAsync(string downloadFileName, string physicalFilePath, string mime = "application/octet-stream")
    {
        if (!File.Exists(physicalFilePath))
        {
            throw new FileNotFoundException($"Couldn't be not found {physicalFilePath}", physicalFilePath);
        }

        using var stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        await DownloadAsync(new DownloadOption() { FileName = downloadFileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="fileContent">文件内容 byte[] 数组</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public Task DownloadAsync(string downloadFileName, byte[] fileContent, string mime = "application/octet-stream") => DownloadAsync(new DownloadOption() { FileName = downloadFileName, FileContent = fileContent, Mime = mime });

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task<string> CreateUrlAsync(string downloadFileName, Stream stream, string mime = "application/octet-stream")
    {
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        return await CreateUrlAsync(new DownloadOption() { FileName = downloadFileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="fileContent">文件内容 byte[] 数组</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public Task<string> CreateUrlAsync(string downloadFileName, byte[] fileContent, string mime = "application/octet-stream") => CreateUrlAsync(new DownloadOption() { FileName = downloadFileName, FileContent = fileContent, Mime = mime });

    /// <summary>
    /// 下载文件夹方法
    /// </summary>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="folder">文件夹路径</param>
    /// <param name="mime"></param>
    /// <returns></returns>
    public async Task DownloadFolderAsync(string downloadFileName, string folder, string mime = "application/octet-stream")
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
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);
        await DownloadAsync(new DownloadOption() { FileName = downloadFileName, FileContent = bytes, Mime = mime });
    }

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="option">文件下载选项</param>
    public async Task DownloadAsync(DownloadOption option)
    {
        var cb = Cache.LastOrDefault().Callback;
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
        var ret = "";
        var cb = CacheUrl.LastOrDefault().Callback;
        if (cb != null)
        {
            ret = await cb.Invoke(option);
        }
        return ret;
    }
}
