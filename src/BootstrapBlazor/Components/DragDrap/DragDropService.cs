// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">拖拽服务</para>
/// <para lang="en">Drag Drop Service</para>
/// </summary>
/// <typeparam name="T"></typeparam>
internal class DragDropService<T>
{
    /// <summary>
    /// <para lang="zh">活动的Item</para>
    /// <para lang="en">Active Item</para>
    /// </summary>
    public T? ActiveItem { get; set; }

    /// <summary>
    /// <para lang="zh">悬停的项目</para>
    /// <para lang="en">Drag Target Item</para>
    /// </summary>
    public T? DragTargetItem { get; set; }

    /// <summary>
    /// <para lang="zh">被拖拽的Items</para>
    /// <para lang="en">Dragged Items</para>
    /// </summary>
    public List<T>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">活动的Id</para>
    /// <para lang="en">Active Id</para>
    /// </summary>
    public int? ActiveSpacerId { get; set; }

    /// <summary>
    /// <para lang="zh">之前的位置</para>
    /// <para lang="en">Previous Index</para>
    /// </summary>
    public int? OldIndex { get; set; }

    /// <summary>
    /// <para lang="zh">通知刷新</para>
    /// <para lang="en">Notify StateChanged</para>
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
