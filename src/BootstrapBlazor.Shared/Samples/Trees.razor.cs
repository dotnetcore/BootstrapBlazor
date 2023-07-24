// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Trees
/// </summary>
public sealed partial class Trees
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private ConsoleLogger? Logger2 { get; set; }

    private List<TreeItem> Items { get; set; } = TreeDataFoo.GetTreeItems();

    private List<TreeItem> Items2 { get; set; } = TreeDataFoo.GetTreeItems();

    private List<TreeItem> AccordionItems { get; set; } = TreeDataFoo.GetTreeItems();

    private List<TreeItem> CheckedItems { get; set; } = GetCheckedItems();

    private List<TreeItem> DisabledItems { get; set; } = GetDisabledItems();

    private List<TreeItem> ExpandItems { get; set; } = GetExpandItems();

    private static List<TreeItem> GetIconItems() => TreeDataFoo.GetTreeItems();

    private List<TreeItem> ClickExpandItems { get; set; } = TreeDataFoo.GetTreeItems();

    private List<TreeItem> ValidateItems { get; set; } = TreeDataFoo.GetTreeItems();

    private Foo Model => Foo.Generate(LocalizerFoo);

    private List<TreeItem>? AsyncItems { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await OnLoadAsyncItems();
    }

    private static List<TreeItem> GetExpandItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[1].IsCollapsed = false;
        return ret;
    }

    private static List<TreeItem> GetDisabledItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[1].Items[1].IsDisabled = true;
        return ret;
    }

    private Task OnTreeItemClick(TreeItem item)
    {
        Logger.Log($"TreeItem: {item.Text} clicked");
        return Task.CompletedTask;
    }

    private static List<TreeItem> GetCheckedItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[1].Items[1].Checked = true;
        return ret;
    }

    private Task OnTreeItemChecked(List<TreeItem> items)
    {
        Logger.Log($"Currently selected {items.Count} item");
        return Task.CompletedTask;
    }

    private Task OnValidateTreeItemClick(TreeItem item)
    {
        return Task.CompletedTask;
    }

    private static List<TreeItem> GetLazyItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[1].Items[0].IsCollapsed = false;
        ret[1].Items[1].Text = "lazy loading";
        ret[1].Items[1].HasChildNode = true;
        ret[1].Items[2].Text = "lazy loading delay";
        ret[1].Items[2].HasChildNode = true;
        ret[1].Items[2].Key = "Delay";

        return ret;
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
                        Text = "lazy loading child node 1",
                        HasChildNode = true
                    },
                    new TreeItem() { Text = "lazy loading child node 2" }
            });
            item.ShowLoading = false;
        }
    }

    private static List<TreeItem> GetTemplateItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[0].Template = BootstrapDynamicComponent.CreateComponent<CustomerTreeItem>().Render();
        return ret;
    }

    private static List<TreeItem> GetColorItems()
    {
        var ret = TreeDataFoo.GetTreeItems();
        ret[0].CssClass = "text-primary";
        ret[1].CssClass = "text-success";
        ret[2].CssClass = "text-danger";
        return ret;
    }

    private Task OnTreeItemChecked2(List<TreeItem> items)
    {
        Logger2.Log($"Currently selected {items.Count} item");
        return Task.CompletedTask;
    }

    private async Task OnLoadAsyncItems()
    {
        AsyncItems = null;
        await Task.Delay(2000);
        AsyncItems = TreeDataFoo.GetTreeItems();
    }

    private class CustomerTreeItem : ComponentBase
    {
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }

        /// <summary>
        /// BuildRenderTree
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.Icon), "fa-solid fa-font-awesome");
            builder.AddAttribute(2, nameof(Button.Text), "Click");
            builder.AddAttribute(3, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                ToastService.Warning("Custom TreeItem", "Test the TreeItem button click event");
            }));
            builder.CloseComponent();
        }
    }

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
