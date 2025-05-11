// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 类型过滤器基类
/// /// </summary>
public abstract class FilterBase : BootstrapModuleComponentBase, IFilterAction
{
    /// <summary>
    /// 
    /// </summary>
    protected string? FilterRowClassString => CssBuilder.Default("filter-row")
        .AddClass("active", HasFilter)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected virtual FilterLogic Logic { get; set; }

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    protected string? FieldKey { get; set; }

    /// <summary>
    /// 获得 是否为 HeaderRow 呈现模式 默认为 false
    /// </summary>
    protected bool IsHeaderRow => TableFilter?.IsHeaderRow ?? false;

    /// <summary>
    /// 获得 当前过滤条件是否激活
    /// </summary>
    protected bool HasFilter => TableFilter?.HasFilter ?? false;

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    [Parameter]
    public int Count { get; set; }

    /// <summary>
    /// 获得/设置 条件候选项 请尽量使用静态数据 避免组件性能损失
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 所属 TableFilter 实例
    /// </summary>
    [CascadingParameter]
    protected TableFilter? TableFilter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (TableFilter != null)
        {
            TableFilter.FilterAction = this;
            FieldKey = TableFilter.FieldKey;
        }
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
    public virtual Task SetFilterConditionsAsync(FilterKeyValueAction filter) => OnFilterValueChanged();

    /// <summary>
    /// 过滤按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnFilterValueChanged()
    {
        if (TableFilter != null)
        {
            await TableFilter.OnFilterAsync();
            StateHasChanged();
        }
    }

    /// <summary>
    /// 重置按钮回调方法
    /// </summary>
    /// <returns></returns>
    protected async Task OnClearFilter()
    {
        if (TableFilter != null)
        {
            Reset();
            await TableFilter.OnFilterAsync();
        }
    }
}
