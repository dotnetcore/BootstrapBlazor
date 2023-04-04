// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Trees
/// </summary>
public sealed partial class Trees
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Items",
            Description = "Menu data set",
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
            Name = "OnTreeItemClick",
            Description = "Callback delegate when tree control node is clicked",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnTreeItemChecked",
            Description = "Callback delegate when tree control node is selected",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnExpandNode",
            Description = "Tree control node expand callback delegate",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnCheckedItems",
            Description = "The tree control gets the callback delegate of all selected nodes",
            Type = "Func<List<TreeItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private static IEnumerable<AttributeItem> GetTreeItemAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(TreeItem.Key),
            Description = "TreeItem ID",
            Type = "object?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Items",
            Description = "Child node data source",
            Type = "IEnumerable<TreeItem>",
            ValueList = " — ",
            DefaultValue = "new List<TreeItem>(20)"
        },
        new AttributeItem() {
            Name = "Text",
            Description = "Display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Icon",
            Description = "Show icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "CssClass",
            Description = "Node custom style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Checked",
            Description = "Is selected",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(TreeItem.IsDisabled),
            Description = "Is disabled",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsCollapsed",
            Description = "Whether to expand",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(TreeItem.Tag),
            Description = "TreeItem Additional data",
            Type = "object?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TreeItem.HasChildNode),
            Description = "Whether there are child nodes",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem() {
            Name = nameof(TreeItem.ShowLoading),
            Description = "Whether to show child node loading animation",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new AttributeItem()
        {
            Name = nameof(TreeItem.Template),
            Description = "Child node template",
            Type = nameof(RenderFragment),
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
