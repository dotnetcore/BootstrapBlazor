// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private Foo Model => Foo.Generate(Localizer);

        private static IEnumerable<TreeItem> GetItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一" },
                new TreeItem() { Text = "导航二" },
                new TreeItem() { Text = "导航三" }
            };

            ret[0].AddItem(new TreeItem() { Text = "子菜单" });

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

        private static IEnumerable<TreeItem> GetIconItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一", Icon = "fa fa-fa fa-fw" },
                new TreeItem() { Text = "导航二", Icon = "fa fa-fa fa-fw" },
                new TreeItem() { Text = "导航三", Icon = "fa fa-fa fa-fw" }
            };

            ret[1].AddItem(new TreeItem() { Text = "子菜单一", Icon = "fa fa-fa fa-fw" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单二", Icon = "fa fa-fa fa-fw" });
            ret[1].AddItem(new TreeItem() { Text = "子菜单三", Icon = "fa fa-fa fa-fw" });

            return ret;
        }

        private static IEnumerable<TreeItem> GetLazyItems()
        {
            var ret = new List<TreeItem>
            {
                new TreeItem() { Text = "导航一", IsExpanded = true  },
                new TreeItem() { Text = "懒加载", HasChildNode = true },
                new TreeItem() { Text = "懒加载延时",  HasChildNode = true , Key = "Delay" }
            };

            ret[0].AddItem(new TreeItem() { Text = "子菜单一", Icon = "fa fa-fa fa-fw" });
            ret[0].AddItem(new TreeItem() { Text = "子菜单二", Icon = "fa fa-fa fa-fw" });
            ret[0].AddItem(new TreeItem() { Text = "子菜单三", Icon = "fa fa-fa fa-fw" });

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

        private static async Task OnExpandNode(TreeItem item)
        {
            if (!item.Items.Any() && item.HasChildNode && !item.ShowLoading)
            {
                item.ShowLoading = true;
                if (item.Key?.ToString() == "Delay")
                {
                    await Task.Delay(800);
                }
                item.AddItem(new TreeItem() {
                    Text = "懒加载子节点1",
                    HasChildNode = true
                });
                item.AddItem(new TreeItem() { Text = "懒加载子节点2" });
                item.ShowLoading = false;
            }
        }



        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
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
                Name = "ClickToggleNode",
                Description = "是否点击节点时展开或者收缩子项",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowCheckbox",
                Description = "是否显示 CheckBox",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowIcon",
                Description = "是否显示 Icon",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowSkeleton",
                Description = "是否显示加载骨架屏",
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
            },
            new AttributeItem() {
                Name = "OnExpandNode",
                Description = "树形控件节点展开回调委托",
                Type = "Func<TreeItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        private static IEnumerable<AttributeItem> GetTreeItemAttributes() => new AttributeItem[]
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
                Name = "Icon",
                Description = "显示图标",
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
            new AttributeItem() {
                Name = nameof(TreeItem.HasChildNode),
                Description = "是否有子节点",
                Type = "bool",
                ValueList = " true|false ",
                DefaultValue = " false "
            },new AttributeItem() {
                Name = nameof(TreeItem.ShowLoading),
                Description = "是否显示子节点加载动画",
                Type = "bool",
                ValueList = " true|false ",
                DefaultValue = " false "
            }

        };
    }
}
