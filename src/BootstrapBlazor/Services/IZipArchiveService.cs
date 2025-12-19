// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// <param name="entries">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    /// <returns>归档数据流</returns>
    Task<Stream> ArchiveAsync(IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null);

    /// <summary>
    /// 将文件归档方法
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entries">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    Task ArchiveAsync(string archiveFile, IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null);

    /// <summary>
    /// 将指定目录归档方法
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="directoryName">要归档文件夹</param>
    /// <param name="compressionLevel">压缩率</param>
    /// <param name="includeBaseDirectory">是否包含本目录 默认 false</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <param name="token"></param>
    Task ArchiveDirectoryAsync(string archiveFile, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null, CancellationToken token = default);

    /// <summary>
    /// 解压缩归档文件到指定文件夹异步方法
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="destinationDirectoryName">解压缩文件夹</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    /// <param name="token"></param>
    Task<bool> ExtractToDirectoryAsync(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null, CancellationToken token = default);

    /// <summary>
    /// 获得归档压缩文件中指定归档文件
    /// </summary>
    /// <param name="archiveFile">归档文件</param>
    /// <param name="entryFile">解压缩文件</param>
    /// <param name="overwriteFiles">是否覆盖文件 默认 false 不覆盖</param>
    /// <param name="encoding">编码方式 默认 null 内部使用 UTF-8</param>
    ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null);
}

/// <summary>
/// 归档项实体类
/// </summary>
public readonly record struct ArchiveEntry
{
    /// <summary>
    /// 获得 物理文件
    /// </summary>
    public string SourceFileName { get; init; }

    /// <summary>
    /// 获得 归档项
    /// </summary>
    public string EntryName { get; init; }

    /// <summary>
    /// 获得 压缩配置
    /// </summary>
    public CompressionLevel? CompressionLevel { get; init; }
}
