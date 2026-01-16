// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PdfOptions 实例用于设置导出 Pdf 相关选项</para>
/// <para lang="en">PdfOptions instance used to set export Pdf related options</para>
/// </summary>
public class PdfOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否横向打印 默认 false</para>
    /// <para lang="en">Get/Set whether to print landscape default false</para>
    /// </summary>
    public bool Landscape { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否打印背景色 默认 false</para>
    /// <para lang="en">Get/Set whether to print background default false</para>
    /// </summary>
    public bool PrintBackground { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 纸张格式 默认 A4</para>
    /// <para lang="en">Get/Set paper format default A4</para>
    /// </summary>
    public PaperFormat Format { get; set; } = PaperFormat.A4;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示页眉页脚 默认 false</para>
    /// <para lang="en">Get/Set whether to display header and footer default false</para>
    /// </summary>
    public bool DisplayHeaderFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 放大比例 默认 1 取值 0.1 到 2 之间</para>
    /// <para lang="en">Get/Set scale default 1 between 0.1 and 2</para>
    /// </summary>
    public decimal Scale { get; set; } = 1;

    /// <summary>
    /// <para lang="zh">获得/设置 页面边距 默认 未设置</para>
    /// <para lang="en">Get/Set page margin default not set</para>
    /// </summary>
    public MarginOptions MarginOptions { get; set; } = new();
}
