// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        new() { Text = "navigation one", Id = "1010", Icon = "fa-solid fa-font-awesome" },
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
        return CascadingTree(items).ToList();
    }

    /// <summary>
    /// TreeFoo 带选择框树状数据集
    /// </summary>
    /// <returns></returns>
    public static List<TreeViewItem<TreeFoo>> GetCheckedTreeItems(string? parentId=null)
    {
        return new List<TreeViewItem<TreeFoo>>
            {
            new(new TreeFoo()
            {
                Id = $"{parentId}-101",
                ParentId=parentId
            })
            {
                Text = "navigation one",
                HasChildren = true
            },
            new(new TreeFoo()
            {
                Id = $"{parentId}-102",
                ParentId=parentId
            })
            {
                Text = "navigation two",
                CheckedState = CheckboxState.Checked
            }
        };
    }

    /// <summary>
    /// 树状数据层次化方法
    /// </summary>
    /// <param name="items">数据集合</param>
    public static IEnumerable<TreeViewItem<TreeFoo>> CascadingTree(IEnumerable<TreeFoo> items) => items.CascadingTree(null,
        (foo, parent) => foo.ParentId == parent?.Value.Id,
        foo => new TreeViewItem<TreeFoo>(foo)
        {
            Text = foo.Text,
            Icon = foo.Icon,
            IsActive = foo.IsActive
        }).ToList();
}
