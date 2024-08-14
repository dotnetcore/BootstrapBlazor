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

    private readonly SortableOption _option1 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost"
    };

    private readonly SortableOption _option2 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group"
    };

    private readonly SortableOption _option31 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group-clone",
        Clone = true,
        Putback = false
    };

    private readonly SortableOption _option32 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group-clone",
    };

    private readonly SortableOption _option4 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group-clone",
        Clone = true,
        Putback = false
    };

    private readonly SortableOption _option5 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group-clone",
        Sort = false
    };

    private readonly SortableOption _option6 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Handle = "i"
    };

    private readonly SortableOption _option7 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
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
    }

    private Task OnUpdate(List<SortableListItem> items)
    {
        var oldIndex = items[0].OldIndex;
        var newIndex = items[0].NewIndex;
        var item = Items[oldIndex];
        Items.RemoveAt(oldIndex);
        Items.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate1(List<SortableListItem> items)
    {
        var oldIndex = items[0].OldIndex;
        var newIndex = items[0].NewIndex;
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate2(List<SortableListItem> items)
    {
        var oldIndex = items[0].OldIndex;
        var newIndex = items[0].NewIndex;
        var item = Items[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove1(List<SortableListItem> items)
    {
        var oldIndex = items[0].OldIndex;
        var newIndex = items[0].NewIndex;
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove2(List<SortableListItem> items)
    {
        var oldIndex = items[0].OldIndex;
        var newIndex = items[0].NewIndex;
        var item = Items2[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateMultiDrag(List<SortableListItem> items)
    {
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
}
