using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Paginations
    {
        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> disableList = new List<string>() { "1", "5", "Next" };
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {  
            new AttributeItem() {
                Name = "Total",
                Description = "分页总页数",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "1"
            },
            new AttributeItem() {
                Name = "CurrentPage",
                Description = "当前页",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "1"
            }
        };
    }
}
