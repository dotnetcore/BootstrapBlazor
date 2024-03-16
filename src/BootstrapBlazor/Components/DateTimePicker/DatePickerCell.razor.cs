// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePickerCell 组件
/// </summary>
public sealed partial class DatePickerCell
{
    private string? ClassString => CssBuilder.Default("cell")
        .AddClass("is-solar-term", ShowLunar && ShowSolarTerm && string.IsNullOrEmpty(CalendarFestivals.GetFestival(Value)) && Value.GetSolarTermName() != null)
        .AddClass("is-festival", ShowLunar && !string.IsNullOrEmpty(CalendarFestivals.GetFestival(Value)) && Value.GetSolarTermName() == null)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 日期
    /// </summary>
    [Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// 获得/设置 日期
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击回调方法 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<DateTime, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得/设置 单元格模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? Template { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国阴历历法 默认 false
    /// </summary>
    [Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国 24 节气 默认 false
    /// </summary>
    [Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// 获得/设置 是否节日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// 获得/设置 是否显示休假日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowHolidays { get; set; }

    [Inject]
    [NotNull]
    private ICalendarFestivals? CalendarFestivals { get; set; }

    [Inject]
    [NotNull]
    private ICalendarHolidays? CalendarHolidays { get; set; }

    private string GetLunarText(DateTime dateTime) => dateTime.ToLunarText(ShowSolarTerm, ShowFestivals ? CalendarFestivals : null);
}
