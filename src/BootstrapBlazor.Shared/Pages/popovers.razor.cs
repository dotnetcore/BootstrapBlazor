using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Popovers
    {
        private string ValueString => "弹出框";

        private string Title => "弹出框标题";

        private string Content => "这里是弹出框正文，此处支持 <code>html</code> 标签，也可以内置一个 <code>Table</code>";

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Cotent",
                Description = "Popover 弹窗内容",
                Type = "string",
                ValueList = "",
                DefaultValue = "Popover"
            },
            new AttributeItem() {
                Name = "IsHtml",
                Description = "内容中是否包含 Html 代码",
                Type = "boolean",
                ValueList = "",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Placement",
                Description = "位置",
                Type = "Placement",
                ValueList = "Auto / Top / Left / Bottom / Right",
                DefaultValue = "Auto"
            },
            new AttributeItem() {
                Name = "Title",
                Description = "Popover 弹窗标题",
                Type = "string",
                ValueList = "",
                DefaultValue = "Popover"
            },
        };
    }
}
