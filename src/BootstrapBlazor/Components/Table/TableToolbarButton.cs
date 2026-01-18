// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表格 Toolbar 按钮组件</para>
/// <para lang="en">Table toolbar button component</para>
/// </summary>
[JSModuleNotInherited]
public class TableToolbarButton<TItem> : ButtonBase, ITableToolbarButton<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 按钮点击后回调委托</para>
    /// <para lang="en">Gets or sets button click callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 Table Toolbar 实例</para>
    /// <para lang="en">Gets or sets Table Toolbar instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [CascadingParameter]
    protected TableToolbar<TItem>? Toolbar { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Toolbar?.AddButton(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        return ValueTask.CompletedTask;
    }
}
