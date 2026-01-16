// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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
}
