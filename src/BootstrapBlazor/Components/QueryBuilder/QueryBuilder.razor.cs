// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">QueryBuilder 组件</para>
/// <para lang="en">QueryBuilder Component</para>
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TModel))]
#endif
public partial class QueryBuilder<TModel> where TModel : notnull, new()
{
    private string? ClassString => CssBuilder.Default("query-builder")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 过滤模型 <see cref="FilterKeyValueAction"/> 实例值</para>
    /// <para lang="en">Gets or sets the filter model <see cref="FilterKeyValueAction"/> value</para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public FilterKeyValueAction? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Filter 回调方法，支持双向绑定</para>
    /// <para lang="en">Gets or sets the filter callback method. Supports two-way binding</para>
    /// </summary>
    [Parameter]
    public EventCallback<FilterKeyValueAction> ValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 逻辑运算符</para>
    /// <para lang="en">Gets or sets the logic operator</para>
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 模板</para>
    /// <para lang="en">Gets or sets the template</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Header 区域，默认为 true</para>
    /// <para lang="en">Gets or sets whether to show Header area. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 Header 模板，默认为 null</para>
    /// <para lang="en">Gets or sets the header template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<FilterKeyValueAction>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 增加过滤条件图标</para>
    /// <para lang="en">Gets or sets the add filter condition icon</para>
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 移除过滤条件图标</para>
    /// <para lang="en">Gets or sets the remove filter condition icon</para>
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 减少过滤条件图标</para>
    /// <para lang="en">Gets or sets the reduce filter condition icon</para>
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组合过滤条件文本</para>
    /// <para lang="en">Gets or sets the group filter condition text</para>
    /// </summary>
    [Parameter]
    public string? GroupText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件文本</para>
    /// <para lang="en">Gets or sets the filter condition text</para>
    /// </summary>
    [Parameter]
    public string? ItemText { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<QueryBuilder<TModel>>? Localizer { get; set; }

    private bool _inited = false;

    private List<SelectedItem>? Operations { get; set; }

    private readonly List<SelectedItem> _dropdownItems =
    [
        new("Group", "Group"),
        new("Item", "Item")
    ];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _fields.AddRange(typeof(TModel).GetProperties().Select(p => new SelectedItem(p.Name, Utility.GetDisplayName<TModel>(p.Name))));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderMinusIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.QueryBuilderRemoveIcon);
        GroupText ??= Localizer[nameof(GroupText)];
        ItemText ??= Localizer[nameof(ItemText)];

        Value ??= new();
        Value.FilterLogic = Logic;

        Operations ??=
        [
            new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new SelectedItem("GreaterThan", Localizer["GreaterThan"].Value),
            new SelectedItem("LessThan", Localizer["LessThan"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value ),
            new SelectedItem("Contains", Localizer["Contains"].Value ),
            new SelectedItem("NotContains", Localizer["NotContains"].Value )
        ];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _inited = true;
            StateHasChanged();
        }
    }

    private async Task OnClickRemoveFilter(FilterKeyValueAction parent, FilterKeyValueAction filter)
    {
        parent.Filters.Remove(filter);

        await OnFilterChanged();
    }

    private async Task OnClickAddFilter(FilterKeyValueAction filter)
    {
        filter.Filters.Add(new());

        await OnFilterChanged();
    }

    private async Task OnFilterChanged()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private Task SetFilterLogic(FilterKeyValueAction? parent, FilterKeyValueAction filter, FilterLogic logic)
    {
        if (parent == null)
        {
            Logic = logic;
        }
        filter.FilterLogic = logic;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnAddFilterGroup(FilterKeyValueAction filter)
    {
        filter.Filters.Add(new GroupFilterKeyValueAction());

        await OnFilterChanged();
    }

    private async Task OnAddFilterItem(FilterKeyValueAction filter)
    {
        filter.Filters.Add(new FilterKeyValueAction());

        await OnFilterChanged();
    }

    private async Task OnClickRemove(FilterKeyValueAction? parent, FilterKeyValueAction filter)
    {
        filter.Filters.Clear();
        parent?.Filters.Remove(filter);

        await OnFilterChanged();
    }

    private static Color GetColorByFilter(FilterKeyValueAction filter, FilterLogic logic) => filter.FilterLogic == logic
        ? Color.Primary
        : Color.Secondary;

    private readonly List<SelectedItem> _fields = [];

    private bool IsShowHeader(FilterKeyValueAction filter) => ShowHeader && IsGroup(filter);

    private static bool IsGroup(FilterKeyValueAction filter) => filter is GroupFilterKeyValueAction || filter.Filters.Count > 0;
}
