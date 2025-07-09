// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    private List<TreeViewItem<TreeFoo>> NormalItems { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> Items { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> EditItems { get; } = TreeFoo.GetTreeItems();

    private bool AutoCheckChildren { get; set; }

    private bool AutoCheckParent { get; set; }

    private List<TreeViewItem<TreeFoo>> DisabledItems { get; } = GetDisabledItems();

    private List<TreeViewItem<TreeFoo>>? AccordionItems { get; } = TreeFoo.GetAccordionItems();

    private List<TreeViewItem<TreeFoo>> ExpandItems { get; } = GetExpandItems();

    private List<TreeViewItem<TreeFoo>> CheckedItems { get; set; } = GetCheckedItems();

    private static List<TreeViewItem<TreeFoo>> IconItems { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> ClickExpandItems { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> FormItems { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> CheckedItems2 { get; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> KeyboardItems { get; } = TreeFoo.GetTreeItems();

    private List<SelectedItem> SelectedItems { get; } = TreeFoo.GetItems().Select(x => new SelectedItem(x.Id, x.Text)).ToList();

    private TreeView<TreeFoo>? SetActiveTreeView { get; set; }

    private List<TreeViewItem<TreeFoo>>? AsyncItems { get; set; }

    private List<TreeViewItem<TreeFoo>>? MaxItems { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>>? SearchItems1 { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>>? SearchItems2 { get; set; } = TreeFoo.GetTreeItems();

    private List<TreeViewItem<TreeFoo>> VirtualizeItems { get; } = TreeFoo.GetVirtualizeTreeItems();

    private List<TreeViewItem<TreeFoo>> LazyItems { get; } = TreeFoo.GetLazyItems();

    private List<TreeViewItem<TreeFoo>> ColorItems { get; } = TreeFoo.GetColorItems();

    private List<TreeViewItem<TreeFoo>> TemplateItems { get; } = TreeFoo.GetTemplateItems();

    private Foo Model => Foo.Generate(LocalizerFoo);

    private bool _showSearch;

    private string? _selectedValue;

    private Task OnTreeItemClick(TreeViewItem<TreeFoo> item)
    {
        Logger1.Log($"TreeItem: {item.Text} clicked");
        return Task.CompletedTask;
    }

    private Task OnTreeItemKeyboardClick(TreeViewItem<TreeFoo> item)
    {
        _selectedValue = item.Value.Text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void OnRefresh()
    {
        CheckedItems = GetCheckedItems();
    }

    private void OnClickAddNode()
    {
        CheckedItems.Add(new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"Id-{DateTime.Now.Ticks}" }) { Text = DateTime.Now.ToString() });
    }

    private static List<TreeViewItem<TreeFoo>> GetCheckedItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[1].IsActive = true;
        ret[1].Items[1].CheckedState = CheckboxState.Checked;
        return ret;
    }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnMaxSelectedCountExceed()
    {
        return ToastService.Information(Localizer["OnMaxSelectedCountExceedTitle"], Localizer["OnMaxSelectedCountExceedContent", 2]);
    }

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

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(200);
        var item = node.Value;
        return new TreeViewItem<TreeFoo>[] { new(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id }) { Text = "懒加载子节点1", HasChildren = true }, new(new TreeFoo() { Id = $"{item.Id}-102", ParentId = item.Id }) { Text = "懒加载子节点2" } };
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

    private static async Task<List<TreeViewItem<TreeFoo>>?> OnSearchAsync(string searchText)
    {
        await Task.Delay(20);

        List<TreeViewItem<TreeFoo>>? items = null;
        if (!string.IsNullOrEmpty(searchText))
        {
            items =
            [
                new TreeViewItem<TreeFoo>(new TreeFoo() { Text = searchText }) { Text = searchText },
            ];
        }
        return items;
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandVirtualNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(500);
        var items = new List<TreeViewItem<TreeFoo>>();
        Enumerable.Range(1, 1000).ToList().ForEach(i =>
        {
            var text = $"{node.Text}-{i}";
            items.Add(new TreeViewItem<TreeFoo>(new TreeFoo() { Text = text }) { Text = text, HasChildren = Random.Shared.Next(100) > 80 });
        });
        return items;
    }

    private Task<bool> OnUpdateCallbackAsync(TreeFoo foo, string? text)
    {
        foo.Text = text;
        return Task.FromResult(true);
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
            Type = "IEnumerable<TreeViewItem>",
            ValueList = " — ",
            DefaultValue = "new List<TreeViewItem>(20)"
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
            Type = "Func<TreeViewItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeView<string>.OnTreeItemChecked),
            Description = "Callback delegate when tree control node is selected",
            Type = "Func<TreeViewItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TreeView<string>.OnExpandNodeAsync),
            Description = "Tree control node expand callback delegate",
            Type = "Func<TreeViewItem, Task>",
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
            Name = nameof(TreeView<string>.IsVirtualize),
            Description = "Virtualize",
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
        },
        new()
        {
            Name = nameof(TreeView<string>.MaxSelectedCount),
            Description = "The maximum count of selected node",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(TreeView<string>.OnMaxSelectedCountExceed),
            Description = "Select the callback method when the maximum number of nodes is reached",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    private static AttributeItem[] GetTreeItemAttributes() =>
    [
        new()
        {
            Name = nameof(TreeViewItem<TreeFoo>.Items),
            Description = "Child node data source",
            Type = "List<TreeViewItem<TItem>>",
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
            Name = nameof(TreeViewItem<TreeFoo>.Template),
            Description = "Child node template",
            Type = nameof(RenderFragment),
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
