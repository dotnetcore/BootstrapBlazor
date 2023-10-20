// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器事件,通用属性帮助类
/// </summary>
partial class BootstrapBlazorJSHelper
{
    private IJSRuntime JSRuntime { get; }

    private IVersionService JSVersionService { get; }

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<BootstrapBlazorJSHelper>? Interop { get; }

    private List<string> guidList { get; } = [];

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="consoleType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ValueTask Console(ConsoleType consoleType, params object?[]? args) => InvokeVoidAsync("doConsole", consoleType.ToDescriptionString(), args);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public ValueTask ConsoleClear() => InvokeVoidAsync("doConsoleClear");

}
