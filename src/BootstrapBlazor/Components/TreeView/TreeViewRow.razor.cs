// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeViewRow component
/// </summary>
public partial class TreeViewRow<TItem>
{
    /// <summary>
    /// Gets or sets whether the node is active. Default is false.
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the node index. Default is 0.
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the tree node item. Default is null.
    /// </summary>
    [Parameter, NotNull]
    public TreeViewItem<TItem>? Item { get; set; }

    /// <summary>
    /// Gets or sets the loading icon for tree nodes.
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon for tree nodes.
    /// </summary>
    [Parameter]
    public string? NodeIcon { get; set; }

    /// <summary>
    /// Gets or sets the icon for expanded tree nodes.
    /// </summary>
    [Parameter]
    public string? ExpandNodeIcon { get; set; }

    /// <summary>
    /// Gets or sets whether the entire component is disabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether to show checkboxes. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false.
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; }

    /// <summary>
    /// Get or sets the node click event callback.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnToggleNodeAsync { get; set; }

    /// <summary>
    /// Get or sets the node checkbox state change event callback.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, CheckboxState, Task>? OnCheckStateChanged { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of selected items.
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked before the node state changes.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, CheckboxState, Task<bool>>? OnBeforeStateChangedCallback { get; set; }

    /// <summary>
    /// Gets or sets whether to show icons. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// Gets or sets the click event callback. Default is null.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnClick { get; set; }

    /// <summary>
    /// Gets or sets whether show the toolbar of tree view item. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// A callback method that determines whether to show the toolbar of the tree view item.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? ShowToolbarCallback { get; set; }

    /// <summary>
    /// Gets or sets the title of the popup-window. Default is null.
    /// </summary>
    [Parameter]
    public string? ToolbarEditTitle { get; set; }

    /// <summary>
    /// Gets or sets the title of the popup-window. Default is null.
    /// </summary>
    [Parameter]
    public string? ToolbarEditLabelText { get; set; }

    /// <summary>
    /// Gets or sets the toolbar content template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ToolbarTemplate { get; set; }

    /// <summary>
    /// Gets or sets the update the tree text value callback. Default is null.
    /// <para>If return true will update the tree text value, otherwise will not update.</para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?, Task<bool>>? OnUpdateCallbackAsync { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    [CascadingParameter]
    private ContextMenuZone? ContextMenuZone { get; set; }

    private string? ContentClassString => CssBuilder.Default("tree-content")
        .AddClass("active", IsActive)
        .AddClass("tree-drop-pass", PreviewDrop)
        .Build();

    private string? CaretClassString => CssBuilder.Default("node-icon")
        .AddClass("visible", Item.HasChildren || Item.Items.Count > 0)
        .AddClass(NodeIcon, !Item.IsExpand)
        .AddClass(ExpandNodeIcon, Item.IsExpand)
        .AddClass("disabled", !CanTriggerClickNode)
        .Build();

    private string? NodeLoadingClassString => CssBuilder.Default("node-icon node-loading")
        .AddClass(LoadingIcon)
        .Build();

    private string? NodeClassString => CssBuilder.Default("tree-node")
        .AddClass("disabled", ItemDisabledState)
        .Build();

    private string? ItemTextClassString => CssBuilder.Default("tree-node-text")
        .AddClass(Item.CssClass)
        .Build();

    private string? IconClassString => CssBuilder.Default("tree-icon")
        .AddClass(Item.Icon)
        .AddClass(Item.ExpandIcon, Item.IsExpand && !string.IsNullOrEmpty(Item.ExpandIcon))
        .Build();

    private bool IsPreventDefault => ContextMenuZone != null;

    private bool _touchStart = false;

    private bool _isBusy = false;

    private bool _showToolbar = false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (ShowToolbarCallback != null)
        {
            _showToolbar = await ShowToolbarCallback(Item);
        }
        else
        {
            _showToolbar = ShowToolbar;
        }
    }

    private async Task OnTouchStart(TouchEventArgs e)
    {
        if (!_isBusy && ContextMenuZone != null)
        {
            _isBusy = true;
            _touchStart = true;

            // 延时保持 TouchStart 状态
            // keep the TouchStart state for a while
            var delay = Options.CurrentValue.ContextMenuOptions.OnTouchDelay;
            await Task.Delay(delay);
            if (_touchStart)
            {
                var args = new MouseEventArgs()
                {
                    ClientX = e.Touches[0].ClientX,
                    ClientY = e.Touches[0].ClientY,
                    ScreenX = e.Touches[0].ScreenX,
                    ScreenY = e.Touches[0].ScreenY,
                };
                await OnContextMenu(args);

                // prevents the menu from being activated repeatedly
                await Task.Delay(delay);
            }
            _isBusy = false;
        }
    }

    private void OnTouchEnd()
    {
        _touchStart = false;
    }

    private async Task OnContextMenu(MouseEventArgs e)
    {
        if (ContextMenuZone != null)
        {
            await ContextMenuZone.OnContextMenu(e, Item.Value);
        }
    }

    private string? GetTreeRowStyle()
    {
        var level = 0;
        var parent = Item.Parent;
        while (parent != null)
        {
            level++;
            parent = parent.Parent;
        }
        return $"--bb-tree-view-level: {level};";
    }

    private bool CanTriggerClickNode => Item.CanTriggerClickNode(IsDisabled, CanExpandWhenDisabled);

    private bool ItemDisabledState => Item.IsDisabled || IsDisabled;

    private async Task ToggleNodeAsync()
    {
        if (OnToggleNodeAsync != null)
        {
            await OnToggleNodeAsync(Item);
        }
    }

    private async Task CheckStateChanged(CheckboxState state)
    {
        if (OnCheckStateChanged != null)
        {
            await OnCheckStateChanged(Item, state);
        }
    }

    private async Task<bool> TriggerBeforeStateChangedCallback(CheckboxState state)
    {
        var ret = true;
        if (OnBeforeStateChangedCallback != null)
        {
            ret = await OnBeforeStateChangedCallback(Item, state);
        }
        return ret;
    }

    private async Task ClickRow()
    {
        if (OnClick != null)
        {
            await OnClick(Item);
        }
    }

    #region Draggable


    /// <summary>
    /// Gets or sets whether the node can be dragged. Default is false.
    /// </summary>
    [Parameter]
    public bool Draggable { get; set; }

    /// <summary>
    /// Gets or sets whether to preview the drop target when dragging. Default is false.
    /// </summary>
    [Parameter]
    public bool PreviewDrop { get; set; }

    private bool _draggingItem;
    private bool _expandAfterDrop;

    /// <summary>
    /// Triggered when the item is dragged
    /// </summary>
    [Parameter]
    public Action<TreeViewItem<TItem>>? OnItemDragStart { get; set; }

    /// <summary>
    /// Triggered when the item drag ends
    /// </summary>
    [Parameter]
    public Action? OnItemDragEnd { get; set; }

    /// <summary>
    /// Triggered when an item is dropped
    /// </summary>
    [Parameter][Required]
    public Func<TreeDropEventArgs<TItem>, Task> OnItemDrop { get; set; } = null!;

    private async Task DragStart(DragEventArgs e)
    {
        _draggingItem = true;
        if (Item.IsExpand)
        {
            _expandAfterDrop = true;
            await ToggleNodeAsync();
        }else
        {
            _expandAfterDrop = false;
        }
        OnItemDragStart?.Invoke(Item);
    }

    private void DragEnd(DragEventArgs e)
    {
        _draggingItem = false;
        _previewChildLast = false;
        _previewChildFirst = false;
        _previewBelow = false;
        OnItemDragEnd?.Invoke();
    }

    private bool _previewChildLast;
    private bool _previewChildFirst;
    private bool _previewBelow;

    private void DragEnterChildInside(DragEventArgs e)
    {
        _previewChildLast = true;
    }

    private void DragLeaveChildInside(DragEventArgs e)
    {
        _previewChildLast = false;
    }

    private void DragEnterChildBelow(DragEventArgs e)
    {
        if ((Item.HasChildren || Item.Items.Any()) && Item.IsExpand)
        {
            _previewChildFirst = true;
            _previewBelow = false;
        }
        else
        {
            _previewChildFirst = false;
            _previewBelow = true;
        }
    }

    private void DragLeaveChildBelow(DragEventArgs e)
    {
        _previewChildFirst = false;
        _previewBelow = false;
    }

    private async Task DropChildInside(DragEventArgs e)
    {
        _previewChildFirst = false;
        _previewChildLast = false;
        _previewBelow = false;
        // 拖放到内部，作为最后一个子节点
        var args = new TreeDropEventArgs<TItem>
        {
            Target = Item,
            DropType = TreeDropType.AsLastChild,
            ExpandAfterDrop = _expandAfterDrop
        };
        var dropTask = OnItemDrop.Invoke(args);
        await dropTask;
    }

    private async Task DropChildBelow(DragEventArgs e)
    {
        _previewChildFirst = false;
        _previewChildLast = false;
        _previewBelow = false;
        var args = new TreeDropEventArgs<TItem>
        {
            Target = Item,
            ExpandAfterDrop = _expandAfterDrop,
        };
        // 对象展开状态，则作为第一个子节点
        if ((Item.HasChildren || Item.Items.Any()) && Item.IsExpand)
        {
            args.DropType = TreeDropType.AsFirstChild;
        }
        else
        {
            args.DropType = TreeDropType.AsSiblingBelow;
        }
        var dropTask = OnItemDrop.Invoke(args);
        await dropTask;
    }


    #endregion
}
