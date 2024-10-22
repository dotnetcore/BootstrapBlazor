// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
