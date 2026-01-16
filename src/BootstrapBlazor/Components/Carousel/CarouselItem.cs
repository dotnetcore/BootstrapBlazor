// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">CarouselItem 类</para>
///  <para lang="en">CarouselItem class</para>
/// </summary>
public class CarouselItem : ComponentBase, IDisposable
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件 默认 null</para>
    ///  <para lang="en">Gets or sets the child content. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Caption 文字 默认 null 可设置 <see cref="CaptionTemplate"/> 自定义</para>
    ///  <para lang="en">Gets or sets the Caption text. Default is null. Can be customized by setting <see cref="CaptionTemplate"/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Caption { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Caption 样式 默认 null</para>
    ///  <para lang="en">Gets or sets the Caption style. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CaptionClass { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Caption 模板 默认 null</para>
    ///  <para lang="en">Gets or sets the Caption template. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? CaptionTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Slider 切换时间间隔 默认 5000</para>
    ///  <para lang="en">Gets or sets the Slider interval. Default is 5000</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Interval { get; set; } = 5000;

    [CascadingParameter]
    private Carousel? Carousel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 Interval 字符串</para>
    ///  <para lang="en">Get Interval string</para>
    /// </summary>
    internal string? IntervalString => Interval == 5000 ? null : Interval.ToString();

    /// <summary>
    ///  <para lang="zh">OnInitialized 方法</para>
    ///  <para lang="en">OnInitialized method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        Carousel?.AddItem(this);
    }

    /// <summary>
    ///  <para lang="zh">获得 当前 Item Caption 样式字符串</para>
    ///  <para lang="en">Get the current Item Caption style string</para>
    /// </summary>
    public string? GetCaptionClassString => CssBuilder.Default("carousel-caption")
        .AddClass(CaptionClass)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得 是否显示 Caption</para>
    ///  <para lang="en">Get whether to show Caption</para>
    /// </summary>
    internal bool ShowCaption => CaptionTemplate != null || !string.IsNullOrEmpty(Caption);

    /// <summary>
    ///  <para lang="zh">Dispose 方法</para>
    ///  <para lang="en">Dispose method</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Carousel?.RemoveItem(this);
        }
    }

    /// <summary>
    ///  <para lang="zh">Dispose 方法</para>
    ///  <para lang="en">Dispose method</para>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
