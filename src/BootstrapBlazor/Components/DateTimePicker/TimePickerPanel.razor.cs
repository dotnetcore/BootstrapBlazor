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
    /// 是否显示表盘刻度 默认 false
    /// </summary>
    [Parameter]
    public bool ShowClockScale { get; set; }

    /// <summary>
    /// 是否显示秒 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// 是否显示分钟 默认 true
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// 是否自动切换 小时、分钟、秒 自动切换 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoSwitch { get; set; } = true;

    [CascadingParameter]
    private DatePickerBody? DatePicker { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TimePickerPanel>? Localizer { get; set; }

    private string? CurrentDateString => HasDate ? DatePicker!.Value.ToString(DatePicker.DateFormat) : null;

    private bool HasDate => DatePicker != null && DatePicker.Value != DateTime.MinValue;

    /// <summary>
    /// is hour or min or sec mode
    /// </summary>
    private TimeMode Mode { get; set; } = TimeMode.Hour;

    private string? HourClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-hour")
        .AddClass("fade", Mode != TimeMode.Hour)
        .Build();

    private string? MinuteClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-minute")
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

    private string? AMPMString => IsAM ? Localizer["AMText"] : Localizer["PMText"];

    private string HourValue => (Value.Hours > 12 ? (Value.Hours - 12) : Value.Hours).ToString("D2");

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
        CurrentValue = Value.Hours > 12 ? Value.Subtract(TimeSpan.FromHours(12)) : Value.Add(TimeSpan.FromHours(12));
    }

    private void SetTimePeriod(int hour)
    {
        var val = Value.Hours + hour;
        CurrentValue = new TimeSpan(GetSafeHour(val), Value.Minutes, Value.Seconds);
    }

    /// <summary>
    /// 设置小时调用此方法
    /// </summary>
    [JSInvokable]
    public void SetTime(int hour, int minute, int second)
    {
        if (IsAutoSwitch)
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
        }

        CurrentValue = new TimeSpan(GetSafeHour(IsAM ? hour : hour + 12), minute, second);
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    private static int GetSafeHour(int val)
    {
        if (val < 0)
        {
            val += 12;
        }
        if (val > 23)
        {
            val -= 12;
        }
        return val;
    }

    private void SwitchView() => DatePicker?.SwitchDateView();

    private enum TimeMode
    {
        Hour,
        Minute,
        Second
    }
}
