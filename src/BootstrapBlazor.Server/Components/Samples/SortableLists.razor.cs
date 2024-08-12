﻿namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// SortableList 示例
/// </summary>
public partial class SortableLists
{
    [NotNull]
    private List<Foo>? Items { get; set; }

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

    private readonly SortableOption _option = new()
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
        Items = Foo.GenerateFoo(FooLocalizer);
    }
}
