// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Tree 组件</para>
/// <para lang="en">Tree Component</para>
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
    /// <para lang="zh">获得/设置 是否显示加载动画，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the loading animation. Default is false</para>
    /// </summary>
    [Obsolete("Deprecated. Please remove it.")]
    [ExcludeFromCodeCoverage]
    public bool IsReset { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示树视图项的工具栏，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the toolbar of tree view item. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowToolbar { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确定是否显示树视图项工具栏的回调方法</para>
    /// <para lang="en">Gets or sets the callback method that determines whether to show the toolbar of the tree view item</para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? ShowToolbarCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 整个组件是否被禁用，默认为 false</para>
    /// <para lang="en">Gets or sets whether the entire component is disabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件被禁用时，节点是否可以展开或折叠，默认为 false</para>
    /// <para lang="en">Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树视图是否具有手风琴行为，默认为 false。虚拟滚动模式不支持手风琴行为</para>
    /// <para lang="en">Gets or sets whether the tree view has accordion behavior. Default is false. Accordion behavior is not supported in virtual scrolling mode</para>
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击节点是否展开或折叠其子节点，默认为 false</para>
    /// <para lang="en">Gets or sets whether clicking a node expands or collapses its children. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ClickToggleNode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击节点是否切换其复选框状态，默认为 false。仅在 ShowCheckbox 为 true 时有效</para>
    /// <para lang="en">Gets or sets whether clicking a node toggles its checkbox state. Default is false. Effective when ShowCheckbox is true</para>
    /// </summary>
    [Parameter]
    public bool ClickToggleCheck { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示加载骨架，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the loading skeleton. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示搜索栏，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the search bar. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示重置搜索按钮，默认为 true</para>
    /// <para lang="en">Gets or sets whether to show the reset search button. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowResetSearchButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 搜索栏模板，默认为 null</para>
    /// <para lang="en">Gets or sets the search bar template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索图标，默认未设置，使用内置主题图标</para>
    /// <para lang="en">Gets or sets the search icon. Default is not set, using the built-in theme icon</para>
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清除搜索图标，默认未设置，使用内置主题图标</para>
    /// <para lang="en">Gets or sets the clear search icon. Default is not set, using the built-in theme icon</para>
    /// </summary>
    [Parameter]
    public string? ClearSearchIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the search callback method. Default is null</para>
    /// </summary>
    /// <remarks>Enabled by setting <see cref="ShowSearch"/> to true.</remarks>
    [Parameter]
    public Func<string?, Task<List<TreeViewItem<TItem>>?>>? OnSearchAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分层数据集合</para>
    /// <para lang="en">Gets or sets the hierarchical data collection</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeViewItem<TItem>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示复选框，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show checkboxes. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最多选中项数</para>
    /// <para lang="en">Gets or sets the maximum number of selected items</para>
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 超过最多选中项数时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the maximum number of selected items is exceeded</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnMaxSelectedCountExceed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示图标，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show icons. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树项被点击时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when a tree item is clicked</para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击节点前的回调方法</para>
    /// <para lang="en">Gets or sets the callback method before a tree item is clicked</para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<bool>>? OnBeforeTreeItemClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树项被选中时的回调方法</para>
    /// <para lang="en">Gets or sets the callback method when a tree item is checked</para>
    /// </summary>
    [Parameter]
    public Func<List<TreeViewItem<TItem>>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 节点展开时获取子数据的回调方法</para>
    /// <para lang="en">Gets or sets the callback method to get child data when a node is expanded</para>
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<IEnumerable<TreeViewItem<TItem>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树节点的加载图标</para>
    /// <para lang="en">Gets or sets the loading icon for tree nodes</para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树节点的图标</para>
    /// <para lang="en">Gets or sets the icon for tree nodes</para>
    /// </summary>
    [Parameter]
    public string? NodeIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开状态的树节点图标</para>
    /// <para lang="en">Gets or sets the icon for expanded tree nodes</para>
    /// </summary>
    [Parameter]
    public string? ExpandNodeIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用键盘导航，默认为 false。 <para>ArrowLeft 收缩节点</para>
    /// <para>ArrowRight 展开节点</para>
    /// <para>ArrowUp 移动到上一个节点</para>
    /// <para>ArrowDown 移动到下一个节点</para>
    /// <para>Space 选择当前节点</para>
    ///</para>
    /// <para lang="en">Gets or sets whether to enable keyboard navigation. Default is false. <para>ArrowLeft collapses the node</para>
    /// <para>ArrowRight expands the node</para>
    /// <para>ArrowUp moves to the previous node</para>
    /// <para>ArrowDown moves to the next node</para>
    /// <para>Space selects the current node</para>
    ///</para>
    /// </summary>
    [Parameter]
    public bool EnableKeyboard { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 键盘导航时的滚动至视图选项，默认为 null，使用 { behavior: "smooth", block: "nearest", inline: "start" }</para>
    /// <para lang="en">Gets or sets the scroll into view options for keyboard navigation. Default is null, using { behavior: "smooth", block: "nearest", inline: "start" }</para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewOptions? ScrollIntoViewOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用虚拟滚动，默认为 false</para>
    /// <para lang="en">Gets or sets whether to enable virtual scrolling. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动时的行高，默认为 29f</para>
    /// <para lang="en">Gets or sets the row height for virtual scrolling. Default is 29f</para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 29f;

    /// <summary>
    /// <para lang="zh">获得/设置 虚拟滚动时的超扫描计数，默认为 10</para>
    /// <para lang="en">Gets or sets the overscan count for virtual scrolling. Default is 10</para>
    /// </summary>
    /// <remarks>Effective when <see cref="IsVirtualize"/> is set to true.</remarks>
    [Parameter]
    public int OverscanCount { get; set; } = 10;

    /// <summary>
    /// <para lang="zh">获得/设置 工具栏内容模板，默认为 null</para>
    /// <para lang="en">Gets or sets the toolbar content template. Default is null</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口标题，默认为 null</para>
    /// <para lang="en">Gets or sets the title of the popup-window. Default is null</para>
    /// </summary>
    [Parameter]
    public string? ToolbarEditTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口标签文本，默认为 null</para>
    /// <para lang="en">Gets or sets the title of the popup-window. Default is null</para>
    /// </summary>
    [Parameter]
    public string? ToolbarEditLabelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 更新树文本值的回调，默认为 null. 如果返回 true 则更新树文本值，否则不更新</para>
    /// <para lang="en">Gets or sets the update the tree text value callback. Default is null. If return true will update the tree text value, otherwise will not update</para>
    /// </summary>
    [Parameter]
    public Func<TItem, string?, Task<bool>>? OnUpdateCallbackAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 节点状态变化时是否自动更新子节点，默认为 false</para>
    /// <para lang="en">Gets or sets whether to automatically update child nodes when the node state changes. Default is false</para>
    /// </summary>
    [Parameter]
    public bool AutoCheckChildren { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 节点状态变化时是否自动更新父节点，默认为 false</para>
    /// <para lang="en">Gets or sets whether to automatically update parent nodes when the node state changes. Default is false</para>
    /// </summary>
    [Parameter]
    public bool AutoCheckParent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许拖放操作，默认为 false</para>
    /// <para lang="en">Gets or sets a value indicating whether drag-and-drop operations are allowed. Default is false</para>
    /// </summary>
    [Parameter]
    public bool AllowDrag { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拖动标签页结束回调方法</para>
    /// <para lang="en">Gets or sets 拖动标签页结束callback method</para>
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
    protected override bool ShouldRender() => _shouldRender;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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
    /// <para lang="zh">Triggers the end of a drag-and-drop operation within the tree view</para>
    /// <para lang="en">Triggers the end of a drag-and-drop operation within the tree view</para>
    /// </summary>
    /// <remarks>This method is invoked via JavaScript interop to signal the completion of a drag-and-drop
    /// action. If a handler is assigned to <see cref="OnDragItemEndAsync"/>, it will be invoked with the drag
    /// context.</remarks>
    /// <param name="originIndex">The zero-based index of the item being dragged from its original position.</param>
    /// <param name="currentIndex">The zero-based index of the item's current position after the drag operation.</param>
    /// <param name="isChildren">A value indicating whether the drag operation involves child items.</param>
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
