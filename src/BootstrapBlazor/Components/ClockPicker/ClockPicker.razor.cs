// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Runtime.CompilerServices;

namespace BootstrapBlazor.Components;

/// <summary>
/// ClockPicker 组件
/// </summary>
public partial class ClockPicker
{
    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-clock-picker")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否显示表盘刻度 默认 false
    /// </summary>
    [Parameter]
    public bool ShowClockScale { get; set; }

    /// <summary>
    /// 获得/设置 是否显示秒 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示分钟 默认 true
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动切换 小时、分钟、秒 自动切换 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoSwitch { get; set; } = true;

    [CascadingParameter]
    [NotNull]
    private DatePickerBody? DatePicker { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ClockPicker>? Localizer { get; set; }

    private string? CurrentDateString => DatePicker.Value.ToString(DatePicker.DateFormat);

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

    private string HourValue => (Value.Hours > 12 ? (Value.Hours - 12) : Value.Hours).ToString("D2");

#if NET6_0
    private string _version = "NET6.0";
#else
    private string _version = $"NET{Environment.Version}";
#endif

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
            await InvokeVoidAsync("update", Id, new { Hour = Value.Hours, Minute = Value.Minutes, Second = Value.Seconds, Version = _version });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, Hour = Value.Hours, Minute = Value.Minutes, Second = Value.Seconds, Version = _version });

    private void SetMode(TimeMode mode) => Mode = mode;

    /// <summary>
    /// 复位方法
    /// </summary>
    internal void Reset()
    {
        Mode = TimeMode.Hour;
    }

    private void SetTimePeriod(int hour)
    {
        var val = Value.Hours + hour;
        CurrentValue = new TimeSpan(GetSafeHour(val), Value.Minutes, Value.Seconds);
    }

    /// <summary>
    /// JSInvoke 调用此方法
    /// </summary>
    [JSInvokable]
    public void SetTime(int hour, int minute, int second)
    {
        if (IsAutoSwitch)
        {
            if (Mode == TimeMode.Hour && ShowMinute)
            {
                Mode = TimeMode.Minute;
            }
            else if (Mode == TimeMode.Minute && ShowSecond)
            {
                Mode = TimeMode.Second;
            }
        }

        if (IsAM && hour == 12)
        {
            hour = 0;
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

    private void SwitchView()
    {
        Mode = TimeMode.Hour;
        DatePicker.SwitchDateView();
    }

    private enum TimeMode
    {
        Hour,
        Minute,
        Second
    }
}
