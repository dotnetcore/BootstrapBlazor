// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 弹窗组件示例代码
/// </summary>
public sealed partial class Dialogs
{
    private bool IsKeyboard { get; set; }

    private Task KeyboardOnClick() => DialogService.Show(new DialogOption()
    {
        IsKeyboard = IsKeyboard,
        Title = "I am the popup created by the service",
        BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.ChildContent)] = new RenderFragment(builder => builder.AddContent(0, "Button"))
        }).Render()
    });

    private void OnClickKeyboard()
    {
        IsKeyboard = !IsKeyboard;
    }

    private Task OnResizeDialogClick() => DialogService.Show(new DialogOption()
    {
        Title = "Resize dialog",
        ShowResize = true,
        BodyTemplate = builder => builder.AddContent(0, "I am content")
    });

    private DialogBodyFoo? BodyFooComponent { get; set; }

    private async Task TriggerUpdateBodyAsync(string val)
    {
        if (BodyFooComponent != null)
        {
            await BodyFooComponent.UpdateAsync(val);
        }
    }

    private Task OnCustomerHeaderClick() => DialogService.Show(new DialogOption()
    {
        HeaderTemplate = BootstrapDynamicComponent.CreateComponent<DialogHeaderFoo>(new Dictionary<string, object?>
        {
            [nameof(DialogHeaderFoo.OnValueChanged)] = new Func<string, Task>(val => TriggerUpdateBodyAsync(val))
        }).Render(),
        BodyTemplate = builder =>
        {
            builder.OpenComponent<DialogBodyFoo>(0);
            builder.AddComponentReferenceCapture(1, obj => BodyFooComponent = (DialogBodyFoo)obj);
            builder.CloseComponent();
        },
    });

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnCustomerHeaderToolbarClick() => DialogService.Show(new DialogOption()
    {
        Title = Localizer["HeaderToolbarTemplateDialogTitle"],
        HeaderToolbarTemplate = builder =>
        {
            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.Icon), "fa-solid fa-print");
            builder.AddAttribute(1, nameof(Button.OnClickWithoutRender), () => ToastService.Success(Localizer["HeaderToolbarTemplateDialogTitle"], Localizer["HeaderToolbarTemplateToastContent"]));
            builder.CloseComponent();
        }
    });

    private Task ComponentOnClick() => DialogService.Show(new DialogOption()
    {
        Title = "Built-in Counter component",
        Component = BootstrapDynamicComponent.CreateComponent<Counter>()
    });

    private Task BodyContextOnClick() => DialogService.Show(new DialogOption()
    {
        Title = "Built-in Counter component",
        BodyContext = "I'm a passer",
        BodyTemplate = builder =>
        {
            builder.OpenComponent<DemoComponent>(0);
            builder.CloseComponent();
        }
    });

    private int ApplyDataPrimaryId { get; set; } = 1;

    private async Task OnClickShowDataById()
    {
        var op = new DialogOption
        {
            Title = "Data query window",
            ShowFooter = false,
            BodyContext = ApplyDataPrimaryId,
            Component = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>()
        };

        await DialogService.Show(op);
    }

    private async Task CloseButtonShow()
    {
        var option = new DialogOption()
        {
            Title = "Close the popup with DialogCloseButton",
            Component = BootstrapDynamicComponent.CreateComponent<DialogCloseButton>()
        };
        await DialogService.Show(option);
    }

    private async Task ShowNoHeaderCloseButtonDialog()
    {
        var option = new DialogOption
        {
            Title = "Header no close button",
            ShowHeaderCloseButton = false,
            Component = BootstrapDynamicComponent.CreateComponent<DialogCloseButton>(new Dictionary<string, object?>
            {
                [nameof(Button.Text)] = "Click to close the popup"
            })
        };

        await DialogService.Show(option);
    }

    private async Task ShowDialogLoop()
    {
        await DialogService.Show(new DialogOption()
        {
            Title = $"Multiple Pop-up",
            IsDraggable = true,
            IsKeyboard = true,
            IsBackdrop = true,
            Component = BootstrapDynamicComponent.CreateComponent<DialogDemo>()
        });
    }

    [NotNull]
    private ConsoleLogger? ModalDialogLogger { get; set; }

    private int DemoValue1 { get; set; } = 1;

    private async Task OnResultDialogClick()
    {
        var result = await DialogService.ShowModal<ResultDialogDemo>(new ResultDialogOption()
        {
            Title = "Modal popup with return value",
            ComponentParameters = new Dictionary<string, object>
            {
                [nameof(ResultDialogDemo.Value)] = DemoValue1,
                [nameof(ResultDialogDemo.ValueChanged)] = EventCallback.Factory.Create<int>(this, v => DemoValue1 = v)
            }
        });

        ModalDialogLogger.Log($"The return value of the popup window is: {result} The return value of the component is: {DemoValue1}");
    }

    private async Task OnConfirmModalClick()
    {
        var result = await DialogService.ShowModal(Localizer["ConfirmDialogModalTitle"], Localizer["ConfirmDialogModalContent"]);

        ModalDialogLogger.Log($"The return value of the popup window is: {result} no component provider");
    }

    private async Task OnEditDialogClick()
    {
        var option = new EditDialogOption<Foo>()
        {
            Title = "Edit popup",
            Model = new Foo(),
            RowType = RowType.Inline,
            ShowLoading = true,
            ItemsPerRow = 2,
            ItemChangedType = ItemChangedType.Update,
            OnEditAsync = async context =>
            {
                await Task.Delay(2000);
                return false;
            }
        };
        await DialogService.ShowEditDialog(option);
    }

    private async Task OnSearchDialogClick()
    {
        var option = new SearchDialogOption<Foo>()
        {
            Title = "Search popup",
            Model = new Foo(),
            RowType = RowType.Inline,
            ItemsPerRow = 2,
        };
        await DialogService.ShowSearchDialog(option);
    }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    private async Task OnSaveDialogClick()
    {
        var foo = Foo.Generate(LocalizerFoo);
        await DialogService.ShowSaveDialog<DialogSaveDetail>("Save", () =>
        {
            // 此处可以访问 foo 实例进行入库操作等
            return Task.FromResult(true);
        }, parameters =>
        {
            parameters.Add(nameof(DialogSaveDetail.Value), foo);
        });
    }

    private Task OnSizeDialogClick() => DialogService.Show(new DialogOption()
    {
        Title = "full screen window",
        FullScreenSize = FullScreenSize.ExtraLarge,
        BodyTemplate = builder => builder.AddContent(0, "Full screen when screen is less than 1200px")
    });

    private int DataPrimaryId { get; set; } = 1;

    private async Task OnPrintDialogClick()
    {
        var op = new DialogOption
        {
            Title = "Data query window",
            ShowPrintButton = true,
            ShowPrintButtonInHeader = true,
            ShowFooter = false,
            BodyContext = DataPrimaryId,
            Component = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>()
        };

        await DialogService.Show(op);
    }

    private async Task OnExportPdfDialogClick()
    {
        var op = new DialogOption
        {
            Title = "Export Pdf Demo",
            ShowExportPdfButton = true,
            ShowExportPdfButtonInHeader = true,
            ShowFooter = false,
            BodyContext = DataPrimaryId,
            Component = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>()
        };

        await DialogService.Show(op);
    }

    private async Task OnMaximizeDialogClick()
    {
        await DialogService.Show(new DialogOption()
        {
            Title = $"Controllable maximized popup",
            ShowMaximizeButton = true,
            BodyTemplate = builder =>
            {
                builder.AddContent(0, "Click the maximize button in the Header to pop up the window to full screen");
            }
        });
    }

    private Task OnErrorDialog() => DialogService.Show(new DialogOption()
    {
        Title = "Click the button to report the error test",
        Component = BootstrapDynamicComponent.CreateComponent<ErrorCounter>()
    });

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private IEnumerable<string> Emails { get; set; } = Array.Empty<string>();

    private string? EmailInputValue { get; set; }

    private async Task OnEmailButtonClick()
    {
        var result = await DialogService.ShowModal<ResultDialogDemo2>(new ResultDialogOption()
        {
            Title = Localizer["EmailDialogTitle"],
            BodyContext = new ResultDialogDemo2.FooContext() { Count = 10, Emails = EmailInputValue },
            ButtonYesText = Localizer["EmailDialogButtonYes"],
            ButtonYesIcon = "fa-solid fa-magnifying-glass",
            ComponentParameters = new Dictionary<string, object>
            {
                [nameof(ResultDialogDemo2.Emails)] = Emails,
                [nameof(ResultDialogDemo2.EmailsChanged)] = EventCallback.Factory.Create<IEnumerable<string>>(this, v => Emails = v)
            }
        });

        if (result == DialogResult.Yes)
        {
            EmailInputValue = string.Join(";", Emails);
        }
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Component",
            Description = "Parameters of the component referenced in the dialog Body",
            Type = "DynamicComponent",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyContext",
            Description = "pop-up window",
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = "Modal body ModalHeader template",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyTemplate",
            Description = "Modal body ModalBody component",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterTemplate",
            Description = "ModalFooter component at the bottom of the modal",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsCentered",
            Description = "Whether to center vertically",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsScrolling",
            Description = "Whether to scroll when the text of the pop-up window is too long",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCloseButton",
            Description = "whether to show the close button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowHeaderCloseButton",
            Description = "Whether to display the close button on the right side of the title bar",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowFooter",
            Description = "whether to display Footer",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(DialogOption.ShowPrintButton),
            Description = "Whether to show the print button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(DialogOption.ShowPrintButtonInHeader),
            Description = "Whether the print button is displayed in the Header",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Size",
            Description = "Size of dialog",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "Large"
        },
        new()
        {
            Name = nameof(DialogOption.FullScreenSize),
            Description = "Full screen when smaller than a certain size",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "Title",
            Description = "Popup title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " not set "
        },
        new()
        {
            Name = nameof(DialogOption.PrintButtonText),
            Description = "print button display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "The value set in the resource file"
        },
        new()
        {
            Name = nameof(DialogOption.ShowMaximizeButton),
            Description = "Whether to show the maximize button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
