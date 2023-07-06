// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

class DefaultPdfService : IHtml2Pdf
{
    private IJSRuntime JSRuntime { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSRuntime"></param>
    public DefaultPdfService(IJSRuntime jSRuntime)
    {
        JSRuntime = jSRuntime;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="html"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> ExportAsync(string html, string? fileName = null)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("exportPdf", html, fileName);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModule("./_content/BootstrapBlazor.Html2Pdf/export.js");
}
