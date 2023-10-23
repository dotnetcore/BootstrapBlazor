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

    #region RunEval Demo

    private string evalContent { get; set; } = """
        const currentUrl = window.location.href;

        `当前URL: ${currentUrl}`;
        """;

    private string evalResult { get; set; } = string.Empty;

    private async Task RunEval() => evalResult = await JSRuntime.Eval<string>(evalContent);

    #endregion

    #region GetElementProperties Demo

    private string propertiesId { get; set; } = "GetElementProperties";

    private string propertiesTag1 { get; set; } = "offsetWidth";

    private string? propertiesResult1 { get; set; }

    private string propertiesTag2 { get; set; } = "offsetHeight";

    private string? propertiesResult2 { get; set; }
    private string propertiesTag3 { get; set; } = "classList.value";

    private string? propertiesResult3 { get; set; }

    private async Task GetElementProperties()
    {
        propertiesResult1 = $"属性值为:{await JSRuntime.GetElementProperties<object>(propertiesId, propertiesTag1)}";
        propertiesResult2 = $"属性值为:{await JSRuntime.GetElementProperties<object>(propertiesId, propertiesTag2)}";
        propertiesResult3 = $"属性值为:{await JSRuntime.GetElementProperties<object>(propertiesId, propertiesTag3)}";
    }

    #endregion

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
        },
        new()
        {
            Name = "GetIsMobileDevice",
            Description = "获取是否为移动设备",
            Parameters = " - ",
            ReturnValue = "ValueTask<bool>"
        },
        new()
        {
            Name = "Eval",
            Description = "动态运行js代码",
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        },
        new()
        {
            Name = "GetElementProperties",
            Description = "获取元素的指定属性",
            Parameters = " - ",
            ReturnValue = "ValueTask<T>"
        }
    };
}
