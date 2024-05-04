// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ExportPdfButtonOptions 配置类
/// </summary>
public class ExportPdfButtonOptions
{
    /// <summary>
    /// 获得/设置 导出 Pdf 选择器 默认为 null
    /// </summary>
    public string? Selector { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 元素 Id 默认为 null 
    /// </summary>
    public string? ElementId { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 所需样式表文件集合 默认为 null
    /// </summary>
    public List<string>? StyleTags { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 所需脚本文件集合 默认为 null
    /// </summary>
    public List<string>? ScriptTags { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 按钮显示文字 默认为资源文件中 导出 Pdf 文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 按钮图标 未设置 取当前图标主题下导出 Pdf 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 按钮颜色 默认 Color.Primary
    /// </summary>
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 导出 Pdf 文件名 默认为 null 未设置时使用 pdf-时间戳.pdf
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 之前回调委托 默认为 null
    /// </summary>
    public Func<Task>? OnBeforeExport { get; set; }

    /// <summary>
    /// 获得/设置 下载 Pdf 之前回调委托 默认为 null
    /// </summary>
    public Func<Stream, Task>? OnBeforeDownload { get; set; }

    /// <summary>
    /// 获得/设置 下载 Pdf 之后回调委托 默认为 null
    /// </summary>
    public Func<string, Task>? OnAfterDownload { get; set; }

    /// <summary>
    /// 获得/设置 是否自动下载 Pdf 默认为 true
    /// </summary>
    public bool AutoDownload { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否异步导出 默认为 true
    /// </summary>
    public bool IsAsync { get; set; } = true;
}
