// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="DownloadService"/> 扩展类
/// </summary>
public static class DownloadServiceExtensions
{
    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="stream">文件流</param>
    /// <returns></returns>
    public static Task DownloadFromStreamAsync(this DownloadService download, string downloadFileName, Stream stream) => download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });

    /// <summary>
    /// 下载文件方法
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="data">Byte[] 数组</param>
    /// <returns></returns>
    public static Task DownloadFromByteArrayAsync(this DownloadService download, string downloadFileName, byte[] data) => download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = new MemoryStream(data) });

    /// <summary>
    /// 下载文件夹方法
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="folder">文件夹路径</param>
    /// <returns></returns>
    public static async Task DownloadFolderAsync(this DownloadService download, string downloadFileName, string folder)
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
        await download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });
    }

    /// <summary>
    /// 获取文件连接方法
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName">文件名</param>
    /// <param name="url">文件地址</param>
    /// <returns></returns>
    public static Task DownloadFromUrlAsync(this DownloadService download, string downloadFileName, string url) => download.DownloadFromUrlAsync(new DownloadOption() { FileName = downloadFileName, Url = url });

    /// <summary>
    /// 下载文件扩展方法
    /// </summary>
    /// <param name="download"></param>
    /// <param name="fileName"></param>
    /// <param name="physicalFilePath"></param>
    /// <returns></returns>
    public static async Task DownloadFromFileAsync(this DownloadService download, string fileName, string physicalFilePath)
    {
        if (File.Exists(physicalFilePath))
        {
            using var stream = File.OpenRead(physicalFilePath);
            await download.DownloadFromStreamAsync(fileName, stream);
        }
    }
}
