// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// TreeViews
/// </summary>
public sealed partial class TreeViews
{
    [NotNull]
    private ConsoleLogger? Logger1 { get; set; }

    [NotNull]
    private ConsoleLogger? Logger2 { get; set; }

    [NotNull]
    private ConsoleLogger? Logger3 { get; set; }

    private bool DisableCanExpand { get; set; }
    private bool IsDisabled { get; set; }

    private List<TreeViewItem<TreeFoo>> Items { get; set; } = TreeFoo.GetTreeItems();

    private bool AutoCheckChildren { get; set; }

    private bool AutoCheckParent { get; set; }

    private List<TreeViewItem<TreeFoo>> DisabledItems { get; set; } = GetDisabledItems();

    private List<TreeViewItem<TreeFoo>> ExpandItems { get; set; } = GetExpandItems();

    private List<TreeViewItem<TreeFoo>> CheckedItems { get; set; } = GetCheckedItems();

    private static List<TreeViewItem<TreeFoo>> GetIconItems() => TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> GetClickExpandItems { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> GetFormItems { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> CheckedItems2 { get; set; } = TreeFoo.GetTreeItems();

    private List<SelectedItem> SelectedItems { get; set; } = TreeFoo.GetItems().Select(x => new SelectedItem(x.Id, x.Text)).ToList();

    private TreeView<TreeFoo>? SetActiveTreeView { get; set; }

    private List<TreeViewItem<TreeFoo>>? AsyncItems { get; set; }

    private List<TreeViewItem<TreeFoo>>? SearchItems { get; set; } = TreeFoo.GetTreeItems();

    private Foo Model => Foo.Generate(LocalizerFoo);

    private Task OnTreeItemClick(TreeViewItem<TreeFoo> item)
    {
        Logger1.Log($"TreeItem: {item.Text} clicked");
        return Task.CompletedTask;
    }

    private void OnRefresh()
    {
        CheckedItems = GetCheckedItems();
    }

    private static List<TreeViewItem<TreeFoo>> GetCheckedItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].IsActive = true;
        ret[1].Items[1].CheckedState = CheckboxState.Checked;
        return ret;
    }

    private bool IsReset { get; set; }

    private List<SelectedItem> ResetItems { get; } =
    [
        new("True", "Reset"),
        new("False", "Keep")
    ];

    private Task OnTreeItemChecked(List<TreeViewItem<TreeFoo>> items)
    {
        Logger2.Log($"当前共选中{items.Count}项");
        return Task.CompletedTask;
    }

