// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePickerPanel 子组件
/// </summary>
public partial class TimePickerPanel
{
    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-timepanel").AddClassFromAttributes(AdditionalAttributes).Build();

    private int Hour { get; set; } = 12;

    private int Min { get; set; } = 0;

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时委托方法
    /// </summary>
    [Parameter]
    public EventCallback<TimeSpan> ValueChanged { get; set; }

    /// <summary>
    /// 设置小时调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetHour(int hour)
    {
        Hour = hour;
        Value = new TimeSpan(Hour, Min, 0);
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// 设置分钟调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetMin(int min)
    {
        Min = min;
        Value = new TimeSpan(Hour, Min, 0);
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// 设置AM_PM调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetAmPm(bool to_am)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }


    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Hour, Min);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (Value.Hours > 0)
        {
            Hour = Value.Hours;
        }
        else if (Value.Minutes > 0)
        {
            Min = Value.Minutes;
        }
        else
        {
            Value = new TimeSpan(Hour, Min, 0);
        }
    }
}
