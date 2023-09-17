// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePickerPanel 子组件
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
    /// 获得/设置 AM/PM
    /// </summary>
    [Parameter]
    public bool IsAM { get; set; }

    /// <summary>
    /// 获得/设置 AM/PM 值变化时委托方法
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsAMChanged { get; set; }

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
    /// localizer
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<TimePickerPanel>? Localizer { get; set; }

    /// <summary>
    /// the am/pm text
    /// </summary>
    private string am_pm { get; set; } = string.Empty;

    /// <summary>
    /// the am btn class
    /// </summary>
    private string? am_class => CssBuilder.Default("btn")
        .AddClass("active", IsAM)
        .Build();

    /// <summary>
    /// the pm btn class
    /// </summary>
    private string? pm_class => CssBuilder.Default("btn")
        .AddClass("active", !IsAM)
        .Build();

    /// <summary>
    /// switch to am/pm
    /// </summary>
    /// <param name="is_am">am/pm text</param>
    private void SwitchToAM(bool is_am)
    {
        IsAM = is_am;
        am_pm = is_am ? Localizer["AMText"].Value : Localizer["PMText"].Value;
    }

    /// <summary>
    /// is hour or min or sec mode
    /// </summary>
    private TimeMode Mode { get; set; } = TimeMode.Hour;

    /// <summary>
    /// hour text class
    /// </summary>
    private string? HourHeaderClass => CssBuilder.Default("part hour")
        .AddClass("active", Mode == TimeMode.Hour)
        .Build();

    /// <summary>
    /// min text class
    /// </summary>
    private string? MinusHeaderClass => CssBuilder.Default("part min")
        .AddClass("active", Mode == TimeMode.Minute)
        .Build();

    /// <summary>
    /// sec text class
    /// </summary>
    private string? SecondHeaderClass => CssBuilder.Default("part sec")
        .AddClass("active", Mode == TimeMode.Second)
        .Build();

    /// <summary>
    /// hour face class
    /// </summary>
    private string? HourClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-hour")
        .AddClass("face-off", Mode != TimeMode.Hour)
        .Build();

    /// <summary>
    /// min face class
    /// </summary>
    private string? MinusClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-min")
        .AddClass("face-off", Mode != TimeMode.Minute)
        .Build();

    /// <summary>
    /// min face class
    /// </summary>
    private string? SecondClass => CssBuilder.Default("bb-clock-panel bb-clock-panel-sec")
        .AddClass("face-off", Mode != TimeMode.Second)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        var dt = DateTime.Now;
        IsAM = dt.Hour < 12;
        SwitchToAM(IsAM);

        Value = new TimeSpan(IsAM ? dt.Hour : dt.AddHours(-12).Hour, dt.Minute, dt.Second);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value.Hours, Value.Minutes, Value.Seconds);

    private void SetMode(TimeMode mode) => Mode = mode;

    /// <summary>
    /// 设置小时调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetTime(int hour, int min, int sec)
    {
        Value = new TimeSpan(hour, min, sec);
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        //这里不知道为什么，Value的值会延迟刷新
        switch (Mode)
        {
            case TimeMode.Hour:
                Mode = TimeMode.Minute;
                break;
            case TimeMode.Minute:
                Mode = TimeMode.Second;
                break;
            case TimeMode.Second:
                break;
            default:
                break;
        }
    }

    private enum TimeMode
    {
        Hour,
        Minute,
        Second
    }
}
