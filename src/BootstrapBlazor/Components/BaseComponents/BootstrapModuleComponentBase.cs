// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Bootstrap blazor JavaScript isolation base class
/// </summary>
public abstract class BootstrapModuleComponentBase : IdComponentBase, IAsyncDisposable
{
    /// <summary>
    /// Instance of <see cref="JSModule"/>
    /// </summary>
    protected JSModule? Module { get; set; }

    /// <summary>
    /// 获得/设置 脚本路径
    /// </summary>
    protected string? ModulePath { get; set; }

    /// <summary>
    /// 获得/设置 是否自动调用 init 默认 true
    /// </summary>
    protected bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动调用 dispose 默认 true
    /// </summary>
    protected bool AutoInvokeDispose { get; set; } = true;

    /// <summary>
    /// 获得/设置 DotNetObjectReference 实例
    /// </summary>
    protected DotNetObjectReference<BootstrapModuleComponentBase>? Interop { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnLoadJSModule();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !string.IsNullOrEmpty(ModulePath))
        {
            Module ??= await JSRuntime.LoadModule(ModulePath);

            if (AutoInvokeInit)
            {
                await InvokeInitAsync();
            }
        }
    }

    /// <summary>
    /// 加载 JS Module 方法
    /// </summary>
    protected virtual void OnLoadJSModule()
    {
        var type = this.GetType();
        var inherited = type.GetCustomAttribute<JSModuleNotInheritedAttribute>() == null;
        if (inherited)
        {
            var attributes = type.GetCustomAttributes<JSModuleAutoLoaderAttribute>();
            if (attributes.Any())
            {
                var attr = attributes.First();
                AutoInvokeDispose = attr.AutoInvokeDispose;
                AutoInvokeInit = attr.AutoInvokeInit;

                if (attr.JSObjectReference)
                {
                    Interop = DotNetObjectReference.Create(this);
                }

                ModulePath = attr is BootstrapModuleAutoLoaderAttribute loader ? loader.LoadModulePath(type) : attr.Path;
            }
        }
    }

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <returns></returns>
    protected virtual Task InvokeInitAsync() => InvokeVoidAsync("init", Id);

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected Task InvokeVoidAsync(string identifier, params object?[]? args) => InvokeVoidAsync(identifier, CancellationToken.None, args);

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="timeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected async Task InvokeVoidAsync(string identifier, TimeSpan timeout, params object?[]? args)
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync(identifier, timeout, args);
        }
    }

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected async Task InvokeVoidAsync(string identifier, CancellationToken cancellationToken = default, params object?[]? args)
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync(identifier, cancellationToken, args);
        }
    }

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected Task<TValue?> InvokeAsync<TValue>(string identifier, params object?[]? args) => InvokeAsync<TValue?>(identifier, CancellationToken.None, args);

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="timeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected async Task<TValue?> InvokeAsync<TValue>(string identifier, TimeSpan timeout, params object?[]? args)
    {
        TValue? ret = default;
        if (Module != null)
        {
            ret = await Module.InvokeAsync<TValue>(identifier, timeout, args);
        }
        return ret;
    }

    /// <summary>
    /// call JavaScript method
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected async Task<TValue?> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken = default, params object?[]? args)
    {
        TValue? ret = default;
        if (Module != null)
        {
            ret = await Module.InvokeAsync<TValue>(identifier, cancellationToken, args);
        }
        return ret;
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
            // 销毁 DotNetObjectReference 实例
            Interop?.Dispose();

            // 销毁 JSModule
            if (Module != null)
            {
                if (AutoInvokeDispose)
                {
                    await Module.InvokeVoidAsync("dispose", Id);
                }

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