    private static List<TreeViewItem<TreeFoo>> GetDisabledItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[1].IsDisabled = true;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetAccordionItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[0].HasChildren = true;
        return ret;
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(800);
        var item = node.Value;
        return new TreeViewItem<TreeFoo>[] { new(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id }) { Text = "懒加载子节点1", HasChildren = true }, new(new TreeFoo() { Id = $"{item.Id}-102", ParentId = item.Id }) { Text = "懒加载子节点2" } };
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> CustomCheckedNodeOnExpandNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(800);
        var item = node.Value;
        return TreeFoo.GetCheckedTreeItems(item.Id);
    }

    private static List<TreeViewItem<TreeFoo>> GetCustomCheckedItems()
    {
        var ret = TreeFoo.GetCheckedTreeItems();
        ret[0].IsExpand = true;
        ret[0].Items= TreeFoo.GetCheckedTreeItems();
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetExpandItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].IsExpand = true;
        return ret;
    }

    private static Task OnFormTreeItemClick(TreeViewItem<TreeFoo> item)
    {
        return Task.CompletedTask;
    }

    private static List<TreeViewItem<TreeFoo>> GetLazyItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].Items[0].IsExpand = true;
        ret[2].Text = "懒加载延时";
        ret[2].HasChildren = true;
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetTemplateItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].Template = foo => BootstrapDynamicComponent.CreateComponent<CustomerTreeItem>(new Dictionary<string, object?>() { [nameof(CustomerTreeItem.Foo)] = foo }).Render();
        return ret;
    }

    private static List<TreeViewItem<TreeFoo>> GetColorItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].CssClass = "text-primary";
        ret[1].CssClass = "text-success";
        ret[2].CssClass = "text-danger";
        return ret;
    }

    private Task OnTreeItemChecked2(List<TreeViewItem<TreeFoo>> items)
    {
        Logger3.Log($"当前共选中{items.Count}项");
        return Task.CompletedTask;
    }

    private async Task OnLoadAsyncItems()
    {
        AsyncItems = null;
        await Task.Delay(2000);
        AsyncItems = TreeFoo.GetTreeItems();
        AsyncItems[2].Text = "延时加载";
        AsyncItems[2].HasChildren = true;
    }

    private Task SelectedItemOnChanged(SelectedItem selectedItem)
    {
        var treeViewItem = FindTreeViewItem(Items, item => item.Value.Id == selectedItem.Value);
        if (treeViewItem != null)
        {
            SetActiveTreeView?.SetActiveItem(treeViewItem);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }

    private static TreeViewItem<T>? FindTreeViewItem<T>(IEnumerable<TreeViewItem<T>> source, Func<TreeViewItem<T>, bool> func)
    {
        var ret = source.FirstOrDefault(func);
        if (ret == null)
        {
            var items = source.SelectMany(e => e.Items);
            if (items.Any())
            {
                ret = FindTreeViewItem(items, func);
            }
        }

        return ret;
    }

    private Task OnSearchAsync(string searchText)
    {
        SearchItems = string.IsNullOrEmpty(searchText) ? TreeFoo.GetTreeItems() : [];
        StateHasChanged();
        return Task.CompletedTask;
    }

    private class CustomerTreeItem : ComponentBase
    {
        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }

        [Parameter]
        [NotNull]
        public TreeFoo? Foo { get; set; }

        /// <summary>
        /// BuildRenderTree
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(3, "span");
            builder.AddAttribute(4, "class", "me-3");
            builder.AddContent(5, Foo.Text);
            builder.CloseElement();

            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.Icon), "fa-solid fa-font-awesome");
            builder.AddAttribute(2, nameof(Button.Text), "Click");
            builder.AddAttribute(3, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                ToastService.Warning("自定义 TreeItem", "测试 TreeItem 按钮点击事件");
            }));
            builder.CloseComponent();
        }
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Items",
            Description = "menu data set",
            Type = "IEnumerable<TreeItem>",
            ValueList = " — ",
            DefaultValue = "new List<TreeItem>(20)"
        },
        new()
        {
            Name = "ClickToggleNode",
            Description = "Whether to expand or contract children when a node is clicked",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCheckbox",
            Description = "Whether to display CheckBox",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowIcon",
            Description = "Whether to display Icon",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSkeleton",
            Description = "Whether to display the loading skeleton screen",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TreeView<string>.OnTreeItemClick),
            Description = "Callback delegate when tree control node is clicked",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeView<string>.OnTreeItemChecked),
            Description = "Callback delegate when tree control node is selected",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeView<string>.OnExpandNodeAsync),
            Description = "Tree control node expand callback delegate",
            Type = "Func<TreeItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeView<string>.IsDisabled),
            Description = "Disable tree view",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TreeView<string>.CanExpandWhenDisabled),
            Description = "Whether to expand when the control node is disabled",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];

    private static AttributeItem[] GetTreeItemAttributes() =>
    [
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Items),
            Description = "Child node data source",
            Type = "List<TreeItem<TItem>>",
            ValueList = " — ",
            DefaultValue = "new ()"
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Text),
            Description = "Display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Icon),
            Description = "Show icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.CssClass),
            Description = "Node custom style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.CheckedState),
            Description = "Is selected",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.IsDisabled),
            Description = "Is disabled",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.IsExpand),
            Description = "Whether to expand",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.HasChildren),
            Description = "Whether there are child nodes",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.ShowLoading),
            Description = "Whether to show child node loading animation",
            Type = "bool",
            ValueList = " true|false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Template),
            Description = "Child node template",
            Type = nameof(RenderFragment),
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
