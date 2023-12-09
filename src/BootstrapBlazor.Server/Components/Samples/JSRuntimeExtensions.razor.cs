// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

    private bool IsMobile { get; set; }

    private async Task GetIsMobile() => IsMobile = await Module.IsMobile();

    private string evalContent = """
        const currentUrl = window.location.href;

        `当前URL: ${currentUrl}`;
        """;

    private string evalResult { get; set; } = string.Empty;

    private async Task RunEval() => evalResult = await Module.Eval<string>(evalContent);

    private string functionContent = """
        const currentUrl = window.location.href;

        return `当前URL: ${currentUrl}`;
        """;

    private string functionResult { get; set; } = string.Empty;

    private async Task RunFunction() => functionResult = await Module.Function<string>(functionContent);

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "OpenUrl",
            Description = Localizer["OpenUrlAttr"].Value,
            Parameters = " — ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "IsMobile",
            Description = Localizer["IsMobileAttr"].Value,
            Parameters = " — ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "Eval",
            Description = Localizer["EvalAttr"].Value,
            Parameters = " — ",
            ReturnValue = "ValueTask<T>"
        },
        new()
        {
            Name = "Function",
            Description = Localizer["FunctionAttr"].Value,
            Parameters = " — ",
            ReturnValue = "ValueTask<T>"
        }
    ];

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
