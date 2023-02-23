// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        var op = new SwalOption
        {
            Category = SwalCategory.Success,
            Title = "Title",
            Content = "Content",
            ShowClose = false,
            ButtonTemplate = new RenderFragment(builder =>
            {
                builder.OpenComponent<DialogCloseButton>(0);
                builder.AddAttribute(1, nameof(Button.Text), "Close");
                builder.CloseComponent();
            })
        };
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
            Title = Localizer["SwalOptionTitle"],
            Content = Localizer["SwalOptionContent"]
        };
        var ret = await SwalService.ShowModal(op);

        Trace.Log($"{Localizer["SwalConsoleInfo"]}：{ret}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Category",
            Description = Localizer["AttrCategory"],
            Type = "SwalCategory",
            ValueList = "Success/Error/Information/Warning/Question",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = "Title",
            Description = Localizer["AttrTitle"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Cotent",
            Description = Localizer["AttrContent"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Delay",
            Description = Localizer["AttrDelay"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new AttributeItem() {
            Name = "IsAutoHide",
            Description = Localizer["AttrAutoHide"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowClose",
            Description = Localizer["AttrShowClose"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = Localizer["AttrShowFooter"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsConfirm",
            Description = Localizer["AttrIsConfirm"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = Localizer["AttrBodyContext"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = Localizer["AttrBodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = Localizer["AttrFooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ButtonTemplate",
            Description = Localizer["AttrButtonTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
