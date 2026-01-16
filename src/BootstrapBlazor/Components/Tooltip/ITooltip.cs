// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITooltip 接口
///</para>
/// <para lang="en">ITooltip 接口
///</para>
/// </summary>
public interface ITooltip
{
    /// <summary>
    /// <para lang="zh">获得/设置 位置
    ///</para>
    /// <para lang="en">Gets or sets 位置
    ///</para>
    /// </summary>
    Placement Placement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示内容
    ///</para>
    /// <para lang="en">Gets or sets displaycontent
    ///</para>
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容是否为 Html 默认 false
    ///</para>
    /// <para lang="en">Gets or sets contentwhether为 Html Default is false
    ///</para>
    /// </summary>
    bool IsHtml { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 触发方式 可组合 click focus hover manual 默认为 focus hover
    ///</para>
    /// <para lang="en">Gets or sets 触发方式 可组合 click focus hover manual Default is为 focus hover
    ///</para>
    /// </summary>
    /// <remarks>设置 manual 时，请使用 <see cref="Tooltip"/> 组件实例方法 <see cref="Tooltip.Show(int?)"/> <see cref="Tooltip.Hide(int?)"/> <see cref="Tooltip.Toggle(int?)"/> 对弹窗状态进行控制</remarks>
    string? Trigger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式 默认 null
    ///</para>
    /// <para lang="en">Gets or sets 自定义style Default is null
    ///</para>
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    string? CustomClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示隐藏延时 默认 null
    ///</para>
    /// <para lang="en">Gets or sets display隐藏延时 Default is null
    ///</para>
    /// </summary>
    /// <remarks>Delay showing and hiding the tooltip (ms)—doesn’t apply to manual trigger type. If a number is supplied, delay is applied to both hide/show. Object structure is: delay: { "show": 500, "hide": 100 }.</remarks>
    string? Delay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否对 <see cref="Title"/> 进行关键字过滤 默认 true
    ///</para>
    /// <para lang="en">Gets or sets whether对 <see cref="Title"/> 进行关键字过滤 Default is true
    ///</para>
    /// </summary>
    bool Sanitize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 元素选择器
    ///</para>
    /// <para lang="en">Gets or sets 元素选择器
    ///</para>
    /// </summary>
    string? Selector { get; set; }
}
