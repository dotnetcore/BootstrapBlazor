// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 日期选择组件
    /// </summary>
    public sealed partial class DatePickerBody
    {
        /// <summary>
        /// 获得/设置 日历框开始时间
        /// </summary>
        private DateTime StartDate
        {
            get
            {
                if (CurrentDate == DateTime.MinValue)
                {
                    CurrentDate = DateTime.Today;
                }

                var d = CurrentDate.AddDays(1 - CurrentDate.Day);
                d = d.AddDays(0 - (int)d.DayOfWeek);
                return d;
            }
        }

        /// <summary>
        /// 获得/设置 日历框结束时间
        /// </summary>
        private DateTime EndDate => StartDate.AddDays(42);

        /// <summary>
        /// 获得/设置 当前日历框月份
        /// </summary>
        private DateTime CurrentDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// 获得/设置 是否显示时刻选择框
        /// </summary>
        private bool ShowTimePicker { get; set; }

        /// <summary>
        /// 获得/设置 组件样式
        /// </summary>
        private string? ClassName => CssBuilder.Default("picker-panel date-picker")
            .AddClass("d-none", !IsShown)
            .Build();

        /// <summary>
        /// 获得/设置 日期样式
        /// </summary>
        private string? GetDayClass(DateTime day) => CssBuilder.Default("")
            .AddClass("prev-month", day.Month < CurrentDate.Month)
            .AddClass("next-month", day.Month > CurrentDate.Month)
            .AddClass("current", day == OriginaValue && !IsRange && day.Month == CurrentDate.Month)
            .AddClass("start", IsRange && day == Ranger!.SelectedValue.Start)
            .AddClass("end", IsRange && day == Ranger!.SelectedValue!.End.Date)
            .AddClass("range", IsRange && CurrentDate.Month >= Ranger!.SelectedValue.Start.Month && (Ranger!.SelectedValue.Start != DateTime.MinValue) && (Ranger!.SelectedValue.End != DateTime.MinValue) && (day.Ticks >= Ranger!.SelectedValue.Start.Ticks) && (day.Ticks <= Ranger!.SelectedValue.End.Ticks))
            .AddClass("today", day == DateTime.Today)
            .AddClass("disabled", IsDisabled(day))
            .Build();

        private bool IsDisabled(DateTime day) => (MinValue != null && MaxValue != null) && (day < MinValue || day > MaxValue);

        /// <summary>
        /// 获得 年月日时分秒视图样式
        /// </summary>
        private string? DateTimeViewClassName => CssBuilder.Default("date-picker-time-header")
            .AddClass("d-none", ViewModel != DatePickerViewModel.DateTime)
            .AddClass("is-open", ShowTimePicker)
            .Build();
        /// <summary>
        /// 获得 上一月按钮样式
        /// </summary>
        private string? PrevMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-left")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 下一月按钮样式
        /// </summary>
        private string? NextMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-right")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        private string? DateViewClassName => CssBuilder.Default("date-table")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        private string? YearViewClassName => CssBuilder.Default("year-table")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Year)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        private string? MonthViewClassName => CssBuilder.Default("month-table")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        private string? CurrentMonthViewClassName => CssBuilder.Default("date-picker-header-label")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Date)
            .Build();

        [NotNull]
        private string? YearText { get; set; }

        /// <summary>
        /// 获得 年显示文字
        /// </summary>
        private string? YearString => CurrentViewModel switch
        {
            DatePickerViewModel.Year => GetYearPeriod(),
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

        private bool IsRange { get; set; }

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        private DatePickerViewModel CurrentViewModel { get; set; }

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        [Parameter]
        public DatePickerViewModel ViewModel { get; set; } = DatePickerViewModel.Date;

        /// <summary>
        /// 获得/设置 日期格式字符串 默认为 "yyyy-MM-dd"
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DateFormat { get; set; }

        /// <summary>
        /// 获得/设置 是否显示快捷侧边栏 默认不显示
        /// </summary>
        [Parameter]
        public bool ShowSidebar { get; set; }

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
        /// 获得/设置 是否显示 Footer 区域 默认为 true 显示
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; } = true;

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
        /// 获得/设置 是否显示本组件默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool IsShown { get; set; }

        /// <summary>
        /// 获得/设置 是否允许为空 默认 false 不允许为空
        /// </summary>
        [Parameter]
        public bool AllowNull { get; set; }

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

        private DateTime OriginaValue { get; set; }

        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public DateTime Value
        {
            get { return CurrentDate.AddTicks(CurrentTime.Ticks); }
            set
            {
                OriginaValue = value.Date;
                CurrentDate = value.Date;
                CurrentTime = value - CurrentDate;
            }
        }

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
        /// 获得/设置 是否为 Range 内使用 默认为 false
        /// </summary>
        [CascadingParameter]
        public DateTimeRange? Ranger { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<DateTimePicker<DateTime>>? Localizer { get; set; }

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
        private string? Weekago { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            IsRange = Ranger != null;

            CurrentViewModel = ViewModel;

            // 计算开始与结束时间 每个组件显示 6 周数据
            if (Value == DateTime.MinValue)
            {
                Value = DateTime.Today;
            }

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
            Weekago ??= Localizer[nameof(Weekago)];
        }

        /// <summary>
        /// 点击上一年按钮时调用此方法
        /// </summary>
        private void OnClickPrevYear()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentViewModel == DatePickerViewModel.Year ? CurrentDate.AddYears(-20) : CurrentDate.AddYears(-1);
            Ranger?.UpdateStart(CurrentDate);
        }

        /// <summary>
        /// 点击上一月按钮时调用此方法
        /// </summary>
        private void OnClickPrevMonth()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentDate.AddMonths(-1);
            Ranger?.UpdateStart(CurrentDate);
        }

        /// <summary>
        /// 点击下一年按钮时调用此方法
        /// </summary>
        private void OnClickNextYear()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentViewModel == DatePickerViewModel.Year ? CurrentDate.AddYears(20) : CurrentDate.AddYears(1);
            Ranger?.UpdateEnd(CurrentDate);
        }

        /// <summary>
        /// 点击下一月按钮时调用此方法
        /// </summary>
        private void OnClickNextMonth()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentDate.AddMonths(1);
            Ranger?.UpdateEnd(CurrentDate);
        }

        /// <summary>
        /// Day 选择时触发此方法
        /// </summary>
        /// <param name="d"></param>
        private async Task OnClickDateTime(DateTime d)
        {
            ShowTimePicker = false;
            CurrentDate = d;
            OriginaValue = d;
            Ranger?.UpdateValue(d);
            if (!IsRange)
            {
                if (!ShowFooter) await ClickConfirmButton();
                StateHasChanged();
            }
        }

        private async Task OnClickShortLink(DateTime d)
        {
            await OnClickDateTime(d);

            if (ShowFooter) await ClickConfirmButton();
        }

        /// <summary>
        /// 设置组件显示视图方法
        /// </summary>
        /// <param name="view"></param>
        private Task SwitchView(DatePickerViewModel view)
        {
            ShowTimePicker = false;
            CurrentViewModel = view;
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置组件显示视图方法
        /// </summary>
        /// <param name="view"></param>
        /// <param name="d"></param>
        private void SwitchView(DatePickerViewModel view, DateTime d)
        {
            CurrentDate = d;
            SwitchView(view);
        }

        /// <summary>
        /// 通过当前时间计算年跨度区间
        /// </summary>
        /// <returns></returns>
        private string GetYearPeriod()
        {
            var start = CurrentDate.AddYears(0 - CurrentDate.Year % 20).Year;
            return string.Format(YearPeriodText, start, start + 19);
        }

        /// <summary>
        /// 获取 年视图下的年份
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private DateTime GetYear(int year) => CurrentDate.AddYears(year - (CurrentDate.Year % 20));

        /// <summary>
        /// 获取 年视图下月份单元格显示文字
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private string? GetYearText(int year) => $"{GetYear(year).Year}";

        /// <summary>
        /// 获取 年视图下的年份单元格样式
        /// </summary>
        /// <returns></returns>
        private string? GetYearClassName(int year) => CssBuilder.Default()
            .AddClass("current", CurrentDate.AddYears(year - (CurrentDate.Year % 20)).Year == Value.Year)
            .AddClass("today", CurrentDate.AddYears(year - (CurrentDate.Year % 20)).Year == DateTime.Today.Year)
            .Build();

        /// <summary>
        /// 获取 年视图下的月份
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private DateTime GetMonth(int month) => CurrentDate.AddMonths(month - CurrentDate.Month);

        /// <summary>
        /// 获取 月视图下的月份单元格样式
        /// </summary>
        /// <returns></returns>
        private string? GetMonthClassName(int month) => CssBuilder.Default()
            .AddClass("current", CurrentDate.Year == Value.Year && month == Value.Month)
            .AddClass("today", CurrentDate.Year == DateTime.Today.Year && month == DateTime.Today.Month)
            .Build();

        /// <summary>
        /// 获取 日视图下日单元格显示文字
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private static string? GetDayText(int day) => $"{day}";

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
            Value = ViewModel switch
            {
                DatePickerViewModel.DateTime => DateTime.Now,
                _ => DateTime.Today
            };
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
                await OnClear.Invoke();
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
                await OnConfirm.Invoke();
            }
        }

        private bool Validate() => (!MinValue.HasValue || Value >= MinValue.Value) && (!MaxValue.HasValue || Value <= MaxValue.Value);

        /// <summary>
        /// 
        /// </summary>
        private void OnTimePickerClose()
        {
            ShowTimePicker = false;
            StateHasChanged();
        }
    }
}
