// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// 归档配置类
/// </summary>
public class ArchiveOptions
{
    /// <summary>
    /// 获得/设置 压缩等级 默认 <see cref="CompressionLevel.Optimal"/>
    /// </summary>
    public CompressionLevel CompressionLevel { get; set; }

    /// <summary>
    /// 获得/设置 归档模式 默认 <see cref="ZipArchiveMode.Create"/>
    /// </summary>
    public ZipArchiveMode Mode { get; set; } = ZipArchiveMode.Create;

    /// <summary>
    /// 获得/设置 归档结束后是否关闭流 默认 false 归档后关闭相关流
    /// </summary>
    internal bool LeaveOpen { get; set; }

    /// <summary>
    /// 获得/设置 编码方法 默认空 内部使用 UTF-8 编码
    /// </summary>
    public Encoding? Encoding { get; set; }

    /// <summary>
    /// 获得/设置 获取文件流回调方法
    /// </summary>
    public Func<string, Task<Stream>>? ReadStreamAsync;
}
