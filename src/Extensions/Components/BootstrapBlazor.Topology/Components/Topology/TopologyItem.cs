// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Topology 数据项实体类
/// </summary>
public class TopologyItem
{
    /// <summary>
    /// 获得/设置 对象 Id
    /// </summary>
    public string? ID { get; set; }

    /// <summary>
    /// 获得/设置 对象关联 Tag 位号值
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组合组件状态切换值 一般从 0 开始
    /// </summary>
    public double ShowChild { get; set; }

    /// <summary>
    /// 获得/设置 显示文字颜色
    /// </summary>
    public string? TextColor { get; set; }

    /// <summary>
    /// 获得/设置 背景颜色
    /// </summary>
    public string? Background { get; set; }

    /// <summary>
    /// 获得/设置 对象 title 属性一般用于 tooltip 显示
    /// </summary>
    public string? Title { get; set; }
}
