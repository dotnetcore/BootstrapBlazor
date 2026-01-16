// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">归档配置类</para>
/// <para lang="en">Archive configuration class</para>
/// </summary>
public class ArchiveOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 压缩等级 默认 <see cref="CompressionLevel.Optimal"/></para>
    /// <para lang="en">Get/Set compression level default <see cref="CompressionLevel.Optimal"/></para>
    /// </summary>
    public CompressionLevel CompressionLevel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 归档模式 默认 <see cref="ZipArchiveMode.Create"/></para>
    /// <para lang="en">Get/Set archive mode default <see cref="ZipArchiveMode.Create"/></para>
    /// </summary>
    public ZipArchiveMode Mode { get; set; } = ZipArchiveMode.Create;

    /// <summary>
    /// <para lang="zh">获得/设置 归档结束后是否关闭流 默认 false 归档后关闭相关流</para>
    /// <para lang="en">Get/Set whether to leave the stream open after the archive is finished default false</para>
    /// </summary>
    internal bool LeaveOpen { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编码方法 默认空 内部使用 UTF-8 编码</para>
    /// <para lang="en">Get/Set encoding default null using UTF-8 internally</para>
    /// </summary>
    public Encoding? Encoding { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 获取文件流回调方法</para>
    /// <para lang="en">Get/Set get file stream callback method</para>
    /// </summary>
    public Func<string, Task<Stream>>? ReadStreamAsync { get; set; }
}
