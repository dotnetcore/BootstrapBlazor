// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

class TreeViewToolbar<TItem> : ComponentBase
{
    [Parameter, NotNull]
    public Func<TreeViewItem<TItem>, Task<bool>>? ShowToolbarAsync { get; set; }

    [Parameter, NotNull]
    public TreeViewItem<TItem>? Item { get; set; }

    [Parameter, NotNull]
    public RenderFragment<TreeViewItem<TItem>>? ChildContent { get; set; }

    private bool _showToolbar = false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _showToolbar = await ShowToolbarAsync(Item);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (_showToolbar)
        {
            builder.AddContent(0, ChildContent(Item));
        }
    }
}
