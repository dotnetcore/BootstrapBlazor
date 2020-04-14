using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Collapses
    {
        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void ButtonClick(MouseEventArgs e)
        {
            Trace?.Log($"Button Clicked");
        }

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
                ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Size",
                Description = "尺寸",
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
             new AttributeItem() {
                Name = "CollapseContent",
                Description = "折叠内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CollapsedText",
                Description = "折叠后显示的文字",
                Type = "string",
                ValueList = "",
                DefaultValue = "展开"
            },
            new AttributeItem() {
                Name = "ExpandedText",
                Description = "展开后显示的文字",
                Type = "string",
                ValueList = "",
                DefaultValue = "折叠"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
             new AttributeItem() {
                Name = "IsCollapsed",
                Description = "是否收缩折叠面板",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "true"
            },
        };
    }
}
