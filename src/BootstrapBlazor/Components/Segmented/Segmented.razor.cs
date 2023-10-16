namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Segmented
{
    private string? ClassString(SegmentedItem item)
    {
        return CssBuilder.Default("segmented-item")
               .AddClass("segmented-item-selected", item.Active)
               .AddClass("segmented-item-disabled", item.IsDisabled)
               .AddClass("slideInLeft", item.Active)
               .Build();
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SegmentedItem>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public SegmentedItem? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<SegmentedItem> ValueChanged { get; set; } 

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Value = Items.First();
    }

    private async Task OnClick(SegmentedItem item)
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
