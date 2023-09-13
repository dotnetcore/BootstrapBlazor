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

    /// <summary>
    /// is am or pm
    /// </summary>
    private bool IsAM { get; set; }

    /// <summary>
    /// the am/pm text
    /// </summary>
    private LocalizedString? am_pm { get; set; }

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
        am_pm = is_am ? Localizer["AMText"] : Localizer["PMText"];
    }

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
