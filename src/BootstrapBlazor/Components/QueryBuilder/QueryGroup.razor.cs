// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryGroup 组件
/// </summary>
public partial class QueryGroup : IDisposable
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
    protected FilterKeyValueAction _filter = new();

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
