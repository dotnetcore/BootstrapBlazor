// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SegmentedOption 类
/// </summary>
public class SegmentedOption<TValue>
{
    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 选项值
    /// </summary>
    public TValue? Value { get; set; }

    /// <summary>
    /// 获得/设置 是否选中
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认 false
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 组件内容
    /// </summary>
    public RenderFragment? ChildContent { get; set; }
}
