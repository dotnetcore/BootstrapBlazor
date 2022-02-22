// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Markdown组件设置项
/// </summary>
internal class MarkdownOption
{
    /// <summary>
    /// 获得/设置 编辑器呈现界面：markdown,wysiwyg(所见即所得)
    /// </summary>
    public string InitialEditType { get; set; } = "markdown";

    /// <summary>
    /// 获得/设置 预览方式：vertical(分栏), tab(tab页)
    /// </summary>
    public string PreviewStyle { get; set; } = "vertical";

    /// <summary>
    /// 获得/设置 高度：px值，默认300px
    /// </summary>
    public string Height { get; set; } = "300px";

    /// <summary>
    /// 获得/设置 最小高度：px值，默认200px
    /// </summary>
    public string MinHeight { get; set; } = "200px";

    /// <summary>
    /// 获得/设置 语言，默认为英文，如果改变，需要自行引入语言包
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 获得/设置 提示信息
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// 获得/设置 Markdown 内容
    /// </summary>
    public string? InitialValue { get; set; }

    /// <summary>
    /// 是否为浏览器模式
    /// </summary>
    public bool? Viewer { get; set; } = false;

    /// <summary>
    /// 获得/设置 主题类型
    /// </summary>
    public string? Theme { get; set; }

    /// <summary>
    /// 获得/设置 是否启用代码高亮
    /// </summary>
    public bool? EnableHighlight { get; set; }
}
