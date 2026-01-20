// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
}
