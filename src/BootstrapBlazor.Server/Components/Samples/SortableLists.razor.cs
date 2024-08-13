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

    private readonly SortableOption _option3 = new()
    {
        RootSelector = ".sl-list",
        GhostClass = "sl-item-ghost",
        Group = "group-clone",
        Clone = true
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
    }

    private Task OnUpdate(int oldIndex, int newIndex)
    {
        var item = Items[oldIndex];
        Items.RemoveAt(oldIndex);
        Items.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate1(int oldIndex, int newIndex)
    {
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdate2(int oldIndex, int newIndex)
    {
        var item = Items[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove1(int oldIndex, int newIndex)
    {
        var item = Items1[oldIndex];
        Items1.RemoveAt(oldIndex);
        Items2.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnRemove2(int oldIndex, int newIndex)
    {
        var item = Items[oldIndex];
        Items2.RemoveAt(oldIndex);
        Items1.Insert(newIndex, item);
        StateHasChanged();
        return Task.CompletedTask;
    }
}
