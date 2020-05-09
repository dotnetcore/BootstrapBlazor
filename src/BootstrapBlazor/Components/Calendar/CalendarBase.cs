using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Calendar 组件基类
    /// </summary>
    public abstract class CalendarBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 当前日历框年月
        /// </summary>
        protected string Title => $"{Value.Year} 年 {Value.Month} 月";

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
        public Action<DateTime>? OnValueChanged { get; set; }

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Value == DateTime.MinValue) Value = DateTime.Today;
        }

        /// <summary>
        /// 选中日期时回调此方法
        /// </summary>
        /// <param name="d"></param>
        protected void OnClickDay(DateTime d)
        {
            Value = d;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
            StateHasChanged();
        }

        /// <summary>
        /// 右侧快捷切换月按钮回调此方法
        /// </summary>
        /// <param name="offset"></param>
        protected void OnChangeMonth(int offset)
        {
            if (offset == 0) Value = DateTime.Today;
            else Value = Value.AddMonths(offset);
        }
    }
}
