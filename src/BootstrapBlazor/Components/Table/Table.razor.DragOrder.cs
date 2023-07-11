// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Table<TItem>
{
    /// <summary>
    /// 是否允许拖放标题栏更改栏位顺序，默认为否
    /// </summary>
    [Parameter]
    public bool AllowDragOrder { get; set; }

    /// <summary>
    /// 拖放源项
    /// </summary>
    private ITableColumn? dragItem;

    /// <summary>
    /// 拖放目标项
    /// </summary>
    private ITableColumn? dragOverItem;

    /// <summary>
    /// 拖放效果
    /// </summary>
    private string? dragEffect = "move";

    private string? DraggableString => AllowDragOrder ? "true" : null;

    /// <summary>
    /// 是否接受拖放
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool IsAllowDrop(ITableColumn item)
    {
        var ret = false;
        if (dragItem != null && dragOverItem == item && dragItem != item)
        {
            var visibleColumns = GetVisibleColumns().ToList();
            if (visibleColumns.IndexOf(dragItem) != visibleColumns.IndexOf(item) - 1)
            {
                ret = true;
            }
        }
        dragEffect = ret ? "move" : "none";
        return ret;
    }

    /// <summary>
    /// 拖放开始
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Task OnDragStart(ITableColumn item)
    {
        dragItem = item;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 拖放进入
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Task OnDragEnter(ITableColumn item)
    {
        dragOverItem = item;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 拖放结束
    /// </summary>
    /// <returns></returns>
    protected Task OnDragEnd()
    {
        dragItem = null;
        dragOverItem = null;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 拖放释放至目标
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected Task OnDrop(ITableColumn item)
    {
        if (dragItem != null && dragItem != item && IsAllowDrop(item))
        {
            Columns.Remove(dragItem);
            var dropIndex = Columns.IndexOf(item);
            Columns.Insert(dropIndex, dragItem);
        }

        dragItem = null;
        dragOverItem = null;

        return Task.CompletedTask;
    }
}
