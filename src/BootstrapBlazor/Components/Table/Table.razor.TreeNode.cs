// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否为树形数据 默认为 false</para>
    /// <para lang="en">Get/Set Whether it is tree data. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsTree { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 生成树状结构回调方法</para>
    /// <para lang="en">Get/Set Callback for generating tree structure</para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task<IEnumerable<TableTreeNode<TItem>>>>? TreeNodeConverter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树形数据节点展开式回调委托方法</para>
    /// <para lang="en">Get/Set Callback delegate for expanding tree data node</para>
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TableTreeNode<TItem>>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 树形数据集合</para>
    /// <para lang="en">Get/Set Tree Data Collection</para>
    /// </summary>
    private List<TableTreeNode<TItem>> TreeRows { get; } = new(100);

    /// <summary>
    /// <para lang="zh">获得/设置 是否正在加载子项 默认为 false</para>
    /// <para lang="en">Get/Set Whether loading children. Default false</para>
    /// </summary>
    private bool IsLoadChildren { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数型结构小箭头图标</para>
    /// <para lang="en">Get/Set Tree Node Icon</para>
    /// </summary>
    [Parameter]
    public string? TreeIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数型结构展开小箭头图标</para>
    /// <para lang="en">Get/Set Tree Node Expand Icon</para>
    /// </summary>
    [Parameter]
    public string? TreeExpandIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数型结构正在加载图标</para>
    /// <para lang="en">Get/Set Tree Node Loading Icon</para>
    /// </summary>
    [Parameter]
    public string? TreeNodeLoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 缩进大小 默认为 16 单位 px</para>
    /// <para lang="en">Get/Set Indent Size. Default 16 px</para>
    /// </summary>
    [Parameter]
    public int IndentSize { get; set; } = 16;

    /// <summary>
    /// <para lang="zh">树形数据小箭头缩进</para>
    /// <para lang="en">Tree Node Indent</para>
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    protected string? GetTreeStyleString(int degree) => CssBuilder.Default()
        .AddClass($"margin-left: {degree * IndentSize}px;")
        .Build();

    /// <summary>
    /// <para lang="zh">树形数据展开小箭头</para>
    /// <para lang="en">Tree Node Expand Arrow</para>
    /// </summary>
    /// <param name="isExpand"></param>
    /// <returns></returns>
    protected string? GetTreeClassString(bool isExpand) => CssBuilder.Default("is-tree")
        .AddClass(TreeIcon, !IsLoadChildren && !isExpand)
        .AddClass(TreeExpandIcon, !IsLoadChildren && isExpand)
        .AddClass(TreeNodeLoadingIcon, IsLoadChildren)
        .Build();

    /// <summary>
    /// <para lang="zh">节点缓存类实例</para>
    /// <para lang="en">Node Cache Instance</para>
    /// </summary>
    [NotNull]
    protected ExpandableNodeCache<TableTreeNode<TItem>, TItem>? TreeNodeCache { get; set; }

    /// <summary>
    /// <para lang="zh">展开收缩树形数据节点方法</para>
    /// <para lang="en">Toggle Tree Node Method</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Func<Task> ToggleTreeRow(TItem item) => async () =>
    {
        if (!IsLoadChildren)
        {
            if (TreeNodeCache.TryFind(TreeRows, item, out var node))
            {
                // <para lang="zh">重建当前节点缓存</para>
                // <para lang="en">Rebuild current node cache</para>
                IsLoadChildren = true;
                node.IsExpand = !node.IsExpand;
                await TreeNodeCache.ToggleNodeAsync(node, GetChildrenRowAsync);
                IsLoadChildren = false;

                // <para lang="zh">清除缓存</para>
                // <para lang="en">Clear cache</para>
                _rowsCache = null;

                // <para lang="zh">更新 UI</para>
                // <para lang="en">Update UI</para>
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
