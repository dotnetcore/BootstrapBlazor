// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DragDropService 拖拽服务</para>
/// <para lang="en">DragDropService component</para>
/// </summary>
/// <typeparam name="T"></typeparam>
internal class DragDropService<T>
{
    /// <summary>
    /// <para lang="zh">获得/设置 活动的Item</para>
    /// <para lang="en">Gets or sets Active Item</para>
    /// </summary>
    public T? ActiveItem { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 悬停的项目</para>
    /// <para lang="en">Gets or sets Drag Target Item</para>
    /// </summary>
    public T? DragTargetItem { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 被拖拽的 Items</para>
    /// <para lang="en">Gets or sets Dragged Items</para>
    /// </summary>
    public List<T>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 活动的 Id</para>
    /// <para lang="en">Gets or sets Active Id</para>
    /// </summary>
    public int? ActiveSpacerId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 之前的位置</para>
    /// <para lang="en">Gets or sets Previous Index</para>
    /// </summary>
    public int? OldIndex { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 通知刷新方法</para>
    /// <para lang="en">Gets or sets Notify StateChanged</para>
    /// </summary>
    public EventHandler? StateHasChanged { get; set; }

    /// <summary>
    /// <para lang="zh">重置方法</para>
    /// <para lang="en">Reset method</para>
    /// </summary>
    public void Reset()
    {
        if (OldIndex is >= 0 && Items != null && ActiveItem != null)
        {
            Items.Insert(OldIndex.Value, ActiveItem);
        }
        Commit();
    }

    /// <summary>
    /// <para lang="zh">提交方法</para>
    /// <para lang="en">Commit method</para>
    /// </summary>
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
