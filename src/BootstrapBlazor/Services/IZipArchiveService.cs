// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// Zip 归档服务
/// </summary>
public interface IZipArchiveService
{
    /// <summary>
    /// 将文件归档方法
    /// </summary>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    /// <returns>归档数据流</returns>
    Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    /// 将文件归档方法
    /// </summary>
    /// <param name="archiveFileName">归档文件</param>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    Task ArchiveAsync(string archiveFileName, IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    /// 将指定目录归档方法
    /// </summary>
    /// <param name="archiveFileName">归档文件</param>
    /// <param name="directoryName">要归档文件夹</param>
    /// <param name="compressionLevel"></param>
    /// <param name="includeBaseDirectory"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    Task ArchiveDirectory(string archiveFileName, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null);

    /// <summary>
    /// 解压缩归档文件到指定文件夹
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="destinationDirectoryName">解压缩文件夹</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <returns></returns>
    bool ExtractToDirectory(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null);

    /// <summary>
    /// 获得归档压缩文件中指定归档文件
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entryFile">解压缩文件</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <returns></returns>
    ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null);
}
