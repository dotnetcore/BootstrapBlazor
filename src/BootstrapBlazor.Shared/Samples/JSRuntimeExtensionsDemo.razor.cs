// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// JSRuntimeExtensionsDemo
/// </summary>
public partial class JSRuntimeExtensionsDemo
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    private readonly string _consoleText = "Hello BootstrapBlazor";

    private decimal WindowInnerHeight { get; set; }

    private decimal WindowOuterHeight { get; set; }

    private decimal WindowInnerWidth { get; set; }

    private decimal WindowOuterWidth { get; set; }

    private decimal DocumentClientHeight { get; set; }

    private decimal DocumentClientWidth { get; set; }

    private decimal GetDocumentScrollHeight { get; set; }

    private decimal GetDocumentScrollWidth { get; set; }

    private decimal GetDocumentScrollTop { get; set; }

    private decimal GetDocumentScrollLeft { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await GetProperties();
    }

    private async Task GetProperties()
    {
        WindowInnerHeight = await JSRuntime.GetWindowInnerHeight();
        WindowOuterHeight = await JSRuntime.GetWindowOuterHeight();
        WindowInnerWidth = await JSRuntime.GetWindowInnerWidth();
        WindowOuterWidth = await JSRuntime.GetWindowOuterWidth();
        DocumentClientHeight = await JSRuntime.GetDocumentClientHeight();
        DocumentClientWidth = await JSRuntime.GetDocumentClientWidth();
        GetDocumentScrollHeight = await JSRuntime.GetDocumentScrollHeight();
        GetDocumentScrollWidth = await JSRuntime.GetDocumentScrollWidth();
        GetDocumentScrollTop = await JSRuntime.GetDocumentScrollTop();
        GetDocumentScrollLeft = await JSRuntime.GetDocumentScrollLeft();

    }

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
    new()
    {
        Name = "ChangeMetaAsync",
        Description = "动态添加连接到head标签中",
        Parameters = " - ",
        ReturnValue = "ValueTask"
    },
    new()
    {
        Name = "Console",
        Description = "输出到浏览器控制台",
        Parameters = " - ",
        ReturnValue = "ValueTask"
    },
    new()
    {
        Name = "ConsoleClear",
        Description = "清空浏览器控制台",
        Parameters = " - ",
        ReturnValue = "ValueTask"
    }
    };
}
