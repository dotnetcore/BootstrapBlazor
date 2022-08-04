// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared;

class TreeFoo
{
    [Key]
    public string? Id { get; set; }

    public string? ParentId { get; set; }

    public string? Text { get; set; }

    public string Icon { get; set; } = "fa fa-fa";

    public bool IsActive { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<TreeViewItem<TreeFoo>> GetTreeItems()
    {
        var items = new List<TreeFoo>
        {
            new TreeFoo() { Text = "导航一", Id = "1010", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "导航二", Id = "1020", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "导航三", Id = "1030", Icon = "fa fa-fa" },

            new TreeFoo() { Text = "子菜单一", Id = "1040", ParentId = "1020", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "子菜单二", Id = "1050", ParentId = "1020", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "子菜单三", Id = "1060", ParentId = "1020", Icon = "fa fa-fa" },

            new TreeFoo() { Text = "孙菜单一", Id = "1070", ParentId = "1050", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "孙菜单二", Id = "1080", ParentId = "1050", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "孙菜单三", Id = "1090", ParentId = "1050", Icon = "fa fa-fa" },

            new TreeFoo() { Text = "曾孙菜单一", Id = "1100", ParentId = "1080", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "曾孙菜单二", Id = "1110", ParentId = "1080", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "曾孙菜单三", Id = "1120", ParentId = "1080", Icon = "fa fa-fa" },

            new TreeFoo() { Text = "曾曾孙菜单一", Id = "1130", ParentId = "1100", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "曾曾孙菜单二", Id = "1140", ParentId = "1100", Icon = "fa fa-fa" },
            new TreeFoo() { Text = "曾曾孙菜单三", Id = "1150", ParentId = "1100", Icon = "fa fa-fa" }
        };

        // 算法获取属性结构数据
        return CascadingTree(items).ToList();
    }

    /// <summary>
    /// 树状数据层次化方法
    /// </summary>
    /// <param name="items">数据集合</param>
    /// <param name="parent">父级节点</param>
    public static IEnumerable<TreeViewItem<TreeFoo>> CascadingTree(IEnumerable<TreeFoo> items, TreeViewItem<TreeFoo>? parent = null) => items.Where(i => i.ParentId == parent?.Value.Id).Select(i =>
    {
        var item = new TreeViewItem<TreeFoo>(i)
        {
            Text = i.Text,
            Icon = i.Icon
        };
        item.Items = CascadingTree(items, item).ToList();
        item.Parent = parent;
        return item;
    });
}
