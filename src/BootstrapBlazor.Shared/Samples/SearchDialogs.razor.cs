// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = "Whether to show labels",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Model",
            Description = "Generic parameters are used for rendering UI",
            Type = "TModel",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Items",
            Description = "Set of search criteria",
            Type = "IEnumerable<IEditorItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DialogBodyTemplate",
            Description = "SearchDialog Body Template",
            Type = "RenderFragment<TModel>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ResetButtonText",
            Description = "Reset button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "reset"
        },
        new AttributeItem() {
            Name = "QueryButtonText",
            Description = "Query button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "Inquire"
        },
        new AttributeItem() {
            Name = "OnResetSearchClick",
            Description = "Rreset callback delegate",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnSearchClick",
            Description = "Search callback delegate",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ItemsPerRow",
            Description = "Displays the number of components per line",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RowType",
            Description = "Set the component layout",
            Type = "RowType",
            ValueList = "Row|Inline",
            DefaultValue = "Row"
        },
        new AttributeItem() {
            Name = "LabelAlign",
            Description = "Inline Label alignment in layout mode",
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        }
    };
}
