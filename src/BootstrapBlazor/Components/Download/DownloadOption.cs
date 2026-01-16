// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">文件下载配置类</para>
/// <para lang="en">Download Option Class</para>
/// </summary>
public class DownloadOption
{
    /// <summary>
    /// <para lang="zh">获取/设置 要下载的文件数据字节数组</para>
    /// <para lang="en">Get/Set File Content Byte Array</para>
    /// </summary>
    public Stream? FileStream { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载地址</para>
    /// <para lang="en">Get/Set Download Url</para>
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 下载文件另存为文件名</para>
    /// <para lang="en">Get/Set Save as Filename</para>
    /// </summary>
    public string? FileName { get; set; }
}
