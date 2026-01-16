// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Circle 组件基类</para>
/// <para lang="en">Circle component base class</para>
/// </summary>
public abstract class CircleBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// <para lang="zh">获得 组件样式字符串</para>
    /// <para lang="en">Get component style string</para>
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("circle")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 预览框 Style 属性</para>
    /// <para lang="en">Get preview box Style property</para>
    /// </summary>
    protected string? PrevStyleString => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Width}px;", Width > 0)
        .AddClass("transform: rotate(-90deg);")
        .Build();

    /// <summary>
    /// <para lang="zh">获得 进度条样式</para>
    /// <para lang="en">Get progress bar style</para>
    /// </summary>
    protected string? ProgressClassString => CssBuilder.Default("circle-progress")
        .AddClass($"circle-{Color.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 Dash 字符串</para>
    /// <para lang="en">Get/Set Dash string</para>
    /// </summary>
    protected string DashString => $"{CircleLength}, {CircleLength}";

    /// <summary>
    /// <para lang="zh">获得/设置 圆形进度半径</para>
    /// <para lang="en">Get/Set circular progress radius</para>
    /// </summary>
    protected string CircleDiameter => $"{Width / 2}";

    /// <summary>
    /// <para lang="zh">获得/设置 半径</para>
    /// <para lang="en">Get/Set radius</para>
    /// </summary>
    protected string CircleR => $"{Width / 2 - StrokeWidth}";

    /// <summary>
    /// <para lang="zh">获得 圆形周长</para>
    /// <para lang="en">Get circular circumference</para>
    /// </summary>
    protected double CircleLength => Math.Round((Width / 2 - StrokeWidth) * 2 * Math.PI, 2);

    /// <summary>
    /// <para lang="zh">获得/设置 文件预览框宽度</para>
    /// <para lang="en">Get/Set file preview box width</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public virtual int Width { get; set; } = 120;

    /// <summary>
    /// <para lang="zh">获得/设置 进度条宽度 默认为 2</para>
    /// <para lang="en">Get/Set progress bar width, default is 2</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public virtual int StrokeWidth { get; set; } = 2;

    /// <summary>
    /// <para lang="zh">获得/设置 组件进度条颜色</para>
    /// <para lang="en">Get/Set component progress bar color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示进度百分比 默认显示</para>
    /// <para lang="en">Get/Set whether to show progress percentage, default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowProgress { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Get/Set child content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 检查 StrokeWidth 参数
        if (Width / 2 < StrokeWidth) StrokeWidth = 2;
        Width = Math.Max(6, Width);
    }
}
