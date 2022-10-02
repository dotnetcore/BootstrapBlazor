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
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "MaxItems",
            Description = Localizer["A1"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = "null"
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["A1"],
            Type = "RenderFragment<TItem>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = nameof(Dropzone<MethodItem>.Accepts),
            Description = Localizer["M1"],
            Parameters = "Func<TItem?, TItem?, bool>",
            ReturnValue = "bool "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.AllowsDrag),
            Description = Localizer["M2"],
            Parameters = "TItem",
            ReturnValue = "bool"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.CopyItem),
            Description = Localizer["M3"],
            Parameters = "TItem, TItem",
            ReturnValue = "TItem"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.ItemWrapperClass),
            Description = Localizer["M4"],
            Parameters = "TItem",
            ReturnValue = "string"
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDrop),
            Description = Localizer["M5"],
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDropRejected),
            Description = Localizer["M6"],
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnReplacedItemDrop),
            Description = Localizer["M7"],
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Dropzone<MethodItem>.OnItemDropRejectedByMaxItemLimit),
            Description = Localizer["M8"],
            Parameters = " — ",
            ReturnValue = " — "
        }
    };
}
