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
    private List<EditFooTree>? AllItems { get; set; }

    [NotNull]
    private IEnumerable<FooTree>? TreeItems { get; set; }

    [NotNull]
    private IEnumerable<ExpandFooTree>? ExpandTreeItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private int level = 0;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        TreeItems = FooTree.Generate(Localizer);
        AllItems = new List<EditFooTree>();
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 10, 1));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 20, 2));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 30, 11));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 40, 12));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 50, 21));
        AllItems.AddRange(EditFooTree.Generate(Localizer, AllItems, 60, 22));

        ExpandTreeItems = ExpandFooTree.Generate(20, 1);
    }

    private async Task<IEnumerable<ExpandFooTree>> OnExpandTreeExpand(ExpandFooTree foo)
    {
        await Task.Delay(1000);
        return ExpandFooTree.Generate(2, foo.Id * 10 + 10).Select(i =>
        {
            i.Name = Localizer["Foo.Name", $"{foo.Id:d2}{i.Id:d2}"];
            return i;
        });
    }

    private async Task<IEnumerable<FooTree>> OnTreeExpand(FooTree foo)
    {
        await Task.Delay(1000);
        return FooTree.Generate(Localizer, level++ < 2, foo.Id + 10).Select(i =>
        {
            i.Name = Localizer["Foo.Name", $"{foo.Id:d2}{i.Id:d2}"];
            return i;
        });
    }

    private Task<EditFooTree> OnAddAsync() => Task.FromResult(new EditFooTree() { AllItems = AllItems, DateTime = DateTime.Now });

    private Task<bool> OnSaveAsync(EditFooTree item, ItemChangedType changedType)
    {
        if (changedType == ItemChangedType.Add)
        {
            item.Id = AllItems.Max(i => i.Id) + 1;
            AllItems.Add(item);
        }
        else
        {
            var oldItem = AllItems.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.ParentId = item.ParentId;
                oldItem.Name = item.Name;
                oldItem.DateTime = item.DateTime;
                oldItem.Address = item.Address;
                oldItem.Count = item.Count;
            }
        }
        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<EditFooTree> items)
    {
        items.ToList().ForEach(i => AllItems.Remove(i));
        return Task.FromResult(true);
    }

    private Task<QueryData<EditFooTree>> OnQueryAsync(QueryPageOptions _)
    {
        return Task.FromResult(new QueryData<EditFooTree>()
        {
            Items = AllItems.Where(f => f.ParentId == 0)
        });
    }

    private async Task<IEnumerable<EditFooTree>> OnTreeExpandQuery(EditFooTree foo)
    {
        await Task.Delay(50);
        return AllItems.Where(f => f.ParentId == foo.Id);
    }

    private class FooTree : Foo
    {
        private static readonly Random random = new();

        public IEnumerable<FooTree>? Children { get; set; }

        public bool HasChildren { get; set; }

        public static IEnumerable<FooTree> Generate(IStringLocalizer<Foo> localizer, bool hasChildren = true, int seed = 0) => Enumerable.Range(1, 2).Select(i => new FooTree()
        {
            Id = i + seed,
            Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel,
            HasChildren = hasChildren
        }).ToList();
    }

    private class EditFooTree : Foo
    {
        private static readonly Random random = new();

        [NotNull]
        public List<EditFooTree>? AllItems { get; set; }

        public int ParentId { get; set; }

        public IEnumerable<EditFooTree>? Children { get; set; }

        public bool HasChildren => AllItems.Any(i => i.ParentId == Id);

        public static IEnumerable<EditFooTree> Generate(IStringLocalizer<Foo> localizer, List<EditFooTree> list, int seed = 0, int parentId = 0) => Enumerable.Range(1, 2).Select(i => new EditFooTree()
        {
            Id = i + seed,
            ParentId = parentId,
            Name = localizer["Foo.Name", $"{seed:d2}{(i + seed):d2}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
            Count = random.Next(1, 100),
            AllItems = list
        }).ToList();
    }

    private class ExpandFooTree : Foo, ITableTreeItem<ExpandFooTree>
    {
        public List<ExpandFooTree> Children { get; set; } = new();

        public bool IsExpand
        {
            get => Children.Any();
            set
            {
                if (!value) Children.Clear();
            }
        }

        public void SetChildren(IEnumerable<ExpandFooTree> items)
        {
            Children = items.ToList();
        }

        IEnumerable<ITableTreeItem<ExpandFooTree>>? ITableTreeItem<ExpandFooTree>.Children => Children;

        private static readonly Random random = new();

        public static IEnumerable<ExpandFooTree> Generate(int count, int id = 1)
        {
            while (count > 0)
            {
                int n = random.Next(Math.Max(1, count / 8), count);
                count -= n;
                yield return new ExpandFooTree()
                {
                    Id = id,
                    Name = "Foo" + id,
                    DateTime = System.DateTime.Now,
                    Count = random.Next(1, 100),
                    Complete = random.Next(1, 100) > 50,
                    Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel,
                    Children = Generate(n - 1, (id++) * 10).ToList()
                };
            }
        }
    }
}
