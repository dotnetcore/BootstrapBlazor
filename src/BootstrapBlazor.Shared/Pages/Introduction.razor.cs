// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages;

/// <summary>
/// 
/// </summary>
public partial class Introduction : IAsyncDisposable
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<Introduction>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [NotNull]
    private string[]? LocalizerUrls { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        LocalizerUrls = new string[]
        {
            WebsiteOption.Value.BootstrapAdminLink,
            WebsiteOption.Value.BootstrapAdminLink + "/stargazers",
            WebsiteOption.Value.BootstrapAdminLink + "/badge/star.svg?theme=gvp",
            WebsiteOption.Value.BootstrapAdminLink
        };
    }

    private bool IsRender { get; set; }

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
            IsRender = true;
            await JSRuntime.InvokeVoidAsync("$.bb_open");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (IsRender)
        {
            await JSRuntime.InvokeVoidAsync("$.bb_open", "dispose");
        }
        GC.SuppressFinalize(this);
    }
}
