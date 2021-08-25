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
        private BlockLogger? Trace { get; set; }

        private BlockLogger? TraceChecked { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private Foo Model => Foo.Generate(Localizer);

        private List<TreeItem> Items { get; set; } = TreeDataFoo.GetTreeItems();

        private List<TreeItem> CheckedItems { get; set; } = GetCheckedItems();

        private static List<TreeItem> GetCheckedItems()
        {
            var ret = TreeDataFoo.GetTreeItems();
            ret[1].Items[1].Checked = true;
            return ret;
        }

        private List<TreeItem> DisabledItems { get; set; } = GetDisabledItems();

        private static List<TreeItem> GetDisabledItems()
        {
            var ret = TreeDataFoo.GetTreeItems();
            ret[1].Items[1].IsDisabled = true;
            return ret;
        }

        private static List<TreeItem> GetIconItems()
        {
            var ret = TreeDataFoo.GetTreeItems();
            ret[1].Items[0].Icon = "fa fa-fa";
            ret[1].Items[1].Icon = "fa fa-fa";
            ret[1].Items[2].Icon = "fa fa-fa";
            return ret;
        }

        private static List<TreeItem> GetLazyItems()
        {
            var ret = TreeDataFoo.GetTreeItems();
            ret[1].Items[0].IsExpanded = true;
            ret[1].Items[1].Text = "懒加载";
            ret[1].Items[1].HasChildNode = true;
            ret[1].Items[2].Text = "懒加载延时";
            ret[1].Items[2].HasChildNode = true;
            ret[1].Items[2].Key = "Delay";

            return ret;
        }

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
                item.Items.AddRange(new TreeItem[]
                {
                    new TreeItem()
                    {
                        Text = "懒加载子节点1",
                        HasChildNode = true
                    },
                    new TreeItem() { Text = "懒加载子节点2" }
                });
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
