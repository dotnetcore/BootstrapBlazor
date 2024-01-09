// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 可悬浮弹窗组件接口
/// </summary>
internal interface IPopoverBaseComponent
{
    /// <summary>
    /// 获得/设置 弹窗位置 默认为 Bottom
    /// </summary>
    Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 参数 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    bool ShowShadow { get; set; }

    /// <summary>
    /// 获得/设置 是否使用 Popover 渲染下拉框 默认 false
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// 获得/设置 弹窗偏移量 默认 [0, 10]
    /// </summary>
    public string? Offset { get; set; }
}
