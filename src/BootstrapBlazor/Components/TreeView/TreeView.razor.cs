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
public partial class TreeView<TItem> where TItem : class
{
    /// <summary>
    /// 获得/设置 Tree 组件实例引用
    /// </summary>
    private ElementReference TreeElement { get; set; }

    [NotNull]
    private string? GroupName { get; set; }

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
    private static string? GetCaretClassString(TreeViewItem<TItem> item) => CssBuilder.Default("fa fa-caret-right")
        .AddClass("visible", item.HasChildren || item.Items.Any())
        .AddClass("fa-rotate-90", item.IsExpand)
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
    /// 获得/设置 是否显示 Radio 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowRadio { get; set; }

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

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TreeView<TItem>>? Localizer { get; set; }

    /// <summary>
    /// 节点缓存类实例
    /// </summary>
    [NotNull]
    protected TreeNodeCache<TreeViewItem<TItem>, TItem>? treeNodeCache = null;

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
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 初始化节点缓存
        treeNodeCache ??= new(ComparerItem);

        GroupName = this.GetHashCode().ToString();
        NotSetOnTreeExpandErrorMessage = Localizer[nameof(NotSetOnTreeExpandErrorMessage)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Items != null)
        {
            if (!IsReset)
            {
                treeNodeCache.IsChecked(Items);

                // 从数据源中恢复当前 active 节点
                if (ActiveItem != null)
                {
                    ActiveItem = treeNodeCache.Find(Items, ActiveItem.Value, out _);
                }
            }
            else
            {
                treeNodeCache.Reset();
            }

            // 设置 ActiveItem 默认值
            ActiveItem ??= Items.FirstOrDefaultActiveItem();
        }
    }

    /// <summary>
    /// OnParametersSetAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (Items != null)
        {
            if (Items.Any())
            {
                await CheckExpand(Items);
            }

            async Task CheckExpand(IEnumerable<TreeViewItem<TItem>> nodes)
            {
                // 恢复当前节点状态
                foreach (var node in nodes)
                {
                    await treeNodeCache.CheckExpandAsync(node, GetChildrenRowAsync);

                    if (node.Items.Any())
                    {
                        await CheckExpand(node.Items);
                    }
                }
            }
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

        if (ShowRadio)
        {
            await OnRadioClick(item);
        }
        else if (ShowCheckbox && ClickToggleCheck)
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
            await treeNodeCache.ToggleNodeAsync(node, GetChildrenRowAsync);

            // 展开此节点关闭其他同级节点
            if (node.IsExpand)
            {
                // 通过 item 找到父节点
                var nodes = treeNodeCache.FindParentNode(Items, node)?.Items ?? Items;
                foreach (var n in nodes)
                {
                    if (n != node)
                    {
                        // 收缩同级节点
                        n.IsExpand = false;
                        await treeNodeCache.ToggleNodeAsync(n, GetChildrenRowAsync);
                    }
                }
            }
        }
        else
        {
            // 重建缓存 并且更改节点展开状态
            await treeNodeCache.ToggleNodeAsync(node, GetChildrenRowAsync);
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
    /// <returns></returns>
    private async Task OnCheckStateChanged(TreeViewItem<TItem> item)
    {
        item.CheckedState = ToggleCheckState(item.CheckedState);

        if (AutoCheckChildren)
        {
            // 向下级联操作
            item.SetChildrenCheck<TreeViewItem<TItem>, TItem>(item.CheckedState, treeNodeCache);
        }

        if (AutoCheckParent)
        {
            // 向上级联操作
            item.SetParentCheck(item.CheckedState, treeNodeCache);
        }

        // 更新 选中状态缓存
        treeNodeCache.ToggleCheck(item);

        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(GetCheckedItems().ToList());
        }

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

    private async Task OnRadioClick(TreeViewItem<TItem> item)
    {
        item.CheckedState = ToggleCheckState(item.CheckedState);

        // 单选移除已选择
        if (ActiveItem != null)
        {
            ActiveItem.CheckedState = CheckboxState.UnChecked;
            treeNodeCache.ToggleCheck(ActiveItem);
        }
        ActiveItem = item;
        ActiveItem.CheckedState = CheckboxState.Checked;
        treeNodeCache.ToggleCheck(item);

        // 其他设置为 false
        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(new List<TreeViewItem<TItem>> { item });
        }

        StateHasChanged();
    }

    /// <summary>
    /// 比较数据是否相同
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    protected bool ComparerItem(TItem a, TItem b) => ModelEqualityComparer?.Invoke(a, b)
        ?? Utility.GetKeyValue<TItem, object>(a, CustomKeyAttribute)?.Equals(Utility.GetKeyValue<TItem, object>(b, CustomKeyAttribute))
        ?? ModelComparer.EqualityComparer(a, b)
        ?? a.Equals(b);
}
