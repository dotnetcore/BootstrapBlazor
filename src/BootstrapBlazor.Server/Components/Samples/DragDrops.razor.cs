// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// DragDrops
/// </summary>
public partial class DragDrops
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// OnItemDropRejectedByMaxItemLimit
    /// </summary>
    /// <param name="item"></param>
    private void OnItemDropRejectedByMaxItemLimit(string item)
    {
        Logger.Log($"{item}由于超过最大数量限制被禁止");
    }

    /// <summary>
    /// OnItemDropRejected
    /// </summary>
    /// <param name="item"></param>
    private void OnItemDropRejected(string item)
    {
        Logger.Log($"{item}被拒绝");
    }

    /// <summary>
    /// OnReplacedItemDrop
    /// </summary>
    /// <param name="item"></param>
    private void OnReplacedItemDrop(string item)
    {
        Logger.Log($"新元素放在{item}下");
    }

    /// <summary>
    /// OnItemDrop
    /// </summary>
    /// <param name="item"></param>
    private void OnItemDrop(string item)
    {
        Logger.Log($"{item}被放下");
    }

    [NotNull]
    private List<string>? StrList1 { get; set; }

    [NotNull]
    private List<string>? StrList2 { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        StrList1 =
        [
            "1",
            "2",
            "3",
            "4",
            "5"
        ];
        StrList2 =
        [
            "6",
            "7",
            "8",
            "9",
            "10"
        ];
    }

    /// <summary>
    /// GetMethods
    /// </summary>
    /// <returns></returns>
    private MethodItem[] GetMethods() =>
    [
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
    ];
}
