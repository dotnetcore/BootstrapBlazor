// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// SweetAlerts
/// </summary>
public partial class SweetAlerts
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

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

    private async Task ShowModal()
    {
        var op = new SwalOption()
        {
            Title = Localizer["SwalOptionTitle"],
            Content = Localizer["SwalOptionContent"]
        };
        var ret = await SwalService.ShowModal(op);

        Logger.Log($"{Localizer["SwalConsoleInfo"]}：{ret}");
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

    private async Task ShowCloseSwal()
    {
        var op = new SwalOption()
        {
            Category = SwalCategory.Error,
            Title = "Oops...",
            Content = "Something went wrong!",
            ShowClose = false
        };
        await SwalService.Show(op);
        await Task.Delay(4000);
        await op.CloseAsync();
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

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Category",
            Description = Localizer["AttrCategory"],
            Type = "SwalCategory",
            ValueList = "Success/Error/Information/Warning/Question",
            DefaultValue = "Success"
        },
        new()
        {
            Name = "Title",
            Description = Localizer["AttrTitle"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new()
        {
            Name = "Content",
            Description = Localizer["AttrContent"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new()
        {
            Name = "Delay",
            Description = Localizer["AttrDelay"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new()
        {
            Name = "IsAutoHide",
            Description = Localizer["AttrAutoHide"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowClose",
            Description = Localizer["AttrShowClose"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowFooter",
            Description = Localizer["AttrShowFooter"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsConfirm",
            Description = Localizer["AttrIsConfirm"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "BodyContext",
            Description = Localizer["AttrBodyContext"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyTemplate",
            Description = Localizer["AttrBodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterTemplate",
            Description = Localizer["AttrFooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ButtonTemplate",
            Description = Localizer["AttrButtonTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
