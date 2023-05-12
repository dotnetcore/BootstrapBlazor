// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Tree 组件
/// </summary>
[ExcludeFromCodeCoverage]
[Obsolete("请使用 TreeView 代替，未来几个版本后将会删除")]
public partial class Tree
{
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
    private static string? GetIconClassString(TreeItem item) => CssBuilder.Default("tree-icon")
        .AddClass(item.Icon)
        .Build();

    /// <summary>
    /// 获得/设置 TreeItem 小箭头样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetCaretClassString(TreeItem item) => CssBuilder.Default("node-icon")
        .AddClass("invisible", !item.HasChildNode && !item.Items.Any())
        .AddClass(NodeIcon, item.IsCollapsed)
        .AddClass(ExpandNodeIcon, !item.IsCollapsed)
        .Build();

    /// <summary>
    /// 获得/设置 当前行样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string? GetItemClassString(TreeItem item) => CssBuilder.Default("tree-item")
        .AddClass("active", ActiveItem == item)
        .Build();

    /// <summary>
    /// 获得/设置 TreeNode 样式
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetTreeNodeClassString(TreeItem item) => CssBuilder.Default("tree-ul")
        .AddClass("show", !item.IsCollapsed)
        .Build();

    /// <summary>
    /// 获得/设置 选中节点 默认 null
    /// </summary>
    [Parameter]
    public TreeItem? ActiveItem { get; set; }

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
    /// 获得/设置 Tree Node 节点图标
    /// </summary>
    [Parameter]
    public string? NodeIcon { get; set; }

    /// <summary>
    /// 获得/设置 Tree Node 展开节点图标
    /// </summary>
    [Parameter]
    public string? ExpandNodeIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TreeItem>? Items { get; set; }

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
    public Func<TreeItem, Task>? OnTreeItemClick { get; set; }

    /// <summary>
    /// 获得/设置 树形控件节点选中时回调委托
    /// </summary>
    [Parameter]
    public Func<List<TreeItem>, Task>? OnTreeItemChecked { get; set; }

    /// <summary>
    /// 获得/设置 节点展开前回调委托
    /// </summary>
    [Parameter]
    public Func<TreeItem, Task>? OnExpandNode { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        GroupName = this.GetHashCode().ToString();
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
    /// 选中节点时触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClick(TreeItem item)
    {
        if (!item.IsDisabled)
        {
            ActiveItem = item;
            if (ClickToggleNode)
            {
                await OnExpandRowAsync(item);
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
                var status = item.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
                await OnStateChanged(status, item);
            }
        }
    }

    /// <summary>
    /// 更改节点是否展开方法
    /// </summary>
    /// <param name="item"></param>
    private async Task OnExpandRowAsync(TreeItem item)
    {
        if (IsAccordion)
        {
            foreach (var rootNode in Items.Where(p => !p.IsCollapsed && p != item))
            {
                rootNode.IsCollapsed = true;
            }
        }
        item.IsCollapsed = !item.IsCollapsed;
        if (OnExpandNode != null)
        {
            await OnExpandNode(item);
        }
    }

    /// <summary>
    /// 节点 Checkbox 状态改变时触发此方法
    /// </summary>
    /// <param name="state"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private async Task OnStateChanged(CheckboxState state, TreeItem item)
    {
        // 向下级联操作
        item.CascadeSetCheck(item.Checked);

        if (OnTreeItemChecked != null)
        {
            await OnTreeItemChecked(GetCheckedItems().ToList());
        }
    }

    /// <summary>
    /// 获得 所有选中节点集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TreeItem> GetCheckedItems() => Items.Aggregate(new List<TreeItem>(), (t, item) =>
    {
        t.Add(item);
        t.AddRange(item.GetAllSubItems());
        return t;
    }).Where(i => i.Checked);

    private async Task OnRadioClick(TreeItem item)
    {
        if (!item.IsDisabled)
        {
            if (ActiveItem != null)
            {
                ActiveItem.Checked = false;
            }
            ActiveItem = item;
            ActiveItem.Checked = true;

            // 其他设置为 false
            if (OnTreeItemChecked != null)
            {
                await OnTreeItemChecked(new List<TreeItem> { item });
            }
        }
    }

    private static CheckboxState CheckState(TreeItem item)
    {
        return item.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
    }
}
