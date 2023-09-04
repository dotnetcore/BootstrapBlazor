// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryBuilder 组件
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
    /// 获得/设置 过滤模型 <see cref="FilterKeyValueAction"/> 实例值
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public FilterKeyValueAction? Value { get; set; }

    /// <summary>
    /// 获得/设置 Filter 回调方法 支持双向绑定
    /// </summary>
    [Parameter]
    public EventCallback<FilterKeyValueAction> ValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 逻辑运算符
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// 获得/设置 模板
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Header 区域 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 Header 模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment<FilterKeyValueAction>? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载骨架屏 默认 false 不显示 使用 <see cref="ShowLoadingInFirstRender" /> 参数值
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// 获得/设置 首次加载是否显示加载动画 默认 true 显示 设置 <see cref="ShowSkeleton"/> 值覆盖此参数
    /// </summary>
    [Parameter]
    public bool ShowLoadingInFirstRender { get; set; } = true;

    /// <summary>
    /// 获得/设置 增加过滤条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 移除过滤条件图标
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少过滤条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 获得/设置 组合过滤条件文本
    /// </summary>
    [Parameter]
    public string? GroupText { get; set; }

    /// <summary>
    /// 获得/设置 过滤条件文本
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

    private readonly List<SelectedItem> _dropdownItems = new()
    {
        new("Group", "Group"),
        new("Item", "Item")
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Value ??= new FilterKeyValueAction();
        Value.Filters ??= new();

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

        Operations ??= new()
        {
            new SelectedItem("GreaterThanOrEqual", Localizer["GreaterThanOrEqual"].Value),
            new SelectedItem("LessThanOrEqual", Localizer["LessThanOrEqual"].Value),
            new SelectedItem("GreaterThan", Localizer["GreaterThan"].Value),
            new SelectedItem("LessThan", Localizer["LessThan"].Value),
            new SelectedItem("Equal", Localizer["Equal"].Value),
            new SelectedItem("NotEqual", Localizer["NotEqual"].Value ),
            new SelectedItem("Contains", Localizer["Contains"].Value ),
            new SelectedItem("NotContains", Localizer["NotContains"].Value )
        };
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
        parent.Filters?.Remove(filter);

        await OnFilterChanged();
    }

    private async Task OnClickAddFilter(FilterKeyValueAction filter)
    {
        filter.Filters ??= new();
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
        filter.Filters ??= new();
        filter.Filters.Add(new FilterKeyValueAction() { Filters = new() { new() } });

        await OnFilterChanged();
    }

    private async Task OnAddFilterItem(FilterKeyValueAction filter)
    {
        filter.Filters ??= new();
        filter.Filters.Add(new FilterKeyValueAction() { });

        await OnFilterChanged();
    }

    private async Task OnClickRemove(FilterKeyValueAction? parent, FilterKeyValueAction filter)
    {
        filter.Filters?.Clear();
        parent?.Filters?.Remove(filter);

        await OnFilterChanged();
    }

    private static Color GetColorByFilter(FilterKeyValueAction filter, FilterLogic logic) => filter.FilterLogic == logic ? Color.Primary : Color.Secondary;

    private readonly List<SelectedItem> _fields = new();

    RenderFragment RenderFilters(FilterKeyValueAction? parent, FilterKeyValueAction filter) => builder =>
    {
        if (filter.Filters != null)
        {
            var index = 0;
            builder.OpenElement(index++, "ul");
            builder.AddAttribute(index++, "class", "qb-group");
            if (filter.HasFilters() && ShowHeader)
            {
                builder.OpenElement(index++, "li");
                builder.AddAttribute(index++, "class", "qb-item");
                builder.AddContent(index++, RenderHeader(parent, filter));
                builder.CloseElement();
            }
            foreach (var f in filter.Filters)
            {
                if (f.HasFilters())
                {
                    RenderFilterItem(ref index, RenderFilters(filter, f));
                }
                else
                {
                    RenderFilterItem(ref index, RenderFilter(filter, f));
                }
            }
            builder.CloseElement();
        }

        void RenderFilterItem(ref int sequence, RenderFragment fragment)
        {
            builder.OpenElement(sequence++, "li");
            builder.AddAttribute(sequence++, "class", "qb-item");
            builder.AddAttribute(sequence++, "data-bb-logic", Localizer[filter.FilterLogic.ToString()]);
            builder.AddContent(sequence++, fragment);
            builder.CloseElement();
        }
    };
}
