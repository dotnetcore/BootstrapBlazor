// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// Delay showing and hiding the tooltip (ms)—doesn’t apply to manual trigger type. If a number is supplied, delay is applied to both hide/show. Object structure is: delay: { "show": 500, "hide": 100 }. default value null
    /// </summary>
    string? Delay { get; set; }

    /// <summary>
    /// Enable or disable the sanitization. If activated 'template', 'content' and 'title' options will be sanitized.
    /// </summary>
    bool Sanitize { get; set; }

    /// <summary>
    /// If a selector is provided, tooltip objects will be delegated to the specified targets.
    /// </summary>
    string? Selector { get; set; }
}
