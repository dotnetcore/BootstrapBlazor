// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 树形数据演示示例代码
/// </summary>
public partial class TablesTree
{
    [NotNull]
    private List<TreeFoo>? TreeItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 模拟数据从数据库中获得
        TreeItems = TreeFoo.GenerateFoos(Localizer, 3);

        // 插入 Id 为 1 的子项
        TreeItems.AddRange(TreeFoo.GenerateFoos(Localizer, 2, 1, 100));

        // 插入 Id 为 101 的子项
        TreeItems.AddRange(TreeFoo.GenerateFoos(Localizer, 3, 101, 1010));
    }

    private static Task<IEnumerable<TableTreeNode<TreeFoo>>> TreeNodeConverter(IEnumerable<TreeFoo> items)
    {
        // 构造树状数据结构
        var ret = BuildTreeNodes(items, 0);
        return Task.FromResult(ret);

        IEnumerable<TableTreeNode<TreeFoo>> BuildTreeNodes(IEnumerable<TreeFoo> items, int parentId)
        {
            var ret = new List<TableTreeNode<TreeFoo>>();
            ret.AddRange(items.Where(i => i.ParentId == parentId).Select((foo, index) => new TableTreeNode<TreeFoo>(foo)
            {
                // 此处为示例，假设偶行数据都有子数据
                HasChildren = index % 2 == 0,
                // 如果子项集合有值 则默认展开此节点
                IsExpand = items.Any(i => i.ParentId == foo.Id),
                // 获得子项集合
                Items = BuildTreeNodes(items.Where(i => i.ParentId == foo.Id), foo.Id)
            }));
            return ret;
        }
    }

    [Inject]
    [NotNull]
    private ICacheManager? CacheManager { get; set; }

    private Task<IEnumerable<TableTreeNode<TreeFoo>>> OnTreeExpand(TreeFoo foo)
    {
        // 模拟从数据库中查询
        return CacheManager.GetOrCreateAsync($"{foo.Id}", async entry =>
        {
            await Task.Delay(1000);
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return TreeFoo.GenerateFoos(Localizer, 2, foo.Id, foo.Id * 100).Select(i => new TableTreeNode<TreeFoo>(i));
        });
    }

    //private static bool TreeNodeEqualityComparer(Foo a, Foo b) => a.Id == b.Id;

    private static Task<TreeFoo> OnAddAsync() => Task.FromResult(new TreeFoo() { DateTime = DateTime.Now });

    private Task<bool> OnSaveAsync(TreeFoo item, ItemChangedType changedType)
    {
        if (changedType == ItemChangedType.Add)
        {
            item.Id = TreeItems.Max(i => i.Id) + 1;
            TreeItems.Add(item);
        }
        else
        {
            var oldItem = TreeItems.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.Name = item.Name;
                oldItem.DateTime = item.DateTime;
                oldItem.Address = item.Address;
                oldItem.Count = item.Count;
            }
        }
        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<TreeFoo> items)
    {
        TreeItems.RemoveAll(foo => items.Any(i => i.Id == foo.Id));
        return Task.FromResult(true);
    }

    private Task<QueryData<TreeFoo>> OnQueryAsync(QueryPageOptions _)
    {
        var items = TreeFoo.GenerateFoos(Localizer, 4);

        // 插入 Id 为 1 的子项
        items.AddRange(TreeFoo.GenerateFoos(Localizer, 2, 1, 100));

        // 插入 Id 为 101 的子项
        items.AddRange(TreeFoo.GenerateFoos(Localizer, 3, 101, 1010));

        var data = new QueryData<TreeFoo>()
        {
            Items = items
        };
        return Task.FromResult(data);
    }

    private class TreeFoo : Foo
    {
        public int ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<TreeFoo> GenerateFoos(IStringLocalizer<Foo> localizer, int count = 80, int parentId = 0, int id = 0) => Enumerable.Range(1, count).Select(i => new TreeFoo()
        {
            Id = id + i,
            ParentId = parentId,
            Name = localizer["Foo.Name", $"{id + i:d4}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{Random.Next(1000, 2000)}"],
            Count = Random.Next(1, 100),
            Complete = Random.Next(1, 100) > 50,
            Education = Random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
        }).ToList();
    }
}
