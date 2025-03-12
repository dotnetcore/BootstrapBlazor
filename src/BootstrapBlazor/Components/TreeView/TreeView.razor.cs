// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Tree component
/// </summary>
[CascadingTypeParameter(nameof(TItem))]
public partial class TreeView<TItem> : IModelEqualityComparer<TItem>
{
    private string? ClassString => CssBuilder.Default("tree-view")
        .AddClass("is-fixed-search", ShowSearch && IsFixedSearch)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? LoadingClassString => CssBuilder.Default("table-loading")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? GetIconClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-icon")
        .AddClass(item.Icon)
        .AddClass(item.ExpandIcon, item.IsExpand && !string.IsNullOrEmpty(item.ExpandIcon))
        .Build();

    private string? GetCaretClassString(TreeViewItem<TItem> item) => CssBuilder.Default("node-icon")
        .AddClass("visible", item.HasChildren || item.Items.Count > 0)
        .AddClass(NodeIcon, !item.IsExpand)
        .AddClass(ExpandNodeIcon, item.IsExpand)
        .AddClass("disabled", IsDisabled || (!CanExpandWhenDisabled && item.IsDisabled))
        .Build();

    private string? NodeLoadingClassString => CssBuilder.Default("node-icon node-loading")
        .AddClass(LoadingIcon)
        .Build();

    private string? GetContentClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-content")
        .AddClass("active", _activeItem == item)
        .Build();

    private string? GetNodeClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-node")
        .AddClass("disabled", GetItemDisabledState(item))
        .Build();

    private bool CanTriggerClickNode(TreeViewItem<TItem> item) => !IsDisabled && (CanExpandWhenDisabled || !item.IsDisabled);

    private bool TriggerNodeLabel(TreeViewItem<TItem> item) => !GetItemDisabledState(item);

    private bool GetItemDisabledState(TreeViewItem<TItem> item) => item.IsDisabled || IsDisabled;

    private TreeViewItem<TItem>? _activeItem;

    /// <summary>
    /// Gets or sets whether to show the loading animation. Default is false.
    /// </summary>
    [Obsolete("Deprecated. Please remove it.")]
    [ExcludeFromCodeCoverage]
    public bool IsReset { get; set; }

    /// <summary>
    /// Gets or sets whether the tree view is editable. Default is false.
    /// </summary>
    [Parameter]
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets whether the entire component is disabled. Default is false.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether nodes can be expanded or collapsed when the component is disabled. Default is false.
    /// </summary>
    [Parameter]
    public bool CanExpandWhenDisabled { get; set; }

    /// <summary>
    /// Gets or sets whether the tree view has accordion behavior. Default is false. Accordion behavior is not supported in virtual scrolling mode.
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// Gets or sets whether clicking a node expands or collapses its children. Default is false.
    /// </summary>
    [Parameter]
    public bool ClickToggleNode { get; set; }

    /// <summary>
    /// Gets or sets whether clicking a node toggles its checkbox state. Default is false. Effective when <see cref="ShowCheckbox"/> is true.
    /// </summary>
    [Parameter]
    public bool ClickToggleCheck { get; set; }

    /// <summary>
    /// Gets or sets whether to show the loading skeleton. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// Gets or sets whether to show the search bar. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowSearch { get; set; }

    /// <summary>
    /// Gets or sets whether the search bar is fixed. Default is false.
    /// </summary>
    [Parameter]
    public bool IsFixedSearch { get; set; }

    /// <summary>
    /// Gets or sets whether to show the reset search button. Default is true.
    /// </summary>
    [Parameter]
    public bool ShowResetSearchButton { get; set; } = true;

    /// <summary>
    /// Gets or sets the search bar template. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment? SearchTemplate { get; set; }

    /// <summary>
    /// Gets or sets the search icon. Default is not set, using the built-in theme icon.
    /// </summary>
    [Parameter]
    public string? SearchIcon { get; set; }

    /// <summary>
    /// Gets or sets the clear search icon. Default is not set, using the built-in theme icon.
    /// </summary>
    [Parameter]
    public string? ClearSearchIcon { get; set; }

