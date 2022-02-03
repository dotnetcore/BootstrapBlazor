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
    [NotNull]
    public byte[]? FileContent { get; set; }

    /// <summary>
    /// 获取/设置 下载文件另存为文件名
    /// </summary>
    [NotNull]
    public string? FileName { get; set; }

    /// <summary>
    /// 获取/设置 要下载的文件MIME，默认application/octet-stream
    /// </summary>
    public string Mime { get; set; } = "application/octet-stream";
}
