// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 类型过滤器基类
/// /// </summary>
public abstract class FilterBase : ComponentBase, IFilterAction
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
    protected bool HasFilter => TableFilter?.HasFilter ?? false; // IsHeaderRow 为真时使用 TableFilter 不为空

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    [Parameter]
    public int Count { get; set; }

    /// <summary>
    /// 获得/设置 条件候选项
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 所属 TableFilter 实例
    /// </summary>
    [CascadingParameter]
    protected TableFilter? TableFilter { get; set; }

    /// <summary>
    /// OnInitialized 方法
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
    public abstract IEnumerable<FilterKeyValueAction> GetFilterConditions();

    /// <summary>
    /// 设置过滤集合方法
    /// </summary>
    /// <param name="conditions"></param>
    public virtual Task SetFilterConditionsAsync(IEnumerable<FilterKeyValueAction> conditions) => OnFilterValueChanged();

    /// <summary>
    /// 
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
    /// 
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
