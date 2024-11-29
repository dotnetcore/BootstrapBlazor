﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

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
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = "Whether to show labels",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Model",
            Description = "Generic parameters are used for rendering UI",
            Type = "TModel",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Items",
            Description = "Set of search criteria",
            Type = "IEnumerable<IEditorItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DialogBodyTemplate",
            Description = "SearchDialog Body Template",
            Type = "RenderFragment<TModel>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ResetButtonText",
            Description = "Reset button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "reset"
        },
        new()
        {
            Name = "QueryButtonText",
            Description = "Query button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "Inquire"
        },
        new()
        {
            Name = "OnResetSearchClick",
            Description = "Reset callback delegate",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnSearchClick",
            Description = "Search callback delegate",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemsPerRow",
            Description = "Displays the number of components per line",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "RowType",
            Description = "Set the component layout",
            Type = "RowType",
            ValueList = "Row|Inline",
            DefaultValue = "Row"
        },
        new()
        {
            Name = "LabelAlign",
            Description = "Inline Label alignment in layout mode",
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        }
    ];
}
