// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePickerPanel 组件
/// </summary>
public partial class TimePickerPanel
{
    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-time-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

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

    [Inject]
    [NotNull]
    private IStringLocalizer<TimePickerPanel>? Localizer { get; set; }

    /// <summary>
    /// is hour or min or sec mode
    /// </summary>
    private TimeMode Mode { get; set; } = TimeMode.Hour;

    private string? HourClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-hour")
        .AddClass("fade", Mode != TimeMode.Hour)
        .Build();

    private string? MinusClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-minute")
        .AddClass("fade", Mode != TimeMode.Minute)
        .Build();

    private string? SecondClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-second")
        .AddClass("fade", Mode != TimeMode.Second)
        .Build();

    private string? ButtonAMClassString => CssBuilder.Default("btn btn-am")
        .AddClass("active", IsAM)
        .Build();

    private string? ButtonPMClassString => CssBuilder.Default("btn btn-pm")
        .AddClass("active", !IsAM)
        .Build();

    private bool IsAM => Value.Hours < 12;

    private string? IsAMString => IsAM ? "true" : null;

    private string? AMPMString => IsAM ? Localizer["AMText"] : Localizer["PMText"];

    private string HourValue => (Value.Hours > 12 ? (Value.Hours - 12) : Value.Hours).ToString("D2");

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Value == TimeSpan.Zero)
        {
            Value = DateTime.Now.TimeOfDay;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender && Module != null)
        {
            await Module.InvokeVoidAsync("update", Id, Value.Hours, Value.Minutes, Value.Seconds);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value.Hours, Value.Minutes, Value.Seconds);

    private void SetMode(TimeMode mode) => Mode = mode;

    private void SetTime()
    {
        Value = Value.Hours > 12 ? Value.Subtract(TimeSpan.FromHours(12)) : Value.Add(TimeSpan.FromHours(12));
    }

    private void SetTimePeriod(int hour)
    {
        Value = Value.Add(TimeSpan.FromHours(hour));
    }

    /// <summary>
    /// 设置小时调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetTime(int hour, int min, int sec)
    {
        switch (Mode)
        {
            case TimeMode.Hour:
                Mode = TimeMode.Minute;
                break;
            case TimeMode.Minute:
                Mode = TimeMode.Second;
                break;
            case TimeMode.Second:
            default:
                break;
        }

        Value = new TimeSpan(hour, min, sec);
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private enum TimeMode
    {
        Hour,
        Minute,
        Second
    }
}
