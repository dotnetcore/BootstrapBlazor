using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Trees
    {
        private Logger? Trace { get; set; }

        private Logger? TraceChecked { get; set; }

        private static IEnumerable<TreeItem> GetItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一" },
                new TreeItem() { Text = "导航二" },
                new TreeItem() { Text = "导航三" }
            };

            ret[1].AddItem(new TreeItem() { Text = "子菜单一" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单二" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单三" });

            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1一" });
            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1二" });

            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2一" });
            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单二" });

            return ret;
        }

        private IEnumerable<TreeItem> Items { get; set; } = GetItems();

        private static IEnumerable<TreeItem> GetCheckedItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一" },
                new TreeItem() { Text = "导航二", Checked = true, IsExpanded = true },
                new TreeItem() { Text = "导航三" }
            };

            ret[1].AddItem(new TreeItem() { Text = "子菜单一" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单二", IsExpanded = true });
            ret[1].AddItem(new TreeItem() { Text = "子菜单三" });

            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1一" });
            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1二" });

            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2一" });
            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单二" });

            return ret;
        }

        private IEnumerable<TreeItem> CheckedItems { get; set; } = GetCheckedItems();

        private static IEnumerable<TreeItem> GetDisabledItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一" },
                new TreeItem() { Text = "导航二", Disabled = true },
                new TreeItem() { Text = "导航三" }
            };

            ret[1].AddItem(new TreeItem() { Text = "子菜单一" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单二" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单三" });

            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1一" });
            ret[1].Items.ElementAt(0).AddItem(new TreeItem() { Text = "孙菜单1二" });

            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2一" });
            ret[1].Items.ElementAt(1).AddItem(new TreeItem() { Text = "孙菜单2二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾孙菜单二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new TreeItem() { Text = "曾曾孙菜单二" });

            return ret;
        }

        private IEnumerable<TreeItem> DisabledItems { get; set; } = GetDisabledItems();

        private Task OnTreeItemClick(TreeItem item)
        {
            Trace?.Log($"TreeItem: {item.Text} clicked");
            return Task.CompletedTask;
        }

        private Task OnTreeItemChecked(TreeItem item)
        {
            var state = item.Checked ? "选中" : "未选中";
            TraceChecked?.Log($"TreeItem: {item.Text} {state}");
            return Task.CompletedTask;
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
                Description = "菜单数据集合",
                Type = "IEnumerable<TreeItem>",
                ValueList = " — ",
                DefaultValue = "new List<TreeItem>(20)"
            },
            new AttributeItem() {
                Name = "ShowCheckbox",
                Description = "是否显示 CheckBox",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnTreeItemClick",
                Description = "树形控件节点点击时回调委托",
                Type = "Func<TreeItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnTreeItemChecked",
                Description = "树形控件节点选中时回调委托",
                Type = "Func<TreeItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        private IEnumerable<AttributeItem> GetTreeItemAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = nameof(TreeItem.Key),
                Description = "TreeItem 标识",
                Type = "object?",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "子节点数据源",
                Type = "IEnumerable<TreeItem>",
                ValueList = " — ",
                DefaultValue = "new List<TreeItem>(20)"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Checked",
                Description = "是否被选中",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Disabled",
                Description = "是否被禁用",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsExpanded",
                Description = "是否展开",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = nameof(TreeItem.Tag),
                Description = "TreeItem 附加数据",
                Type = "object?",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
