// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public partial class DragDrops
{
    [NotNull]
    private List<string>? StrList1 { get; set; }

    [NotNull]
    private List<string>? StrList2 { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    ///
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        StrList1 = new List<string>()
        {
            "1",
            "2",
            "3",
            "4",
            "5"
        };
        StrList2 = new List<string>()
        {
            "6",
            "7",
            "8",
            "9",
            "10"
        };
    }

    private void OnReplacedItemDrop(string item)
    {
        Trace?.Log($"新元素放在{item}下");
    }

    private void OnItemDrop(string item)
    {
        Trace?.Log($"{item}被放下");
    }

    private void OnItemDropRejected(string item)
    {
        Trace?.Log($"{item}被拒绝");
    }

    private void OnItemDropRejectedByMaxItemLimit(string item)
    {
        Trace?.Log($"{item}由于超过最大数量限制被禁止");
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "MaxItems",
            Description = "最大数量,null为不限制",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = "null"
        },
        new()
        {
            Name = "ChildContent",
            Description = "内容组件",
            Type = "RenderFragment<TItem>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };

    private static IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = nameof(Dropzone<MethodItem>.Accepts),
            Description = "是否运行拖放",
            Parameters = "Func<TItem?, TItem?, bool>",
            ReturnValue = "bool "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.AllowsDrag),
            Description = "节点是否允许被拖拽",
            Parameters = "TItem",
            ReturnValue = "bool"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.CopyItem),
            Description = "复制一个新的 Item 到目标位置",
            Parameters = "TItem, TItem",
            ReturnValue = "TItem"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.ItemWrapperClass),
            Description = "针对 Item 添加特殊的 css class",
            Parameters = "TItem",
            ReturnValue = "string"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDrop),
            Description = "Item 释放时的事件",
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDropRejected),
            Description = "Item 释放被拒绝时的事件",
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnReplacedItemDrop),
            Description = "当 Item 在另一个 Item 上，不是空白处被释放时的事件",
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDropRejectedByMaxItemLimit),
            Description = "Item 因为 Dropzone 内最大数量超限被拒绝时的事件",
            Parameters = " — ",
            ReturnValue = " — "
        }
    };
}
