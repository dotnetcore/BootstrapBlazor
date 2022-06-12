// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;
public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 是否为树形数据 默认为 false
    /// </summary>
    /// <remarks>是否有子项请使用 <seealso cref="HasChildrenColumnName"/> 树形进行设置，此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public bool IsTree { get; set; }
    /// <summary>
    /// 获得/设置 树形数据节点展开式回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TItem>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// 获得/设置 树形数据已展开集合
    /// </summary>
    [NotNull]
    private List<TableTreeNode<TItem>>? TreeRows { get; set; }

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
    public string TreeIcon { get; set; } = "fa fa-fw fa-caret-right";

    /// <summary>
    /// 树形数据小箭头缩进
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? GetTreeStyleString(TItem item) => CssBuilder.Default()
        .AddClass($"margin-left: {GetIndentSize(item)}px;")
        .Build();

    private string GetIndentSize(TItem item)
    {
        // 查找递归层次
        var indent = 0;
        if (TryGetTreeNodeByItem(item, out var node))
        {
            while (node.Parent != null)
            {
                indent += IndentSize;
                node = node.Parent;
            }
        }
        return indent.ToString();
    }

    /// <summary>
    /// 树形数据展开小箭头
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? GetTreeClassString(TItem item) => CssBuilder.Default("is-tree")
        .AddClass(TreeIcon, CheckTreeChildren(item) && !IsLoadChildren)
        .AddClass("fa-rotate-90", IsExpand(item))
        .AddClass("fa-spin fa-spinner", IsLoadChildren)
        .Build();

    private bool IsExpand(TItem item)
    {
        var ret = false;
        if (TryGetTreeNodeByItem(item, out var node))
        {
            ret = node.IsExpand;
        }
        return ret;
    }

    /// <summary>
    /// 展开收缩树形数据节点方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Func<Task> ToggleTreeRow(TItem item) => async () =>
    {
        if (OnTreeExpand == null)
        {
            throw new InvalidOperationException(NotSetOnTreeExpandErrorMessage);
        }

        if (!IsLoadChildren)
        {
            if (TryGetTreeNodeByItem(item, out var node))
            {
                node.IsExpand = !node.IsExpand;

                // 无子项时通过回调方法延时加载
                if (node.Children.Count == 0)
                {
                    IsLoadChildren = true;
                    var nodes = await OnTreeExpand(item);
                    IsLoadChildren = false;

                    node.Children.AddRange(nodes.Select(i => new TableTreeNode<TItem>(i)
                    {
                        HasChildren = CheckTreeChildren(i),
                        Parent = node
                    }));
                }
                StateHasChanged();
            }
        }
    };

    private bool TryGetTreeNodeByItem(TItem item, [MaybeNullWhen(false)] out TableTreeNode<TItem> node)
    {
        TableTreeNode<TItem>? n = null;
        foreach (var v in TreeRows)
        {
            if (v.Value == item)
            {
                n = v;
                break;
            }

            if (v.Children != null)
            {
                n = GetTreeNodeByItem(item, v.Children);
            }

            if (n != null)
            {
                break;
            }
        }
        node = n;
        return n != null;
    }

    private TableTreeNode<TItem>? GetTreeNodeByItem(TItem item, IEnumerable<TableTreeNode<TItem>> nodes)
    {
        TableTreeNode<TItem>? ret = null;
        foreach (var node in nodes)
        {
            if (node.Value == item)
            {
                ret = node;
                break;
            }

            if (node.Children.Any())
            {
                ret = GetTreeNodeByItem(item, node.Children);
            }

            if (ret != null)
            {
                break;
            }
        }
        return ret;
    }

    /// <summary>
    /// 通过设置的 HasChildren 属性得知是否有子节点用于显示 UI
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool CheckTreeChildren(TItem item)
    {
        var ret = false;
        if (HasChildrenCallback != null)
        {
            ret = HasChildrenCallback(item);
        }
        else
        {
            var v = Utility.GetPropertyValue<TItem, object?>(item, HasChildrenColumnName);
            if (v is bool b)
            {
                ret = b;
            }
        }
        return ret;
    }

    private List<TItem> GetTreeRows()
    {
        var ret = new List<TItem>();
        ReloadTreeNodes(ret, TreeRows);
        return ret;
    }

    private void ReloadTreeNodes(List<TItem> items, IEnumerable<TableTreeNode<TItem>> nodes)
    {
        foreach (var node in nodes)
        {
            items.Add(node.Value);

            if (node.IsExpand && node.Children.Any())
            {
                ReloadTreeNodes(items, node.Children);
            }
        }
    }
}