    /// <summary>
    /// Gets or sets the search callback method. Default is null.
    /// </summary>
    /// <remarks>Enabled by setting <see cref="ShowSearch"/> to true.</remarks>
    [Parameter]
    public Func<string?, Task<List<TreeViewItem<TItem>>?>>? OnSearchAsync { get; set; }

    /// <summary>
    /// Gets or sets the hierarchical data collection.
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeViewItem<TItem>>? Items { get; set; }

    /// <summary>
    /// Gets or sets whether to show checkboxes. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of selected items.
    /// </summary>
    [Parameter]
    public int MaxSelectedCount { get; set; }

    /// <summary>
    /// Gets or sets the callback method when the maximum number of selected items is exceeded.
    /// </summary>
    [Parameter]
    public Func<Task>? OnMaxSelectedCountExceed { get; set; }

    /// <summary>
    /// Gets or sets whether to show icons. Default is false.
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// Gets or sets the callback method when a tree item is clicked.
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// Gets or sets the callback method when a tree item is checked.
    /// </summary>
    [Parameter]
    public Func<List<TreeViewItem<TItem>>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// Gets or sets the callback method to get child data when a node is expanded.
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
    /// Gets or sets whether to enable keyboard navigation. Default is false.
    /// <para>ArrowLeft collapses the node.</para>
    /// <para>ArrowRight expands the node.</para>
    /// <para>ArrowUp moves to the previous node.</para>
    /// <para>ArrowDown moves to the next node.</para>
    /// <para>Space selects the current node.</para>
    /// </summary>
    [Parameter]
    public bool EnableKeyboard { get; set; }

    /// <summary>
    /// Gets or sets the scroll into view options for keyboard navigation. Default is null, using { behavior: "smooth", block: "nearest", inline: "start" }.
    /// </summary>
    [Parameter]
    public ScrollIntoViewOptions? ScrollIntoViewOptions { get; set; }

    /// <summary>
    /// Gets or sets whether to enable virtual scrolling. Default is false.
    /// </summary>
    [Parameter]
    public bool IsVirtualize { get; set; }

    /// <summary>
    /// Gets or sets the row height for virtual scrolling. Default is 38.
    /// </summary>
    /// <remarks>Effective when <see cref="ScrollMode"/> is set to Virtual.</remarks>
    [Parameter]
    public float RowHeight { get; set; } = 38f;

    [CascadingParameter]
    private ContextMenuZone? ContextMenuZone { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TreeView<TItem>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    [NotNull]
    private TreeNodeCache<TreeViewItem<TItem>, TItem>? _treeNodeStateCache = null;

    /// <summary>
    /// Gets or sets whether to automatically update child nodes when the node state changes. Default is false.
    /// </summary>
    [Parameter]
    public bool AutoCheckChildren { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically update parent nodes when the node state changes. Default is false.
    /// </summary>
    [Parameter]
    public bool AutoCheckParent { get; set; }

    private string? _searchText;

    private string? EnableKeyboardString => EnableKeyboard ? "true" : null;

    private bool _shouldRender = true;

    private static string? GetItemTextClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-node-text")
        .AddClass(item.CssClass)
        .Build();

    private bool _init;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _treeNodeStateCache = new(this);
        NotSetOnTreeExpandErrorMessage = Localizer[nameof(NotSetOnTreeExpandErrorMessage)];
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
                // 开启 Checkbox 功能时初始化选中节点
                _treeNodeStateCache.IsChecked(Items);
            }

            // 从数据源中恢复当前 active 节点
            if (_activeItem != null)
            {
                _activeItem = _treeNodeStateCache.Find(Items, _activeItem.Value, out _);
            }

            if (_init == false)
            {
                // 设置 ActiveItem 默认值
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
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, Method = nameof(TriggerKeyDown) });

    private bool _keyboardArrowUpDownTrigger;

    /// <summary>
    /// 客户端用户键盘操作处理方法 由 JavaScript 调用
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [JSInvokable]
    public async ValueTask TriggerKeyDown(string key)
    {
        // 通过 ActiveItem 找到兄弟节点
        // 如果兄弟节点没有时，找到父亲节点
        if (_activeItem != null)
        {
            if (key == "ArrowUp" || key == "ArrowDown")
            {
                _keyboardArrowUpDownTrigger = true;
                await ActiveTreeViewItem(key, _activeItem);
            }
            else if (key == "ArrowLeft" || key == "ArrowRight")
            {
                await OnToggleNodeAsync(_activeItem, true);
            }
        }
    }

