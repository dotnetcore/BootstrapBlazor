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
    /// is am or pm
    /// </summary>
    private bool IsAM { get; set; }

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
    /// is hour or min mode
    /// </summary>
    private bool IsHour { get; set; } = true;

    /// <summary>
    /// hour text class
    /// </summary>
    private string? hour_class => CssBuilder.Default("part hour").AddClass("active", IsHour).Build();

    /// <summary>
    /// min text class
    /// </summary>
    private string? min_class => CssBuilder.Default("part min").AddClass("active", !IsHour).Build();

    /// <summary>
    /// hour face class
    /// </summary>
    private string? hourface_class => CssBuilder.Default("face-set hour").AddClass("face-off", !IsHour).Build();

    /// <summary>
    /// min face class
    /// </summary>
    private string? minface_class => CssBuilder.Default("face-set min").AddClass("face-off", IsHour).Build();

    /// <summary>
    /// switch hour or min mode
    /// </summary>
    /// <param name="is_hour"></param>
    private void SwitchToHour(bool is_hour)
    {
        IsHour = is_hour;
        //invoke js method to switch hour or min pass the ishour parameter
    }

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



        Value = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Value);

    /// <summary>
    /// 设置小时调用此方法
    /// </summary>
    [JSInvokable]
    public async Task SetTime(DateTime dt)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
