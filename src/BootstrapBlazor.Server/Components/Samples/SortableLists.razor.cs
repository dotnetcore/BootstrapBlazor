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
    private List<Foo>? ItemsSwaps { get; set; }

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
    }

    private Task OnUpdate(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items[oldIndex];
        Items.RemoveAt(oldIndex);
        Items.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate1(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate2(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items2[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove1(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove2(SortableEvent @event)
    {
        var oldIndex = @event.OldIndex;
        var newIndex = @event.NewIndex;
        var item = Items2[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
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
        var item = ItemsSwaps[oldIndex];
        ItemsSwaps.RemoveAt(oldIndex);
        ItemsSwaps.Insert(newIndex, item);
        return Task.CompletedTask;
    }
}
