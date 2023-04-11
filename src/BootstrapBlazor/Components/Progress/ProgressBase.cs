// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Progress 进度条组件
/// </summary>
public abstract class ProgressBase : BootstrapComponentBase
{
    /// <summary>
    /// 获得/设置 控件高度默认 20px
    /// </summary>
    [Parameter] public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 颜色
    /// </summary>
    [Parameter] public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示进度条值 默认 false
    /// </summary>
    /// <value></value>
    [Parameter] public bool IsShowValue { get; set; }

    /// <summary>
    /// 获得/设置 是否显示为条纹 默认 false
    /// </summary>
    /// <value></value>
    [Parameter] public bool IsStriped { get; set; }

    /// <summary>
    /// 获得/设置 是否动画 默认 false
    /// </summary>
    /// <value></value>
    [Parameter] public bool IsAnimated { get; set; }

    /// <summary>
    /// 获得/设置 组件进度值
    /// </summary>
    [Parameter]
    public double Value { get; set; }

    /// <summary>
    /// 获得/设置 进度值修约小数位数,默认0(即保留为整数)
    /// </summary>
    [Parameter] public int Round { get; set; } = 0;

    /// <summary>
    /// 获得/设置 进度标签文本
    /// </summary>
    [Parameter] public string Text { get; set; } = string.Empty;
}
