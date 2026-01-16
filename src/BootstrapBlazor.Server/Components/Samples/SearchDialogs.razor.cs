// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// SearchDialogs
/// </summary>
public sealed partial class SearchDialogs
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private async Task ShowColumnsDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "Search popup",
            Model = model,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowDialog()
    {
        var option = new SearchDialogOption<Foo>()
        {
            Title = "search popup",
            Model = new Foo(),
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Logger.Log("Close button is clicked");
                return Task.CompletedTask;
            },
            OnResetSearchClick = () =>
            {
                Logger.Log("Reset button is clicked");
                return Task.CompletedTask;
            },
            OnSearchClick = () =>
            {
                Logger.Log("Search button is clicked");
                return Task.CompletedTask;
            }
        };

        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowInlineDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "Search popup",
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            Model = model,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    private async Task ShowInlineAlignDialog()
    {
        var model = new Foo();
        var option = new SearchDialogOption<Foo>()
        {
            Title = "Search popup",
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            LabelAlign = Alignment.Right,
            Model = model,
            Items = Utility.GenerateColumns<Foo>(p => p.GetFieldName() == nameof(Foo.Name) || p.GetFieldName() == nameof(Foo.Address))
        };
        await DialogService.ShowSearchDialog(option);
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
}
