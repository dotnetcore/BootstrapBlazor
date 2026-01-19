// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Paginations
/// </summary>
public sealed partial class Paginations
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnPageClick(int pageIndex)
    {
        Logger.Log($"PageIndex: {pageIndex}");
        return Task.CompletedTask;
    }

    private Alignment Alignment { get; set; } = Alignment.Right;

    [NotNull]
    private List<SelectedItem>? AlignmentItems { get; set; }

    [NotNull]
    private List<SelectedItem>? PageItemsSource { get; set; }

    private int PageItems { get; set; } = 2;

    private int PageCount => 200 / PageItems;

    private string PageInfoText => $"每页 {PageItems} 条 共 {PageCount} 页";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AlignmentItems ??=
        [
            new("Left", "Start"),
            new("Center", "Center"),
            new("Right", "End")
        ];

        PageItemsSource =
        [
            new("2", "2条/页"),
            new("4", "4条/页"),
            new("10", "10条/页"),
            new("20", "20条/页")
        ];
    }
}
