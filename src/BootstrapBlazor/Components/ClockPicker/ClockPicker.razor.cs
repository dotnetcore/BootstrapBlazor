// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ClockPicker 组件</para>
/// <para lang="en">ClockPicker component</para>
/// </summary>
public partial class ClockPicker
{
    /// <summary>
    /// <para lang="zh">获得/设置 样式</para>
    /// <para lang="en">Get/Set style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-clock-picker")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示表盘刻度 默认 false</para>
    /// <para lang="en">Get/Set whether to show clock scale, default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowClockScale { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示秒 默认 true</para>
    /// <para lang="en">Get/Set whether to show second, default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示分钟 默认 true</para>
    /// <para lang="en">Get/Set whether to show minute, default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动切换 小时、分钟、秒 自动切换 默认 true</para>
    /// <para lang="en">Get/Set whether to automatically switch hour/minute/second, default is true</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">is hour or min or sec mode
    ///</para>
    /// <para lang="en">is hour or min or sec mode
    ///</para>
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
    /// <para lang="zh">复位方法</para>
    /// <para lang="en">Reset method</para>
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
    /// <para lang="zh">JSInvoke 调用此方法</para>
    /// <para lang="en">JSInvoke calls this method</para>
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
