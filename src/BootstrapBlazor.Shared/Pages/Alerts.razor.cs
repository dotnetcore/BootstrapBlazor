using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Alerts
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DismissClick(MouseEventArgs e)
        {
            Trace?.Log($"Alert Dismissed");
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnDismiss",
                Description="关闭警告框回调方法",
                Type ="EventCallback<MouseEventArgs>"
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
                Name = "ChildContent",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowDismiss",
                Description = "关闭按钮",
                Type = "bool",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowBar",
                Description = "是否显示左侧 Bar",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
