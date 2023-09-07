// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 日期选择组件
/// </summary>
public partial class DatePickerBody
{
    /// <summary>
    /// 获得/设置 日历框开始时间
    /// </summary>
    private DateTime StartDate
    {
        get
        {
            var d = GetSafeDayDateTime(CurrentDate, 1 - CurrentDate.Day);
            d = GetSafeDayDateTime(d, 0 - (int)d.DayOfWeek);
            return d;
        }
    }

    /// <summary>
    /// 获得/设置 日历框结束时间
    /// </summary>
    private DateTime EndDate => GetSafeDayDateTime(StartDate, 42);

    /// <summary>
    /// 获得/设置 当前日历框月份
    /// </summary>
    private DateTime CurrentDate { get; set; }

    /// <summary>
    /// 获得/设置 当前日历框时刻值
    /// </summary>
    private TimeSpan CurrentTime { get; set; }

    /// <summary>
    /// 获得/设置 是否显示时刻选择框
    /// </summary>
    private bool ShowTimePicker { get; set; }

    private string? ClassString => CssBuilder.Default("picker-panel")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 日期样式
    /// </summary>
    private string? GetDayClass(DateTime day, bool overflow) => CssBuilder.Default("")
        .AddClass("prev-month", day.Month < CurrentDate.Month)
        .AddClass("next-month", day.Month > CurrentDate.Month)
        .AddClass("current", day == Value && Ranger == null && day.Month == CurrentDate.Month && !overflow)
        .AddClass("start", Ranger != null && day == Ranger.SelectedValue.Start!.Value.Date)
        .AddClass("end", Ranger != null && day == Ranger.SelectedValue.End!.Value.Date)
        .AddClass("range", Ranger != null && day >= Ranger.SelectedValue.Start && day <= Ranger.SelectedValue.End)
        .AddClass("today", day == DateTime.Today)
        .AddClass("disabled", IsDisabled(day) || overflow)
        .Build();

    private bool IsDisabled(DateTime day) => (MinValue.HasValue && day < MinValue.Value) || (MaxValue.HasValue && day > MaxValue.Value);

    /// <summary>
    /// 获得 年月日时分秒视图样式
    /// </summary>
    private string? DateTimeViewClassName => CssBuilder.Default("date-picker-time-header")
        .AddClass("d-none", ViewMode != DatePickerViewMode.DateTime)
        .AddClass("is-open", ShowTimePicker)
        .Build();

    /// <summary>
    /// 获得 上一月按钮样式
    /// </summary>
    private string? PrevMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-left")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 下一月按钮样式
    /// </summary>
    private string? NextMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-right")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? DateViewClassName => CssBuilder.Default("date-table")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? YearViewClassName => CssBuilder.Default("year-table")
        .AddClass("d-none", CurrentViewMode != DatePickerViewMode.Year)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? MonthViewClassName => CssBuilder.Default("month-table")
        .AddClass("d-none", CurrentViewMode != DatePickerViewMode.Month)
        .Build();

    /// <summary>
    /// 获得 年月日显示表格样式
    /// </summary>
    private string? CurrentMonthViewClassName => CssBuilder.Default("date-picker-header-label")
        .AddClass("d-none", CurrentViewMode == DatePickerViewMode.Year || CurrentViewMode == DatePickerViewMode.Month)
        .Build();

    [NotNull]
    private string? YearText { get; set; }

    /// <summary>
    /// 获得 年显示文字
    /// </summary>
    private string? YearString => CurrentViewMode switch
    {
        DatePickerViewMode.Year => GetYearPeriod(),
        _ => string.Format(YearText, CurrentDate.Year)
    };

    [NotNull]
    private string? MonthText { get; set; }

    private string MonthString => string.Format(MonthText, Months.ElementAtOrDefault(CurrentDate.Month - 1));

    [NotNull]
    private string? YearPeriodText { get; set; }

    /// <summary>
    /// 获得 日期数值字符串
    /// </summary>
    private string? DateValueString => CurrentDate.ToString(DateFormat);

