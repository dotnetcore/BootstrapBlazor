﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Locators
/// </summary>
public partial class Locators
{
    [Inject]
    [NotNull]
    IStringLocalizer<Locators>? Localizer { get; set; }

    [Inject]
    [NotNull]
    WebClientService? ClientService { get; set; }

    [Inject]
    [NotNull]
    IIPLocatorFactory? IPLocatorFactory { get; set; }

    private string? Ip { get; set; }

    private string? Location { get; set; }

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name = "firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            var clientInfo = await ClientService.GetClientInfo();
            Ip = clientInfo.Ip;
            StateHasChanged();
        }
    }

    private async Task OnClick()
    {
        if (!string.IsNullOrEmpty(Ip))
        {
            var provider = IPLocatorFactory.Create();
            Location = await provider.Locate(Ip);
        }
    }
}
