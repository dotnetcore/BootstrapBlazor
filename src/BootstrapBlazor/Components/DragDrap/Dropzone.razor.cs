// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">拖拽容器</para>
/// <para lang="en">Drag Drop Container</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class Dropzone<TItem> : IDisposable
{
    /// <summary>
    /// <para lang="zh">获取/设置 拖拽列表</para>
    /// <para lang="en">Get/Set Items to Drag</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public List<TItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获取/设置 最大数量 默认 null 不限制</para>
    /// <para lang="en">Get/Set Max Items. Default is null (unlimited)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? MaxItems { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Get/Set Child Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<TItem>? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 每个 Item 的特殊 class</para>
    /// <para lang="en">Get/Set Item Wrapper Class</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, string>? ItemWrapperClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 复制内容</para>
    /// <para lang="en">Get/Set Copy Item Delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, TItem>? CopyItem { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许拖拽释放</para>
    /// <para lang="en">Get/Set Accepts Delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem?, TItem?, bool>? Accepts { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当拖拽因为数量超限被禁止时调用</para>
    /// <para lang="en">Get/Set Callback for drop rejection by max item limit</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDropRejectedByMaxItemLimit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当拖拽被禁止时调用</para>
    /// <para lang="en">Get/Set Callback for drop rejection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDropRejected { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 返回被替换的 Item</para>
    /// <para lang="en">Get/Set Callback for Replaced Item Drop</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnReplacedItemDrop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 返回放下的 Item</para>
    /// <para lang="en">Get/Set Callback for Item Drop</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TItem> OnItemDrop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前节点是否允许被拖拽</para>
    /// <para lang="en">Get/Set Whether current item allows drag</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<TItem, bool>? AllowsDrag { get; set; }

    [Inject]
    [NotNull]
    private DragDropService<TItem>? DragDropService { get; set; }

    private string? ItemClass => CssBuilder.Default()
        .AddClass("bb-dd-process", DragDropService.ActiveItem != null)
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

    private string IsItemDraggable(TItem? item)
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

                    // 增加这行代码后单元测试有问题，等排查后再决定是否加上
                    // Add this line caused unit test issue, decide later
                    // commit the changes
                    //DragDropService.Commit();
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
        // If not dropped on an Item, add to the end
        if (DragDropService.DragTargetItem == null)
        {
            // 当从其他位置拖拽过来的时候
            // When dragged from another specific location
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
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
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
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose Method</para>
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
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose Method</para>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
