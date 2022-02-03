// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Pages;

/// <summary>
/// 
/// </summary>
public sealed partial class Index
{
    private ElementReference TypeElement { get; set; }

    private string? BodyClassString => CssBuilder.Default(Localizer["BodyClassString"])
        .Build();

    [Inject]
    private IJSRuntime? JSRuntime { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Index>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? Options { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && JSRuntime != null)
        {
            await JSRuntime.InvokeVoidAsync("$.indexTyper", TypeElement, Localizer["DynamicText"].Value.ToCharArray(), Localizer["DynamicText1"].Value.ToCharArray(), Localizer["DynamicText2"].Value.ToCharArray());
        }
    }
}
