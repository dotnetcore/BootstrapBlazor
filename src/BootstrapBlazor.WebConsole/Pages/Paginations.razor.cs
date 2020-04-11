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
        private Logger? Trace { get; set; }

        private void OnPageClick(int pageIndex, int pageItems)
        {
            Trace?.Log($"PageIndex: {pageIndex} PageItems: {pageItems}");
        }

        private void OnPageItemsChanged(int pageItems)
        {
            Trace?.Log($"PageItems: {pageItems}");
        }

        private IEnumerable<int> PageItems => new int[] { 3, 10, 20, 40 };

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
