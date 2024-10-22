// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 拖拽服务
/// </summary>
/// <typeparam name="T"></typeparam>
internal class DragDropService<T>
{
    /// <summary>
    /// 活动的Item
    /// </summary>
    public T? ActiveItem { get; set; }

    /// <summary>
    /// 悬停的项目
    /// </summary>
    public T? DragTargetItem { get; set; }

    /// <summary>
    /// 被拖拽的Items
    /// </summary>
    public List<T>? Items { get; set; }

    /// <summary>
    /// 活动的Id
    /// </summary>
    public int? ActiveSpacerId { get; set; }

    /// <summary>
    /// 之前的位置
    /// </summary>
    public int? OldIndex { get; set; }

    /// <summary>
    /// 通知刷新
    /// </summary>
    public EventHandler? StateHasChanged { get; set; }

    public void Reset()
    {
        if (OldIndex is >= 0 && Items != null && ActiveItem != null)
        {
            Items.Insert(OldIndex.Value, ActiveItem);
        }
        Commit();
    }

    public void Commit()
    {
        ActiveItem = default;
        ActiveSpacerId = null;
        Items = null;
        DragTargetItem = default;

        if (StateHasChanged != null)
        {
            StateHasChanged(this, EventArgs.Empty);
        }
    }
}
