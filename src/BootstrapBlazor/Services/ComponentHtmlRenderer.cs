// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

class ComponentHtmlRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : IComponentHtmlRenderer
{
    private IServiceProvider ServiceProvider { get; set; } = serviceProvider;

    private ILoggerFactory LoggerFactory { get; set; } = loggerFactory;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<string> RenderAsync<TComponent>(IDictionary<string, object?>? parameters = null) where TComponent : IComponent
    {
        using var htmlRenderer = new HtmlRenderer(ServiceProvider, LoggerFactory);
        parameters ??= new Dictionary<string, object?>();
        var paras = ParameterView.FromDictionary(parameters);
        var html = await htmlRenderer.RenderComponentAsync<TComponent>(paras);
        return html.ToHtmlString();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<string> RenderAsync(Type componentType, IDictionary<string, object?>? parameters = null)
    {
        using var htmlRenderer = new HtmlRenderer(ServiceProvider, LoggerFactory);
        parameters ??= new Dictionary<string, object?>();
        var paras = ParameterView.FromDictionary(parameters);
        var output = await htmlRenderer.RenderComponentAsync(componentType, paras);
        return output.ToHtmlString();
    }
}
