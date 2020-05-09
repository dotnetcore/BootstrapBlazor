using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DateTimePickers
    {
        /// <summary>
        /// 
        /// </summary>
        private TimeSpan SpanValue { get; set; } = DateTime.Now.Subtract(DateTime.Today);

        /// <summary>
        /// 
        /// </summary>
        private string SpanValue2 { get; set; } = DateTime.Now.ToString("HH:mm:ss");

        /// <summary>
        /// 
        /// </summary>
        private Logger? DateLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? TimeLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? DateTimeLogger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private DateTime BindValue { get; set; } = DateTime.Today;

        /// <summary>
        /// 
        /// </summary>
        private string BindValueString
        {
            get
            {
                return BindValue.ToString("yyyy-MM-dd");
            }
            set
            {
                if (DateTime.TryParse(value, out var d))
                {
                    BindValue = d;
                }
                else
                {
                    BindValue = DateTime.Today;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        private void DateValueChanged(DateTime d)
        {
            DateLogger?.Log($"选择的日期为: {d:yyyy-MM-dd}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        private void TimeValueChanged(TimeSpan d)
        {
            TimeLogger?.Log($"选择的时间为: {d:hh\\:mm\\:ss}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        private void DateTimeValueChanged(DateTime d)
        {
            DateTimeLogger?.Log($"选择的时间为: {d:yyyy-MM-dd}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        private void OnValueChange(TimeSpan ts)
        {
            SpanValue2 = ts.ToString("hh\\:mm\\:ss");
            StateHasChanged();
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnClickConfirm",
                Description="确认按钮回调委托",
                Type ="Action"
            },
            new EventItem()
            {
                Name = "OnValueChanged",
                Description="组件值改变时回调委托",
                Type ="Action<DateTime>"
            },
            new EventItem()
            {
                Name = "ValueChanged",
                Description="组件值改变时回调委托供双向绑定使用",
                Type ="EventCallback<DateTime>"
            },
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "DateFormat",
                Description = "日期格式字符串 默认为 yyyy-MM-dd",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "yyyy-MM-dd"
            },
            new AttributeItem() {
                Name = "IsShown",
                Description = "是否显示本组件",
                Type = "boolean",
                ValueList = "",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description = "是否显示本组件 Footer 区域",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "TimeFormat",
                Description = "时间格式字符串 默认为 hh\\:mm\\:ss",
                Type = "string",
                ValueList = "",
                DefaultValue = "hh\\:mm\\:ss"
            },
            new AttributeItem() {
                Name = "Value",
                Description = "组件值与ValueChanged作为双向绑定的值",
                Type = "DateTime",
                ValueList = " — ",
                DefaultValue = " — "
            },
             new AttributeItem() {
                Name = "ViewModel",
                Description = "获得/设置 组件显示模式 默认为显示年月日模式",
                Type = "DatePickerViewModel",
                ValueList = " Date / DateTime / Year / Month",
                DefaultValue = "Date"
            },
        };
    }
}
