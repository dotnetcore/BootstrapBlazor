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

    private List<TreeViewItem<TreeFoo>> DraggableItems { get; set; } = [];

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var items = GetDraggableItems();
        DraggableItems = TreeFoo.CascadingTree(items);
        DraggableItems[0].IsExpand = true;
        if (DraggableItems.Count > 1)
        {
            DraggableItems[1].IsExpand = true;
        }
        if (DraggableItems.Count > 2)
        {
            DraggableItems[2].IsExpand = true;
        }
    }

    private Task OnTreeItemClick(TreeViewItem<TreeFoo> item)
    {
        Logger1.Log($"TreeItem: {item.Text} clicked");
        return Task.CompletedTask;
    }

    private Task OnDragItemEndAsync(TreeViewDragContext<TreeFoo> context)
    {
        // 本例是使用静态数据模拟数据库操作的，实战中应该是更新节点的父级 Id 可能还需要更改排序字段等信息，然后重构 TreeView 数据源即可
        // 根据 context 处理原始数据
        var items = GetDraggableItems();
        var source = items.Find(i => i.Id == context.Source.Value.Id);
        if (source != null)
        {
            var target = items.Find(i => i.Id == context.Target.Value.Id);
            if (target != null)
            {
                source.ParentId = context.IsChildren ? target.Id : target.ParentId;
            }
        }

        Action<TreeViewItem<TreeFoo>>? cb = null;
        if (context.IsChildren)
        {
            // 自动展开目标节点
            cb = item =>
            {
                if (item.Value.Id == context.Target.Value.Id)
                {
                    item.IsExpand = true;
                }
            };
        }
        DraggableItems = TreeFoo.CascadingTree(items, cb);
        DraggableItems[0].IsExpand = true;
        if (DraggableItems.Count > 1)
        {
            DraggableItems[1].IsExpand = true;
        }
        if (DraggableItems.Count > 2)
        {
            DraggableItems[2].IsExpand = true;
        }

        StateHasChanged();
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

    private static List<TreeFoo>? _dragItems = null;
    private static List<TreeFoo> GetDraggableItems()
    {
        _dragItems ??=
        [
            new() { Text = "Item A", Id = "1", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item D", Id = "4", ParentId = "1", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item E", Id = "5", ParentId = "1", Icon = "fa-solid fa-font-awesome" },

            new() { Text = "Item B", Id = "2", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item F", Id = "6", ParentId = "2", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item G", Id = "9", ParentId = "2", Icon = "fa-solid fa-font-awesome" },

            new() { Text = "Item C", Id = "3", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item H", Id = "7", ParentId = "3", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Item I", Id = "8", ParentId = "3", Icon = "fa-solid fa-font-awesome" },

        ];
        return _dragItems;
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
