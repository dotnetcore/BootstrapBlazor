﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得/设置 树形数据模式子项字段是否有子节点属性名称 默认为 HasChildren 无法提供时请设置 <see cref="HasChildrenCallback"/> 回调方法
    /// </summary>
    /// <remarks>此参数在 <see cref="IsExcel"/> 模式下不生效</remarks>
    [Parameter]
    public string HasChildrenColumnName { get; set; } = "HasChildren";

    /// <summary>
    /// 获得/设置 是否有子节点回调方法 默认为 null 用于未提供 <see cref="HasChildrenColumnName"/> 列名时使用
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? HasChildrenCallback { get; set; }

    /// <summary>
    /// 获得/设置 树形数据节点展开式回调委托方法
    /// </summary>
    [Parameter]
    public Func<TItem, Task<IEnumerable<TItem>>>? OnTreeExpand { get; set; }

    /// <summary>
    /// 获得/设置 树形数据已展开集合
    /// </summary>
    [NotNull]
    private IEnumerable<ITableTreeItem<TItem>>? TreeRows { get; set; }

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
    protected string? GetTreeStyleString(int degree)
    {
        return CssBuilder.Default()
        .AddClass($"margin-left: {degree * IndentSize}px;")
        .Build();
    }

    /// <summary>
    /// 树形数据展开小箭头
    /// </summary>
    /// <param name="isExpand"></param>
    /// <returns></returns>
    protected string? GetTreeClassString(bool isExpand)
    {
        return CssBuilder.Default("is-tree fa fa-fw ")
        .AddClass(TreeIcon, !IsLoadChildren)
        .AddClass("fa-rotate-90", !IsLoadChildren && isExpand)
        .AddClass("fa-spin fa-spinner", IsLoadChildren)
        .Build();
    }

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
                    if (OnTreeExpand != null)
                    {
                        node.SetChildren(await OnTreeExpand(item));
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
}
