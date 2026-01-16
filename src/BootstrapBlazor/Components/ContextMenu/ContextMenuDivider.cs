// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenuDivider 组件</para>
/// <para lang="en">A component that defines a menu item as a divider in a context menu.</para>
/// </summary>
public class ContextMenuDivider : Divider, IContextMenuItem, IDisposable
{
    [CascadingParameter]
    [NotNull]
    private ContextMenu? ContextMenu { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ContextMenu.AddItem(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void BuildRenderTree(RenderTreeBuilder builder) { }

    private bool disposedValue;

    /// <summary>
    /// <para lang="zh">释放资源方法</para>
    /// <para lang="en">Method to release resources.</para>
    /// </summary>
    /// <param name="disposing">
    ///     <para lang="zh">是否释放托管资源</para>
    ///     <para lang="en">Flags whether to release managed resources</para>
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                ContextMenu.RemoveItem(this);
            }
            disposedValue = true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
