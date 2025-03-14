// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeViewToolbar component
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class TreeViewToolbar<TItem> : ComponentBase
{
    /// <summary>
    /// Gets or sets the tree view item. Default is null.
    /// </summary>
    [Parameter, NotNull]
    public TreeViewItem<TItem>? Item { get; set; }

    /// <summary>
    /// Gets or sets the item changed event callback.
    /// </summary>
    [Parameter]
    public EventCallback<TreeViewItem<TItem>> ItemChanged { get; set; }

    /// <summary>
    /// Gets or sets the update the tree text value callback. Default is null.
    /// <para>If return true will update the tree text value, otherwise will not update.</para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?, Task<bool>>? OnUpdateCallbackAsync { get; set; }

    /// <summary>
    /// Gets or sets the child content of the tree view toolbar. Default is null.
    /// </summary>
    [Parameter, NotNull]
    public RenderFragment<TItem>? BodyTemplate { get; set; }

    /// <summary>
    /// Gets or sets the title of the popup-window. Default is null.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the title of the popup-window. Default is null.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    private string? _text;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _text = Item.Text;
    }

    private async Task OnConfirm()
    {
        var ret = true;
        if (OnUpdateCallbackAsync != null)
        {
            ret = await OnUpdateCallbackAsync(Item.Value, _text);
        }

        if (ret)
        {
            Item.Text = _text;
            if (ItemChanged.HasDelegate)
            {
                await ItemChanged.InvokeAsync(Item);
            }
        }
    }
}
