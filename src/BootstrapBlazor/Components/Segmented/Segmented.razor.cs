namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Segmented
{
    private string? ClassString(SelectedItem item)
    {
        return CssBuilder.Default("segmented-item")
               .AddClass("segmented-item-selected", item.Active)
               .AddClass("segmented-item-disabled", item.IsDisabled)
               .Build();
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public SelectedItem? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<SelectedItem> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Value = Items.First();
    }

    private async Task OnClick(SelectedItem item)
    {
        Value.Active = !Value.Active;
        item.Active = true;
        Value = item;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
