﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器事件,通用属性帮助类
/// </summary>
partial class BootstrapBlazorJSHelper : IBootstrapBlazorJSHelper, IJSRuntimeEventHandler
{
    private IJSRuntime JSRuntime { get; }

    private IVersionService JSVersionService { get; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<BootstrapBlazorJSHelper>? Interop { get; }

    private List<string> guidList { get; } = new List<string>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSRuntime"></param>
    /// <param name="versionService"></param>
    public BootstrapBlazorJSHelper(IJSRuntime jSRuntime, IVersionService versionService)
    {
        JSRuntime = jSRuntime;
        JSVersionService = versionService;
        Interop = DotNetObjectReference.Create(this);
    }

    private ValueTask<IJSObjectReference> ImportModule() => JSRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/BootstrapBlazor/modules/event-services.js?v={JSVersionService.GetVersion()}");

    private async ValueTask InternalRegisterEvent(DOMEvents eventName, params object?[]? args)
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

    private async ValueTask InvokeVoidAsync(string identifier, params object?[] args)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public ValueTask RegisterEvent(DOMEvents eventName) => InternalRegisterEvent(eventName);

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
    /// <para>方法已弃用，请使用新的方法：<see cref="RunJSWithEval(string)"/></para>
    /// </summary>
    /// <param name="scripts"></param>
    /// <returns></returns>
    [Obsolete("旧方法 RunEval 已过期，请使用新方法 RunJSWithEval")]
    public ValueTask RunEval(string scripts) => InvokeVoidAsync("runJSWithEval", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// <para>方法已弃用，请使用新的方法：<see cref="RunJSWithEval(string)"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scripts"></param>
    /// <returns></returns>
    [Obsolete("旧方法 RunEval 已过期，请使用新方法 RunJSWithEval")]
    public ValueTask<T?> RunEval<T>(string scripts) => InvokeAsync<T>("runJSWithEval", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="scripts"></param>
    /// <returns></returns>
    public ValueTask RunJSWithEval(string scripts) => InvokeVoidAsync("runJSWithEval", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scripts"></param>
    /// <returns></returns>
    public ValueTask<T?> RunJSWithEval<T>(string scripts) => InvokeAsync<T>("runJSWithEval", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="scripts"></param>
    /// <returns></returns>
    public ValueTask RunJSWithFunction(string scripts) => InvokeVoidAsync("runJSWithFunction", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scripts"></param>
    /// <returns></returns>
    public ValueTask<T?> RunJSWithFunction<T>(string scripts) => InvokeAsync<T>("runJSWithFunction", scripts);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public ValueTask AddLink(string link) => InvokeVoidAsync("doAddLink", link);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="link"></param>
    /// <param name="rel"></param>
    /// <returns></returns>
    public ValueTask AddLink(string link, string rel) => InvokeVoidAsync("doAddLinkWithRel", link, rel);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public ValueTask RemoveLink(string link) => InvokeVoidAsync("doRemoveLink", link);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    public ValueTask AddScript(string script) => InvokeVoidAsync("doAddScript", script);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="script"></param>
    /// <returns></returns>
    public ValueTask RemoveScript(string script) => InvokeVoidAsync("doRemoveScript", script);
}
