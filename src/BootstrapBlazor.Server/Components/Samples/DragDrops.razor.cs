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

    private void OnItemDropRejectedByMaxItemLimit(string item)
    {
        Logger.Log($"{item}由于超过最大数量限制被禁止");
    }

    private void OnItemDropRejected(string item)
    {
        Logger.Log($"{item}被拒绝");
    }

    private void OnReplacedItemDrop(string item)
    {
        Logger.Log($"新元素放在{item}下");
    }

    private void OnItemDrop(string item)
    {
        Logger.Log($"{item}被放下");
    }

    [NotNull]
    private List<string>? StrList1 { get; set; }

    [NotNull]
    private List<string>? StrList2 { get; set; }

    /// <summary>
    /// <inheritdoc/>
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
}
