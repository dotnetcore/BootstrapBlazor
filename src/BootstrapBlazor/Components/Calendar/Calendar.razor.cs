// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    private static string? GetCalendarCellString(CalendarCellValue item) => CssBuilder.Default()
            .AddClass("prev", item.CellValue.Month < item.CalendarValue.Month)
            .AddClass("next", item.CellValue.Month > item.CalendarValue.Month)
            .AddClass("current", item.CellValue.Month == item.CalendarValue.Month)
            .AddClass("is-selected", item.CellValue.Ticks == item.CalendarValue.Ticks)
            .AddClass("is-today", item.CellValue.Ticks == DateTime.Today.Ticks)
            .Build();

    private string? ClassString => CssBuilder.Default("calendar")
        .AddClassFromAttributes(AdditionalAttributes)
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
        WeekLists = GetWeekList();
        PreviousWeek = Localizer[nameof(PreviousWeek)];
        NextWeek = Localizer[nameof(NextWeek)];
        WeekText = Localizer[nameof(WeekText)];
        WeekHeaderText = Localizer[nameof(WeekHeaderText)];
        WeekNumberText = Localizer[nameof(WeekNumberText), GetWeekCount()];
        Months = [.. Localizer[nameof(Months)].Value.Split(',')];
    }

    /// <summary>
    /// 获得/设置 日历框开始时间
    /// </summary>
    protected DateTime StartDate
    {
        get
        {
            var d = Value.AddDays(1 - Value.Day);
            d = d.AddDays((int)FirstDayOfWeek - (int)d.DayOfWeek);
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
    /// 获得/设置 值改变时回调委托
    /// </summary>
    [Parameter]
    public Func<DateTime, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示周视图 默认为 <see cref="CalendarViewMode.Month"/> 月视图
    /// </summary>
    [Parameter]
    public CalendarViewMode ViewMode { get; set; }

    /// <summary>
    /// 获得/设置 周内容 <see cref="CalendarViewMode.Week"/> 时有效
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 列头模板
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Body 模板仅 <see cref="CalendarViewMode.Month"/> 有效
    /// </summary>
    [Parameter]
    public RenderFragment<BodyTemplateContext>? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 单元格模板
    /// </summary>
    [Parameter]
    public RenderFragment<CalendarCellValue>? CellTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示年按钮
    /// </summary>
    [Parameter]
    public bool ShowYearButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 星期第一天 默认 <see cref="DayOfWeek.Sunday"/>
    /// </summary>
    [Parameter]
    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Sunday;

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
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }

    /// <summary>
    /// 右侧快捷切换年按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected async Task OnChangeYear(int offset)
    {
        Value = Value.AddYears(offset);
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }

    /// <summary>
    /// 右侧快捷切换月按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected async Task OnChangeMonth(int offset)
    {
        if (offset == 0)
        {
            Value = DateTime.Today;
        }
        else
        {
            Value = Value.AddMonths(offset);
        }
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }

    /// <summary>
    /// 右侧快捷切换周按钮回调此方法
    /// </summary>
    /// <param name="offset"></param>
    protected async Task OnChangeWeek(int offset)
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
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }

    private CalendarCellValue CreateCellValue(DateTime cellValue)
    {
        var val = new CalendarCellValue()
        {
            CellValue = cellValue,
            CalendarValue = Value
        };
        val.DefaultCss = GetCalendarCellString(val);
        return val;
    }

    private BodyTemplateContext GetBodyTemplateContext(DateTime week)
    {
        var context = new BodyTemplateContext();
        context.Values.AddRange(Enumerable.Range(0, 7).Select(i => CreateCellValue(week.AddDays(i))));
        return context;
    }
    private List<string> GetWeekList()
    {
        var list = Localizer[nameof(WeekLists)].Value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        // 调整顺序
        var firstDayIndex = (int)FirstDayOfWeek;
        return [.. list.Skip(firstDayIndex), .. list.Take(firstDayIndex)];
    }
}
