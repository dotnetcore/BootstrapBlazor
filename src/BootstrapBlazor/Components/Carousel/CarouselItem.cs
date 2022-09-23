// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CarouselItem 类
/// </summary>
public class CarouselItem : ComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 子组件 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 Caption 文字 默认 null 可设置 <see cref="CaptionTemplate"/> 自定义
    /// </summary>
    [Parameter]
    public string? Caption { get; set; }

    /// <summary>
    /// 获得/设置 Caption 样式 默认 null
    /// </summary>
    [Parameter]
    public string? CaptionClass { get; set; }

    /// <summary>
    /// 获得/设置 Caption 模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? CaptionTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Slider 切换时间间隔 默认 5000
    /// </summary>
    [Parameter]
    public int Interval { get; set; } = 5000;

    [CascadingParameter]
    private Carousel? Carousel { get; set; }

    /// <summary>
    /// 获得 Interval 字符串
    /// </summary>
    internal string? IntervalString => Interval == 5000 ? null : Interval.ToString();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Carousel?.AddItem(this);
    }

    /// <summary>
    /// 获得 当前 Item Caption 样式字符串
    /// </summary>
    public string? GetCaptionClassString => CssBuilder.Default("carousel-caption")
        .AddClass(CaptionClass)
        .Build();

    /// <summary>
    /// 获得 是否显示 Caption
    /// </summary>
    internal bool ShowCaption => CaptionTemplate != null || !string.IsNullOrEmpty(Caption);

    /// <summary>
    /// Dispose 方法
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
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
