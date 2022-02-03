// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

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

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Task OnClick() => DialogService.Show(new DialogOption()
    {
        IsKeyboard = IsKeyboard,
        Title = "我是服务创建的弹出框",
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
            Title = "利用代码关闭弹出框",
        };
        option.BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.Text)] = "点击关闭弹窗",
            [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await option.Dialog.Close())
        }).Render();
        await DialogService.Show(option);
    }

    private async Task ShowNoHeaderCloseButtonDialog()
    {
        var option = new DialogOption()
        {
            Title = "Header 中无关闭按钮",
            ShowHeaderCloseButton = false
        };
        option.BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
        {
            [nameof(Button.Text)] = "点击关闭弹窗",
            [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () => await option.Dialog.Close())
        }).Render();
        await DialogService.Show(option);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Task OnClickCounter() => DialogService.Show(new DialogOption()
    {
        Title = "自带的 Counter 组件",
        Component = BootstrapDynamicComponent.CreateComponent<Counter>()
    });

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Task OnClickParameter() => DialogService.Show(new DialogOption()
    {
        Title = "自带的 Counter 组件",
        BodyContext = "我是传参",
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
            Title = "数据查询窗口",
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
            Title = "带返回值模态弹出框",
            ComponentParamters = new Dictionary<string, object>
            {
                [nameof(ResultDialogDemo.Value)] = DemoValue1,
                [nameof(ResultDialogDemo.ValueChanged)] = EventCallback.Factory.Create<int>(this, v => DemoValue1 = v)
            }
        });

        Trace.Log($"弹窗返回值为: {result} 组件返回值为: {DemoValue1}");
    }

    private Task OnSizeDialogClick() => DialogService.Show(new DialogOption()
    {
        Title = "全屏窗口",
        FullScreenSize = FullScreenSize.ExtraLarge,
        BodyTemplate = builder => builder.AddContent(0, "屏幕小于 1200px 时全屏显示")
    });

    private async Task OnPrintDialogClick()
    {
        var op = new DialogOption()
        {
            Title = "数据查询窗口",
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

    private string? InputValue { get; set; }

    private IEnumerable<string> Emails { get; set; } = Array.Empty<string>();

    private async Task OnEmailButtonClick()
    {
        var result = await DialogService.ShowModal<ResultDialogDemo2>(new ResultDialogOption()
        {
            Title = "选择收件人",
            BodyContext = new ResultDialogDemo2.FooContext() { Count = 10, Emails = InputValue },
            ButtonYesText = "选择",
            ButtonYesIcon = "fa fa-search",
            ComponentParamters = new Dictionary<string, object>
            {
                // 用于初始化已选择的用户邮件
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
            Title = "编辑弹窗",
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
            Title = "搜索弹窗",
            Model = new Foo(),
            RowType = RowType.Inline,
            ItemsPerRow = 2,
        };
        await DialogService.ShowSearchDialog(option);
    }

    private async Task OnSaveDialogClick()
    {
        var foo = Foo.Generate(Localizer);
        await DialogService.ShowSaveDialog<DialogSaveDetail>("保存", () =>
        {
            // 此处可以访问 foo 实例进行入库操作等
            return Task.FromResult(true);
        }, new Dictionary<string, object?>
        {
            ["Value"] = foo
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
            Description = "对话框 Body 中引用的组件的参数",
            Type = "DynamicComponent",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = "弹窗传参",
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "HeaderTemplate",
            Description = "模态主体 ModalHeader 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = "模态主体 ModalBody 组件",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = "模态底部 ModalFooter 组件",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsCentered",
            Description = "是否垂直居中",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsScrolling",
            Description = "是否弹窗正文超长时滚动",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCloseButton",
            Description = "是否显示关闭按钮",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowHeaderCloseButton",
            Description = "是否显示标题栏右侧关闭按钮",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "是否显示 Footer",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButton),
            Description = "是否显示打印按钮",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButtonInHeader),
            Description = "打印按钮是否显示在 Header 中",
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
            Description = "小于特定尺寸时全屏",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "弹窗标题",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " 未设置 "
        },
        new AttributeItem() {
            Name = nameof(DialogOption.PrintButtonText),
            Description = "打印按钮显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "资源文件中设定值"
        }
    };
}
