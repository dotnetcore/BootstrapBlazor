// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table Tree 资料结构接口
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <remarks>
/// <code>接口实作方式:<br/> class <typeparamref name="TItem"/> : <see cref="ITableTreeItem{TItem}"/> </code>
/// </remarks>
internal interface ITableTreeItem<TItem>
{
    /// <summary>
    /// 实例化接口
    /// </summary>
    /// <typeparam name="TNew"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ITableTreeItem<TNew> New<TNew>(TNew item) where TNew : class
    {
        if (item is ITableTreeItem<TNew> ret)
        {
            return ret;
        }
        else
        {
            return new TableTreeNode<TNew>(item);
        }
    }

    /// <summary>
    /// 获得 子节点集合
    /// </summary>
    public IEnumerable<ITableTreeItem<TItem>>? Children { get; }

    /// <summary>
    /// 设置 子节点集合
    /// </summary>
    public void SetChildren(IEnumerable<TItem> items);

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }
}
