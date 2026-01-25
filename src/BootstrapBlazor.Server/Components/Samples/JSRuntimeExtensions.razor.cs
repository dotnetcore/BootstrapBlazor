// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// JSRuntimeExtensions Demo
/// </summary>
public partial class JSRuntimeExtensions : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    [NotNull]
    private JSModule? Module { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Module = await JSRuntime.LoadUtility();
        }
    }

    private const string Url = "https://www.blazor.zone/";

    private async Task OpenUrl_Blank() => await Module.OpenUrl(Url);

    private async Task OpenUrl_Self() => await Module.OpenUrl(Url, "_self");

    private bool? IsMobile { get; set; }

    private async Task GetIsMobile() => IsMobile = await Module.IsMobile();

    private string evalContent = """
        const currentUrl = window.location.href;

        `当前URL: ${currentUrl}`;
        """;

    private string? evalResult { get; set; }

    private async Task RunEval() => evalResult = await Module.Eval<string?>(evalContent);

    private string functionContent = """
        const currentUrl = window.location.href;

        return `当前URL: ${currentUrl}`;
        """;

    private string? functionResult { get; set; }

    private async Task RunFunction() => functionResult = await Module.Function<string?>(functionContent);

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && Module != null)
        {
            await Module.DisposeAsync();
            Module = null;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
