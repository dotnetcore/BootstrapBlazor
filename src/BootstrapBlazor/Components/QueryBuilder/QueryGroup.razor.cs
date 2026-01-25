// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">QueryGroup 组件</para>
/// <para lang="en">QueryGroup Component</para>
/// </summary>
public partial class QueryGroup : IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 逻辑运算符</para>
    /// <para lang="en">Gets or sets the logic operator</para>
    /// </summary>
    [Parameter]
    public FilterLogic Logic { get; set; }

    /// <summary>
    /// <para lang="zh">获得 过滤条件集合</para>
    /// <para lang="en">Gets the filter collection</para>
    /// </summary>
    [CascadingParameter]
    protected List<FilterKeyValueAction>? Filters { get; set; }

    /// <summary>
    /// <para lang="zh">获得 过滤条件集合</para>
    /// <para lang="en">Gets the filter collection</para>
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
    /// <para lang="zh">释放资源</para>
    /// <para lang="en">Dispose</para>
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
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
