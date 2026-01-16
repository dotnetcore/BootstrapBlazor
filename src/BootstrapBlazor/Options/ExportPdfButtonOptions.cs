// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ExportPdfButtonOptions 配置类</para>
/// <para lang="en">ExportPdfButtonOptions configuration class</para>
/// </summary>
public class ExportPdfButtonOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 选择器 默认为 null</para>
    /// <para lang="en">Get/Set export Pdf selector default null</para>
    /// </summary>
    public string? Selector { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 元素 Id 默认为 null</para>
    /// <para lang="en">Get/Set export Pdf element Id default null</para>
    /// </summary>
    public string? ElementId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 所需样式表文件集合 默认为 null</para>
    /// <para lang="en">Get/Set export Pdf style tags default null</para>
    /// </summary>
    public List<string>? StyleTags { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 所需脚本文件集合 默认为 null</para>
    /// <para lang="en">Get/Set export Pdf script tags default null</para>
    /// </summary>
    public List<string>? ScriptTags { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 按钮显示文字 默认为资源文件中 导出 Pdf 文字</para>
    /// <para lang="en">Get/Set export Pdf button text default use resource file</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 按钮图标 未设置 取当前图标主题下导出 Pdf 图标</para>
    /// <para lang="en">Get/Set export Pdf button icon default use current icon theme</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 按钮颜色 默认 Color.Primary</para>
    /// <para lang="en">Get/Set export Pdf button color default Color.Primary</para>
    /// </summary>
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 文件名 默认为 null 未设置时使用 pdf-时间戳.pdf</para>
    /// <para lang="en">Get/Set export Pdf file name default null using pdf-timestamp.pdf when not set</para>
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 之前回调委托 默认为 null</para>
    /// <para lang="en">Get/Set on before export callback default null</para>
    /// </summary>
    public Func<Task>? OnBeforeExport { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载 Pdf 之前回调委托 默认为 null</para>
    /// <para lang="en">Get/Set on before download callback default null</para>
    /// </summary>
    public Func<Stream, Task>? OnBeforeDownload { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载 Pdf 之后回调委托 默认为 null</para>
    /// <para lang="en">Get/Set on after download callback default null</para>
    /// </summary>
    public Func<string, Task>? OnAfterDownload { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动下载 Pdf 默认为 true</para>
    /// <para lang="en">Get/Set whether auto download Pdf default true</para>
    /// </summary>
    public bool AutoDownload { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否异步导出 默认为 true</para>
    /// <para lang="en">Get/Set whether async export default true</para>
    /// </summary>
    public bool IsAsync { get; set; } = true;
}
