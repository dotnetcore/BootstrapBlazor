// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Silder 组件
/// </summary>
public partial class Slider : IDisposable
{
    /// <summary>
    /// 获得/设置 JSInterop 实例
    /// </summary>
    private JSInterop<Slider>? Interop { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private static string? ClassName => CssBuilder.Default("slider")
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? SliderClassName => CssBuilder.Default("slider-runway")
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 获得 Bar 位置样式
    /// </summary>
    private string? BarStyle => CssBuilder.Default("left: 0%;")
        .AddClass($"width: {Value}%;")
        .Build();

    /// <summary>
    /// 获得 按钮位置样式
    /// </summary>
    private string? ButtonStyle => CssBuilder.Default()
        .AddClass($"left: {Value}%;")
        .Build();

    /// <summary>
    /// 获得/设置 当前组件 DOM 实例
    /// </summary>
    private ElementReference SliderElement { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Interop = new JSInterop<Slider>(JSRuntime);
            await Interop.InvokeVoidAsync(this, SliderElement, "bb_slider", nameof(SetValue));
        }
    }

    /// <summary>
    /// SetValue 方法
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public void SetValue(int val)
    {
        Value = val;
        if (ValueChanged.HasDelegate)
        {
            ValueChanged.InvokeAsync(val);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {

        if (disposing && Interop != null)
        {
            Interop.Dispose();
            Interop = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
