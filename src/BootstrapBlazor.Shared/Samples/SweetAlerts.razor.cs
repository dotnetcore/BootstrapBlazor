// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class SweetAlerts
{
    private Task OnSwal(SwalCategory cate) => SwalService.Show(new SwalOption()
    {
        Category = cate,
        Title = "Sweet"
    });

    private Task ShowTitle() => SwalService.Show(new SwalOption()
    {
        Category = SwalCategory.Success,
        Title = "Title"
    });

    private Task ShowContent() => SwalService.Show(new SwalOption()
    {
        Category = SwalCategory.Success,
        Content = "Content"
    });

    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    private async Task ShowButtons()
    {
        var op = new SwalOption()
        {
            Category = SwalCategory.Success,
            Title = "Title",
            Content = "Content",
            ShowClose = false
        };
        op.ButtonTemplate = new RenderFragment(builder =>
        {
            builder.OpenComponent<Button>(0);
            builder.AddAttribute(1, nameof(Button.Text), "custom close button");
            builder.AddAttribute(2, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, async () => await op.Close()));
            builder.CloseComponent();
        });
        await SwalService.Show(op);
    }

    private async Task ShowComponent()
    {
        var op = new SwalOption()
        {
            BodyTemplate = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "text-center");
                builder.OpenComponent<Counter>(2);
                builder.CloseComponent();
                builder.CloseElement();
            })
        };
        await SwalService.Show(op);
    }

    private async Task ShowFooterComponent()
    {
        var op = new SwalOption()
        {
            Category = SwalCategory.Error,
            Title = "Oops...",
            Content = "Something went wrong!",
            ShowFooter = true,
            FooterTemplate = BootstrapDynamicComponent.CreateComponent<SwalFooter>().Render()
        };
        await SwalService.Show(op);
    }

    private async Task ShowAutoCloseSwal()
    {
        var op = new SwalOption()
        {
            Category = SwalCategory.Error,
            Title = "Oops...",
            Content = "Something went wrong!",
            ShowFooter = true,
            IsAutoHide = true,
            Delay = 4000,
            FooterTemplate = BootstrapDynamicComponent.CreateComponent<SwalFooter>().Render()
        };
        await SwalService.Show(op);
    }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private async Task ShowModal()
    {
        var op = new SwalOption()
        {
            Title = "模态对话框示例",
            Content = "模态对话框内容，不同按钮返回不同值"
        };
        var ret = await SwalService.ShowModal(op);

        Trace.Log($"模态弹窗返回值为：{ret}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Category",
            Description = "popover type",
            Type = "SwalCategory",
            ValueList = "Success/Error/Information/Warning/Question",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popup title",
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Cotent",
            Description = "popup content",
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Delay",
            Description = "Auto hide interval",
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new AttributeItem() {
            Name = "IsAutoHide",
            Description = "Whether to automatically hide",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowClose",
            Description = "Whether to show the close button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "Whether to show the footer template",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsConfirm",
            Description = "Whether it is confirmation popup mode",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = "pop-up window",
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = "Modal body display component",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = "Modal body footer component",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ButtonTemplate",
            Description = "Modal button template",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
