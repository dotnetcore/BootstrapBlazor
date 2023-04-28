// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// <inheritdoc/>
/// </summary>
partial class JSRuntimeEventHandler : IJSRuntimeEventHandler
{
    private IJSRuntime JSRuntime { get; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<JSRuntimeEventHandler>? Interop { get; }

    private List<string> guidList { get; } = new List<string>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSRuntime"></param>
    public JSRuntimeEventHandler(IJSRuntime jSRuntime)
    {
        JSRuntime = jSRuntime;
        Interop = DotNetObjectReference.Create(this);
    }

    private ValueTask<IJSObjectReference> ImportModule() => JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor/modules/helper.js");

    private async Task InternalRegisterEvent(BootStrapBlazorEventType eventName, params object?[]? args)
    {
        var guid = Guid.NewGuid();
        guidList.Add($"{guid}");

        var arguments = new List<object?> { guid, Interop, $"JSInvokOn{eventName}", eventName };
        if (args != null)
        {
            arguments.AddRange(args);
        }
        await InvokeVoidAsync("addEventListener", arguments.ToArray());
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public Task RegisterEvent(BootStrapBlazorEventType eventName) => InternalRegisterEvent(eventName);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task RegisterEvent(BootStrapBlazorEventType eventName, string id) => InternalRegisterEvent(eventName, id);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public Task RegisterEvent(BootStrapBlazorEventType eventName, ElementReference element) => InternalRegisterEvent(eventName, null, element);

    private async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        Module ??= await ImportModule();
        await Module.InvokeVoidAsync(identifier, args);
    }

    private async ValueTask<TValue?> InvokeAsync<TValue>(string identifier, params object?[]? args)
    {
        Module ??= await ImportModule();
        return await Module.InvokeAsync<TValue?>(identifier, args);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public ValueTask<T?> GetElementPropertiesByTagFromIdAsync<T>(string id, string tag) => InvokeAsync<T?>("getElementPropertiesByTagFromId", id, tag);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tag"></param>
    /// <returns></returns>
    public ValueTask<T?> GetDocumentPropertiesByTagAsync<T>(string tag) => InvokeAsync<T?>("getDocumentPropertiesByTag", tag);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public ValueTask<T?> GetElementPropertiesByTagAsync<T>(ElementReference element, string tag) => InvokeAsync<T?>("getElementPropertiesByTag", element, tag);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="scripts"></param>
    /// <returns></returns>
    public ValueTask RunEval(string scripts) => InvokeVoidAsync("runJSEval", scripts);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            Interop.Dispose();

            if (Module != null)
            {
                guidList.ForEach(async x => await Module.InvokeVoidAsync("dispose", x));
                guidList.Clear();

                await Module.DisposeAsync();
                Module = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
