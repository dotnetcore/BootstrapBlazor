// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Tree 组件
/// </summary>
#if NET6_0_OR_GREATER
[CascadingTypeParameter(nameof(TItem))]
#endif
public partial class TreeView<TItem> : IModelEqualityComparer<TItem>
{
    /// <summary>
    /// 获得/设置 Tree 组件实例引用
    /// </summary>
    private ElementReference TreeElement { get; set; }

    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("tree-view")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 Loading 样式集合
    /// </summary>
    private string? LoadingClassString => CssBuilder.Default("table-loading")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 TreeItem 图标
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetIconClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-icon")
        .AddClass(item.Icon)
        .AddClass(item.ExpandIcon, item.IsExpand && !string.IsNullOrEmpty(item.ExpandIcon))
        .Build();

    /// <summary>
    /// 获得/设置 TreeItem 小箭头样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetCaretClassString(TreeViewItem<TItem> item) => CssBuilder.Default("node-icon")
        .AddClass("visible", item.HasChildren || item.Items.Any())
        .AddClass(NodeIcon, !item.IsExpand)
        .AddClass(ExpandNodeIcon, item.IsExpand)
        .Build();

    /// <summary>
    /// 获得/设置 当前行样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetItemClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-item")
        .AddClass("active", ActiveItem == item)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 Tree 样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetTreeClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-ul")
        .AddClass("show", item.IsExpand)
        .Build();

    private static string? GetNodeClassString(TreeViewItem<TItem> item) => CssBuilder.Default("tree-node")
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private static bool TriggerNodeArrow(TreeViewItem<TItem> item) => !item.IsDisabled && (item.HasChildren || item.Items.Any());

    private static bool TriggerNodeLabel(TreeViewItem<TItem> item) => !item.IsDisabled;

    /// <summary>
    /// 获得/设置 选中节点 默认 null
    /// </summary>
    private TreeViewItem<TItem>? ActiveItem { get; set; }

    /// <summary>
    /// 获得/设置 是否为手风琴效果 默认为 false
    /// </summary>
    [Parameter]
    public bool IsAccordion { get; set; }

    /// <summary>
    /// 获得/设置 是否点击节点时展开或者收缩子项 默认 false
    /// </summary>
    [Parameter]
    public bool ClickToggleNode { get; set; }

    /// <summary>
    /// 获得/设置 是否点击节点自动切换 Checkbox 状态 默认 false <see cref="ShowCheckbox"/> 时生效
    /// </summary>
    [Parameter]
    public bool ClickToggleCheck { get; set; }

    /// <summary>
    /// 获得/设置 是否显示加载骨架屏 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSkeleton { get; set; }

    /// <summary>
    /// 获得/设置 页面刷新是否重置已加载数据 默认 false
    /// </summary>
    [Parameter]
    public bool IsReset { get; set; }

    /// <summary>
    /// 获得/设置 带层次数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeViewItem<TItem>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 CheckBox 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowCheckbox { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Icon 图标 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; }

    /// <summary>
    /// 获得/设置 树形控件节点点击时回调委托
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// 获得/设置 树形控件节点选中时回调委托
    /// </summary>
    [Parameter]
    public Func<List<TreeViewItem<TItem>>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// 获得/设置 点击节点获取子数据集合回调方法
    /// </summary>
    [Parameter]
    public Func<TreeViewItem<TItem>, Task<IEnumerable<TreeViewItem<TItem>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Type CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

    /// <summary>
    /// 获得/设置 比较数据是否相同回调方法 默认为 null
    /// </summary>
    /// <remarks>提供此回调方法时忽略 <see cref="CustomKeyAttribute"/> 属性</remarks>
    [Parameter]
    public Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 获得/设置 Tree Node 节点图标
    /// </summary>
    [Parameter]
    public string? NodeIcon { get; set; }

