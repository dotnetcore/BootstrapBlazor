// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Tree 组件
/// </summary>
public partial class Tree
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
    private static string? GetCaretClassString(TreeItem item) => CssBuilder.Default("fa fa-caret-right")
        .AddClass("invisible", !item.HasChildNode && !item.Items.Any())
        .AddClass("fa-rotate-90", !item.IsCollapsed)
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

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        GroupName = this.GetHashCode().ToString();
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        //if (ActiveItem != null)
        //{
        //    var item = ActiveItem;
        //    while (item.Parent != null)
        //    {
        //        item.Parent.IsExpanded = true;
        //        item = item.Parent;
        //    }
        //}
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
    private async Task OnClick(TreeItem item)
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
            var checkedItems = Items.Aggregate(new List<TreeItem>(), (t, item) =>
            {
                t.Add(item);
                t.AddRange(item.GetAllSubItems());
                return t;
            });
            await OnTreeItemChecked(checkedItems);
        }
    }

    private async Task OnRadioClick(TreeItem item)
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

    private static CheckboxState CheckState(TreeItem item)
    {
        return item.Checked ? CheckboxState.Checked : CheckboxState.UnChecked;
    }
}
