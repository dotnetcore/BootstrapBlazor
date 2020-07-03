using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 日期选择组件
    /// </summary>
    public abstract class DatePickerBodyBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 日历框开始时间
        /// </summary>
        protected DateTime StartDate
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
        protected DateTime EndDate => StartDate.AddDays(42);

        /// <summary>
        /// 获得/设置 当前日历框月份
        /// </summary>
        protected DateTime CurrentDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected TimeSpan CurrentTime { get; set; }

        /// <summary>
        /// 获得/设置 是否显示时刻选择框
        /// </summary>
        protected bool ShowTimePicker { get; set; }

        /// <summary>
        /// 获得/设置 组件样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("picker-panel date-picker")
            .AddClass("d-none", !IsShown)
            .Build();

        /// <summary>
        /// 获得/设置 日期样式
        /// </summary>
        protected string? GetDayClass(DateTime day) => CssBuilder.Default("")
            .AddClass("prev-month", day.Month < CurrentDate.Month)
            .AddClass("next-month", day.Month > CurrentDate.Month)
            .AddClass("current", day.Ticks == CurrentDate.Ticks)
            .AddClass("today", day.Ticks == DateTime.Today.Ticks)
            .Build();

        /// <summary>
        /// 获得 年月日时分秒视图样式
        /// </summary>
        protected string? DateTimeViewClassName => CssBuilder.Default("date-picker-time-header")
            .AddClass("d-none", ViewModel != DatePickerViewModel.DateTime)
            .AddClass("is-open", ShowTimePicker)
            .Build();
        /// <summary>
        /// 获得 上一月按钮样式
        /// </summary>
        protected string? PrevMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-left")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 下一月按钮样式
        /// </summary>
        protected string? NextMonthClassName => CssBuilder.Default("picker-panel-icon-btn pick-panel-arrow-right")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        protected string? DateViewClassName => CssBuilder.Default("date-table")
            .AddClass("d-none", CurrentViewModel == DatePickerViewModel.Year || CurrentViewModel == DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        protected string? YearViewClassName => CssBuilder.Default("year-table")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Year)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        protected string? MonthViewClassName => CssBuilder.Default("month-table")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Month)
            .Build();

        /// <summary>
        /// 获得 年月日显示表格样式
        /// </summary>
        protected string? CurrentMonthViewClassName => CssBuilder.Default("date-picker-header-label")
            .AddClass("d-none", CurrentViewModel != DatePickerViewModel.Date)
            .Build();

        /// <summary>
        /// 获得 年显示文字
        /// </summary>
        protected string? YearString => CurrentViewModel switch
        {
            DatePickerViewModel.Year => $"{GetYearPeriod()}",
            _ => $"{CurrentDate.Year} 年"
        };

        /// <summary>
        /// 获得 日期数值字符串
        /// </summary>
        protected string? DateValueString => CurrentDate.ToString(DateFormat);

        /// <summary>
        /// 获得 日期数值字符串
        /// </summary>
        protected string? TimeValueString => CurrentTime.ToString(TimeFormat);

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        protected DatePickerViewModel CurrentViewModel { get; set; }

        /// <summary>
        /// 获得/设置 组件显示模式 默认为显示年月日模式
        /// </summary>
        [Parameter]
        public DatePickerViewModel ViewModel { get; set; } = DatePickerViewModel.Date;

        /// <summary>
        /// 获得/设置 日期格式字符串 默认为 "yyyy-MM-dd"
        /// </summary>
        [Parameter]
        public string DateFormat { get; set; } = "yyyy-MM-dd";

        /// <summary>
        /// 获得/设置 时间格式字符串 默认为 "hh\\:mm\\:ss"
        /// </summary>
        [Parameter]
        public string TimeFormat { get; set; } = "hh\\:mm\\:ss";

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
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public DateTime Value
        {
            get { return CurrentDate.AddTicks(CurrentTime.Ticks); }
            set
            {
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
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CurrentViewModel = ViewModel;


            // 计算开始与结束时间 每个组件显示 6 周数据
            if (Value == DateTime.MinValue)
            {
                Value = DateTime.Today;
            }
        }

        /// <summary>
        /// 点击上一年按钮时调用此方法
        /// </summary>
        protected void OnClickPrevYear()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentViewModel == DatePickerViewModel.Year ? CurrentDate.AddYears(-20) : CurrentDate.AddYears(-1);
        }

        /// <summary>
        /// 点击上一月按钮时调用此方法
        /// </summary>
        protected void OnClickPrevMonth()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentDate.AddMonths(-1);
        }

        /// <summary>
        /// 点击下一年按钮时调用此方法
        /// </summary>
        protected void OnClickNextYear()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentViewModel == DatePickerViewModel.Year ? CurrentDate.AddYears(20) : CurrentDate.AddYears(1);
        }

        /// <summary>
        /// 点击下一月按钮时调用此方法
        /// </summary>
        protected void OnClickNextMonth()
        {
            ShowTimePicker = false;
            CurrentDate = CurrentDate.AddMonths(1);
        }

        /// <summary>
        /// Day 选择时触发此方法
        /// </summary>
        /// <param name="d"></param>
        protected Task OnClickDateTime(DateTime d)
        {
            ShowTimePicker = false;
            CurrentDate = d;
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置组件显示视图方法
        /// </summary>
        /// <param name="view"></param>
        protected void SwitchView(DatePickerViewModel view)
        {
            ShowTimePicker = false;
            CurrentViewModel = view;
        }

        /// <summary>
        /// 设置组件显示视图方法
        /// </summary>
        /// <param name="view"></param>
        /// <param name="d"></param>
        protected void SwitchView(DatePickerViewModel view, DateTime d)
        {
            ShowTimePicker = false;
            CurrentViewModel = view;
            CurrentDate = d;
            StateHasChanged();
        }

        /// <summary>
        /// 通过当前时间计算年跨度区间
        /// </summary>
        /// <returns></returns>
        protected string GetYearPeriod()
        {
            var start = CurrentDate.AddYears(0 - CurrentDate.Year % 20).Year;
            return $"{start} 年 - {start + 19} 年";
        }

        /// <summary>
        /// 获取 年视图下的年份
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        protected DateTime GetYear(int year) => CurrentDate.AddYears(year - (CurrentDate.Year % 20));

        /// <summary>
        /// 获取 年视图下月份单元格显示文字
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        protected string? GetYearText(int year) => $"{GetYear(year).Year}";

        /// <summary>
        /// 获取 年视图下的年份单元格样式
        /// </summary>
        /// <returns></returns>
        protected string? GetYearClassName(int year) => CssBuilder.Default()
            .AddClass("current", CurrentDate.AddYears(year - (CurrentDate.Year % 20)).Year == Value.Year)
            .AddClass("today", CurrentDate.AddYears(year - (CurrentDate.Year % 20)).Year == DateTime.Today.Year)
            .Build();

        /// <summary>
        /// 获取 年视图下的月份
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        protected DateTime GetMonth(int month) => CurrentDate.AddMonths(month - CurrentDate.Month);

        /// <summary>
        /// 获取 月视图下的月份单元格样式
        /// </summary>
        /// <returns></returns>
        protected string? GetMonthClassName(int month) => CssBuilder.Default()
            .AddClass("current", CurrentDate.Year == Value.Year && month == Value.Month)
            .AddClass("today", CurrentDate.Year == DateTime.Today.Year && month == DateTime.Today.Month)
            .Build();

        /// <summary>
        /// 获取 日视图下日单元格显示文字
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        protected string? GetDayText(int day) => $"{day}";

        /// <summary>
        /// 获取 月视图下月份单元格显示文字
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        protected string? GetMonthText(int month) => month switch
        {
            1 => "一月",
            2 => "二月",
            3 => "三月",
            4 => "四月",
            5 => "五月",
            6 => "六月",
            7 => "七月",
            8 => "八月",
            9 => "九月",
            10 => "十月",
            11 => "十一月",
            12 => "十二月",
            _ => ""
        };

        /// <summary>
        /// 时刻选择框点击时调用此方法
        /// </summary>
        protected void OnClickTimeInput()
        {
            ShowTimePicker = true;
        }

        /// <summary>
        /// 点击 此刻时调用此方法
        /// </summary>
        protected Task ClickNowButton()
        {
            ShowTimePicker = false;
            Value = ViewModel switch
            {
                DatePickerViewModel.DateTime => DateTime.Now,
                _ => DateTime.Today
            };
            return Task.CompletedTask;
        }

        /// <summary>
        /// 点击 清除按钮调用此方法
        /// </summary>
        /// <returns></returns>
        protected async Task ClickClearButton()
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
        protected async Task ClickConfirmButton()
        {
            ShowTimePicker = false;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            if (OnConfirm != null)
            {
                await OnConfirm.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnTimePickerClose()
        {
            ShowTimePicker = false;
            StateHasChanged();
        }
    }
}
