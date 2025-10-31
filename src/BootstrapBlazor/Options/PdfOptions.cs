// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// PdfOptions 实例用于设置导出 Pdf 相关选项
/// </summary>
public class PdfOptions
{
    /// <summary>
    /// 获得/设置 是否横向打印 默认 false
    /// </summary>
    public bool Landscape { get; set; }

    /// <summary>
    /// 获得/设置 是否打印背景色 默认 false
    /// </summary>
    public bool PrintBackground { get; set; }

    /// <summary>
    /// 获得/设置 纸张格式 默认 A4
    /// </summary>
    public PaperFormat Format { get; set; } = PaperFormat.A4;

    /// <summary>
    /// 获得/设置 是否显示页眉页脚 默认 false
    /// </summary>
    public bool DisplayHeaderFooter { get; set; }

    /// <summary>
    /// 获得/设置 放大比例 默认 1 取值 0.1 到 2 之间
    /// </summary>
    public decimal Scale { get; set; } = 1;

    /// <summary>
    /// 获得/设置 页面边距 默认 未设置
    /// </summary>
    public MarginOptions MarginOptions { get; set; } = new();
}
