using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Collapses
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void ButtonClick(MouseEventArgs e)
        {
            Trace?.Log($"Button Clicked");
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "CollapseItems",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowArrow",
                Description = "是否显示指示箭头",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAccordion",
                Description = "是否手风琴效果",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
