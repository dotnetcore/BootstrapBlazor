﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Watermark 组件
/// </summary>
public partial class Watermark
{
    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 水印文本 默认 BootstrapBlazor
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 字体大小 如 1rem 12px 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? FontSize { get; set; }

    /// <summary>
    /// 获得/设置 颜色 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// 获得/设置 水印的旋转角度 默认 null 45°
    /// </summary>
    [Parameter]
    public string? Rotate { get; set; }

    /// <summary>
    /// 获得/设置 水印元素的 z-index 值 默认 null
    /// </summary>
    [Parameter]
    public int? ZIndex { get; set; }

    private string? ClassString => CssBuilder.Default("bb-watermark")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= "BootstrapBlazor";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id, new
            {
                Text,
                FontSize,
                Color,
                Rotate,
                ZIndex
            });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new
    {
        Text,
        FontSize,
        Color,
        Rotate,
        ZIndex
    });
}