    /// <summary>
    /// 获得 日期数值字符串
    /// </summary>
    private string? TimeValueString => CurrentTime.ToString(TimeFormat);

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    private DatePickerViewMode CurrentViewMode { get; set; }

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// 获得/设置 日期格式字符串 默认为 "yyyy-MM-dd"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 获得/设置 是否显示快捷侧边栏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSidebar { get; set; }

    /// <summary>
    /// 获得/设置 侧边栏模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<Func<DateTime, Task>>? SidebarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示左侧控制按钮 默认显示
    /// </summary>
    [Parameter]
    public bool ShowLeftButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示右侧控制按钮 默认显示
    /// </summary>
    [Parameter]
    public bool ShowRightButtons { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Footer 区域 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 时间格式字符串 默认为 "hh\\:mm\\:ss"
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间 PlaceHolder 字符串
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimePlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 日期 PlaceHolder 字符串
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DatePlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否允许为空 默认 false 不允许为空
    /// </summary>
    [Parameter]
    public bool AllowNull { get; set; }

    /// <summary>
    /// 获得/设置 是否点击确认关闭弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnClear { get; set; }

    /// <summary>
    /// 获得/设置 清空按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 此刻按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NowButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确定按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值改变时回调委托供双向绑定使用
    /// </summary>
    [Parameter]
    public EventCallback<DateTime> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最大值
    /// </summary>
    [Parameter]
    public DateTime? MaxValue { get; set; }

    /// <summary>
    /// 获得/设置 当前日期最小值
    /// </summary>
    [Parameter]
    public DateTime? MinValue { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Parameter]
    public string? PreviousYearIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Parameter]
    public string? NextYearIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Parameter]
    public string? PreviousMonthIcon { get; set; }

    /// <summary>
    /// 获得/设置 上一年图标
    /// </summary>
    [Parameter]
    public string? NextMonthIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否为 Range 内使用 默认为 false
    /// </summary>
    [CascadingParameter]
    private DateTimeRange? Ranger { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [NotNull]
    private string? AiraPrevYearLabel { get; set; }

    [NotNull]
    private string? AiraNextYearLabel { get; set; }

    [NotNull]
    private string? AiraPrevMonthLabel { get; set; }

    [NotNull]
    private string? AiraNextMonthLabel { get; set; }

    [NotNull]
    private List<string>? Months { get; set; }

    [NotNull]
    private List<string>? MonthLists { get; set; }

    [NotNull]
    private List<string>? WeekLists { get; set; }

    [NotNull]
    private string? Today { get; set; }

    [NotNull]
    private string? Yesterday { get; set; }

    [NotNull]
    private string? Week { get; set; }

    private Dictionary<DatePickerViewMode, List<DatePickerViewMode>> AllowSwitchModes { get; } = new Dictionary<DatePickerViewMode, List<DatePickerViewMode>>
    {
        [DatePickerViewMode.DateTime] = new List<DatePickerViewMode>()
        {
            DatePickerViewMode.DateTime,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        },
        [DatePickerViewMode.Date] = new List<DatePickerViewMode>()
        {
            DatePickerViewMode.Date,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        },
        [DatePickerViewMode.Month] = new List<DatePickerViewMode>()
        {
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        },
        [DatePickerViewMode.Year] = new List<DatePickerViewMode>()
        {
            DatePickerViewMode.Year
        }
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CurrentViewMode = ViewMode;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CurrentDate = Value.Date;
        CurrentTime = Value - CurrentDate;

        DatePlaceHolder ??= Localizer[nameof(DatePlaceHolder)];
        TimePlaceHolder ??= Localizer[nameof(TimePlaceHolder)];
        TimeFormat ??= Localizer[nameof(TimeFormat)];
        DateFormat ??= Localizer[nameof(DateFormat)];

        AiraPrevYearLabel ??= Localizer[nameof(AiraPrevYearLabel)];
        AiraNextYearLabel ??= Localizer[nameof(AiraNextYearLabel)];
        AiraPrevMonthLabel ??= Localizer[nameof(AiraPrevMonthLabel)];
        AiraNextMonthLabel ??= Localizer[nameof(AiraNextMonthLabel)];

        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        NowButtonText ??= Localizer[nameof(NowButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];

        YearText ??= Localizer[nameof(YearText)];
        MonthText ??= Localizer[nameof(MonthText)];
        YearPeriodText ??= Localizer[nameof(YearPeriodText)];
        MonthLists = Localizer[nameof(MonthLists)].Value.Split(',').ToList();
        Months = Localizer[nameof(Months)].Value.Split(',').ToList();
        WeekLists = Localizer[nameof(WeekLists)].Value.Split(',').ToList();

        Today ??= Localizer[nameof(Today)];
        Yesterday ??= Localizer[nameof(Yesterday)];
        Week ??= Localizer[nameof(Week)];

        PreviousYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousYearIcon);
        PreviousMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousMonthIcon);
        NextMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextMonthIcon);
        NextYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextYearIcon);
    }

    private void SetValue(DateTime val)
    {
        if (val != Value)
        {
            Value = val;
            CurrentDate = Value.Date;
            CurrentTime = Value - CurrentDate;
        }
    }

    /// <summary>
    /// 点击上一年按钮时调用此方法
    /// </summary>
    private void OnClickPrevYear()
    {
        ShowTimePicker = false;
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year ? GetSafeYearDateTime(CurrentDate, -20) : GetSafeYearDateTime(CurrentDate, -1);
        Ranger?.UpdateStart(CurrentDate);
    }

    /// <summary>
    /// 点击上一月按钮时调用此方法
    /// </summary>
    private void OnClickPrevMonth()
    {
        ShowTimePicker = false;
        CurrentDate = GetSafeMonthDateTime(CurrentDate, -1);
        Ranger?.UpdateStart(CurrentDate);
    }

    /// <summary>
    /// 点击下一年按钮时调用此方法
    /// </summary>
    private void OnClickNextYear()
    {
        ShowTimePicker = false;
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year ? GetSafeYearDateTime(CurrentDate, 20) : GetSafeYearDateTime(CurrentDate, 1);
        Ranger?.UpdateEnd(CurrentDate);
    }

    /// <summary>
    /// 点击下一月按钮时调用此方法
    /// </summary>
    private void OnClickNextMonth()
    {
        ShowTimePicker = false;
        CurrentDate = GetSafeMonthDateTime(CurrentDate, 1);
        Ranger?.UpdateEnd(CurrentDate);
    }

    /// <summary>
    /// Day 选择时触发此方法
    /// </summary>
    /// <param name="d"></param>
    private async Task OnClickDateTime(DateTime d)
    {
        ShowTimePicker = false;
        SetValue(d + CurrentTime);
        Ranger?.UpdateValue(d);
        if (Ranger == null)
        {
            if (!ShowFooter || AutoClose)
            {
                await ClickConfirmButton();
            }
            else
            {
                StateHasChanged();
            }
        }
    }

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    private async Task SwitchView(DatePickerViewMode view)
    {
        ShowTimePicker = false;
        SetValue(CurrentDate);
        if (AllowSwitchModes[ViewMode].Contains(view))
        {
            CurrentViewMode = view;
            StateHasChanged();
        }
        else if (AutoClose)
        {
            await ClickConfirmButton();
        }
    }

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    /// <param name="d"></param>
    private async Task SwitchView(DatePickerViewMode view, DateTime d)
    {
        CurrentDate = d;
        await SwitchView(view);
    }

    /// <summary>
    /// 通过当前时间计算年跨度区间
    /// </summary>
    /// <returns></returns>
    private string GetYearPeriod()
    {
        var start = GetSafeYearDateTime(CurrentDate, 0 - CurrentDate.Year % 20).Year;
        return string.Format(YearPeriodText, start, start + 19);
    }

    /// <summary>
    /// 获取 年视图下的年份
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private DateTime GetYear(int year) => GetSafeYearDateTime(CurrentDate, year - (CurrentDate.Year % 20));

    /// <summary>
    /// 获取 年视图下月份单元格显示文字
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private string GetYearText(int year) => GetYear(year).Year.ToString();

    /// <summary>
    /// 获取 年视图下的年份单元格样式
    /// </summary>
    /// <returns></returns>
    private string? GetYearClassName(int year, bool overflow) => CssBuilder.Default()
        .AddClass("current", GetSafeYearDateTime(CurrentDate, year - (CurrentDate.Year % 20)).Year == Value.Year)
        .AddClass("today", GetSafeYearDateTime(CurrentDate, year - (CurrentDate.Year % 20)).Year == DateTime.Today.Year)
        .AddClass("disabled", overflow)
        .Build();

    /// <summary>
    /// 获取 年视图下的月份
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private DateTime GetMonth(int month) => GetSafeMonthDateTime(CurrentDate, month - CurrentDate.Month);

    /// <summary>
    /// 获取 月视图下的月份单元格样式
    /// </summary>
    /// <returns></returns>
    private string? GetMonthClassName(int month) => CssBuilder.Default()
        .AddClass("current", month == Value.Month)
        .AddClass("today", CurrentDate.Year == DateTime.Today.Year && month == DateTime.Today.Month)
        .Build();

    /// <summary>
    /// 获取 日视图下日单元格显示文字
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    private static string GetDayText(int day) => day.ToString();

    /// <summary>
    /// 获取 月视图下月份单元格显示文字
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private string GetMonthText(int month) => MonthLists[month - 1];

    /// <summary>
    /// 时刻选择框点击时调用此方法
    /// </summary>
    private void OnClickTimeInput() => ShowTimePicker = true;

    /// <summary>
    /// 点击 此刻时调用此方法
    /// </summary>
    private async Task ClickNowButton()
    {
        var val = ViewMode switch
        {
            DatePickerViewMode.DateTime => DateTime.Now,
            _ => DateTime.Today
        };
        SetValue(val);
        await ClickConfirmButton();
    }

    /// <summary>
    /// 点击 清除按钮调用此方法
    /// </summary>
    /// <returns></returns>
    private async Task ClickClearButton()
    {
        ShowTimePicker = false;
        if (OnClear != null)
        {
            await OnClear();
        }
    }

    /// <summary>
    /// 点击 确认时调用此方法
    /// </summary>
    private async Task ClickConfirmButton()
    {
        ShowTimePicker = false;
        if (Validate() && ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        if (OnConfirm != null)
        {
            await OnConfirm();
        }
    }

    private bool Validate() => (!MinValue.HasValue || Value >= MinValue.Value) && (!MaxValue.HasValue || Value <= MaxValue.Value);

    /// <summary>
    /// 点击时刻窗口关闭处理方法
    /// </summary>
    private void OnTimePickerClose()
    {
        SetValue(CurrentDate + CurrentTime);
        ShowTimePicker = false;
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    protected static DateTime GetSafeYearDateTime(DateTime dt, int year)
    {
        var @base = dt;
        if (year < 0)
        {
            if (DateTime.MinValue.AddYears(0 - year) < dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MinValue.Date;
            }
        }
        else if (year > 0)
        {
            if (DateTime.MaxValue.AddYears(0 - year) > dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MaxValue.Date;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    protected static DateTime GetSafeMonthDateTime(DateTime dt, int month)
    {
        var @base = dt;
        if (month < 0)
        {
            if (DateTime.MinValue.AddMonths(0 - month) < dt)
            {
                @base = dt.AddMonths(month);
            }
            else
            {
                @base = DateTime.MinValue.Date;
            }
        }
        else if (month > 0)
        {
            if (DateTime.MaxValue.AddMonths(0 - month) > dt)
            {
                @base = dt.AddMonths(month);
            }
            else
            {
                @base = DateTime.MaxValue.Date;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    protected static DateTime GetSafeDayDateTime(DateTime dt, int day)
    {
        var @base = dt;
        if (day < 0)
        {
            if (DateTime.MinValue.AddDays(0 - day) < dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MinValue;
            }
        }
        else if (day > 0)
        {
            if (DateTime.MaxValue.AddDays(0 - day) > dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MaxValue;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    protected static bool IsDayOverflow(DateTime dt, int day) => DateTime.MaxValue.AddDays(0 - day) < dt;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    protected static bool IsYearOverflow(DateTime dt, int year)
    {
        var ret = false;
        if (year < 0)
        {
            ret = DateTime.MinValue.AddYears(0 - year) > dt;
        }
        else if (year > 0)
        {
            ret = DateTime.MaxValue.AddYears(0 - year) < dt;
        }
        return ret;
    }
}
