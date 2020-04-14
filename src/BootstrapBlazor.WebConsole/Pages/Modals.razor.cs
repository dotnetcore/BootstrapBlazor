using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Modals
    {
        /// <summary>
        /// 
        /// </summary>
        private Modal? Modal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? BackdropModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? SmailModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? LargeModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? ExtraLargeModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? CenterModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? LongContentModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Modal? ScrollModal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "ModalBody",
                    Description = "ModalBody 代码块",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "ModalFooter",
                    Description = "Footer 代码块",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "弹窗标题",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " 未设置 "
                },
                new AttributeItem() {
                    Name = "Size",
                    Description = "尺寸",
                    Type = "Size",
                    ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                    DefaultValue = "None"
                },
                new AttributeItem() {
                    Name = "IsCentered",
                    Description = "是否垂直居中",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "IsBackdrop",
                    Description = "是否后台关闭弹窗",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsScrolling",
                    Description = "是否弹窗正文超长时滚动",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowCloseButton",
                    Description = "是否显示关闭按钮",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "ShowFooter",
                    Description = "是否显示 Footer",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
            };
        }
    }
}
