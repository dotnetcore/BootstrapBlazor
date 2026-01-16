// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可悬浮弹窗组件接口</para>
/// <para lang="en">Popover base component interface</para>
/// </summary>
internal interface IPopoverBaseComponent
{
    /// <summary>
    /// <para lang="zh">获得/设置 弹窗位置 默认为 Bottom</para>
    /// <para lang="en">Get/Set popover placement default Bottom</para>
    /// </summary>
    Placement Placement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式 参数 默认 null</para>
    /// <para lang="en">Get/Set custom style parameter default null</para>
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    string? CustomClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 true</para>
    /// <para lang="en">Get/Set whether show shadow default true</para>
    /// </summary>
    bool ShowShadow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否使用 Popover 渲染下拉框 默认 false</para>
    /// <para lang="en">Get/Set whether to use Popover to render dropdown default false</para>
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗偏移量 默认 [0, 10]</para>
    /// <para lang="en">Get/Set popover offset default [0, 10]</para>
    /// </summary>
    public string? Offset { get; set; }
}
