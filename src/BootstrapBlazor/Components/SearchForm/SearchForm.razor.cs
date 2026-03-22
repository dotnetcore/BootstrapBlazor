// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索表单类</para>
/// <para lang="en">Search Form Component</para>
/// </summary>
public partial class SearchForm : IShowLabel
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤器改变回调事件 Func 版本</para>
    /// <para lang="en">Gets or sets the filter changed callback event Func version</para>
    /// </summary>
    [Parameter]
    public Func<FilterKeyValueAction, Task>? OnChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每行显示组件数量 默认为 null</para>
    /// <para lang="en">Gets or sets Items Per Row. Default is null</para>
    /// </summary>
    [Parameter]
    public int? ItemsPerRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置行格式 默认 Row 布局</para>
    /// <para lang="en">Gets or sets Row Type. Default is Row</para>
    /// </summary>
    [Parameter]
    public RowType RowType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置 <see cref="RowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐</para>
    /// <para lang="en">Gets or sets Label Alignment in <see cref="RowType" /> Inline mode. Default is None, equivalent to Left</para>
    /// </summary>
    [Parameter]
    public Alignment LabelAlign { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值</para>
    /// <para lang="en">Gets or sets Label Width. Default is null, use global setting <code>--bb-row-label-width</code> if not set</para>
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮模板</para>
    /// <para lang="en">Gets or sets Buttons Template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? Buttons { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示前置标签 默认为 null 未设置时默认显示标签</para>
    /// <para lang="en">Gets or sets Whether to Show Label. Default is null, show label if not set</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null</para>
    /// <para lang="en">Gets or sets Whether to Show Label Tooltip. Default is null</para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定字段信息集合</para>
    /// <para lang="en">Gets or sets the items collection.</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public IEnumerable<ISearchItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 未设置 GroupName 编辑项是否放置在顶部 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show unset GroupName items on top. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowUnsetGroupItemsOnTop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组类型 默认 <see cref="EditorFormGroupType.GroupBox"/></para>
    /// <para lang="en">Gets or sets group type. Default is <see cref="EditorFormGroupType.GroupBox"/></para>
    /// </summary>
    [Parameter]
    public EditorFormGroupType GroupType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索表单本地化配置项</para>
    /// <para lang="en">Gets or sets Search Form Localization Options</para>
    /// </summary>
    [Parameter]
    public SearchFormLocalizerOptions? SearchFormLocalizerOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SearchFormLocalizerOptions>? SearchFormLocalizer { get; set; }

    private string? ClassString => CssBuilder.Default("bb-editor bb-search-form")
        .AddClass("bb-editor-group-row-header", GroupType == EditorFormGroupType.RowHeader)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? FormClassString => CssBuilder.Default("row g-3")
        .AddClass("form-inline", RowType == RowType.Inline)
        .AddClass("form-inline-end", RowType == RowType.Inline && LabelAlign == Alignment.Right)
        .AddClass("form-inline-center", RowType == RowType.Inline && LabelAlign == Alignment.Center)
        .Build();

    private string? FormStyleString => CssBuilder.Default()
        .AddClass($"--bb-row-label-width: {LabelWidth}px;", LabelWidth.HasValue)
        .Build();

    private IEnumerable<ISearchItem> UnsetGroupItems => Items.Where(i => string.IsNullOrEmpty(i.GroupName)).OrderBy(i => i.Order);

    private IEnumerable<KeyValuePair<string, IOrderedEnumerable<ISearchItem>>> GroupItems => Items
        .Where(i => !string.IsNullOrEmpty(i.GroupName))
        .GroupBy(i => i.GroupName).OrderBy(i => i.Key)
        .Select(i => new KeyValuePair<string, IOrderedEnumerable<ISearchItem>>(i.First().GroupName!, i.OrderBy(x => x.GroupOrder)));

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= [];
    }

    private RenderFragment AutoGenerateTemplate(ISearchItem item)
    {
        item.ShowLabel ??= ShowLabel ?? true;
        item.ShowLabelTooltip ??= ShowLabelTooltip;
        item.Metadata ??= item.BuildSearchMetadata(GetSearchOptions());
        item.Metadata.ValueChanged ??= async () =>
        {
            if (OnChanged != null)
            {
                var filter = Items.ToFilter();
                await OnChanged(filter);
            }
        };

        return item.CreateSearchItemComponentByMetadata();
    }

    private SearchFormLocalizerOptions? _options;

    private SearchFormLocalizerOptions GetSearchOptions()
    {
        _options ??= SearchFormLocalizerOptions ?? SearchFormLocalizer.GetSearchFormLocalizerOptions();

        return _options.Value;
    }

    private string? GetCssString(ISearchItem item)
    {
        int cols = Math.Max(0, Math.Min(12, item.Cols));
        double mdCols = 6;
        if (ItemsPerRow.HasValue)
        {
            mdCols = Math.Max(0, Math.Min(12, Math.Ceiling(12d / ItemsPerRow.Value)));
        }

        return CssBuilder.Default("col-12")
            .AddClass($"col-sm-{cols}", cols > 0)
            .AddClass($"col-sm-6 col-md-{mdCols}", mdCols > 0 && cols == 0 && !Utility.IsCheckboxList(item.PropertyType))
            .Build();
    }
}
