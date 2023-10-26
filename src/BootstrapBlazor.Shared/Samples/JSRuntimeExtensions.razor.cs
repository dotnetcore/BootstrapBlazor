// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// JSRuntimeExtensionsDemo
/// </summary>
public partial class JSRuntimeExtensions
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private readonly string _consoleText = "Hello BootstrapBlazor";

    #region OpenBlankUrl Demo

    private string BlankUrl = "https://www.blazor.zone/";

    private async Task OpenBlankUrl()
    {
        await JSRuntime.OpenBlankUrl(BlankUrl);
    }

    #endregion


    #region GetIsMobileDevice Demo

    private bool IsMobileDevice { get; set; }

    private async Task GetIsMobileDevice()
    {
        IsMobileDevice = await JSRuntime.GetIsMobileDevice();
    }

    #endregion

    #region RunEval Demo

    private string evalContent = """
        const currentUrl = window.location.href;

        `当前URL: ${currentUrl}`;
        """;

    private string evalResult { get; set; } = string.Empty;

    private async Task RunEval() => evalResult = await JSRuntime.Eval<string>(evalContent);

    #endregion

    #region RunFunction Demo

    private string functionContent = """
        const currentUrl = window.location.href;

        return `当前URL: ${currentUrl}`;
        """;

    private string functionResult { get; set; } = string.Empty;

    private async Task RunFunction() => functionResult = await JSRuntime.Function<string>(functionContent);

    #endregion

    #region GetCSSValue Demo

    private string propertiesId { get; set; } = "GetElementProperties";

    private string propertiesTag1 { get; set; } = "height";

    private string? propertiesResult1 { get; set; }

    private async Task GetElementCSS()
    {
        propertiesResult1 = await JSRuntime.GetCSSValue<string>(propertiesId, propertiesTag1);
    }

    #endregion

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
            Name = "OpenBlankUrl",
            Description = Localizer["OpenBlankUrlIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "GetIsMobileDevice",
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
            Name = "Console",
            Description = Localizer["JSConsoleIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask"
        },
        new()
        {
            Name = "GetCSSValue",
            Description = Localizer["GetElementCSSIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        }
    };
}
