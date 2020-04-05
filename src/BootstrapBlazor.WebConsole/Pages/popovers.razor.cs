using BootstrapBlazor.WebConsole.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Popovers
    {
        private string TopString => "弹出框在上方";

        private string LeftString => "弹出框在左方";

        private string RightString => "弹出框在右方";

        private string BottomString => "弹出框在下方";

        private string Title => "弹出框标题";

        private string Content => "这里是弹出框正文，此处支持 <code>html</code> 标签，也可以内置一个 <code>Table</code>";

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
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
            new AttributeItem() {
                Name = "Cotent",
                Description = "Popover 弹窗内容",
                Type = "string",
                ValueList = "",
                DefaultValue = "Popover"
            }
        };
    }
}
