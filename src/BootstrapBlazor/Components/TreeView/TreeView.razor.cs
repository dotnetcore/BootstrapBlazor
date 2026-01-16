// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Tree component</para>
/// <para lang="en">Tree component</para>
/// </summary>
[CascadingTypeParameter(nameof(TItem))]
public partial class TreeView<TItem> : IModelEqualityComparer<TItem>
{
    private string? ClassString => CssBuilder.Default("tree-view")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? LoadingClassString => CssBuilder.Default("table-loading")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private TreeViewItem<TItem>? _activeItem;

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the loading animation. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show the loading animation. Default is false.</para>
    /// </summary>
    [Obsolete("Deprecated. Please remove it.")]
    [ExcludeFromCodeCoverage]
    public bool IsReset { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 show the toolbar of tree view item. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether show the toolbar of tree view item. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">获得 or sts A 回调方法 that determines 是否 to show the toolbar of the tree view item.</para>
    /// <para lang="en">Gets or sts A callback method that determines whether to show the toolbar of the tree view item.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? ShowToolbarCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the entire component is disabled. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether the entire component is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 nodes can be expanded or collapsed when the component is disabled. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 the tree view has accordion behavior. 默认为 false. Accordion behavior is not supported in virtual scrolling mode.</para>
    /// <para lang="en">Gets or sets whether the tree view has accordion behavior. Default is false. Accordion behavior is not supported in virtual scrolling mode.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 clicking a node expands or collapses its children. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether clicking a node expands or collapses its children. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ClickToggleNode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 clicking a node toggles its checkbox state. 默认为 false. Effective when <see cref="ShowCheckbox"/> is true.</para>
    /// <para lang="en">Gets or sets whether clicking a node toggles its checkbox state. Default is false. Effective when <see cref="ShowCheckbox"/> is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ClickToggleCheck { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the loading skeleton. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show the loading skeleton. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the search bar. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show the search bar. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show the reset search 按钮. 默认为 true.</para>
    /// <para lang="en">Gets or sets whether to show the reset search button. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowResetSearchButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 the search bar 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the search bar template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search 图标. 默认为 not set, using the built-in theme 图标.</para>
    /// <para lang="en">Gets or sets the search icon. Default is not set, using the built-in theme icon.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the clear search 图标. 默认为 not set, using the built-in theme 图标.</para>
    /// <para lang="en">Gets or sets the clear search icon. Default is not set, using the built-in theme icon.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ClearSearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the search 回调方法. 默认为 null.</para>
    /// <para lang="en">Gets or sets the search callback method. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Enabled by setting <see cref="ShowSearch"/> to true.</remarks>
    [Parameter]
    public Func<string?, Task<List<TreeViewItem<TItem>>?>>? OnSearchAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the hierarchical 数据 集合.</para>
    /// <para lang="en">Gets or sets the hierarchical data collection.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeViewItem<TItem>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show checkboxes. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show checkboxes. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the maximum number of selected items.</para>
    /// <para lang="en">Gets or sets the maximum number of selected items.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 when the maximum number of selected items is exceeded.</para>
    /// <para lang="en">Gets or sets the callback method when the maximum number of selected items is exceeded.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnMaxSelectedCountExceed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to show 图标s. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to show icons. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 when a tree item is clicked.</para>
    /// <para lang="en">Gets or sets the callback method when a tree item is clicked.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击节点前回调方法</para>
    /// <para lang="en">Gets or sets 点击节点前callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? OnBeforeTreeItemClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 when a tree item is checked.</para>
    /// <para lang="en">Gets or sets the callback method when a tree item is checked.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<List<TreeViewItem<TItem>>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调方法 to get child 数据 when a node is expanded.</para>
    /// <para lang="en">Gets or sets the callback method to get child data when a node is expanded.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<IEnumerable<TreeViewItem<TItem>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// <inheritdoc/>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

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
    /// <para lang="zh">获得/设置 是否 to enable keyboard navigation. 默认为 false. <para>ArrowLeft collapses the node.</para>
    /// <para>ArrowRight expands the node.</para>
    /// <para>ArrowUp moves to the previous node.</para>
    /// <para>ArrowDown moves to the next node.</para>
    /// <para>Space selects the current node.</para>
    ///</para>
    /// <para lang="en">Gets or sets whether to enable keyboard navigation. Default is false. <para>ArrowLeft collapses the node.</para>
    /// <para>ArrowRight expands the node.</para>
    /// <para>ArrowUp moves to the previous node.</para>
    /// <para>ArrowDown moves to the next node.</para>
    /// <para>Space selects the current node.</para>
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool EnableKeyboard { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the scroll into view options for keyboard navigation. 默认为 null, using { behavior: "smooth", block: "nearest", inline: "start" }.</para>
    /// <para lang="en">Gets or sets the scroll into view options for keyboard navigation. Default is null, using { behavior: "smooth", block: "nearest", inline: "start" }.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewOptions? ScrollIntoViewOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to enable virtual scrolling. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to enable virtual scrolling. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the row 高度 for virtual scrolling. 默认为 29f.</para>
    /// <para lang="en">Gets or sets the row height for virtual scrolling. Default is 29f.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 29f;

    /// <summary>
    /// <para lang="zh">获得/设置 the overscan count for virtual scrolling. 默认为 10.</para>
    /// <para lang="en">Gets or sets the overscan count for virtual scrolling. Default is 10.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 10;

    /// <summary>
    /// <para lang="zh">获得/设置 the toolbar 内容 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the toolbar content template. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ToolbarTemplate { get; set; }

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
    /// <para lang="zh">获得/设置 the update the tree text value 回调. 默认为 null. <para>If return true will update the tree text value, otherwise will not update.</para>
    ///</para>
    /// <para lang="en">Gets or sets the update the tree text value callback. Default is null. <para>If return true will update the tree text value, otherwise will not update.</para>
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?, Task<bool>>? OnUpdateCallbackAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to automatically update child nodes when the node state changes. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to automatically update child nodes when the node state changes. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoCheckChildren { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to automatically update parent nodes when the node state changes. 默认为 false.</para>
    /// <para lang="en">Gets or sets whether to automatically update parent nodes when the node state changes. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoCheckParent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 a value indicating 是否 drag-and-drop operations are allowed. 默认为 false</para>
    /// <para lang="en">Gets or sets a value indicating whether drag-and-drop operations are allowed. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AllowDrag { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拖动标签页结束回调方法</para>
    /// <para lang="en">Gets or sets 拖动标签页结束callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TreeViewDragContext<TItem>, Task>? OnDragItemEndAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TreeView<TItem>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? EnableKeyboardString => EnableKeyboard ? "true" : null;

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    [NotNull]
    private TreeNodeCache<TreeViewItem<TItem>, TItem>? _treeNodeStateCache = null;

    private string? _searchText;
    private bool _shouldRender = true;
    private bool _init;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _treeNodeStateCache = new(this);
        NotSetOnTreeExpandErrorMessage = Localizer[nameof(NotSetOnTreeExpandErrorMessage)];
        ToolbarEditTitle ??= Localizer[nameof(ToolbarEditTitle)];
        ToolbarEditLabelText ??= Localizer[nameof(ToolbarEditLabelText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NodeIcon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewNodeIcon);
        ExpandNodeIcon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewExpandNodeIcon);
        SearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewSearchIcon);
        ClearSearchIcon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewResetSearchIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.TreeViewLoadingIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        _rows = null;
        if (Items != null)
        {
            if (Items.Count > 0)
            {
                _shouldRender = false;
                await CheckExpand(Items);
                _shouldRender = true;

                _rows = null;
            }

            if (ShowCheckbox && (AutoCheckParent || AutoCheckChildren))
            {
                _treeNodeStateCache.IsChecked(Items);
            }

            if (_activeItem != null)
            {
                _activeItem = _treeNodeStateCache.Find(Items, _activeItem.Value, out _);
            }

            if (_init == false)
            {
                _activeItem ??= Items.FirstOrDefaultActiveItem();
                _activeItem?.SetParentExpand<TreeViewItem<TItem>, TItem>(true);
                _init = true;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_keyboardArrowUpDownTrigger)
        {
            _keyboardArrowUpDownTrigger = false;
            await InvokeVoidAsync("scroll", Id, ScrollIntoViewOptions);
        }

        if (!firstRender && AllowDrag)
        {
            await InvokeVoidAsync("resetTreeViewRow", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override bool ShouldRender() => _shouldRender;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new
    {
        Invoke = Interop,
        Method = nameof(TriggerKeyDown),
        AllowDrag,
        TriggerDragEnd = nameof(TriggerDragEnd)
    });

    private bool _keyboardArrowUpDownTrigger;

    /// <summary>
    /// <para lang="zh">Client-side user keyboard operation handler method called by JavaScript</para>
    /// <para lang="en">Client-side user keyboard operation handler method called by JavaScript</para>
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [JSInvokable]
    public async ValueTask TriggerKeyDown(string key)
    {
        // Find sibling nodes through ActiveItem
        // If there are no sibling nodes, find the parent node
        if (_activeItem != null)
        {
            if (key == "ArrowUp" || key == "ArrowDown")
            {
                _keyboardArrowUpDownTrigger = true;
                await ActiveTreeViewItem(key, _activeItem);
            }
            else if (key == "ArrowLeft" || key == "ArrowRight")
            {
                await OnToggleNodeAsync(_activeItem);
            }
        }
    }

    /// <summary>
    /// <para lang="zh">Triggers the end of a drag-and-drop operation within the tree view.</para>
    /// <para lang="en">Triggers the end of a drag-and-drop operation within the tree view.</para>
    /// </summary>
    /// <remarks>This method is invoked via JavaScript interop to signal the completion of a drag-and-drop
    /// action. If a handler is assigned to <see cref="OnDragItemEndAsync"/>, it will be invoked with the drag
    /// context.</remarks>
    /// <param name="originIndex">The zero-based index of the item being dragged from its original position.</param>
    /// <param name="currentIndex">The zero-based index of the item's current position after the drag operation.</param>
    /// <param name="isChildren">A value indicating whether the drag operation involves child items.</param>
    /// <returns></returns>
    [JSInvokable]
    public async ValueTask TriggerDragEnd(int originIndex, int currentIndex, bool isChildren)
    {
        if (OnDragItemEndAsync != null)
        {
            var context = new TreeViewDragContext<TItem>(
                source: Rows[originIndex],
                target: Rows[currentIndex],
                children: isChildren
            );
            await OnDragItemEndAsync(context);
        }
    }

    /// <summary>
    /// <para lang="zh">Client-side method to query the state of the specified row checkbox, called by JavaScript</para>
    /// <para lang="en">Client-side method to query the state of the specified row checkbox, called by JavaScript</para>
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task<List<CheckboxState>> GetParentsState(List<int> items)
    {
        var rows = Rows;
        var result = items.Select(i =>
        {
            var item = rows[i];
            var checkedState = item.Items[0].CheckedState;
            if (item.Items.Any(s => s.CheckedState != checkedState))
            {
                checkedState = CheckboxState.Indeterminate;
            }
            item.CheckedState = checkedState;
            return checkedState;
        }).ToList();
        return Task.FromResult(result);
    }

    private static bool IsExpand(TreeViewItem<TItem> item) => item is { IsExpand: true, Items.Count: > 0 };

    private List<TreeViewItem<TItem>> GetItems(TreeViewItem<TItem> item) => item.Parent?.Items ?? Items;

    private async Task ActiveTreeViewItem(string key, TreeViewItem<TItem> item)
    {
        var items = GetItems(item);
        var index = items.IndexOf(item);

        if (key == "ArrowUp")
        {
            index--;
            if (index >= 0)
            {
                var currentItem = items[index];
                if (IsExpand(currentItem))
                {
                    await OnClick(currentItem.Items[^1]);
                }
                else
                {
                    await OnClick(currentItem);
                }
            }
            else if (item.Parent != null)
            {
                await OnClick(item.Parent);
            }
        }
        else if (key == "ArrowDown")
        {
            if (IsExpand(item))
            {
                await OnClick(item.Items[0]);
            }
            else
            {
                index++;
                if (index < items.Count)
                {
                    await OnClick(items[index]);
                }
                else if (item.Parent != null)
                {
                    await ActiveParentTreeViewItem(item.Parent);
                }
            }
        }
    }

    private async Task ActiveParentTreeViewItem(TreeViewItem<TItem> item)
    {
        var items = GetItems(item);
        var index = items.IndexOf(item);

        index++;
        if (index < items.Count)
        {
            await OnClick(items[index]);
        }
        else if (item.Parent != null)
        {
            await ActiveParentTreeViewItem(item.Parent);
        }
    }

    private async Task<bool> OnBeforeStateChangedCallback(TreeViewItem<TItem> item, CheckboxState state)
    {
        var ret = true;
        if (MaxSelectedCount > 0)
        {
            if (state == CheckboxState.Checked)
            {
                var items = GetCheckedItems().Where(i => i.HasChildren == false).ToList();
                var count = items.Count + item.GetAllTreeSubItems().Count();
                ret = count < MaxSelectedCount;
            }

            if (!ret && OnMaxSelectedCountExceed != null)
            {
                await OnMaxSelectedCountExceed();
            }
        }
        return ret;
    }

    async Task CheckExpand(IEnumerable<TreeViewItem<TItem>> nodes)
    {
        foreach (var node in nodes)
        {
            await _treeNodeStateCache.CheckExpandAsync(node, GetChildrenRowAsync);

            if (node.Items.Count > 0)
            {
                await CheckExpand(node.Items);
            }
        }
    }

    private async Task<IEnumerable<IExpandableNode<TItem>>> GetChildrenRowAsync(TreeViewItem<TItem> node)
    {
        if (OnExpandNodeAsync == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }

        await InvokeVoidAsync("toggleLoading", Id, Rows.IndexOf(node), true);
        var ret = await OnExpandNodeAsync(node);
        await InvokeVoidAsync("toggleLoading", Id, Rows.IndexOf(node), false);
        return ret;
    }

    private async Task OnClick(TreeViewItem<TItem> item)
    {
        var confirm = true;
        if (OnBeforeTreeItemClick != null)
        {
            confirm = await OnBeforeTreeItemClick(item);
        }

        if (confirm)
        {
            _activeItem = item;
            if (ClickToggleNode)
            {
                await OnToggleNodeAsync(item);
            }

            if (OnTreeItemClick != null)
            {
                await OnTreeItemClick(item);
            }

            if (ShowCheckbox && ClickToggleCheck)
            {
                var state = ToggleCheckState(item.CheckedState);
                await OnCheckStateChanged(item, state);
            }

            StateHasChanged();
        }
    }

    private async Task OnEnterAsync(string? searchText)
    {
        _searchText = searchText;
        await OnClickSearch();
    }

    private Task OnEscAsync(string? searchText) => OnClickResetSearch();

    private List<TreeViewItem<TItem>>? _searchItems;

    private async Task OnClickSearch()
    {
        if (OnSearchAsync != null)
        {
            _searchItems = await OnSearchAsync(_searchText);
            _rows = null;
            StateHasChanged();
        }
    }

    private Task OnClickResetSearch()
    {
        _searchText = null;
        _searchItems = null;
        _rows = null;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">Set the active node</para>
    /// <para lang="en">Set the active node</para>
    /// </summary>
    public void SetActiveItem(TreeViewItem<TItem>? item)
    {
        _activeItem = item;
        _activeItem?.SetParentExpand<TreeViewItem<TItem>, TItem>(true);
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">Set the 数据 source method for <see cref="Items"/></para>
    /// <para lang="en">Set the data source method for <see cref="Items"/></para>
    /// </summary>
    public void SetItems(List<TreeViewItem<TItem>> items)
    {
        Items = items;
        _rows = null;
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">Set the active node</para>
    /// <para lang="en">Set the active node</para>
    /// </summary>
    public void SetActiveItem(TItem item)
    {
        var val = Items.GetAllItems().FirstOrDefault(i => Equals(i.Value, item));
        SetActiveItem(val);
    }

    private static CheckboxState ToggleCheckState(CheckboxState state) => state switch
    {
        CheckboxState.Checked => CheckboxState.UnChecked,
        _ => CheckboxState.Checked
    };

    /// <summary>
    /// <para lang="zh">Toggle node expand collapse state method</para>
    /// <para lang="en">Toggle node expand collapse state method</para>
    /// </summary>
    /// <param name="node"></param>
    private async Task OnToggleNodeAsync(TreeViewItem<TItem> node)
    {
        // 手风琴效果逻辑
        node.IsExpand = !node.IsExpand;

        if (IsAccordion && !IsVirtualize)
        {
            await _treeNodeStateCache.ToggleNodeAsync(node, GetChildrenRowAsync);

            // 展开此节点关闭其他同级节点
            if (node.IsExpand)
            {
                // 通过 item 找到父节点
                var nodes = _treeNodeStateCache.FindParentNode(Items, node)?.Items ?? Items;
                foreach (var n in nodes.Where(n => n != node))
                {
                    // 收缩同级节点
                    n.IsExpand = false;
                    await _treeNodeStateCache.ToggleNodeAsync(n, GetChildrenRowAsync);
                }
            }
            _rows = null;
        }
        else
        {
            // 重建缓存 并且更改节点展开状态
            await _treeNodeStateCache.ToggleNodeAsync(node, GetChildrenRowAsync);
            _rows = null;
        }

        if (ShowCheckbox)
        {
            if (AutoCheckChildren)
            {
                node.SetChildrenCheck(_treeNodeStateCache);
            }
            if (AutoCheckParent)
            {
                node.SetParentCheck(_treeNodeStateCache);
            }
            if (!AutoCheckChildren && AutoCheckParent && node.Items.Count > 0)
            {
                node.Items[0].SetParentCheck(_treeNodeStateCache);
            }
        }
        StateHasChanged();
    }

    private async Task OnCheckStateChanged(TreeViewItem<TItem> item, CheckboxState state)
    {
        item.CheckedState = state;
        _treeNodeStateCache.ToggleCheck(item);

        if (AutoCheckChildren)
        {
            // 向下级联操作
            if (item.CheckedState != CheckboxState.Indeterminate)
            {
                item.SetChildrenCheck(_treeNodeStateCache);
                _ = InvokeVoidAsync("setChildrenState", Id, Rows.IndexOf(item), item.CheckedState);
            }
        }

        if (AutoCheckParent)
        {
            // 向上级联操作
            item.SetParentCheck(_treeNodeStateCache);
            _ = InvokeVoidAsync("setParentState", Id, Interop, nameof(GetParentsState), Rows.IndexOf(item));
        }

        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked([.. GetCheckedItems()]);
        }
    }

    /// <summary>
    /// <para lang="zh">Clear all selected nodes</para>
    /// <para lang="en">Clear all selected nodes</para>
    /// </summary>
    public void ClearCheckedItems()
    {
        Items.ForEach(item =>
        {
            item.CheckedState = CheckboxState.UnChecked;
            _treeNodeStateCache.ToggleCheck(item);
            item.GetAllTreeSubItems().ToList().ForEach(s =>
            {
                s.CheckedState = CheckboxState.UnChecked;
                _treeNodeStateCache.ToggleCheck(s);
            });
        });
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">获得 all selected node 集合s</para>
    /// <para lang="en">Gets all selected node collections</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeViewItem<TItem>> GetCheckedItems() => Items.Aggregate(new List<TreeViewItem<TItem>>(), (t, item) =>
    {
        t.Add(item);
        t.AddRange(item.GetAllSubItems().OfType<TreeViewItem<TItem>>());
        return t;
    }).Where(i => i.CheckedState == CheckboxState.Checked);

    /// <summary>
    /// <para lang="zh">Check if the 数据 is the same</para>
    /// <para lang="en">Check if the data is the same</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TItem? x, TItem? y) => this.Equals<TItem>(x, y);

    private List<TreeViewItem<TItem>>? _rows = null;

    private List<TreeViewItem<TItem>> Rows
    {
        get
        {
            _rows ??= GetTreeItems().ToFlat();
            return _rows;
        }
    }

    private List<TreeViewItem<TItem>> GetTreeItems() => _searchItems ?? Items;

    private bool GetActive(TreeViewItem<TItem> item) => _activeItem == item;

    private int GetIndex(TreeViewItem<TItem> item) => Rows.IndexOf(item);
}
