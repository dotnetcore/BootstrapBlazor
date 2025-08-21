// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITooltip 接口
/// </summary>
public interface ITooltip
{
    /// <summary>
    /// 获得/设置 位置
    /// </summary>
    Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 显示内容
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// 获得/设置 内容是否为 Html 默认 false
    /// </summary>
    bool IsHtml { get; set; }

    /// <summary>
    /// 获得/设置 触发方式 可组合 click focus hover manual 默认为 focus hover
    /// </summary>
    /// <remarks>设置 manual 时，请使用 <see cref="Tooltip"/> 组件实例方法 <see cref="Tooltip.Show(int?)"/> <see cref="Tooltip.Hide(int?)"/> <see cref="Tooltip.Toggle(int?)"/> 对弹窗状态进行控制</remarks>
    string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 显示隐藏延时 默认 null
    /// </summary>
    /// <remarks>Delay showing and hiding the tooltip (ms)—doesn’t apply to manual trigger type. If a number is supplied, delay is applied to both hide/show. Object structure is: delay: { "show": 500, "hide": 100 }.</remarks>
    string? Delay { get; set; }

    /// <summary>
    /// 获得/设置 是否对 <see cref="Title"/> 进行关键字过滤 默认 true
    /// </summary>
    bool Sanitize { get; set; }

    /// <summary>
    /// 获得/设置 元素选择器
    /// </summary>
    string? Selector { get; set; }
}
