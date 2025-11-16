// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Server.Data;

class TreeFoo
{
    [Key]
    [NotNull]
    public string? Id { get; set; }

    public string? ParentId { get; set; }

    [Required]
    [NotNull]
    public string? Text { get; set; }

    public string Icon { get; set; } = "fa-solid fa-font-awesome";

    public bool IsActive { get; set; }

    public static List<TreeFoo> GetItems() =>
    [
        new() { Text = "Navigation one", Id = "1010", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Navigation two", Id = "1020", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Navigation three", Id = "1030", Icon = "fa-solid fa-font-awesome" },

        new() { Text = "Sub menu 1", Id = "1040", ParentId = "1020", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub menu 2", Id = "1050", ParentId = "1020", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub menu 3", Id = "1060", ParentId = "1020", Icon = "fa-solid fa-font-awesome" },

        new() { Text = "Sub Menu One", Id = "1070", ParentId = "1050", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub Menu Two", Id = "1080", ParentId = "1050", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub Menu Three", Id = "1090", ParentId = "1050", Icon = "fa-solid fa-font-awesome", IsActive = true },

        new() { Text = "Sub Menu Two sub menu one", Id = "1100", ParentId = "1080", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub Menu Two sub menu two", Id = "1110", ParentId = "1080", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub Menu Two sub menu three", Id = "1120", ParentId = "1080", Icon = "fa-solid fa-font-awesome" },

        new() { Text = "Sub menu 1", Id = "1130", ParentId = "1100", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub menu 2", Id = "1140", ParentId = "1100", Icon = "fa-solid fa-font-awesome" },
        new() { Text = "Sub menu 3", Id = "1150", ParentId = "1100", Icon = "fa-solid fa-font-awesome" }
    ];

    /// <summary>
    /// TreeFoo 树状数据集
    /// </summary>
    /// <remarks>请勿更改，单元测试使用</remarks>
    /// <returns></returns>
    public static List<TreeViewItem<TreeFoo>> GetTreeItems()
    {
        var items = GetItems();

        // 算法获取属性结构数据
        return CascadingTree(items);
    }

    public static List<TreeViewItem<TreeFoo>> GetAccordionItems()
    {
        List<TreeFoo> items =
        [
            new() { Text = "Node-01", Id = "1010", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Node-02", Id = "1020", Icon = "fa-solid fa-font-awesome" }
        ];

        // 算法获取属性结构数据
        var tree = CascadingTree(items);
        tree[0].HasChildren = true;
        tree[1].HasChildren = true;

        return tree;
    }

    public static List<TreeViewItem<TreeFoo>> GetLazyItems()
    {
        List<TreeFoo> items =
        [
            new() { Text = "LazyNode-01", Id = "1010", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "LazyNode-02", Id = "1020", Icon = "fa-solid fa-font-awesome" }
        ];

        // 算法获取属性结构数据
        var tree = CascadingTree(items);
        tree[0].HasChildren = true;
        tree[1].HasChildren = true;
        return tree;
    }

    public static List<TreeViewItem<TreeFoo>> GetTemplateItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].Template = foo => BootstrapDynamicComponent.CreateComponent<CustomTreeItem>(new Dictionary<string, object?>() { [nameof(CustomTreeItem.Foo)] = foo }).Render();
        return ret;
    }

    public static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandAccordionNodeAsync(TreeViewItem<TreeFoo> node)
    {
        await Task.Delay(200);
        var item = node.Value;
        return new List<TreeViewItem<TreeFoo>>
        {
            new(new TreeFoo() { Id = $"{item.Id}-1", ParentId = item.Id }) { Text = "懒加载子节点" }
        };
    }

    public static List<TreeViewItem<TreeFoo>> GetColorItems()
    {
        var ret = TreeFoo.GetTreeItems();
        ret[0].CssClass = "text-primary";
        ret[1].CssClass = "text-success";
        ret[2].CssClass = "text-danger";
        return ret;
    }

    public static List<TreeViewItem<TreeFoo>> GetVirtualizeTreeItems()
    {
        var ret = new List<TreeViewItem<TreeFoo>>();
        Enumerable.Range(1, 100).ToList().ForEach(i =>
        {
            ret.Add(new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"{i}" })
            {
                Text = $"Root{i}",
                HasChildren = true
            });
        });
        return ret;
    }

    public static List<TreeViewItem<TreeFoo>> GetFlatItems()
    {
        var items = new List<TreeViewItem<TreeFoo>>();
        var foo = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "navigation one", Id = "1010", Icon = "fa-solid fa-font-awesome" }) { Text = "TreeNode 1" };
        items.Add(foo);

        foo = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Navigation two", Id = "1020", Icon = "fa-solid fa-font-awesome" }) { Text = "TreeNode 2", HasChildren = true };
        items.Add(foo);

        var sub = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Sub menu 1", Id = "1040", ParentId = "1020", Icon = "fa-solid fa-font-awesome" }) { Parent = foo, Text = "TreeNode 1001" };
        foo.Items.Add(sub);

        sub = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Sub menu 2", Id = "1050", ParentId = "1020", Icon = "fa-solid fa-font-awesome" }) { Parent = foo, Text = "TreeNode 1002" };
        foo.Items.Add(sub);

        sub = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Sub menu 3", Id = "1060", ParentId = "1020", Icon = "fa-solid fa-font-awesome" }) { Parent = foo, Text = "TreeNode 1003" };
        foo.Items.Add(sub);

        foo = new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Navigation three", Id = "1030", Icon = "fa-solid fa-font-awesome" }) { Text = "TreeNode 2" };
        items.Add(foo);

        return items;
    }

    /// <summary>
    /// TreeFoo 带选择框树状数据集
    /// </summary>
    /// <returns></returns>
    public static List<TreeViewItem<TreeFoo>> GetCheckedTreeItems(string? parentId = null)
    {
        var node1 = new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"{parentId}-101", ParentId = parentId })
        {
            Text = "navigation one",
            HasChildren = true
        };
        var node2 = new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"{parentId}-102", ParentId = parentId })
        {
            Text = "navigation two",
            CheckedState = CheckboxState.Checked
        };
        return [node1, node2];
    }

    /// <summary>
    /// 树状数据层次化方法
    /// </summary>
    /// <param name="items">数据集合</param>
    /// <param name="treeviewItemCallback">节点状态回调方法</param>
    public static List<TreeViewItem<TreeFoo>> CascadingTree(IEnumerable<TreeFoo> items, Action<TreeViewItem<TreeFoo>>? treeviewItemCallback = null) => items.CascadingTree(null, (foo, parent) => foo.ParentId == parent?.Value.Id, foo => new TreeViewItem<TreeFoo>(foo)
    {
        Text = foo.Text,
        Icon = foo.Icon,
        IsActive = foo.IsActive
    }, treeviewItemCallback);
}
