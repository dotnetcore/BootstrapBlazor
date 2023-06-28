// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

internal class DefaultPdfService : IHtml2Pdf
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
    public Task<bool> ExportAsync(string html, string? fileName = null)
    {
        var list = new List<string> { html };
        return ExportAsync(list, fileName);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="snippets"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> ExportAsync(List<string> snippets, string? fileName = null)
    {
        await Task.Delay(200);
        return true;
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
}
