// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// JSRuntimeExtensionsDemo
/// </summary>
public partial class JSRuntimeExtensions : IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    private readonly string _consoleText = "Hello BootstrapBlazor";

    private const string BlankUrl = "https://www.blazor.zone/";

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

    private async Task OpenUrl()
    {
        await Module.OpenUrl(BlankUrl);
    }

    private bool IsMobileDevice { get; set; }

    private async Task GetIsMobileDevice()
    {
        IsMobileDevice = await Module.IsMobile();
    }

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

    private string propertiesId { get; set; } = "GetElementProperties";

    private string propertiesTag1 { get; set; } = "height";

    private string? propertiesResult1 { get; set; }

    private async Task GetElementCSS()
    {
        propertiesResult1 = await Module.GetCSSValue<string>(propertiesId, propertiesTag1);
    }

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

    /// <summary>
    /// 
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
