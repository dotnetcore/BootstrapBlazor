// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Locator
{
    [Inject]
    [NotNull]
    private WebClientService? ClientService { get; set; }

    [Inject]
    [NotNull]
    private IIPLocatorProvider? IPLocator { get; set; }

    private string? Ip { get; set; }

    private string? Location { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
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
            Location = await IPLocator.Locate(Ip);
        }
    }
}
