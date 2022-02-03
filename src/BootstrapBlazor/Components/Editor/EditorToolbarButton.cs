// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 富文本框插件信息
/// </summary>
public class EditorToolbarButton
{
    /// <summary>
    /// 获取或设置 插件名称
    /// </summary>
    [NotNull]
    public string? ButtonName { get; set; }

    /// <summary>
    /// 获取或设置 插件图标
    /// </summary>
    public string? IconClass { get; set; }

    /// <summary>
    /// 获取或设置 插件的提示信息
    /// </summary>
    public string? Tooltip { get; set; }
}
