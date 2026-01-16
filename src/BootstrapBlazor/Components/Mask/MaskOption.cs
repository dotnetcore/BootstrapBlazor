// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MaskOption 配置类</para>
/// <para lang="en">MaskOption Configuration Class</para>
/// </summary>
public class MaskOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 z-index 值 默认 未设置 使用 样式 1050</para>
    /// <para lang="en">Get/Set z-index value. Default not set. Use style 1050</para>
    /// </summary>
    public int? ZIndex { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 opacity 值 默认 未设置 使用 样式 0.5</para>
    /// <para lang="en">Get/Set opacity value. Default not set. Use style 0.5</para>
    /// </summary>
    public float? Opacity { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 background-color 值 默认 未设置 使用 样式 #000</para>
    /// <para lang="en">Get/Set background-color value. Default not set. Use style #000</para>
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Get/Set Child Content</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 遮罩父容器 id 默认 null 未设置</para>
    /// <para lang="en">Get/Set Mask Parent Container ID. Default null</para>
    /// </summary>
    public string? ContainerId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 遮罩父容器选择器 Selector 默认 null 未设置</para>
    /// <para lang="en">Get/Set Mask Parent Container Selector. Default null</para>
    /// </summary>
    public string? Selector { get; set; }
}
