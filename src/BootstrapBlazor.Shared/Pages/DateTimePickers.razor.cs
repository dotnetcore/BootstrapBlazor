using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
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
        private DateTime? BindValue { get; set; } = DateTime.Today;

        private DateTime? BindNullValue { get; set; }

        private string GetNullValueString => BindNullValue.HasValue ? BindNullValue.Value.ToString("yyyy-MM-dd") : "空值";

        /// <summary>
        /// 
        /// </summary>
        private string BindValueString
        {
            get
            {
                return BindValue.HasValue ? BindValue.Value.ToString("yyyy-MM-dd") : "";
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
        private Task DateValueChanged(DateTime d)
        {
            DateLogger?.Log($"选择的日期为: {d:yyyy-MM-dd}");
            return Task.CompletedTask;
        }

        private string FormatterSpanString(TimeSpan ts)
        {
            return ts.ToString("hh\\:mm\\:ss");
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
        private Task DateTimeValueChanged(DateTime? d)
        {
            BindValue = d;
            DateTimeLogger?.Log($"选择的时间为: {d:yyyy-MM-dd}");
            return Task.CompletedTask;
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
                Name = "ValueChanged",
                Description="组件值改变时回调委托供双向绑定使用",
                Type ="EventCallback<DateTime?>"
            },
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示前置标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = "前置标签显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
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
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = "是否禁用 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "TimeFormat",
                Description = "时间格式字符串 默认为 hh:mm:ss",
                Type = "string",
                ValueList = "",
                DefaultValue = "hh:mm:ss"
            },
            new AttributeItem() {
                Name = "Value",
                Description = "组件值与 ValueChanged 作为双向绑定的值",
                Type = "TValue",
                ValueList = "DateTime | DateTime?",
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
