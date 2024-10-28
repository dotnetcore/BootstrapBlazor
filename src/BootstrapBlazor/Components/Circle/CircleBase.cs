﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Circle 组件基类
/// </summary>
public abstract class CircleBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得 组件样式字符串
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("circle")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 预览框 Style 属性
    /// </summary>
    protected string? PrevStyleString => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Width}px;", Width > 0)
        .AddClass("transform: rotate(-90deg);")
        .Build();

    /// <summary>
    /// 获得 进度条样式
    /// </summary>
    protected string? ProgressClassString => CssBuilder.Default("circle-progress")
        .AddClass($"circle-{Color.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得/设置 Dash 字符串
    /// </summary>
    protected string DashString => $"{CircleLength}, {CircleLength}";

    /// <summary>
    /// 获得/设置 圆形进度半径
    /// </summary>
    protected string CircleDiameter => $"{Width / 2}";

    /// <summary>
    /// 获得/设置 半径
    /// </summary>
    protected string CircleR => $"{Width / 2 - StrokeWidth}";

    /// <summary>
    /// 获得 圆形周长
    /// </summary>
    protected double CircleLength => Math.Round((Width / 2 - StrokeWidth) * 2 * Math.PI, 2);

    /// <summary>
    /// 获得/设置 文件预览框宽度
    /// </summary>
    [Parameter]
    public virtual int Width { get; set; } = 120;

    /// <summary>
    /// 获得/设置 进度条宽度 默认为 2
    /// </summary>
    [Parameter]
    public virtual int StrokeWidth { get; set; } = 2;

    /// <summary>
    /// 获得/设置 组件进度条颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示进度百分比 默认显示
    /// </summary>
    [Parameter]
    public bool ShowProgress { get; set; } = true;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 检查 StrokeWidth 参数
        if (Width / 2 < StrokeWidth) StrokeWidth = 2;
        Width = Math.Max(6, Width);
    }
}
