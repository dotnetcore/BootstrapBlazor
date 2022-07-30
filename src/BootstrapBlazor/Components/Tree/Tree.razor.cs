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
public partial class Tree<TItem> where TItem : class
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
    private string? ClassString => CssBuilder.Default("tree")
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
    private static string? GetIconClassString(TreeItem<TItem> item) => CssBuilder.Default("tree-icon")
        .AddClass(item.Icon)
        .Build();

    /// <summary>
    /// 获得/设置 TreeItem 小箭头样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetCaretClassString(TreeItem<TItem> item) => CssBuilder.Default("fa fa-caret-right")
        .AddClass("visible", item.HasChildren || item.Items.Any())
        .AddClass("fa-rotate-90", item.IsExpand)
        .Build();

    /// <summary>
    /// 获得/设置 当前行样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetItemClassString(TreeItem<TItem> item) => CssBuilder.Default("tree-item")
        .AddClass("active", ActiveItem == item)
        .AddClass("disabled", item.IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 Tree 样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetTreeClassString(TreeItem<TItem> item) => CssBuilder.Default("tree-ul")
        .AddClass("show", item.IsExpand)
        .Build();

    private static string? GetNodeClassString(TreeItem<TItem> item) => CssBuilder.Default("tree-node")
        .AddClass("disabled", item.IsDisabled)
        .Build();

    private static bool TriggerNodeArrow(TreeItem<TItem> item) => !item.IsDisabled && (item.HasChildren || item.Items.Any());

    private static bool TriggerNodeLabel(TreeItem<TItem> item) => !item.IsDisabled;

    /// <summary>
    /// 获得/设置 选中节点 默认 null
    /// </summary>
    private TreeItem<TItem>? ActiveItem { get; set; }

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
    public IEnumerable<TreeItem<TItem>>? Items { get; set; }

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
    public Func<TreeItem<TItem>, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// 获得/设置 树形控件节点选中时回调委托
    /// </summary>
    [Parameter]
    public Func<List<TreeItem<TItem>>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// 获得/设置 点击节点获取子数据集合回调方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TreeItem<TItem>>>>? OnExpandNodeAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    [NotNull]
    public Type? CustomKeyAttribute { get; set; } = typeof(KeyAttribute);

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
    private IStringLocalizer<Tree<TItem>>? Localizer { get; set; }

    /// <summary>
    /// 节点缓存类实例
    /// </summary>
    [NotNull]
    protected TreeNodeCache<TreeItem<TItem>, TItem>? treeNodeCache = null;

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

            async Task CheckExpand(IEnumerable<TreeItem<TItem>> nodes)
            {
                // 恢复当前节点状态
                foreach (var node in nodes)
                {
                    await treeNodeCache.CheckExpand(node, GetChildrenRowAsync);

                    if (node.Items.Any())
                    {
                        await CheckExpand(node.Items);
                    }
                }
            }
        }
    }

    private async Task<IEnumerable<IExpandableNode<TItem>>> GetChildrenRowAsync(TreeItem<TItem> node, TItem item)
    {
        if (OnExpandNodeAsync == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }
        node.ShowLoading = true;
        StateHasChanged();

        var ret = await OnExpandNodeAsync(item);
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
    private async Task OnClick(TreeItem<TItem> item)
    {
        ActiveItem = item;
        if (ClickToggleNode)
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
        else if (ShowCheckbox)
        {
            item.Checked = !item.Checked;
            await OnCheckStateChanged(item);
        }
        StateHasChanged();
    }

    /// <summary>
    /// 更改节点是否展开方法
    /// </summary>
    /// <param name="item"></param>
    /// <param name="shouldRender"></param>
    private async Task OnToggleNodeAsync(TreeItem<TItem> item, bool shouldRender = false)
    {
        // 手风琴效果逻辑
        if (IsAccordion)
        {
            item.IsExpand = !item.IsExpand;
            await treeNodeCache.ToggleNodeAsync(item, GetChildrenRowAsync);

            // 展开此节点关闭其他同级节点
            if (item.IsExpand)
            {
                // 通过 item 找到父节点
                var nodes = treeNodeCache.FindParentNode(Items, item)?.Items ?? Items;
                foreach (var node in nodes)
                {
                    if (node != item)
                    {
                        // 收缩同级节点
                        node.IsExpand = false;
                        await treeNodeCache.ToggleNodeAsync(node, GetChildrenRowAsync);
                    }
                }
            }
        }
        else
        {
            await ToggleNodeAsync();
        }

        if (shouldRender)
        {
            StateHasChanged();
        }

        async Task ToggleNodeAsync()
        {
            if (item.HasChildren || item.Items.Any())
            {
                // 重建缓存 并且更改节点展开状态
                await treeNodeCache.ToggleNodeAsync(item, GetChildrenRowAsync);
            }
        }
    }

    /// <summary>
    /// 节点 Checkbox 状态改变时触发此方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private async Task OnCheckStateChanged(TreeItem<TItem> item)
    {
        // 向下级联操作
        item.CascadeSetCheck(item.Checked);

        // TODO: 向上级联操作

        // 更新 选中状态缓存
        treeNodeCache.ToggleCheck(item);

        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(GetCheckedItems().ToList());
        }
    }

    /// <summary>
    /// 获得 所有选中节点集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeItem<TItem>> GetCheckedItems() => Items.Aggregate(new List<TreeItem<TItem>>(), (t, item) =>
    {
        t.Add(item);
        t.AddRange(item.GetAllSubItems());
        return t;
    }).Where(i => i.Checked);

    private async Task OnRadioClick(TreeItem<TItem> item)
    {
        // 单选移除已选择
        if (ActiveItem != null)
        {
            ActiveItem.Checked = false;
            treeNodeCache.ToggleCheck(ActiveItem);
        }
        ActiveItem = item;
        ActiveItem.Checked = true;
        treeNodeCache.ToggleCheck(item);

        // 其他设置为 false
        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(new List<TreeItem<TItem>> { item });
        }
    }

    private static CheckboxState CheckState(TreeItem<TItem> item) => item.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;

    private static CheckboxState CheckCascadeState(TreeItem<TItem> item)
    {
        var ret = item.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
        if (item.Items.Any())
        {
            if (item.Items.All(i => i.Checked))
            {
                item.Checked = true;
                ret = CheckboxState.Checked;
            }
            else if (item.Items.Any(i => i.Checked))
            {
                item.Checked = false;
                ret = CheckboxState.Indeterminate;
            }
        }
        return ret;
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
