// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Calendar
{
    [NotNull]
    private string? PreviousYear { get; set; }

    [NotNull]
    private string? NextYear { get; set; }

    [NotNull]
    private string? PreviousMonth { get; set; }

    [NotNull]
    private string? NextMonth { get; set; }

    [NotNull]
    private string? Today { get; set; }

    [NotNull]
    private string? PreviousWeek { get; set; }

    [NotNull]
    private string? NextWeek { get; set; }

    [NotNull]
    private string? WeekText { get; set; }

    [NotNull]
    private List<string>? WeekLists { get; set; }

    [NotNull]
    private string? WeekHeaderText { get; set; }

    [NotNull]
    private List<string>? Months { get; set; }

    /// <summary>
    /// 获得 当前日历框年月
    /// </summary>
    private string? GetTitle() => Localizer["Title", Value.Year, Months.ElementAt(Value.Month - 1)];

    /// <summary>
    /// 获得 当前日历周文字
    /// </summary>
    [NotNull]
    private string? WeekNumberText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Calendar>? Localizer { get; set; }

    /// <summary>
    /// 获得 周日期
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private string GetWeekDayString(int offset) => $"{Value.AddDays(offset - (int)Value.DayOfWeek).Day}";

    /// <summary>
    /// 获得 周日期样式
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    private string? GetWeekDayClassString(int offset) => CssBuilder.Default("week-header")
            .AddClass("is-today", Value.AddDays(offset - (int)Value.DayOfWeek) == DateTime.Today)
            .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Value == DateTime.MinValue)
        {
            Value = DateTime.Today;
        }

        PreviousYear = Localizer[nameof(PreviousYear)];
        NextYear = Localizer[nameof(NextYear)];
        PreviousMonth = Localizer[nameof(PreviousMonth)];
        NextMonth = Localizer[nameof(NextMonth)];
        Today = Localizer[nameof(Today)];
        WeekLists = Localizer[nameof(WeekLists)].Value.Split(',').ToList();
        PreviousWeek = Localizer[nameof(PreviousWeek)];
        NextWeek = Localizer[nameof(NextWeek)];
        WeekText = Localizer[nameof(WeekText)];
        WeekHeaderText = Localizer[nameof(WeekHeaderText)];
        WeekNumberText = Localizer[nameof(WeekNumberText), GetWeekCount()];
        Months = Localizer[nameof(Months)].Value.Split(',').ToList();
    }
    /// <summary>
    /// 获得/设置 日历框开始时间
    /// </summary>
    protected DateTime StartDate
    {
        get
        {
            var d = Value.AddDays(1 - Value.Day);
            d = d.AddDays(0 - (int)d.DayOfWeek);
            return d;
        }
    }

    /// <summary>
    /// 获得 当前周数
    /// </summary>
    protected int GetWeekCount()
    {
        var gc = new GregorianCalendar();
        return gc.GetWeekOfYear(Value, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
    }

    /// <summary>
    /// 获得/设置 日历框结束时间
    /// </summary>
    protected DateTime EndDate => StartDate.AddDays(42);

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// 获得/设置 值改变时回调委托
    /// </summary>
    [Parameter]
    public EventCallback<DateTime> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示周视图 默认为 CalendarVieModel.Month 月视图
    /// </summary>
    [Parameter]
    public CalendarViewModel ViewModel { get; set; }

    /// <summary>
    /// 获得/设置 周内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 选中日期时回调此方法
    /// </summary>
    /// <param name="value"></param>
    protected async Task OnCellClickCallback(DateTime value)
    {
        Value = value;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        StateHasChanged();
    }

    /// <summary>
    /// 右侧快捷切换年按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected void OnChangeYear(int offset)
    {
        Value = Value.AddYears(offset);
    }

    /// <summary>
    /// 右侧快捷切换月按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected void OnChangeMonth(int offset)
    {
        if (offset == 0)
        {
            Value = DateTime.Today;
        }
        else
        {
            Value = Value.AddMonths(offset);
        }
    }

    /// <summary>
    /// 右侧快捷切换周按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected void OnChangeWeek(int offset)
    {
        if (offset == 0)
        {
            Value = DateTime.Today;
        }
        else
        {
            Value = Value.AddDays(offset);
        }
        WeekNumberText = Localizer[nameof(WeekNumberText), GetWeekCount()];
    }
}
