// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// SortableList 示例
/// </summary>
public partial class SortableLists
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private List<Foo>? Items1 { get; set; }

    [NotNull]
    private List<Foo>? Items2 { get; set; }

    [NotNull]
    private List<Foo>? ItemsMultiDrags { get; set; }

    [NotNull]
    private List<Foo>? ItemsCloneLeft { get; set; }

    [NotNull]
    private List<Foo>? ItemsCloneRight { get; set; }

    [NotNull]
    private List<Foo>? ItemsSwaps { get; set; }

    [NotNull]
    private List<Foo>? AddItems1 { get; set; }

    [NotNull]
    private List<Foo>? AddItems2 { get; set; }

    [NotNull]
    private List<Foo>? AddItems3 { get; set; }

    private readonly SortableOption _option1 = new()
    {
        RootSelector = ".sl-list"
    };

    private readonly SortableOption _option2 = new()
    {
        RootSelector = ".sl-list",
        Group = "group"
    };

    private readonly SortableOption _option31 = new()
    {
        RootSelector = ".sl-list",
        Group = "group-clone",
        Clone = true,
        Putback = false
    };

    private readonly SortableOption _option32 = new()
    {
        RootSelector = ".sl-list",
        Group = "group-clone",
    };

    private readonly SortableOption _option4 = new()
    {
        RootSelector = ".sl-list",
        Group = "group-clone",
        Clone = true,
        Putback = false
    };

    private readonly SortableOption _option5 = new()
    {
        RootSelector = ".sl-list",
        Group = "group-clone",
        Sort = false
    };

    private readonly SortableOption _option6 = new()
    {
        RootSelector = ".sl-list",
        Handle = "i"
    };

    private readonly SortableOption _option7 = new()
    {
        RootSelector = ".sl-list",
        Filter = ".filter"
    };

    private readonly SortableOption _optionTable = new()
    {
        RootSelector = "tbody"
    };

    private readonly SortableOption _optionMulti = new()
    {
        RootSelector = ".sl-list",
        MultiDrag = true
    };

    private readonly SortableOption _optionSwap = new()
    {
        RootSelector = ".sl-list",
        Swap = true
    };

    private readonly SortableOption _optionAdd = new()
    {
        RootSelector = ".sl-list",
        Group = "group-add"
    };

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        //获取随机数据
        //Get random data
        Items = Foo.GenerateFoo(FooLocalizer, 8);
        Items1 = Foo.GenerateFoo(FooLocalizer, 4);
        Items2 = Foo.GenerateFoo(FooLocalizer, 8).Skip(4).ToList();
        ItemsMultiDrags = Foo.GenerateFoo(FooLocalizer, 8);
        ItemsSwaps = Foo.GenerateFoo(FooLocalizer, 8);
        ItemsCloneLeft = Foo.GenerateFoo(FooLocalizer, 4);
        ItemsCloneRight = Foo.GenerateFoo(FooLocalizer, 8).Skip(4).ToList();
        AddItems1 = Foo.GenerateFoo(FooLocalizer, 4);
        AddItems2 = Foo.GenerateFoo(FooLocalizer, 8).Skip(4).ToList();
        AddItems3 = Foo.GenerateFoo(FooLocalizer, 12).Skip(8).ToList();
    }

    private Task OnUpdateTable(SortableEvent @event)
    {
        var oldItem = Items[@event.OldIndex];
        Items.Remove(oldItem);
        Items.Insert(@event.NewIndex, oldItem);

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items[oldIndex];
        Items.RemoveAt(oldIndex);
        Items.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnAdd1(SortableEvent @event)
    {
        var item = Items2[@event.OldIndex];
        Items1.Insert(@event.NewIndex, item);
        return Task.CompletedTask;
    }

    private Task OnUpdate1(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnRemove1(SortableEvent @event)
    {
        Items1.RemoveAt(@event.OldIndex);
        return Task.CompletedTask;
    }

    private Task OnAdd2(SortableEvent @event)
    {
        var item = Items1[@event.OldIndex];
        Items2.Insert(@event.NewIndex, item);
        return Task.CompletedTask;
    }

    private Task OnAddClone(SortableEvent @event)
    {
        var item = ItemsCloneLeft[@event.OldIndex];
        ItemsCloneRight.Insert(@event.NewIndex, new Foo() { Name = item.Name });
        return Task.CompletedTask;
    }

    private Task OnUpdate2(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items2[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnRemove2(SortableEvent @event)
    {
        Items2.RemoveAt(@event.OldIndex);
        return Task.CompletedTask;
    }

    private Task OnUpdateMultiDrag(SortableEvent @event)
    {
        var items = @event.Items;

        // 找到移除元素
        var removeItems = new List<Foo>();
        for (var index = items.Count - 1; index >= 0; index--)
        {
            var item = ItemsMultiDrags[items[index].OldIndex];
            removeItems.Insert(0, item);
            ItemsMultiDrags.RemoveAt(items[index].OldIndex);
        }

        // 插入元素
        for (var index = 0; index < items.Count; index++)
        {
            var item = removeItems[index];
            ItemsMultiDrags.Insert(items[index].NewIndex, item);
        }
        return Task.CompletedTask;
    }

    private Task OnUpdateSwap(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item1 = Utility.Clone(ItemsSwaps[oldIndex]);
        var item2 = Utility.Clone(ItemsSwaps[newIndex]);
        ItemsSwaps[oldIndex] = item2;
        ItemsSwaps[newIndex] = item1;
        return Task.CompletedTask;
    }

    private Task OnAddItems1(SortableEvent @event)
    {
        var foo = @event.FromId == "sl02"
           ? AddItems2[@event.OldIndex]
           : AddItems3[@event.OldIndex];
        AddItems1.Insert(@event.NewIndex, foo);
        return Task.CompletedTask;
    }

    private Task OnUpdateItems1(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = AddItems1[oldIndex];
        AddItems1.RemoveAt(oldIndex);
        AddItems1.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnRemoveItems1(SortableEvent @event)
    {
        AddItems1.RemoveAt(@event.OldIndex);
        return Task.CompletedTask;
    }

    private Task OnAddItems2(SortableEvent @event)
    {
        var foo = @event.FromId == "sl01"
           ? AddItems1[@event.OldIndex]
           : AddItems3[@event.OldIndex];
        AddItems2.Insert(@event.NewIndex, foo);
        return Task.CompletedTask;
    }

    private Task OnUpdateItems2(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = AddItems2[oldIndex];
        AddItems2.RemoveAt(oldIndex);
        AddItems2.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnRemoveItems2(SortableEvent @event)
    {
        AddItems2.RemoveAt(@event.OldIndex);
        return Task.CompletedTask;
    }

    private Task OnAddItems3(SortableEvent @event)
    {
        var foo = @event.FromId == "sl01"
           ? AddItems1[@event.OldIndex]
           : AddItems2[@event.OldIndex];
        AddItems3.Insert(@event.NewIndex, foo);
        return Task.CompletedTask;
    }

    private Task OnUpdateItems3(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = AddItems3[oldIndex];
        AddItems3.RemoveAt(oldIndex);
        AddItems3.Insert(newIndex, item);
        return Task.CompletedTask;
    }

    private Task OnRemoveItems3(SortableEvent @event)
    {
        AddItems3.RemoveAt(@event.OldIndex);
        return Task.CompletedTask;
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(SortableList.Option),
            Description = Localizer["AttributeSortableListOption"],
            Type = "SortableOption",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(SortableList.OnAdd),
            Description = Localizer["AttributeOnAdd"],
            Type = "Func<SortableEvent, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(SortableList.OnUpdate),
            Description = Localizer["AttributeOnUpdate"],
            Type = "Func<SortableEvent, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(SortableList.OnRemove),
            Description = Localizer["AttributeOnRemove"],
            Type = "Func<SortableEvent, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
