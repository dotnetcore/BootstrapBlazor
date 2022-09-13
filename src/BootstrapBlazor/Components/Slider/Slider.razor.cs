// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Silder 组件
/// </summary>
public partial class Slider : IDisposable
{
    /// <summary>
    /// 获得/设置 组件当前值
    /// </summary>
    [Parameter]
    public int Value { get; set; }

    /// <summary>
    /// ValueChanged 回调方法
    /// </summary>
    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 值变化时回调方法
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得 按钮 disabled 属性
    /// </summary>
    protected string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// 获得/设置 是否禁用
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 JSInterop 实例
    /// </summary>
    private JSInterop<Slider>? Interop { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("slider")
        .AddClassFromAttributes(AdditionalAttributes)
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
    public async Task SetValue(int val)
    {
        Value = val;
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(val);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
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
