// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">过滤器基类</para>
/// <para lang="en">Filter base class</para>
/// </summary>
public abstract class FilterBase : BootstrapModuleComponentBase, IFilterAction
{
    private protected const string NullFilterValue = "__bb_filter_is_null__";
    private protected const string NotNullFilterValue = "__bb_filter_is_not_null__";

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="IStringLocalizer{TableFilter}"/> 实例</para>
    /// <para lang="en">Gets or sets the <see cref="IStringLocalizer{TableFilter}"/> instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IStringLocalizer<TableColumnFilter>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 相关字段名称</para>
    /// <para lang="en">Gets or sets related field name</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FieldKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为表头行模式，默认值为 false</para>
    /// <para lang="en">Gets or sets whether header row mode is enabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 条件数量</para>
    /// <para lang="en">Gets or sets condition count</para>
    /// </summary>
    [Parameter]
    public int Count { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 所属 TableFilter 实例</para>
    /// <para lang="en">Gets or sets the TableFilter instance</para>
    /// </summary>
    [CascadingParameter, NotNull]
    protected TableColumnFilter? TableColumnFilter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 从级联参数获取的 <see cref="FilterContext"/> 实例</para>
    /// <para lang="en">Gets or sets the <see cref="FilterContext"/> instance from cascading parameter</para>
    /// </summary>
    [CascadingParameter]
    protected FilterContext? FilterContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 多个条件逻辑关系符号</para>
    /// <para lang="en">Gets or sets the logical operator for multiple conditions</para>
    /// </summary>
    protected FilterLogic Logic { get; set; }

    private protected bool CanFilterNull
    {
        get
        {
            var type = TableColumnFilter?.Column.PropertyType;
            return type == null || !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }
    }

    private protected IEnumerable<SelectedItem> AddNullFilterActions(IEnumerable<SelectedItem> items)
    {
        var result = items.ToList();
        if (CanFilterNull)
        {
            if (!result.Any(item => item.Value == nameof(FilterAction.IsNull)))
            {
                result.Add(new SelectedItem(nameof(FilterAction.IsNull), Localizer["IsNull"].Value));
            }
            if (!result.Any(item => item.Value == nameof(FilterAction.IsNotNull)))
            {
                result.Add(new SelectedItem(nameof(FilterAction.IsNotNull), Localizer["IsNotNull"].Value));
            }
        }
        return result;
    }

    private protected IEnumerable<SelectedItem> AddNullFilterValues(IEnumerable<SelectedItem> items)
    {
        var result = items.ToList();
        if (CanFilterNull)
        {
            if (!result.Any(item => item.Value == NullFilterValue))
            {
                result.Add(new SelectedItem(NullFilterValue, Localizer["IsNull"].Value));
            }
            if (!result.Any(item => item.Value == NotNullFilterValue))
            {
                result.Add(new SelectedItem(NotNullFilterValue, Localizer["IsNotNull"].Value));
            }
        }
        return result;
    }

    private protected static bool IsNullFilterAction(FilterAction action) => action is FilterAction.IsNull or FilterAction.IsNotNull;

    private protected static FilterAction? GetNullFilterAction(string? value) => value switch
    {
        NullFilterValue => FilterAction.IsNull,
        NotNullFilterValue => FilterAction.IsNotNull,
        _ => null
    };

    private protected FilterKeyValueAction CreateCondition(object? value, FilterAction action) => new()
    {
        FieldKey = FieldKey,
        FieldValue = value,
        FilterAction = action
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (TableColumnFilter != null)
        {
            TableColumnFilter.FilterAction = this;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (FilterContext != null)
        {
            FieldKey = FilterContext.FieldKey;
            IsHeaderRow = FilterContext.IsHeaderRow;
            Count = FilterContext.Count;
        }
    }

    /// <summary>
    /// <para lang="zh">重置按钮回调方法</para>
    /// <para lang="en">Reset Button Callback Method</para>
    /// </summary>
    protected virtual async Task OnClearFilter()
    {
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.Reset();
        }

        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">过滤按钮回调方法</para>
    /// <para lang="en">Filter Button Callback Method</para>
    /// </summary>
    protected virtual async Task OnFilterAsync()
    {
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.OnFilterAsync();
        }

        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public abstract FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// <inheritdoc cref="IFilterAction.SetFilterConditionsAsync(FilterKeyValueAction)"/>
    /// </summary>
    /// <param name="filter"></param>
    public virtual Task SetFilterConditionsAsync(FilterKeyValueAction filter) => OnFilterAsync();
}
