// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得/设置 是否充满父元素 默认 false
    /// </summary>
    [Parameter]
    public bool IsBlock { get; set; }

    /// <summary>
    /// 获得/设置 是否自动显示 Tooltip 默认 false
    /// </summary>
    [Parameter]
    public bool ShowTooltip { get; set; }

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
    /// <returns></returns>
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
    /// 点击 SegmentItem 节点 JavaScript 回调触发
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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
