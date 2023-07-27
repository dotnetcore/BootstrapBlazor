// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

class DefaultPdfService : IHtml2Pdf
{
    private IJSRuntime JSRuntime { get; }

    private JSModule? Module { get; set; }

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
        Module ??= await LoadModule();
        return await Module.InvokeAsync<bool>("exportPdf", html, fileName);
    }

    public async Task<Stream> ExportStreamAsync(string html)
    {
        Module ??= await LoadModule();

        var payload = await Module.InvokeAsync<string>("exportPdfAsBase64", html);
        var buffer = Convert.FromBase64String(payload);
        return new MemoryStream(buffer);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModule("./_content/BootstrapBlazor.Html2Pdf/export.js");
}
