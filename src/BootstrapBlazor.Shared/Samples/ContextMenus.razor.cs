namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// ContextMenu 组件示例
/// </summary>
public partial class ContextMenus
{
    private List<TreeViewItem<TreeFoo>> TreeItems { get; set; } = TreeFoo.GetTreeItems();

    private static Task OnCopy(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    private static Task OnPaste(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private Foo? Foo { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        Foo = Foo.Generate(LocalizerFoo);
        Items = Foo.GenerateFoo(LocalizerFoo);
    }
}
