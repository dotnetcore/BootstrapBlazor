// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeViewRow component</para>
/// <para lang="en">TreeViewRow component</para>
/// </summary>
public partial class TreeViewRow<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否 the node is active. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether the node is active. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the node 索引. 默认为 0.</para>
    /// <para lang="en">Gets or sets the node index. Default is 0.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the tree node item. 默认为 null.</para>
    /// <para lang="en">Gets or sets the tree node item. Default is null.</para>
    /// </summary>
    [Parameter, NotNull]
    public TreeViewItem<TItem>? Item { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the loading 图标 for tree nodes.</para>
    /// <para lang="en">Gets or sets the loading icon for tree nodes.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 for tree nodes.</para>
    /// <para lang="en">Gets or sets the icon for tree nodes.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? NodeIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 图标 for expanded tree nodes.</para>
    /// <para lang="en">Gets or sets the icon for expanded tree nodes.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ExpandNodeIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the entire component is disabled. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether the entire component is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show checkboxes. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show checkboxes. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 nodes can be expanded or collapsed when the component is disabled. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">Get or sets the node click event 回调.</para>
    /// <para lang="en">Get or sets the node click event callback.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnToggleNodeAsync { get; set; }

    /// <summary>
    /// <para lang="zh">Get or sets the node checkbox state change event 回调.</para>
    /// <para lang="en">Get or sets the node checkbox state change event callback.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, CheckboxState, Task>? OnCheckStateChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the maximum number of selected items.</para>
    /// <para lang="en">Gets or sets the maximum number of selected items.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调 that is invoked before the node state changes.</para>
    /// <para lang="en">Gets or sets the callback that is invoked before the node state changes.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, CheckboxState, Task<bool>>? OnBeforeStateChangedCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show 图标s. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show icons. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the click event 回调. 默认为 null.</para>
    /// <para lang="en">Gets or sets the click event callback. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the toolbar of tree view item. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether show the toolbar of tree view item. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">A 回调方法 that determines 是否 to show the toolbar of the tree view item.</para>
    /// <para lang="en">A callback method that determines whether to show the toolbar of the tree view item.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? ShowToolbarCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the title of the popup-window. 默认为 null.</para>
    /// <para lang="en">Gets or sets the title of the popup-window. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ToolbarEditTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the title of the popup-window. 默认为 null.</para>
    /// <para lang="en">Gets or sets the title of the popup-window. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ToolbarEditLabelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the toolbar 内容 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the toolbar content template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the update the tree text value 回调. 默认为 null. <para>If return true will update the tree text value, otherwise will not update.</para>
    ///</para>
    /// <para lang="en">Gets or sets the update the tree text value callback. Default is null. <para>If return true will update the tree text value, otherwise will not update.</para>
    ///</para>
    /// <para><version>10.2.2</version></para>
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
        .Build();

    private string? CaretClassString => CssBuilder.Default("node-icon")
        .AddClass("visible", Item.HasChildren || Item.Items.Count > 0)
        .AddClass(NodeIcon, !Item.IsExpand)
        .AddClass(ExpandNodeIcon, Item.IsExpand)
        .AddClass("disabled", GetDisabledStatus())
        .Build();

    private bool GetDisabledStatus()
    {
        if (IsDisabled || Item.IsDisabled)
        {
            return !CanExpandWhenDisabled;
        }

        return false;
    }

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

    private bool CanTriggerClickNode => !GetDisabledStatus();

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
}
