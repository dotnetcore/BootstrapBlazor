// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
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
    /// 获得/设置 AM/PM值变化时委托方法
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

    #region am/pm switch

    /// <summary>
    /// the am/pm text
    /// </summary>
    private string am_pm { get; set; } = string.Empty;

    /// <summary>
    /// the am btn class
    /// </summary>
    private string? am_class => CssBuilder.Default("btn").AddClass("active", IsAM).Build();

    /// <summary>
    /// the pm btn class
    /// </summary>
    private string? pm_class => CssBuilder.Default("btn").AddClass("active", !IsAM).Build();

    /// <summary>
    /// switch to am/pm
    /// </summary>
    /// <param name="is_am">am/pm text</param>
    private void SwitchToAM(bool is_am)
    {
        IsAM = is_am;
        am_pm = is_am ? Localizer["AMText"].Value : Localizer["PMText"].Value;
    }

    #endregion

    #region hour/min switch

    /// <summary>
    /// is hour or min or sec mode
    /// </summary>
    private TimeMode Hms { get; set; } = TimeMode.Hour;

    /// <summary>
    /// hour text class
    /// </summary>
    private string? HourClass => CssBuilder.Default("part hour").AddClass("active", Hms == TimeMode.Hour).Build();

    /// <summary>
    /// min text class
    /// </summary>
    private string? MinClass => CssBuilder.Default("part min").AddClass("active", Hms == TimeMode.Min).Build();

    /// <summary>
    /// sec text class
    /// </summary>
    private string? SecClass => CssBuilder.Default("part sec").AddClass("active", Hms == TimeMode.Sec).Build();

    /// <summary>
    /// hour face class
    /// </summary>
    private string? HourFaceClass => CssBuilder.Default("face-set hour").AddClass("face-off", Hms != TimeMode.Hour).Build();

    /// <summary>
    /// min face class
    /// </summary>
    private string? MinFaceClass => CssBuilder.Default("face-set min").AddClass("face-off", Hms != TimeMode.Min).Build();

    /// <summary>
    /// min face class
    /// </summary>
    private string? SecFaceClass => CssBuilder.Default("face-set sec").AddClass("face-off", Hms != TimeMode.Sec).Build();

    #endregion

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
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {

        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value.Hours, Value.Minutes, Value.Seconds);

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
        switch (Hms)
        {
            case TimeMode.Hour:
                Hms = TimeMode.Min;
                break;
            case TimeMode.Min:
                Hms = TimeMode.Sec;
                break;
            case TimeMode.Sec:
                break;
            default:
                break;
        }
    }

    private enum TimeMode
    {
        Hour,
        Min,
        Sec
    }
}
