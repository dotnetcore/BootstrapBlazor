// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTimePickerCell 组件</para>
/// <para lang="en">DateTimePickerCell component</para>
/// </summary>
public sealed partial class DatePickerCell
{
    private string? ClassString => CssBuilder.Default("cell")
        .AddClass("is-solar-term", ShowLunar && ShowSolarTerm && string.IsNullOrEmpty(CalendarFestivals.GetFestival(Value)) && Value.GetSolarTermName() != null)
        .AddClass("is-festival", ShowLunar && !string.IsNullOrEmpty(CalendarFestivals.GetFestival(Value)) && Value.GetSolarTermName() == null)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 日期</para>
    /// <para lang="en">Gets or sets Date</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 日期</para>
    /// <para lang="en">Gets or sets Date</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮点击回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Button Click Callback Method. Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<DateTime, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 单元格模板 默认 null</para>
    /// <para lang="en">Gets or sets Cell Template. Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示中国阴历历法 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Chinese Lunar Calendar. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示中国 24 节气 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Chinese Solar Term. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否节日 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Festivals. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示休假日 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Holidays. Default is false</para>
    /// <para><version>10.2.2</version></para>
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
