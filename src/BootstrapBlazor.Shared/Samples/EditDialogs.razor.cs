// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class EditDialogs
{
    [Inject]
    [NotNull]
    private IStringLocalizer<EditDialogs>? Localizer { get; set; }

    private Foo Model { get; set; } = new Foo()
    {
        Name = "Name 1234",
        Address = "Address 1234"
    };

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private async Task ShowDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(FooLocalizer);

        var option = new EditDialogOption<Foo>()
        {
            Title = "edit dialog",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Trace.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    private async Task ShowAlignDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(FooLocalizer);

        var option = new EditDialogOption<Foo>()
        {
            Title = "edit dialog",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            LabelAlign = Alignment.Right,
            OnCloseAsync = () =>
            {
                Trace.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    private async Task ShowEditDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbys(FooLocalizer);

        item = items.First(i => i.GetFieldName() == nameof(Foo.Address));
        item.Editable = false;
        item = items.First(i => i.GetFieldName() == nameof(Foo.Count));
        item.Editable = false;

        var option = new EditDialogOption<Foo>()
        {
            Title = "edit dialog",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                Trace.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                Trace.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    /// <summary>
    /// get property method
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
                Description = "Generic parameters are used to render the UI",
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "Edit item collection",
                Type = "IEnumerable<IEditorItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DialogBodyTemplate",
                Description = "EditDialog Body template",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CloseButtonText",
                Description = "Close button text",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "closure"
            },
            new AttributeItem() {
                Name = "SaveButtonText",
                Description = "Save button text",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "save"
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "Save callback delegate",
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
