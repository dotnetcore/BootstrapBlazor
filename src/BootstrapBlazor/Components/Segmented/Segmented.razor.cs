// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Segmented 组件</para>
/// <para lang="en">Segmented Component</para>
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TValue))]
#endif
public partial class Segmented<TValue>
{
    private string? ClassString => CssBuilder.Default("segmented")
        .AddClass($"segmented-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("block", IsBlock)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetLabelClassString(SegmentedOption<TValue> item) => CssBuilder.Default("segmented-item")
        .AddClass("selected", CurrentItem == item)
        .AddClass("disabled", GetDisabled(item))
        .Build();

    [NotNull]
    private SegmentedOption<TValue>? CurrentItem { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项集合 默认 null</para>
    /// <para lang="en">Gets or sets Items. Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SegmentedOption<TValue>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中值 默认 null</para>
    /// <para lang="en">Gets or sets Value. Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中值回调委托 默认 null</para>
    /// <para lang="en">Gets or sets Value Changed Callback Delegate. Default null</para>
    /// </summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中值改变后回调委托方法 默认 null</para>
    /// <para lang="en">Gets or sets Value Changed Callback Method. Default null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认 false</para>
    /// <para lang="en">Gets or sets Whether disabled. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否充满父元素 默认 false</para>
    /// <para lang="en">Gets or sets Whether is block. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsBlock { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动显示 Tooltip 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show tooltip. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件大小 默认值 <see cref="Size.None"/></para>
    /// <para lang="en">Gets or sets Size. Default <see cref="Size.None"/></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 候选项模板 默认 null</para>
    /// <para lang="en">Gets or sets Item Template. Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<SegmentedOption<TValue>>? ItemTemplate { get; set; }

    private readonly List<SegmentedOption<TValue>> _items = [];

    private string? TooltipString => ShowTooltip && IsBlock ? "tooltip" : null;

    private bool GetDisabled(SegmentedOption<TValue> item) => IsDisabled || item.IsDisabled;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<SegmentedOption<TValue>>();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    private IEnumerable<SegmentedOption<TValue>> GetItems()
    {
        CurrentItem ??= _options.FirstOrDefault(i => (i.Value != null && i.Value.Equals(Value)) || i.Active) ?? _options.FirstOrDefault();
        if (CurrentItem != null)
        {
            Value = CurrentItem.Value;
        }
        return _options;
    }

    private IEnumerable<SegmentedOption<TValue>> _options => _items.Concat(Items);

    /// <summary>
    /// <para lang="zh">点击 SegmentItem 节点 JavaScript 回调触发</para>
    /// <para lang="en">JavaScript Callback Triggered when SegmentItem Node Clicked</para>
    /// </summary>
    /// <param name="index"></param>
    [JSInvokable]
    public async Task TriggerClick(int index)
    {
        var options = _options;
        var item = _options.ElementAtOrDefault(index);
        if (item != null && !GetDisabled(item))
        {
            foreach (var op in _options)
            {
                item.Active = item == op;
            }

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
}
