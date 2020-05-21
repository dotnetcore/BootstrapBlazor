using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Rates
    {
        private int BindValue { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private void OnValueChanged(int val)
        {
            Trace?.Log($"评星: {val}");
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ValueChanged",
                Description="值改变时回调委托",
                Type ="EventCallback<int>"
            },
            new EventItem()
            {
                Name = "OnValueChanged",
                Description="值改变时回调委托",
                Type ="Action<int>"
            },
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Value",
                Description = "组件值",
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
