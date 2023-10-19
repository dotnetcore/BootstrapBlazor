namespace BootstrapBlazor.Components;

/// <summary>
/// Segmented 组件
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
        .AddClass("disabled", GetDisabled(item))
        .Build();

    [NotNull]
    private SegmentedOption<TValue>? CurrentItem { get; set; }

    /// <summary>
    /// 获得/设置 选项集合 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SegmentedOption<TValue>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 选中值 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    ///  获得/设置 选中值回调委托 默认 null
    /// </summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 选中值改变后回调委托方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 组件大小 默认值 <see cref="Size.None"/>
    /// </summary>
    [Parameter]
    [NotNull]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 候选项模板 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<SegmentedOption<TValue>>? ItemTemplate { get; set; }

    private readonly List<SegmentedOption<TValue>> _items = new();

    private bool GetDisabled(SegmentedOption<TValue> item) => IsDisabled || item.IsDisabled;

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
        CurrentItem ??= items.FirstOrDefault(i => (i.Value != null && i.Value.Equals(Value)) || i.Active) ?? items.FirstOrDefault();
        if (CurrentItem != null)
        {
            Value = CurrentItem.Value;
        }
        return items;
    }

    private async Task OnClick(SegmentedOption<TValue> item)
    {
        if (!GetDisabled(item))
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
        var items = _items.Concat(Items);
        foreach (var item in items)
        {
            item.Active = item == option;
        }
    }
}
