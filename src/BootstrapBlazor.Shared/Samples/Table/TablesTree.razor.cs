// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 树形数据演示示例代码
/// </summary>
public partial class TablesTree
{
    [NotNull]
    private List<Foo>? TreeItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ICacheManager? CacheManager { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        TreeItems = CacheManager.GetOrCreate($"TableTree-Foos-{CultureInfo.CurrentUICulture.Name}", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            var foos = Foo.GenerateFoo(Localizer, 10);
            return foos;
        });
    }

    private Task<IEnumerable<TableTreeNode<Foo>>> OnBuildFooTreeAsync(IEnumerable<Foo> items)
    {
        var nodes = items.Select((foo, index) =>
        {
            var node = new TableTreeNode<Foo>(foo)
            {
                HasChildren = index % 2 == 0,
                IsExpand = index == 2
            };
            if (index == 2)
            {
                node.Items = CacheManager.GetOrCreate($"TableTree-Foos-{CultureInfo.CurrentUICulture.Name}-{foo.Id}", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(5);
                    var foos = Foo.GenerateFoo(Localizer, 4).Select((foo, index) =>
                    {
                        foo.Id = 1000 + index;
                        foo.Name = Localizer["Foo.Name", $"{foo.Id:d4}"];
                        return new TableTreeNode<Foo>(foo);
                    });
                    return foos;
                });
            }
            return node;
        });
        return Task.FromResult(nodes);
    }

    private async Task<IEnumerable<TableTreeNode<Foo>>> OnTreeExpand(Foo foo)
    {
        await Task.Delay(1000);
        return CacheManager.GetOrCreate($"TableTree-Foos-Children-{CultureInfo.CurrentUICulture.Name}-{foo.Id}", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            var foos = Foo.GenerateFoo(Localizer, 4).Select((i, index) =>
            {
                i.Id = foo.Id * 100 + index;
                i.Name = Localizer["Foo.Name", $"{i.Id:d4}"];
                return new TableTreeNode<Foo>(i);
            });
            return foos;
        });
    }

    private bool TreeNodeEqualityComparer(Foo a, Foo b) => a.Id == b.Id;

    private Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { DateTime = DateTime.Now });

    private Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
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

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        TreeItems.RemoveAll(foo => items.Any(i => i.Id == foo.Id));
        return Task.FromResult(true);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions _)
    {
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = TreeItems
        });
    }
}
