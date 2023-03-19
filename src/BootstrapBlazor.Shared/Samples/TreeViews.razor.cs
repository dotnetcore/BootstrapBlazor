// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// TreeViews
/// </summary>
public sealed partial class TreeViews
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Items",
            Description = "menu data set",
            Type = "IEnumerable<TreeItem>",
            ValueList = " — ",
            DefaultValue = "new List<TreeItem>(20)"
        },
        new AttributeItem() {
            Name = "ClickToggleNode",
            Description = "Whether to expand or contract children when a node is clicked",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCheckbox",
            Description = "Whether to display CheckBox",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowIcon",
            Description = "Whether to display Icon",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowSkeleton",
            Description = "Whether to display the loading skeleton screen",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeView<string>.OnTreeItemClick),
            Description = "Callback delegate when tree control node is clicked",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeView<string>.OnTreeItemChecked),
            Description = "Callback delegate when tree control node is selected",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeView<string>.OnExpandNodeAsync),
            Description = "Tree control node expand callback delegate",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private static IEnumerable<AttributeItem> GetTreeItemAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Items),
            Description = "Child node data source",
            Type = "List<TreeItem<TItem>>",
            ValueList = " — ",
            DefaultValue = "new ()"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Text),
            Description = "Display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.Icon),
            Description = "Show icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.CssClass),
            Description = "Node custom style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.CheckedState),
            Description = "Is selected",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.IsDisabled),
            Description = "Is disabled",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.IsExpand),
            Description = "Whether to expand",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.HasChildren),
            Description = "Whether there are child nodes",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem() {
            Name = nameof(TreeViewItem<TreeFoo>.ShowLoading),
            Description = "Whether to show child node loading animation",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Template),
            Description = "Child node template",
            Type = nameof(RenderFragment),
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
