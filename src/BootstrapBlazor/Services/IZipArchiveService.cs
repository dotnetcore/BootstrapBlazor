// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Zip 归档服务</para>
///  <para lang="en">Zip Archive Service</para>
/// </summary>
public interface IZipArchiveService
{
    /// <summary>
    ///  <para lang="zh">将文件归档方法</para>
    ///  <para lang="en">Archive File Method</para>
    /// </summary>
    /// <param name="files"><para lang="zh">要归档的文件集合</para><para lang="en">Collection of files to archive</para></param>
    /// <param name="options"><para lang="zh">归档配置</para><para lang="en">Archive Options</para></param>
    /// <returns><para lang="zh">归档数据流</para><para lang="en">Archive Data Stream</para></returns>
    Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    ///  <para lang="zh">将文件归档方法</para>
    ///  <para lang="en">Archive File Method</para>
    /// </summary>
    /// <param name="archiveFile"><para lang="zh">归档文件</para><para lang="en">Archive File</para></param>
    /// <param name="files"><para lang="zh">要归档的文件集合</para><para lang="en">要归档的文件collection</para></param>
    /// <param name="options"><para lang="zh">归档配置</para><para lang="en">归档config</para></param>
    Task ArchiveAsync(string archiveFile, IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    ///  <para lang="zh">将文件归档方法</para>
    ///  <para lang="en">Archive File Method</para>
    /// </summary>
    /// <param name="entries"><para lang="zh">要归档项集合</para><para lang="en">Collection of entries to archive</para></param>
    /// <param name="options"><para lang="zh">归档配置</para><para lang="en">归档config</para></param>
    /// <returns><para lang="zh">归档数据流</para><para lang="en">归档data流</para></returns>
    Task<Stream> ArchiveAsync(IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null);

    /// <summary>
    ///  <para lang="zh">将文件归档方法</para>
    ///  <para lang="en">Archive File Method</para>
    /// </summary>
    /// <param name="archiveFile"><para lang="zh">归档文件</para><para lang="en">归档文件</para></param>
    /// <param name="entries"><para lang="zh">要归档项集合</para><para lang="en">要归档项collection</para></param>
    /// <param name="options"><para lang="zh">归档配置</para><para lang="en">归档config</para></param>
    Task ArchiveAsync(string archiveFile, IEnumerable<ArchiveEntry> entries, ArchiveOptions? options = null);

    /// <summary>
    ///  <para lang="zh">将指定目录归档方法</para>
    ///  <para lang="en">Archive Directory Method</para>
    /// </summary>
    /// <param name="archiveFile"><para lang="zh">归档文件</para><para lang="en">归档文件</para></param>
    /// <param name="directoryName"><para lang="zh">要归档文件夹</para><para lang="en">Directory to archive</para></param>
    /// <param name="compressionLevel"><para lang="zh">压缩率</para><para lang="en">Compression Level</para></param>
    /// <param name="includeBaseDirectory"><para lang="zh">是否包含本目录 默认 false</para><para lang="en">Include base directory, default false</para></param>
    /// <param name="encoding"><para lang="zh">编码方式 默认 null 内部使用 UTF-8</para><para lang="en">Encoding, default null, internal use UTF-8</para></param>
    /// <param name="token"></param>
    Task ArchiveDirectoryAsync(string archiveFile, string directoryName, CompressionLevel compressionLevel = CompressionLevel.Optimal, bool includeBaseDirectory = false, Encoding? encoding = null, CancellationToken token = default);

    /// <summary>
    ///  <para lang="zh">解压缩归档文件到指定文件夹异步方法</para>
    ///  <para lang="en">Extract Archive to Directory Async Method</para>
    /// </summary>
    /// <param name="archiveFile"><para lang="zh">归档文件</para><para lang="en">归档文件</para></param>
    /// <param name="destinationDirectoryName"><para lang="zh">解压缩文件夹</para><para lang="en">Destination Directory</para></param>
    /// <param name="overwriteFiles"><para lang="zh">是否覆盖文件 默认 false 不覆盖</para><para lang="en">Overwrite files, default false</para></param>
    /// <param name="encoding"><para lang="zh">编码方式 默认 null 内部使用 UTF-8</para><para lang="en">Encoding, default null, internal use UTF-8</para></param>
    /// <param name="token"></param>
    Task<bool> ExtractToDirectoryAsync(string archiveFile, string destinationDirectoryName, bool overwriteFiles = false, Encoding? encoding = null, CancellationToken token = default);

    /// <summary>
    ///  <para lang="zh">获得归档压缩文件中指定归档文件</para>
    ///  <para lang="en">Get Entry from Archive File</para>
    /// </summary>
    /// <param name="archiveFile"><para lang="zh">归档文件</para><para lang="en">归档文件</para></param>
    /// <param name="entryFile"><para lang="zh">解压缩文件</para><para lang="en">Entry File</para></param>
    /// <param name="overwriteFiles"><para lang="zh">是否覆盖文件 默认 false 不覆盖</para><para lang="en">Overwrite files, default false</para></param>
    /// <param name="encoding"><para lang="zh">编码方式 默认 null 内部使用 UTF-8</para><para lang="en">Encoding, default null, internal use UTF-8</para></param>
    ZipArchiveEntry? GetEntry(string archiveFile, string entryFile, bool overwriteFiles = false, Encoding? encoding = null);
}
