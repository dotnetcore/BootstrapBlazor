namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Segmented
{
    private string? ClassString => CssBuilder.Default("segmented")
        .AddClass("segmented-block", IsBlock)
        .AddClass($"segmented-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? GetLabelClassString(SegmentedItem item) => CssBuilder.Default("segmented-item")
        .AddClass("selected", item.Active)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    [NotNull]
    private SegmentedItem? CurrentItem { get; set; }

    /// <summary>
    /// Get or Set up a data source
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SegmentedItem>? Items { get; set; }

    /// <summary>
    /// Get or Set Value
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    ///  Get or Set ValueChanged Event
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// Get or Set OnValueChanged Event
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// Get or Set IsBlock Property
    /// </summary>
    [Parameter]
    [NotNull]
    public bool IsBlock { get; set; }

    /// <summary>
    /// Get or Set Size Property
    /// </summary>
    [Parameter]
    [NotNull]
    public Size Size { get; set; }

    /// <summary>
    /// Get or Set DisplayItemTemplate
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<SegmentedItem>? DisplayItemTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (string.IsNullOrEmpty(Value))
        {
            SetActive(Items);
            var item = Items.First();
            item.Active = !item.Active;
            Value = item.Value;
            CurrentItem = item;
        }
        else
        {
            SetActive(Items);
            var item = Items.First(s => s.Value == Value);
            if (item != null)
            {
                item.Active = !item.Active;
                CurrentItem = item;
            }
        }
    }

    private async Task OnClick(SegmentedItem item)
    {
        SetActive(Items);
        item.Active = !item.Active;
        Value = item.Value;
        CurrentItem = item;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }

        if (OnValueChanged != null)
        {
            await OnValueChanged(Value);
        }
    }

    private void SetActive(IEnumerable<SegmentedItem> items)
    {
        foreach (var item in items)
        {
            item.Active = false;
        }
    }
}
