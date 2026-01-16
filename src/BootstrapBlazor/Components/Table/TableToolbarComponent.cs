// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 工具栏自定义组件</para>
/// <para lang="en">Table 工具栏自定义component</para>
/// </summary>
public class TableToolbarComponent<TItem> : ComponentBase, IToolbarComponent, IDisposable
{
    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets 子component</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Table Toolbar 实例</para>
    /// <para lang="en">Gets or sets Table Toolbar instance</para>
    /// </summary>
    [CascadingParameter]
    protected TableToolbar<TItem>? Toolbar { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Toolbar?.AddButton(this);
    }

    /// <summary>
    /// <para lang="zh">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</para>
    /// <para lang="en">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Toolbar?.RemoveButton(this);
        }
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