    /// <summary>
    /// 客户端查询指定行选择框状态方法 由 JavaScript 调用
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
                // 展开节点
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
        // 恢复当前节点状态
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

    /// <summary>
    /// 选中节点时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClick(TreeViewItem<TItem> item)
    {
        _activeItem = item;
        if (ClickToggleNode && CanTriggerClickNode(item))
        {
            await OnToggleNodeAsync(item, false);
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
    /// 设置选中节点
    /// </summary>
    public void SetActiveItem(TreeViewItem<TItem>? item)
    {
        _activeItem = item;
        _activeItem?.SetParentExpand<TreeViewItem<TItem>, TItem>(true);
        StateHasChanged();
    }

    /// <summary>
    /// 重新设置 <see cref="Items"/> 数据源方法
    /// </summary>
    public void SetItems(List<TreeViewItem<TItem>> items)
    {
        //FlatItems = null;
        Items = items;
        _rows = null;
        StateHasChanged();
    }

    /// <summary>
    /// 设置选中节点
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
    /// 切换节点展开收缩状态方法
    /// </summary>
    /// <param name="node"></param>
    /// <param name="shouldRender"></param>
    private async Task OnToggleNodeAsync(TreeViewItem<TItem> node, bool shouldRender)
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

        if (shouldRender)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 节点 Checkbox 状态改变时触发此方法
    /// </summary>
    /// <param name="item"></param>
    /// <param name="state"></param>
    /// <returns></returns>
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
    /// 清除 所有选中节点
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
    /// 获得 所有选中节点集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeViewItem<TItem>> GetCheckedItems() => Items.Aggregate(new List<TreeViewItem<TItem>>(), (t, item) =>
    {
        t.Add(item);
        t.AddRange(item.GetAllSubItems().OfType<TreeViewItem<TItem>>());
        return t;
    }).Where(i => i.CheckedState == CheckboxState.Checked);

    /// <summary>
    /// 比较数据是否相同
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TItem? x, TItem? y) => this.Equals<TItem>(x, y);

    private async Task OnContextMenu(MouseEventArgs e, TreeViewItem<TItem> item)
    {
        if (ContextMenuZone != null)
        {
            await ContextMenuZone.OnContextMenu(e, item.Value);
        }
    }

    private bool IsPreventDefault => ContextMenuZone != null;

    /// <summary>
    /// 是否触摸
    /// </summary>
    private bool TouchStart { get; set; }

    /// <summary>
    /// 触摸定时器工作指示
    /// </summary>
    private bool IsBusy { get; set; }

    private async Task OnTouchStart(TouchEventArgs e, TreeViewItem<TItem> item)
    {
        if (!IsBusy && ContextMenuZone != null)
        {
            IsBusy = true;
            TouchStart = true;

            // 延时保持 TouchStart 状态
            var delay = Options.CurrentValue.ContextMenuOptions.OnTouchDelay;
            await Task.Delay(delay);
            if (TouchStart)
            {
                var args = new MouseEventArgs()
                {
                    ClientX = e.Touches[0].ClientX,
                    ClientY = e.Touches[0].ClientY,
                    ScreenX = e.Touches[0].ScreenX,
                    ScreenY = e.Touches[0].ScreenY,
                };
                // 弹出关联菜单
                await OnContextMenu(args, item);

                //延时防止重复激活菜单功能
                await Task.Delay(delay);
            }
            IsBusy = false;
        }
    }

    private void OnTouchEnd()
    {
        TouchStart = false;
    }

    private List<TreeViewItem<TItem>>? _rows = null;

    private List<TreeViewItem<TItem>> Rows
    {
        get
        {
            // 扁平化数据集合
            _rows ??= GetTreeItems().ToFlat<TItem>();
            return _rows;
        }
    }

    private List<TreeViewItem<TItem>> GetTreeItems() => _searchItems ?? Items;

    private static string? GetTreeRowStyle(TreeViewItem<TItem> item)
    {
        var level = 0;
        var parent = item.Parent;
        while (parent != null)
        {
            level++;
            parent = parent.Parent;
        }
        return $"--bb-tree-view-level: {level};";
    }
}
