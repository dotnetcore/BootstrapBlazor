﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 过滤器基类
/// </summary>
public abstract class FilterBase : BootstrapModuleComponentBase, IFilterAction
{
    /// <summary>
    /// 获得/设置 <see cref="IStringLocalizer{TableFilter}"/> 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IStringLocalizer<TableColumnFilter>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FieldKey { get; set; }

    /// <summary>
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    [Parameter]
    public int Count { get; set; }

    /// <summary>
    /// 获得/设置 多个条件逻辑关系符号
    /// </summary>
    protected FilterLogic Logic { get; set; }

    /// <summary>
    /// 获得/设置 所属 TableFilter 实例
    /// </summary>
    [CascadingParameter, NotNull]
    protected TableColumnFilter? TableColumnFilter { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="FilterContext"/> instance from cascading parameter.
    /// </summary>
    [CascadingParameter]
    protected FilterContext? FilterContext { get; set; }

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
    /// 重置按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnClearFilter()
    {
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.Reset();
        }

        StateHasChanged();
    }

    /// <summary>
    /// 过滤按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnFilterAsync()
    {
        if (TableColumnFilter != null)
        {
            await TableColumnFilter.OnFilterAsync();
        }

        StateHasChanged();
    }

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// 获得过滤窗口的所有条件方法
    /// </summary>
    /// <returns></returns>
    public abstract FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// 设置过滤集合方法
    /// </summary>
    /// <param name="filter"></param>
    public virtual Task SetFilterConditionsAsync(FilterKeyValueAction filter) => OnFilterAsync();
}
