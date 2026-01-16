// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="DownloadService"/> 扩展类</para>
/// <para lang="en"><see cref="DownloadService"/> Extensions</para>
/// </summary>
public static class DownloadServiceExtensions
{
    /// <summary>
    /// <para lang="zh">下载文件方法</para>
    /// <para lang="en">Download file</para>
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName"><para lang="zh">文件名</para><para lang="en">File name</para></param>
    /// <param name="stream"><para lang="zh">文件流</para><para lang="en">File stream</para></param>
    /// <returns></returns>
    public static Task DownloadFromStreamAsync(this DownloadService download, string downloadFileName, Stream stream) => download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });

    /// <summary>
    /// <para lang="zh">下载文件方法</para>
    /// <para lang="en">Download file</para>
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName"><para lang="zh">文件名</para><para lang="en">File name</para></param>
    /// <param name="data"><para lang="zh">Byte[] 数组</para><para lang="en">Byte array</para></param>
    /// <returns></returns>
    public static Task DownloadFromByteArrayAsync(this DownloadService download, string downloadFileName, byte[] data) => download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = new MemoryStream(data) });

    /// <summary>
    /// <para lang="zh">下载文件夹方法</para>
    /// <para lang="en">Download folder</para>
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName"><para lang="zh">文件名</para><para lang="en">File name</para></param>
    /// <param name="folder"><para lang="zh">文件夹路径</para><para lang="en">Folder path</para></param>
    /// <returns></returns>
    public static async Task DownloadFolderAsync(this DownloadService download, string downloadFileName, string folder)
    {
        if (!Directory.Exists(folder))
        {
            throw new DirectoryNotFoundException($"Couldn't be not found {folder}");
        }

        // <para lang="zh">打包文件</para>
        // <para lang="en">Zip folder</para>
        var directoryName = folder.TrimEnd('\\', '\r', '\n');
        var destZipFile = $"{directoryName}.zip";
        ZipFile.CreateFromDirectory(folder, destZipFile);

        await using var stream = new FileStream(destZipFile, FileMode.Open);
        await download.DownloadFromStreamAsync(new DownloadOption() { FileName = downloadFileName, FileStream = stream });
    }

    /// <summary>
    /// <para lang="zh">获取文件连接方法</para>
    /// <para lang="en">Get file url</para>
    /// </summary>
    /// <param name="download"></param>
    /// <param name="downloadFileName"><para lang="zh">文件名</para><para lang="en">File name</para></param>
    /// <param name="url"><para lang="zh">文件地址</para><para lang="en">File url</para></param>
    /// <returns></returns>
    public static Task DownloadFromUrlAsync(this DownloadService download, string downloadFileName, string url) => download.DownloadFromUrlAsync(new DownloadOption() { FileName = downloadFileName, Url = url });

    /// <summary>
    /// <para lang="zh">下载文件扩展方法</para>
    /// <para lang="en">Download file extension</para>
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
