// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
