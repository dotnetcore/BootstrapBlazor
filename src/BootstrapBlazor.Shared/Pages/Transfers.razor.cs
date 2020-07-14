using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Transfers : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<SelectedItem>? Items { get; set; }

        private IEnumerable<SelectedItem>? Items1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Enumerable.Range(1, 20).Select(i => new SelectedItem()
            {
                Text = $"备选 {i:d2}",
                Value = i.ToString()
            });

            Items1 = Enumerable.Range(1, 20).Select(i => new SelectedItem()
            {
                Text = $"数据 {i:d2}",
                Value = i.ToString()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void OnItemsChanged(IEnumerable<SelectedItem> items)
        {
            Trace?.Log(string.Join(" ", items.Where(i => i.Active).Select(i => i.Text)));
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Items",
                Description = "组件绑定数据项集合",
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftButtonText",
                Description = "左侧按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftPanelText",
                Description = "左侧面板 Header 显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "列表 1"
            },
            new AttributeItem() {
                Name = "RightButtonText",
                Description = "右侧按钮显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPanelText",
                Description = "右侧面板 Header 显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "列表 2"
            },
            new AttributeItem() {
                Name = "ShowSearch",
                Description = "是否显示搜索框",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "LeftPannelSearchPlaceHolderString",
                Description = "左侧面板中的搜索框 placeholder 字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPannelSearchPlaceHolderString",
                Description = "右侧面板中的搜索框 placeholder 字符串",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            }
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnItemsChanged",
                Description="组件绑定数据项集合选项变化时回调方法",
                Type ="Action<IEnumerable<SelectedItem>>"
            }
        };
    }
}
