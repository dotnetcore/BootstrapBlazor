namespace BootstrapBlazor.Components;

/// <summary>
/// Toolbar 组件
/// </summary>
public partial class Toolbar
{
    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-toolbar")
        .Build();
}
