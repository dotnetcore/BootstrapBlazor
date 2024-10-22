// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 文件下载配置类
/// </summary>
public class DownloadOption
{
    /// <summary>
    /// 获取/设置 要下载的文件数据字节数组
    /// </summary>
    public Stream? FileStream { get; set; }

    /// <summary>
    /// 获得/设置 下载地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 获取/设置 下载文件另存为文件名
    /// </summary>
    public string? FileName { get; set; }
}
