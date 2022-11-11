// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 弹窗组件示例代码
/// </summary>
public sealed partial class Dialogs
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// 获得 弹窗注入服务
    /// </summary>
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    /// <summary>
    /// 获得 Toast注入服务
    /// </summary>
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Dialogs>? Localizer { get; set; }

    private bool IsKeyboard { get; set; }

    private void OnClickKeyboard()
    {
        IsKeyboard = !IsKeyboard;
    }

    private async Task TriggerUpdateBodyAsync(string val)
    {
        if (BodyFooComponent != null)
        {
            await BodyFooComponent.UpdateAsync(val);
        }
    }

    private DialogBodyFoo? BodyFooComponent { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Task OnClick() => DialogService.Show(new DialogOption()
    {
        IsKeyboard = IsKeyboard,
        Title = "I am the popup created by the service",
        BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.ChildContent)] = new RenderFragment(builder => builder.AddContent(0, "我是服务创建的按钮"))
        })
        .Render()
    });

    private async Task Show()
    {
        var option = new DialogOption()
        {
            Title = "Close the popup with code",
        };
        option.BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.Text)] = "Click to close the popup",
            [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await option.Dialog.Close())
        }).Render();
        await DialogService.Show(option);
    }

    private async Task ShowNoHeaderCloseButtonDialog()
    {
        var option = new DialogOption()
        {
            Title = "Header no close button",
            ShowHeaderCloseButton = false
        };
        option.BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.Text)] = "Click to close the popup",
            [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await option.Dialog.Close())
        }).Render();
        await DialogService.Show(option);
    }

    private Task OnClickCounter() => DialogService.Show(new DialogOption()
    {
        Title = "Built-in Counter component",
        Component = BootstrapDynamicComponent.CreateComponent<Counter>()
    });

    private Task OnErrorDialog() => DialogService.Show(new DialogOption()
    {
        Title = "Click the button to report the error test",
        Component = BootstrapDynamicComponent.CreateComponent<ErrorCounter>()
    });

    private async Task OnShownCallbackDialog()
    {
        var option = new DialogOption()
        {
            Title = "点击按钮报错测试"
        };
        option.Component = BootstrapDynamicComponent.CreateComponent<ShownCallbackDummy>(new Dictionary<string, object?>()
        {
            // ShownTodo 方法时组件 ShownCallbackDummy 已经渲染完毕后组件内部调用
            // 此回调中给 Option 实例的 ShownCallbackAsync 回调委托赋值
            // Modal 组件 ShownCallbackAsync 触发后调用 Option 实例的 ShownCallbackAsync
            [nameof(ShownCallbackDummy.ShownTodo)] = new Action<Func<Task>>(cb =>
            {
                option.ShownCallbackAsync = async () =>
                {
                    await cb();
                };
            })
        });
        await DialogService.Show(option);
    }

    private Task OnClickParameter() => DialogService.Show(new DialogOption()
    {
        Title = "Built-in Counter component",
        BodyContext = "I'm a passer",
        BodyTemplate = builder =>
        {
            var index = 0;
            builder.OpenComponent<DemoComponent>(index++);
            builder.CloseComponent();
        }
    });

    private int DataPrimaryId { get; set; } = 1;

    private async Task OnClickShowDataById()
    {
        var op = new DialogOption()
        {
            Title = "Data query window",
            ShowFooter = false,
            BodyContext = DataPrimaryId
        };
        op.BodyTemplate = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>(new Dictionary<string, object?>
        {
            [nameof(DataDialogComponent.OnClose)] = new Action(async () => await op.Dialog.Close())
        }).Render();

        await DialogService.Show(op);
    }

    private int DemoValue1 { get; set; } = 1;
    private async Task OnResultDialogClick()
    {
        var result = await DialogService.ShowModal<ResultDialogDemo>(new ResultDialogOption()
        {
            Title = "Modal popup with return value",
            ComponentParamters = new Dictionary<string, object>
            {
                [nameof(ResultDialogDemo.Value)] = DemoValue1,
                [nameof(ResultDialogDemo.ValueChanged)] = EventCallback.Factory.Create<int>(this, v => DemoValue1 = v)
            }
        });

        Trace.Log($"The return value of the popup window is: {result} The return value of the component is: {DemoValue1}");
    }

    private Task OnSizeDialogClick() => DialogService.Show(new DialogOption()
    {
        Title = "full screen window",
        FullScreenSize = FullScreenSize.ExtraLarge,
        BodyTemplate = builder => builder.AddContent(0, "Full screen when screen is less than 1200px")
    });

    private async Task OnPrintDialogClick()
    {
        var op = new DialogOption()
        {
            Title = "Data query window",
            ShowPrintButton = true,
            ShowPrintButtonInHeader = true,
            ShowFooter = false,
            BodyContext = DataPrimaryId
        };
        op.BodyTemplate = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>(new Dictionary<string, object?>
        {
            [nameof(DataDialogComponent.OnClose)] = new Action(async () => await op.Dialog.Close())
        }).Render();

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

    private string? InputValue { get; set; }

    private IEnumerable<string> Emails { get; set; } = Array.Empty<string>();

    private async Task OnEmailButtonClick()
    {
        var result = await DialogService.ShowModal<ResultDialogDemo2>(new ResultDialogOption()
        {
            Title = Localizer["EmailDialogTitle"],
            BodyContext = new ResultDialogDemo2.FooContext() { Count = 10, Emails = InputValue },
            ButtonYesText = Localizer["EmailDialogButtonYes"],
            ButtonYesIcon = "fa-solid fa-magnifying-glass",
            ComponentParamters = new Dictionary<string, object>
            {
                [nameof(ResultDialogDemo2.Emails)] = Emails,
                [nameof(ResultDialogDemo2.EmailsChanged)] = EventCallback.Factory.Create<IEnumerable<string>>(this, v => Emails = v)
            }
        });

        if (result == DialogResult.Yes)
        {
            InputValue = string.Join(";", Emails);
        }
    }

    private async Task ShowDialogLoop()
    {
        await DialogService.Show(new DialogOption()
        {
            Title = $"弹窗 {DateTime.Now}",
            Component = BootstrapDynamicComponent.CreateComponent<DialogDemo>()
        });
    }

    private async Task OnEditDialogClick()
    {
        var option = new EditDialogOption<Foo>()
        {
            Title = "Edit popup",
            Model = new Foo(),
            RowType = RowType.Inline,
            ItemsPerRow = 2,
            ItemChangedType = ItemChangedType.Update
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

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Component",
            Description = "Parameters of the component referenced in the dialog Body",
            Type = "DynamicComponent",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = "pop-up window",
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "HeaderTemplate",
            Description = "Modal body ModalHeader template",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = "Modal body ModalBody component",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = "ModalFooter component at the bottom of the modal",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsCentered",
            Description = "Whether to center vertically",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsScrolling",
            Description = "Whether to scroll when the text of the pop-up window is too long",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCloseButton",
            Description = "whether to show the close button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowHeaderCloseButton",
            Description = "Whether to display the close button on the right side of the title bar",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "whether to display Footer",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButton),
            Description = "Whether to show the print button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButtonInHeader),
            Description = "Whether the print button is displayed in the Header",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Size",
            Description = "尺寸",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "Large"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.FullScreenSize),
            Description = "Full screen when smaller than a certain size",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popup title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " not set "
        },
        new AttributeItem() {
            Name = nameof(DialogOption.PrintButtonText),
            Description = "print button display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "The value set in the resource file"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowMaximizeButton),
            Description = "Whether to show the maximize button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
