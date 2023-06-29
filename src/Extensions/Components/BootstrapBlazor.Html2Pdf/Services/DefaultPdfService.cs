// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

class DefaultPdfService : IHtml2Pdf
{
    private IComponentHtmlRenderer HtmlRender { get; }

    private IJSRuntime JSRuntime { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSRuntime"></param>
    /// <param name="render"></param>
    public DefaultPdfService(IJSRuntime jSRuntime, IComponentHtmlRenderer render)
    {
        JSRuntime = jSRuntime;
        HtmlRender = render;
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> ExportAsync(Type componentType, IDictionary<string, object?>? parameters = null, string? fileName = null)
    {
        var html = await HtmlRender.RenderAsync(componentType, parameters);
        return await ExportAsync(html, fileName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="parameters"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> ExportAsync<TComponent>(IDictionary<string, object?>? parameters = null, string? fileName = null) where TComponent : IComponent
    {
        var html = await HtmlRender.RenderAsync<TComponent>(parameters);
        return await ExportAsync(html, fileName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> ExportByIdAsync(string id, string? fileName = null)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("exportPdfById", id, fileName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="element"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> ExportByElementAsync(ElementReference element, string? fileName = null)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("exportPdfByElement", element, fileName);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModule("./_content/BootstrapBlazor.Html2Pdf/export.js");
}
