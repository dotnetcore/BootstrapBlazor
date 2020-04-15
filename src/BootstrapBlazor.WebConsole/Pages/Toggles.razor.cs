using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    ///
    /// </summary>
    public partial class Toggles
    {
        /// <summary>
        ///
        /// </summary>
        protected bool BindValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        protected void OnValueChanged(bool val) => Trace?.Log($"Toggle CurrentValue: {val}");

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ValueChanged",
                Description="获取选择改变的值",
                Type ="EventCallback<bool>"
            },
            new EventItem()
            {
                Name = "OnValueChanged",
                Description="控件值变化时触发此事件",
                Type ="Action<bool>"
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Success"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "120"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnText",
                Description = "组件 On 时显示文本",
                Type = "string",
                ValueList = "—",
                DefaultValue = "展开"
            },
            new AttributeItem() {
                Name = "OffText",
                Description = "组件 Off 时显示文本",
                Type = "string",
                ValueList = "—",
                DefaultValue = "收缩"
            },
            new AttributeItem() {
                Name = "Value",
                Description = "获取值",
                Type = "boolean",
                ValueList = " ",
                DefaultValue = "None"
            },
        };
    }
}
