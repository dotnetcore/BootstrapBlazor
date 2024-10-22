﻿// Licensed to the .NET Foundation under one or more agreements.
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
    /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
    /// </summary>
    string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 显示隐藏延时 默认 null
    /// </summary>
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
