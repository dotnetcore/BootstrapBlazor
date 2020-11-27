// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Calendars
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private DateTime BindValue { get; set; } = DateTime.Today;

        private void OnValueChanged(DateTime ts)
        {
            Trace?.Log($"{ts:yyyy-MM-dd}");
        }

        private string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");

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
                Type ="EventCallback<DateTime>"
            }
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
                Type = "DateTime",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "子组件",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
