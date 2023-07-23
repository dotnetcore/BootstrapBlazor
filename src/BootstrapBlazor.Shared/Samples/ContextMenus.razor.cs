namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// ContextMenu 组件示例
/// </summary>
public partial class ContextMenus
{
    private List<TreeViewItem<TreeFoo>> TreeItems { get; set; } = TreeFoo.GetTreeItems();

    private Task OnCopy(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    private Task OnPaste(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Foo.GenerateFoo(Localizer);
    }
}
