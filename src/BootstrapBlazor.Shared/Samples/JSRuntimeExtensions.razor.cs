// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

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
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Module = await JSRuntime.LoadUtility();
    }

    private const string bbUrl = "https://www.blazor.zone/";

    private async Task OpenUrl_Blank() => await Module.OpenUrl(bbUrl);

    private async Task OpenUrl_Self() => await Module.OpenUrl(bbUrl, "_self");

    private bool IsMobileDevice { get; set; }

    private async Task GetIsMobileDevice() => IsMobileDevice = await Module.IsMobile();

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

    private string elementId1 { get; set; } = "GetElementCSS";

    private string cssPropertieName { get; set; } = "height";

    private string? cssPropertieResult { get; set; }

    private async Task GetElementCSS() => cssPropertieResult = await Module.GetCSSValue<string>(elementId1, cssPropertieName);

    private string elementId2 { get; set; } = "GetElementProperties";

    private string getPropertieName { get; set; } = "clientHeight";

    private decimal getPropertieResult { get; set; }

    private async Task GetElementProperties() => getPropertieResult = await Module.GetElementProperties<decimal>(elementId2, getPropertieName);

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = "ChangeMetaAsync",
            Description = Localizer["ChangeMetaIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = "OpenUrl",
            Description = Localizer["OpenBlankUrlIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "IsMobile",
            Description = Localizer["IsMobileDeviceIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "Eval",
            Description = Localizer["EvalIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        },
        new()
        {
            Name = "Function",
            Description = Localizer["FunctionIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        },
        new()
        {
            Name = "GetCSSValue",
            Description = Localizer["GetElementCSSIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        }
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (Module != null)
        {
            await Module.DisposeAsync();
            Module = null;
        }
        GC.SuppressFinalize(this);
    }
}
