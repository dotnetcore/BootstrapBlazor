﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer 弹出窗参数配置类
/// </summary>
public class DrawerOption
{
    /// <summary>
    /// 获得/设置 Drawer 组件样式
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 抽屉宽度 左右布局时生效
    /// </summary>
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 抽屉高度 上下布局时生效
    /// </summary>
    public string? Height { get; set; }

    /// <summary>
    /// 获得/设置 点击遮罩是否关闭抽屉 默认为 false
    /// </summary>
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 是否显示遮罩 默认为 true 显示遮罩
    /// </summary>
    public bool ShowBackdrop { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件出现位置 默认显示在 Left 位置
    /// </summary>
    public Placement Placement { get; set; } = Placement.Left;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否允许调整大小 默认 false
    /// </summary>
    public bool AllowResize { get; set; }

    /// <summary>
    /// 获得/设置 点击背景遮罩时回调委托方法 默认为 null
    /// </summary>
    public Func<Task>? OnClickBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 关闭当前 Drawer 回调委托 默认 null
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }
}
