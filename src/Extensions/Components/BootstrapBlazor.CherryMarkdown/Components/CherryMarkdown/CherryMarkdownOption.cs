// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 配置信息
/// </summary>
internal class CherryMarkdownOption
{
    /// <summary>
    /// 初始值
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 编辑器选项
    /// </summary>
    public EditorSettings? Editor { get; set; }

    /// <summary>
    /// 工具栏选项
    /// </summary>
    public ToolbarSettings? Toolbars { get; set; }
}
