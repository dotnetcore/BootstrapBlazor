namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Segmented
{

    private string? SegmentedClassString => CssBuilder.Default("segmented")
               .AddClass("segmented-block", IsBlock)
               .AddClass("segmented-lg", Size == Size.Large)
               .AddClass("segmented-sm", Size == Size.Small)
               .Build();

    private string? ClassString(SegmentedItem item)
    {
        return CssBuilder.Default("segmented-item")
               .AddClass("segmented-item-selected", item.Active)
               .AddClass("segmented-item-disabled", item.IsDisabled)
               .Build();
    }

    [NotNull]
    private SegmentedItem? CurrentItem { get; set; }

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
    public string? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public bool IsBlock { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public Size Size { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RenderFragment<SegmentedItem>? DisplayItemTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (string.IsNullOrEmpty(Value))
        {
            var item = Items.First();
            Value = item.Value;
            CurrentItem = item;
        }
        else
        {
            var item = Items.First(s => s.Value == Value);
            foreach (var value in Items)
            {
                value.Active = false;
            }
            item.Active = !item.Active;
            CurrentItem = item;
        }
    }

    private async Task OnClick(SegmentedItem item)
    {
        CurrentItem.Active = !CurrentItem.Active;
        item.Active = !item.Active;
        Value = item.Value;
        CurrentItem = item;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
