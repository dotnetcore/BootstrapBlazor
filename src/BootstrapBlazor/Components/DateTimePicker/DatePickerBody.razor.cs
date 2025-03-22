// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
            d = GetSafeDayDateTime(d, (int)FirstDayOfWeek - (int)d.DayOfWeek);
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
    /// 获得/设置 当前选中时间 未点击确认时 与 Value 可能不一致
    /// </summary>
    private DateTime SelectValue { get; set; }

    /// <summary>
    /// 获得/设置 是否显示时刻选择框
    /// </summary>
    private bool ShowTimePicker { get; set; }

    private string? ClassString => CssBuilder.Default("picker-panel")
        .AddClass("is-sidebar", ShowSidebar)
        .AddClass("is-lunar", ShowLunar)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 日期样式
    /// </summary>
    private string? GetDayClass(DateTime day, bool overflow) => CssBuilder.Default()
        .AddClass("prev-month", day.Month < CurrentDate.Month)
        .AddClass("next-month", day.Month > CurrentDate.Month)
        .AddClass("current", day.Date == SelectValue.Date && Ranger == null && day.Month == SelectValue.Month && !overflow)
        .AddClass("start", Ranger != null && day == Ranger.SelectedValue.Start.Date)
        .AddClass("end", Ranger != null && day == Ranger.SelectedValue.End.Date)
        .AddClass("range", Ranger != null && day >= Ranger.SelectedValue.Start.Date && day <= Ranger.SelectedValue.End.Date)
        .AddClass("today", day == DateTime.Today)
        .AddClass("disabled", IsDisabled(day) || overflow)
        .Build();

    private string? WrapperClassString => CssBuilder.Default("picker-panel-body-main-wrapper")
        .AddClass("is-open", ShowTimePicker)
        .Build();

    private bool IsDisabled(DateTime day) => day < MinValue || day > MaxValue || IsDisableDay(day);

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
    private string? CurrentMonthViewClassName => CssBuilder.Default("picker-panel-header-label")
        .AddClass("d-none", CurrentViewMode is DatePickerViewMode.Year or DatePickerViewMode.Month)
        .Build();

    private ClockPicker? TimePickerPanel { get; set; }

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
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    private DatePickerViewMode CurrentViewMode { get; set; }

    /// <summary>
    /// 获得/设置 组件显示模式 默认为显示年月日模式
    /// </summary>
    [Parameter]
    public DatePickerViewMode ViewMode { get; set; } = DatePickerViewMode.Date;

    /// <summary>
    /// 获得/设置 日期时间格式字符串 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateTimeFormat { get; set; }

    /// <summary>
    /// 获得/设置 日期格式字符串 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 获得/设置 时间格式字符串 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TimeFormat { get; set; }

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
    [Obsolete("已过期，请使用 ShowClearButton 代替 Please use ShowClearButton")]
    [ExcludeFromCodeCoverage]
    public bool AllowNull
    {
        get => ShowClearButton;
        set => ShowClearButton = value;
    }

    /// <summary>
    /// 获得/设置 是否显示 Clear 按钮 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowClearButton { get; set; }

    /// <summary>
    /// 获得/设置 点击日期时是否自动关闭弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool AutoClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 清空按钮回调委托
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
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 年月改变时回调方法
    /// </summary>
    [Parameter]
    public Func<DateTime, Task>? OnDateChanged { get; set; }

    /// <summary>
    /// 获得/设置 日单元格模板
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? DayTemplate { get; set; }

    /// <summary>
    /// 获得/设置 禁用日单元格模板
    /// </summary>
    [Parameter]
    public RenderFragment<DateTime>? DayDisabledTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国阴历历法 默认 false
    /// </summary>
    /// <remarks>日期范围 1901 年 2 月 19 日 - 2101 年 1 月 28 日</remarks>
    [Parameter]
    public bool ShowLunar { get; set; }

    /// <summary>
    /// 获得/设置 是否显示中国 24 节气 默认 false
    /// </summary>
    [Parameter]
    public bool ShowSolarTerm { get; set; }

    /// <summary>
    /// 获得/设置 是否显示节日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowFestivals { get; set; }

    /// <summary>
    /// 获得/设置 是否显示休假日 默认 false
    /// </summary>
    [Parameter]
    public bool ShowHolidays { get; set; }

    /// <summary>
    /// 获得/设置 是否为 Range 内使用 默认为 false
    /// </summary>
    [CascadingParameter]
    private DateTimeRange? Ranger { get; set; }

    /// <summary>
    /// 获取/设置 获得月自定义禁用日期回调方法，默认 null 内部默认启用数据缓存 可通过 <see cref="EnableDisabledDaysCache"/> 参数关闭
    /// </summary>
    [Parameter]
    public Func<DateTime, DateTime, Task<List<DateTime>>>? OnGetDisabledDaysCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否启用获得自定义禁用日期缓存
    /// </summary>
    [Parameter]
    public bool EnableDisabledDaysCache { get; set; } = true;

    /// <summary>
    /// 获得/设置 星期第一天 默认 <see cref="DayOfWeek.Sunday"/>
    /// </summary>
    [Parameter]
    public DayOfWeek FirstDayOfWeek { get; set; } = DayOfWeek.Sunday;

    [Inject]
    [NotNull]
    private ICalendarFestivals? CalendarFestivals { get; set; }

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

    private TimePickerOption TimePickerOption { get; } = new();

    private Dictionary<DatePickerViewMode, List<DatePickerViewMode>> AllowSwitchModes { get; } = new Dictionary<DatePickerViewMode, List<DatePickerViewMode>>
    {
        [DatePickerViewMode.DateTime] =
        [
            DatePickerViewMode.DateTime,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Date] =
        [
            DatePickerViewMode.Date,
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Month] =
        [
            DatePickerViewMode.Month,
            DatePickerViewMode.Year
        ],
        [DatePickerViewMode.Year] =
        [
            DatePickerViewMode.Year
        ]
    };

    private bool IsDateTimeMode => ViewMode == DatePickerViewMode.DateTime && CurrentViewMode == DatePickerViewMode.DateTime;

    private readonly Dictionary<string, List<DateTime>> _monthDisabledDaysCache = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CurrentViewMode = ViewMode;
        CurrentDate = Value.Date;
        CurrentTime = Value.TimeOfDay;

        SelectValue = Value;

        DatePlaceHolder ??= Localizer[nameof(DatePlaceHolder)];
        TimePlaceHolder ??= Localizer[nameof(TimePlaceHolder)];
        DateTimeFormat ??= Localizer[nameof(DateTimeFormat)];
        DateFormat ??= Localizer[nameof(DateFormat)];
        TimeFormat ??= Localizer[nameof(TimeFormat)];

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
        MonthLists = [.. Localizer[nameof(MonthLists)].Value.Split(',')];
        Months = [.. Localizer[nameof(Months)].Value.Split(',')];
        WeekLists = GetWeekList();

        Today ??= Localizer[nameof(Today)];
        Yesterday ??= Localizer[nameof(Yesterday)];
        Week ??= Localizer[nameof(Week)];

        PreviousYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousYearIcon);
        PreviousMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyPreviousMonthIcon);
        NextMonthIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextMonthIcon);
        NextYearIcon ??= IconTheme.GetIconByKey(ComponentIcons.DatePickBodyNextYearIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _render = false;
        await UpdateDisabledDaysCache(true);
        _render = true;
    }

    private bool _render = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override bool ShouldRender() => _render;

    private List<string> GetWeekList()
    {
        var list = Localizer[nameof(WeekLists)].Value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        // 调整顺序
        var firstDayIndex = (int)FirstDayOfWeek;
        return [.. list.Skip(firstDayIndex), .. list.Take(firstDayIndex)];
    }

    private async Task UpdateDisabledDaysCache(bool force)
    {
        if (OnGetDisabledDaysCallback != null)
        {
            var key = $"{StartDate:yyyyMMdd}-{EndDate:yyyyMMdd}";
            if (force && EnableDisabledDaysCache == false)
            {
                _monthDisabledDaysCache.Remove(key);
            }
            if (!_monthDisabledDaysCache.TryGetValue(key, out var disabledDays))
            {
                disabledDays = await OnGetDisabledDaysCallback(StartDate, EndDate);
                _monthDisabledDaysCache.TryAdd(key, disabledDays);
            }
        }
    }

    /// <summary>
    /// 判定当前日期是否为禁用日期
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public bool IsDisableDay(DateTime val)
    {
        bool ret = false;
        if (_monthDisabledDaysCache.TryGetValue($"{StartDate:yyyyMMdd}-{EndDate:yyyyMMdd}", out var disabledDays))
        {
            ret = disabledDays.Contains(val);
        }
        return ret;
    }

    /// <summary>
    /// 清除内部缓存方法
    /// </summary>
    public void ClearDisabledDays() => _monthDisabledDaysCache.Clear();

    private async Task OnValueChanged()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    /// <summary>
    /// 点击上一年按钮时调用此方法
    /// </summary>
    private async Task OnClickPrevYear()
    {
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year
            ? GetSafeYearDateTime(CurrentDate, -20)
            : GetSafeYearDateTime(CurrentDate, -1);

        _render = false;
        await UpdateDisabledDaysCache(false);
        _render = true;

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击上一月按钮时调用此方法
    /// </summary>
    private async Task OnClickPrevMonth()
    {
        CurrentDate = CurrentDate.GetSafeMonthDateTime(-1);

        _render = false;
        await UpdateDisabledDaysCache(false);
        _render = true;

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击下一年按钮时调用此方法
    /// </summary>
    private async Task OnClickNextYear()
    {
        CurrentDate = CurrentViewMode == DatePickerViewMode.Year
            ? GetSafeYearDateTime(CurrentDate, 20)
            : GetSafeYearDateTime(CurrentDate, 1);

        _render = false;
        await UpdateDisabledDaysCache(false);
        _render = true;

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// 点击下一月按钮时调用此方法
    /// </summary>
    private async Task OnClickNextMonth()
    {
        CurrentDate = CurrentDate.GetSafeMonthDateTime(1);

        _render = false;
        await UpdateDisabledDaysCache(false);
        _render = true;

        if (OnDateChanged != null)
        {
            await OnDateChanged(CurrentDate);
        }
    }

    /// <summary>
    /// Day 选择时触发此方法
    /// </summary>
    /// <param name="d"></param>
    private async Task OnClickDateTime(DateTime d)
    {
        CurrentDate = d;
        SelectValue = d;

        if (Ranger != null || ShouldConfirm)
        {
            await ClickConfirmButton();
        }
        else
        {
            StateHasChanged();
        }
    }

    [ExcludeFromCodeCoverage]
    private async Task OnTimeChanged(TimeSpan time)
    {
        CurrentTime = time;
        if (ShouldConfirm)
        {
            await ClickConfirmButton();
        }
        else
        {
            StateHasChanged();
        }
    }

    private bool ShouldConfirm => !IsDateTimeMode && (AutoClose || ShowFooter == false);

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    private async Task SwitchView(DatePickerViewMode view)
    {
        if (AllowSwitchModes[ViewMode].Contains(view))
        {
            CurrentViewMode = view;
            if (view is DatePickerViewMode.Date or DatePickerViewMode.DateTime)
            {
                // update disabled days cache
                await UpdateDisabledDaysCache(false);
            }
            StateHasChanged();
        }
        else if (AutoClose)
        {
            await ClickConfirmButton();
        }
    }

    private void SwitchTimeView()
    {
        ShowTimePicker = true;
    }

    internal void SwitchDateView()
    {
        ShowTimePicker = false;
        StateHasChanged();
    }

    /// <summary>
    /// 设置组件显示视图方法
    /// </summary>
    /// <param name="view"></param>
    /// <param name="d"></param>
    private async Task SwitchView(DatePickerViewMode view, DateTime d)
    {
        CurrentDate = d;

        if (Ranger != null && (ViewMode == DatePickerViewMode.Year || ViewMode == DatePickerViewMode.Month))
        {
            await ClickConfirmButton();
            StateHasChanged();
        }
        else
        {
            await SwitchView(view);
        }
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
        .AddClass("current", GetSafeYearDateTime(SelectValue, year - (SelectValue.Year % 20)).Year == SelectValue.Year)
        .AddClass("today", GetSafeYearDateTime(Value, year - (Value.Year % 20)).Year == DateTime.Today.Year)
        .AddClass("disabled", overflow)
        .Build();

    /// <summary>
    /// 获取 年视图下的月份
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    private DateTime GetMonth(int month) => CurrentDate.GetSafeMonthDateTime(month - CurrentDate.Month);

    /// <summary>
    /// 获取 月视图下的月份单元格样式
    /// </summary>
    /// <returns></returns>
    private string? GetMonthClassName(int month) => CssBuilder.Default()
        .AddClass("current", month == SelectValue.Month)
        .AddClass("today", Value.Year == DateTime.Today.Year && month == DateTime.Today.Month)
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
    /// 点击 此刻时调用此方法
    /// </summary>
    private async Task ClickNowButton()
    {
        var val = ViewMode switch
        {
            DatePickerViewMode.DateTime => DateTime.Now,
            _ => DateTime.Today
        };
        CurrentDate = val.Date;
        CurrentTime = val.TimeOfDay;

        await ClickConfirmButton();
    }

    private async Task ClickConfirmButton()
    {
        ResetTimePickerPanel();

        if (Validate())
        {
            Value = CurrentDate + CurrentTime;
            await OnValueChanged();
        }
        if (OnConfirm != null)
        {
            await OnConfirm();
        }
    }

    /// <summary>
    /// 点击 清除按钮调用此方法
    /// </summary>
    /// <returns></returns>
    private async Task ClickClearButton()
    {
        // 关闭 TimerPicker
        ShowTimePicker = false;

        CurrentDate = DateTime.MinValue;
        CurrentTime = TimeSpan.Zero;

        Value = CurrentDate + CurrentTime;
        await OnValueChanged();

        if (OnClear != null)
        {
            await OnClear();
        }
    }

    private async Task OnClickSidebarButton(DateTime d)
    {
        CurrentDate = d.Date;
        CurrentTime = d.TimeOfDay;

        await ClickConfirmButton();
    }

    private void ResetTimePickerPanel()
    {
        // 关闭 TimerPicker
        ShowTimePicker = false;

        TimePickerPanel?.Reset();
    }

    private bool Validate() => !IsDisabled(SelectValue);

    /// <summary>
    /// 获得安全的年数据
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    protected static DateTime GetSafeYearDateTime(DateTime dt, int year)
    {
        var @base = year switch
        {
            < 0 => DateTime.MinValue.AddYears(0 - year) < dt ? dt.AddYears(year) : DateTime.MinValue.Date,
            > 0 => DateTime.MaxValue.AddYears(0 - year) > dt ? dt.AddYears(year) : DateTime.MaxValue.Date,
            _ => dt
        };
        return @base;
    }

    /// <summary>
    /// 获得安全的日视图日期
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    private static DateTime GetSafeDayDateTime(DateTime dt, int day)
    {
        var @base = day switch
        {
            < 0 => DateTime.MinValue.AddDays(0 - day) < dt ? dt.AddDays(day) : DateTime.MinValue,
            > 0 => DateTime.MaxValue.AddDays(0 - day) > dt ? dt.AddDays(day) : DateTime.MaxValue,
            _ => dt
        };
        return @base;
    }

    /// <summary>
    /// 判断日视图是否溢出方法
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    private static bool IsDayOverflow(DateTime dt, int day) => DateTime.MaxValue.AddDays(0 - day) < dt;

    /// <summary>
    /// 判断年视图是否溢出方法
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    private static bool IsYearOverflow(DateTime dt, int year)
    {
        var ret = year switch
        {
            < 0 => DateTime.MinValue.AddYears(0 - year) > dt,
            > 0 => DateTime.MaxValue.AddYears(0 - year) < dt,
            _ => false
        };
        return ret;
    }

    private string GetLunarText(DateTime dateTime) => dateTime.ToLunarText(ShowSolarTerm, ShowFestivals ? CalendarFestivals : null);
}
