// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// EditDialogs
/// </summary>
public sealed partial class EditDialogs
{
    [NotNull]
    private ConsoleLogger? NoRenderLogger { get; set; }

    private async Task NoRenderShowEditDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbies(FooLocalizer);

        item = items.First(i => i.GetFieldName() == nameof(Foo.Address));
        item.Ignore = true;
        item = items.First(i => i.GetFieldName() == nameof(Foo.Count));
        item.Ignore = true;

        var option = new EditDialogOption<Foo>()
        {
            Title = "edit dialog",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                NoRenderLogger.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                NoRenderLogger.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    private Foo Model { get; set; } = new Foo()
    {
        Name = "Name 1234",
        Address = "Address 1234"
    };

    private async Task NormalShowDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbies(FooLocalizer);

        var option = new EditDialogOption<Foo>()
        {
            Title = "edit dialog",
            Model = Model,
            Items = items,
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            OnCloseAsync = () =>
            {
                NormalLogger.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                NormalLogger.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    private async Task NormalShowAlignDialog()
    {
        var items = Utility.GenerateEditorItems<Foo>();
        var item = items.First(i => i.GetFieldName() == nameof(Foo.Hobby));
        item.Items = Foo.GenerateHobbies(FooLocalizer);

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
                NormalLogger.Log("close button is clicked");
                return Task.CompletedTask;
            },
            OnEditAsync = context =>
            {
                NormalLogger.Log("save button is clicked");
                return Task.FromResult(true);
            }
        };

        await DialogService.ShowEditDialog(option);
    }

    /// <summary>
    /// get property method
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
            Description = "Generic parameters are used to render the UI",
            Type = "TModel",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Items",
            Description = "Edit item collection",
            Type = "IEnumerable<IEditorItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DialogBodyTemplate",
            Description = "EditDialog Body template",
            Type = "RenderFragment<TModel>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "CloseButtonText",
            Description = "Close button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "closure"
        },
        new()
        {
            Name = "SaveButtonText",
            Description = "Save button text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "save"
        },
        new()
        {
            Name = "OnSaveAsync",
            Description = "Save callback delegate",
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
