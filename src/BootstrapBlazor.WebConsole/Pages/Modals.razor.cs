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
                new AttributeItem()
                {
                    Name = "type",
                    Description = "控件类型",
                    Type = "string",
                    ValueList = "text / number / email / url",
                    DefaultValue = "text"
                },
                new AttributeItem() {
                    Name = "ChildContent",
                    Description = "验证控件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
            };
        }
    }
}
