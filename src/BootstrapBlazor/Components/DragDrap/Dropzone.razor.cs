// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// 拖拽容器
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Dropzone<TItem> : IDisposable
{
    /// <summary>
    /// 获取/设置 拖拽列表
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TItem>? Items { get; set; }

    /// <summary>
    /// 获取/设置 最大数量 默认 null 不限制
    /// </summary>
    [Parameter]
    public int? MaxItems { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 每个 Item 的特殊 class
    /// </summary>
    [Parameter]
    public Func<TItem, string>? ItemWrapperClass { get; set; }

    /// <summary>
    /// 获得/设置 复制内容
    /// </summary>
    [Parameter]
    public Func<TItem, TItem>? CopyItem { get; set; }

    /// <summary>
    /// 获得/设置 是否允许拖拽释放
    /// </summary>
    [Parameter]
    public Func<TItem?, TItem?, bool>? Accepts { get; set; }

    /// <summary>
    /// 获得/设置 当拖拽因为数量超限被禁止时调用
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDropRejectedByMaxItemLimit { get; set; }

    /// <summary>
    /// 获得/设置 当拖拽被禁止时调用
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDropRejected { get; set; }

    /// <summary>
    /// 获得/设置 返回被替换的 Item
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnReplacedItemDrop { get; set; }

    /// <summary>
    /// 获得/设置 返回放下的 Item
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDrop { get; set; }

    /// <summary>
    /// 获得/设置 当前节点是否允许被拖拽
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? AllowsDrag { get; set; }

    [Inject]
    [NotNull]
    private DragDropService<TItem>? DragDropService { get; set; }

    private string? ItemClass => CssBuilder.Default()
        .AddClass("bb-dd-inprogess", DragDropService.ActiveItem != null)
        .Build();

    [ExcludeFromCodeCoverage]
    private string GetItemClass(TItem? item)
    {
        // TODO: 后期完善 使用 CssBuilder 实现
        if (item == null)
        {
            return "";
        }
        var builder = new StringBuilder();
        builder.Append("bb-dd-draggable");
        if (ItemWrapperClass != null)
        {
            builder.Append($" {ItemWrapperClass(item)}");
        }

        var activeItem = DragDropService.ActiveItem;
        if (activeItem == null)
        {
            return builder.ToString();
        }

        if (item.Equals(activeItem))
        {
            builder.Append(" no-pointer-events");
        }

        if (!item.Equals(activeItem) && item.Equals(DragDropService.DragTargetItem))
        {
            builder.Append(IsItemAccepted(DragDropService.DragTargetItem)
                ? " bb-dd-dragged-over"
                : " bb-dd-dragged-over-denied");
        }

        if (AllowsDrag != null && !AllowsDrag(item))
        {
            builder.Append(" bb-dd-noselect");
        }

        return builder.ToString();
    }

    private string GetClassesForSpacing(int spacerId)
    {
        var builder = new StringBuilder();
        builder.Append("bb-dd-spacing");
        if (DragDropService.ActiveItem == null)
        {
            return builder.ToString();
        }
        //if active space id and item is from another dropzone -> always create insert space
        if (DragDropService.ActiveSpacerId == spacerId && Items.IndexOf(DragDropService.ActiveItem) == -1)
        {
            builder.Append(" bb-dd-spacing-dragged-over");
        } // else -> check if active space id and that it is an item that needs space
        else if (DragDropService.ActiveSpacerId == spacerId && (spacerId != Items.IndexOf(DragDropService.ActiveItem)) && (spacerId != Items.IndexOf(DragDropService.ActiveItem) + 1))
        {
            builder.Append(" bb-dd-spacing-dragged-over");
        }

        return builder.ToString();
    }

    private string IsItemDragable(TItem? item)
    {
        if (item == null)
        {
            return "false";
        }
        if (AllowsDrag == null)
        {
            return "true";
        }

        return AllowsDrag(item).ToString();
    }

    private bool IsDropAllowed()
    {
        if (!IsValidItem())
        {
            return false;
        }

        var activeItem = DragDropService.ActiveItem;

        if (IsMaxItemLimitReached())
        {
            OnItemDropRejectedByMaxItemLimit.InvokeAsync(activeItem);
            return false;
        }

        if (!IsItemAccepted(activeItem))
        {
            OnItemDropRejected.InvokeAsync(activeItem);
            return false;
        }

        return true;
    }

    private bool IsItemAccepted(TItem? dragTargetItem)
    {
        if (Accepts == null)
        {
            return true;
        }

        return Accepts(DragDropService.ActiveItem, dragTargetItem);
    }

    private bool IsMaxItemLimitReached()
    {
        var activeItem = DragDropService.ActiveItem;
        return (!Items.Contains(activeItem!) && MaxItems.HasValue && MaxItems == Items.Count);
    }

    private bool IsValidItem()
    {
        return DragDropService.ActiveItem != null;
    }

    private void OnDropItemOnSpacing(int newIndex)
    {
        if (!IsDropAllowed())
        {
            DragDropService.Reset();
            return;
        }

        var activeItem = DragDropService.ActiveItem;

        bool sameDropZone = Equals(DragDropService.Items, Items);

        if (CopyItem == null || sameDropZone)
        {
            Items.Insert(newIndex, activeItem!);
            DragDropService.Commit();
        }
        else
        {
            // for the same zone - do not call CopyItem
            Items.Insert(newIndex, CopyItem(activeItem!));
            DragDropService.Reset();
        }

        //Operation is finished
        OnItemDrop.InvokeAsync(activeItem!);
    }

    private void OnDragStart(TItem item)
    {
        DragDropService.OldIndex = Items.IndexOf(item);
        DragDropService.ActiveItem = item;
        DragDropService.Items = Items;
        Items.Remove(item);
        if (DragDropService.OldIndex >= Items.Count)
        {
            Items.Add(default!);
        }
    }

    private void OnDragEnd()
    {
        if (DragDropService.Items != null)
        {
            if (DragDropService.OldIndex.HasValue)
            {
                if (DragDropService.ActiveItem != null)
                {
                    DragDropService.Items.Insert(DragDropService.OldIndex.Value, DragDropService.ActiveItem);
                }
            }
            StateHasChanged();
        }
        Items.Remove(default!);
    }

    private void OnDragEnter(TItem? item)
    {
        if (item == null)
        {
            return;
        }
        var activeItem = DragDropService.ActiveItem;
        if (activeItem == null)
        {
            return;
        }

        if (IsMaxItemLimitReached())
        {
            return;
        }

        if (!IsItemAccepted(item))
        {
            return;
        }

        DragDropService.DragTargetItem = item;

        StateHasChanged();
    }

    private void OnDragLeave()
    {
        DragDropService.DragTargetItem = default;
        StateHasChanged();
    }

    private void OnDrop()
    {
        if (!IsDropAllowed())
        {
            DragDropService.Reset();
            return;
        }

        var activeItem = DragDropService.ActiveItem;

        // 如果没有释放在Item上，则添加到最后
        if (DragDropService.DragTargetItem == null)
        {
            // 当从其他位置拖拽过来的时候
            if (!Equals(DragDropService.Items, Items) && CopyItem != null)
            {
                Items.Insert(Items.Count, CopyItem(activeItem!));
                DragDropService.Reset();
            }
            else
            {
                Items.Insert(Items.Count, activeItem!);
                DragDropService.Commit();
            }
        }
        else
        {
            OnReplacedItemDrop.InvokeAsync(DragDropService.DragTargetItem);
            if (!Equals(DragDropService.Items, Items) && CopyItem != null)
            {
                Swap(DragDropService.DragTargetItem, CopyItem(activeItem!));
                DragDropService.Reset();
            }
            else
            {
                Swap(DragDropService.DragTargetItem, activeItem!);
                DragDropService.Commit();
            }
        }

        StateHasChanged();
        OnItemDrop.InvokeAsync(activeItem);
    }

    private void Swap(TItem draggedOverItem, TItem activeItem)
    {
        var indexDraggedOverItem = Items.IndexOf(draggedOverItem);
        //insert into new zone
        Items.Insert(indexDraggedOverItem + 1, activeItem);
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        DragDropService.StateHasChanged += ForceRender;
    }

    private void ForceRender(object? sender, EventArgs e)
    {
        StateHasChanged();
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DragDropService.StateHasChanged -= ForceRender;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
