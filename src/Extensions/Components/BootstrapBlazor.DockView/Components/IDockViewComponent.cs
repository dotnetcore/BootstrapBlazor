// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IDockViewComponent 接口
/// </summary>
public interface IDockViewComponent : IDockViewComponentBase
{
    /// <summary>
    /// 获得/设置 组件 Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 获得/设置 组件名称 默认 component golden-layout 渲染使用
    /// </summary>
    string ComponentName { get; set; }

    /// <summary>
    /// 获得/设置 组件是否显示 Header 默认 true 显示
    /// </summary>
    bool ShowHeader { get; set; }

    /// <summary>
    /// 获得/设置 组件 Title
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// 获得/设置 组件 Title 宽度 默认 null 未设置
    /// </summary>
    int? TitleWidth { get; set; }

    /// <summary>
    /// 获得/设置 组件 Title 样式 默认 null 未设置
    /// </summary>
    string? TitleClass { get; set; }

    /// <summary>
    /// 获得/设置 组件 Class 默认 null 未设置
    /// </summary>
    string? Class { get; set; }

    /// <summary>
    /// 获得/设置 组件是否可见 默认 true 可见
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// 获得/设置 组件是否允许关闭 默认 null 使用 DockView 的配置
    /// </summary>
    bool? ShowClose { get; set; }

    /// <summary>
    /// 获得/设置 组件唯一标识值 默认 null 未设置时取 Title 作为唯一标识
    /// </summary>
    string? Key { get; set; }

    /// <summary>
    /// 获得/设置 是否锁定 默认 null 未设置时取 DockView 的配置
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    bool? IsLock { get; set; }

    /// <summary>
    /// 获得/设置 是否显示锁定按钮 默认 null 未设置时取 DockView 的配置
    /// </summary>
    bool? ShowLock { get; set; }
}