    /// <summary>
    /// 获得/设置 Tree Node 展开节点图标
    /// </summary>
    [Parameter]
    public string? ExpandNodeIcon { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TreeView<TItem>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 节点状态缓存类实例
    /// </summary>
    [NotNull]
    protected TreeNodeCache<TreeViewItem<TItem>, TItem>? TreeNodeStateCache { get; set; }

    /// <summary>
    /// 改变节点状态后自动更新子节点 默认 false
    /// </summary>
    [Parameter]
    public bool AutoCheckChildren { get; set; }

    /// <summary>
    /// 改变节点状态后自动更新父节点 默认 false
    /// </summary>
    [Parameter]
    public bool AutoCheckParent { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 初始化节点缓存
        TreeNodeStateCache ??= new(Equals);
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
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        if (Items != null)
        {
            if (IsReset)
            {
                TreeNodeStateCache.Reset();
            }
            else
            {
                if (Items.Any())
                {
                    await CheckExpand(Items);
                }

                if (ShowCheckbox && (AutoCheckParent || AutoCheckChildren))
                {
                    // 开启 Checkbox 功能时初始化选中节点
                    TreeNodeStateCache.IsChecked(Items);
                }

                // 从数据源中恢复当前 active 节点
                if (ActiveItem != null)
                {
                    ActiveItem = TreeNodeStateCache.Find(Items, ActiveItem.Value, out _);
                }
            }

            // 设置 ActiveItem 默认值
            ActiveItem ??= Items.FirstOrDefaultActiveItem();
            ActiveItem?.SetParentExpand<TreeViewItem<TItem>, TItem>(true);

            async Task CheckExpand(IEnumerable<TreeViewItem<TItem>> nodes)
            {
                // 恢复当前节点状态
                foreach (var node in nodes)
                {
                    await TreeNodeStateCache.CheckExpandAsync(node, GetChildrenRowAsync);

                    if (node.Items.Any())
                    {
                        await CheckExpand(node.Items);
                    }
                }
            }
        }
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync(TreeElement, "bb_tree");
        }
    }

    private async Task<IEnumerable<IExpandableNode<TItem>>> GetChildrenRowAsync(TreeViewItem<TItem> node)
    {
        if (OnExpandNodeAsync == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }
        node.ShowLoading = true;

        StateHasChanged();

        var ret = await OnExpandNodeAsync(node);
        node.ShowLoading = false;
        return ret;
    }

    /// <summary>
    /// 选中节点时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClick(TreeViewItem<TItem> item)
    {
        ActiveItem = item;
        if (ClickToggleNode && TriggerNodeArrow(item))
        {
            await OnToggleNodeAsync(item);
        }

        if (OnTreeItemClick != null)
        {
            await OnTreeItemClick(item);
        }

        if (ShowCheckbox && ClickToggleCheck)
        {
            await OnCheckStateChanged(item);
        }

        StateHasChanged();
    }

    private static CheckboxState ToggleCheckState(CheckboxState state) => state switch
    {
        CheckboxState.Checked => CheckboxState.UnChecked,
        _ => CheckboxState.Checked
    };

    /// <summary>
    /// 更改节点是否展开方法
    /// </summary>
    /// <param name="node"></param>
    /// <param name="shouldRender"></param>
    private async Task OnToggleNodeAsync(TreeViewItem<TItem> node, bool shouldRender = false)
    {
        // 手风琴效果逻辑
        node.IsExpand = !node.IsExpand;
        if (IsAccordion)
        {
            await TreeNodeStateCache.ToggleNodeAsync(node, GetChildrenRowAsync);

            // 展开此节点关闭其他同级节点
            if (node.IsExpand)
            {
                // 通过 item 找到父节点
                var nodes = TreeNodeStateCache.FindParentNode(Items, node)?.Items ?? Items;
                foreach (var n in nodes)
                {
                    if (n != node)
                    {
                        // 收缩同级节点
                        n.IsExpand = false;
                        await TreeNodeStateCache.ToggleNodeAsync(n, GetChildrenRowAsync);
                    }
                }
            }
        }
        else
        {
            // 重建缓存 并且更改节点展开状态
            await TreeNodeStateCache.ToggleNodeAsync(node, GetChildrenRowAsync);
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
    /// <param name="shouldRender"></param>
    /// <returns></returns>
    private async Task OnCheckStateChanged(TreeViewItem<TItem> item, bool shouldRender = false)
    {
        item.CheckedState = ToggleCheckState(item.CheckedState);

        if (AutoCheckChildren)
        {
            // 向下级联操作
            item.SetChildrenCheck<TreeViewItem<TItem>, TItem>(item.CheckedState, TreeNodeStateCache);
        }

        if (AutoCheckParent)
        {
            // 向上级联操作
            item.SetParentCheck(item.CheckedState, TreeNodeStateCache);
        }

        // 更新 选中状态缓存
        TreeNodeStateCache.ToggleCheck(item);

        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(GetCheckedItems().ToList());
        }

        if (shouldRender)
        {
            StateHasChanged();
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
            TreeNodeStateCache.ToggleCheck(item);
            item.GetAllTreeSubItems().ToList().ForEach(s =>
            {
                s.CheckedState = CheckboxState.UnChecked;
                TreeNodeStateCache.ToggleCheck(s);
            });
            StateHasChanged();
        });
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
}
