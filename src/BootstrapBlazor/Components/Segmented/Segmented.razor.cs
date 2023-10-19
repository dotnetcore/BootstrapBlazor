namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TValue))]
#endif
public partial class Segmented<TValue>
{
    private string? ClassString => CssBuilder.Default("segmented")
        .AddClass($"segmented-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetLabelClassString(SegmentedOption<TValue> item) => CssBuilder.Default("segmented-item")
        .AddClass("selected", CurrentItem == item)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    [NotNull]
    private SegmentedOption<TValue>? CurrentItem { get; set; }

    /// <summary>
    /// Get or Set up a data source
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SegmentedOption<TValue>>? Items { get; set; }

    /// <summary>
    /// Get or Set Value
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    ///  Get or Set ValueChanged Event
    /// </summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Get or Set OnValueChanged Event
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Get or Set Size Property
    /// </summary>
    [Parameter]
    [NotNull]
    public Size Size { get; set; }

    /// <summary>
    /// Get or Set ItemTemplate
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<SegmentedOption<TValue>>? ItemTemplate { get; set; }

    private List<SegmentedOption<TValue>> _items = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<SegmentedOption<TValue>>();
    }

    private IEnumerable<SegmentedOption<TValue>> GetItems()
    {
        var items = _items.Concat(Items);
        CurrentItem ??= items.FirstOrDefault(i => i.Active) ?? items.FirstOrDefault();
        return items;
    }

    private async Task OnClick(SegmentedOption<TValue> item)
    {
        if (!item.IsDisabled)
        {
            SetActive(item);

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
    }

    private void SetActive(SegmentedOption<TValue> option)
    {
        foreach (var item in Items)
        {
            item.Active = item == option;
        }
    }
}
