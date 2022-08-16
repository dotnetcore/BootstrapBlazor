// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 编辑器设置
/// </summary>
public class EditorSettings
{
    /// <summary>
    /// CodeMirror主题，默认为 default
    /// </summary>
    public string Theme { get; set; } = "default";

    /// <summary>
    /// 编辑器高度，默认为100%
    /// </summary>
    public string Height { get; set; } = "100%";

    /// <summary>
    /// 编辑器显示模式
    /// edit&amp;preview: 双栏编辑预览模式，默认值
    /// editOnly: 只显示编辑器
    /// previewOnly: 预览模式
    /// </summary>
    public string DefaultModel { get; set; } = "edit&preview";

    /// <summary>
    /// 粘贴Html时自动转换为Markdown格式
    /// </summary>
    public bool ConvertWhenPaste { get; set; } = true;
}
