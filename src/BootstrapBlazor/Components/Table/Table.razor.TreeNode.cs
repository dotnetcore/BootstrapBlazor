// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 是否为树形数据 默认为 false
    /// </summary>
    [Parameter]
    public bool IsTree { get; set; }

    /// <summary>
    /// 获得/设置 生成树状结构回调方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task<IEnumerable<TableTreeNode<TItem>>>>? TreeNodeConverter { get; set; }

    /// <summary>
    /// 获得/设置 树形数据节点展开式回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TableTreeNode<TItem>>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// 获得/设置 树形数据集合
    /// </summary>
    private List<TableTreeNode<TItem>> TreeRows { get; } = new(100);

    /// <summary>
    /// 获得/设置 是否正在加载子项 默认为 false
    /// </summary>
    private bool IsLoadChildren { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 数型结构小箭头图标
    /// </summary>
    [Parameter]
    public string? TreeIcon { get; set; }

    /// <summary>
    /// 获得/设置 数型结构展开小箭头图标
    /// </summary>
    [Parameter]
    public string? TreeExpandIcon { get; set; }

    /// <summary>
    /// 获得/设置 数型结构正在加载图标
    /// </summary>
    [Parameter]
    public string? TreeNodeLoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 缩进大小 默认为 16 单位 px
    /// </summary>
    [Parameter]
    public int IndentSize { get; set; } = 16;

    /// <summary>
    /// 树形数据小箭头缩进
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    protected string? GetTreeStyleString(int degree) => CssBuilder.Default()
        .AddClass($"margin-left: {degree * IndentSize}px;")
        .Build();

    /// <summary>
    /// 树形数据展开小箭头
    /// </summary>
    /// <param name="isExpand"></param>
    /// <returns></returns>
    protected string? GetTreeClassString(bool isExpand) => CssBuilder.Default("is-tree")
        .AddClass(TreeIcon, !IsLoadChildren && !isExpand)
        .AddClass(TreeExpandIcon, !IsLoadChildren && isExpand)
        .AddClass(TreeNodeLoadingIcon, IsLoadChildren)
        .Build();

    /// <summary>
    /// 节点缓存类实例
    /// </summary>
    [NotNull]
    protected ExpandableNodeCache<TableTreeNode<TItem>, TItem>? TreeNodeCache { get; set; }

    /// <summary>
    /// 展开收缩树形数据节点方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Func<Task> ToggleTreeRow(TItem item) => async () =>
    {
        if (!IsLoadChildren)
        {
            if (TreeNodeCache.TryFind(TreeRows, item, out var node))
            {
                // 重建当前节点缓存
                IsLoadChildren = true;
                node.IsExpand = !node.IsExpand;
                await TreeNodeCache.ToggleNodeAsync(node, GetChildrenRowAsync);
                IsLoadChildren = false;

                // 清除缓存
                _rowsCache = null;

                // 更新 UI
                StateHasChanged();
            }
        }
    };

    private async Task<IEnumerable<IExpandableNode<TItem>>> GetChildrenRowAsync(TableTreeNode<TItem> node)
    {
        if (OnTreeExpand == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }

        return await OnTreeExpand(node.Value);
    }
}
