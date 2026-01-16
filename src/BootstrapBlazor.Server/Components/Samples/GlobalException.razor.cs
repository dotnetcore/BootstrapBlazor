// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class GlobalException
{
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    [Inject, NotNull]
    private DialogService? DialogService { get; set; }

    private static void OnClick()
    {
        var a = 0;
        _ = 1 / a;
    }

    private Task OnErrorHandleAsync(ILogger logger, Exception ex) => SwalService.Show(new SwalOption()
    {
        Category = SwalCategory.Error,
        Title = "Oops...",
        Content = ex.Message,
        ShowFooter = true,
        FooterTemplate = BootstrapDynamicComponent.CreateComponent<SwalFooter>().Render()
    });

    private Task OnShowDialog() => DialogService.Show(new DialogOption()
    {
        Title = Localizer["DialogTitle"],
        Component = BootstrapDynamicComponent.CreateComponent<MockError>()
    });

    private Task OnGotoPage()
    {
        NavigationManager.NavigateTo("/error-page");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
}
