﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public Func<IEnumerable<TItem>, Task<IEnumerable<TableTreeNode<TItem>>>>? OnBuildTreeAsync { get; set; }

    /// <summary>
    /// 获得/设置 树形数据节点展开式回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TableTreeNode<TItem>>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// 获得/设置 树形数据集合
    /// </summary>
    [NotNull]
    private List<TableTreeNode<TItem>> TreeRows { get; } = new List<TableTreeNode<TItem>>();

    /// <summary>
    /// 获得/设置 是否正在加载子项 默认为 false
    /// </summary>
    private bool IsLoadChildren { get; set; }

    [NotNull]
    private string? NotSetOnTreeExpandErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 数型结构小箭头图标 默认 fa fa-caret-right
    /// </summary>
    [Parameter]
    public string TreeIcon { get; set; } = "fa-caret-right";

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
    protected string? GetTreeClassString(bool isExpand) => CssBuilder.Default("is-tree fa fa-fw")
        .AddClass(TreeIcon, !IsLoadChildren)
        .AddClass("fa-rotate-90", !IsLoadChildren && isExpand)
        .AddClass("fa-spin fa-spinner", IsLoadChildren)
        .Build();

    /// <summary>
    /// 展开收缩树形数据节点方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Func<Task> ToggleTreeRow(TItem item) => async () =>
    {
        if (!IsLoadChildren)
        {
            if (TreeRows.TryFind(item, out var node, IsEqualItems))
            {
                IsLoadChildren = true;
                // 无子项时通过回调方法延时加载
                if (!node.IsExpand)
                {
                    node.IsExpand = true;
                    if (!node.Items.Any() && OnTreeExpand != null)
                    {
                        node.Items = await OnTreeExpand(item);
                    }
                }
                else
                {
                    node.IsExpand = false;
                }
                IsLoadChildren = false;
                StateHasChanged();
            }
        }
    };
}
