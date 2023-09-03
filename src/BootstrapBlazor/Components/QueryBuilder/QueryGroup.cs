// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryGroup 组件
/// </summary>
public class QueryGroup : BootstrapComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 逻辑运算符
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// 过滤条件集合
    /// </summary>
    [CascadingParameter]
    protected List<FilterKeyValueAction>? Filters { get; set; }

    /// <summary>
    /// 过滤条件集合
    /// </summary>
    protected FilterKeyValueAction _filter = new() { Filters = new() };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Filters?.Add(_filter);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _filter.FilterLogic = Logic;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<List<FilterKeyValueAction>>>(10);
        builder.AddAttribute(20, nameof(CascadingValue<List<FilterKeyValueAction>>.IsFixed), true);
        builder.AddAttribute(30, nameof(CascadingValue<List<FilterKeyValueAction>>.Value), _filter.Filters);
        builder.AddAttribute(40, nameof(CascadingValue<List<FilterKeyValueAction>>.ChildContent), ChildContent);
        builder.CloseComponent();
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Filters?.Remove(_filter);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
