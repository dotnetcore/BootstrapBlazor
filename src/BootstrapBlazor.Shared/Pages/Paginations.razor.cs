using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Paginations
    {
        private Logger? Trace { get; set; }

        private Task OnPageClick(int pageIndex, int pageItems)
        {
            Trace?.Log($"PageIndex: {pageIndex} PageItems: {pageItems}");
            return Task.CompletedTask;
        }

        private Task OnPageItemsChanged(int pageItems)
        {
            Trace?.Log($"PageItems: {pageItems}");
            return Task.CompletedTask;
        }

        private IEnumerable<int> PageItems => new int[] { 3, 10, 20, 40 };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "PageIndex",
                Description = "当前页码",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "1"
            },
            new AttributeItem() {
                Name = "PageItems",
                Description = "每页显示数据数量",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "PageItemsSource",
                Description = "每页显示数据数量的外部数据源",
                Type = "IEnumerable<int>",
                ValueList = " — ",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "ShowPaginationInfo",
                Description = "是否显示分页数据汇总信息",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "TotalCount",
                Description = "数据总数",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "—"
            },
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnPageClick",
                Description="第一个参数是当前页码，第二个参数是当前每页设置显示的数据项数量",
                Type ="Action<int, int>"
            },
            new EventItem()
            {
                Name = "OnPageItemsChanged",
                Description="点击设置每页显示数据数量时回调方法",
                Type ="Action<int>"
            }
        };
    }
}
