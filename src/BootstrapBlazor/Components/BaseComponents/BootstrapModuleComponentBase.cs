// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Bootstrap Blazor JavaScript 隔离基类</para>
///  <para lang="en">Bootstrap blazor JavaScript isolation base class</para>
/// </summary>
public abstract class BootstrapModuleComponentBase : IdComponentBase, IAsyncDisposable
{
    /// <summary>
    ///  <para lang="zh"><see cref="JSModule"/> 实例</para>
    ///  <para lang="en">Instance of <see cref="JSModule"/></para>
    /// </summary>
    protected JSModule? Module { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 脚本路径</para>
    ///  <para lang="en">Gets or sets the module path</para>
    /// </summary>
    protected string? ModulePath { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否自动调用 init 默认 true</para>
    ///  <para lang="en">Gets or sets whether to auto invoke init. Default is true</para>
    /// </summary>
    protected bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否自动调用 dispose 默认 true</para>
    ///  <para lang="en">Gets or sets whether to auto invoke dispose. Default is true</para>
    /// </summary>
    protected bool AutoInvokeDispose { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 DotNetObjectReference 实例</para>
    ///  <para lang="en">Gets or sets DotNetObjectReference instance</para>
    /// </summary>
    protected DotNetObjectReference<BootstrapModuleComponentBase>? Interop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Module 是否加载完成</para>
    ///  <para lang="en">Gets or sets whether the module is loaded</para>
    /// </summary>
    protected TaskCompletionSource ModuleLoadTask { get; } = new();

    /// <summary>
    ///  <para lang="zh">获得/设置 Module 是否初始化完成</para>
    ///  <para lang="en">Gets or sets whether the module is initialized</para>
    /// </summary>
    protected TaskCompletionSource ModuleInitTask { get; } = new();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnLoadJSModule();
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !string.IsNullOrEmpty(ModulePath))
        {
            Module ??= await JSRuntime.LoadModule(ModulePath);
            ModuleLoadTask.SetResult();

            if (AutoInvokeInit)
            {
                await InvokeInitAsync();
            }
            ModuleInitTask.SetResult();
        }
    }

    /// <summary>
    ///  <para lang="zh">加载 JS Module 方法</para>
    ///  <para lang="en">Load JS Module method</para>
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
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
    /// </summary>
    /// <returns></returns>
    protected virtual Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected Task InvokeVoidAsync(string identifier, params object?[]? args) => InvokeVoidAsync(identifier, CancellationToken.None, args);

    /// <summary>
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
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
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
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
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    protected Task<TValue?> InvokeAsync<TValue>(string identifier, params object?[]? args) => InvokeAsync<TValue?>(identifier, CancellationToken.None, args);

    /// <summary>
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
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
    ///  <para lang="zh">调用 JavaScript 方法</para>
    ///  <para lang="en">Call JavaScript method</para>
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
    ///  <para lang="zh">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</para>
    ///  <para lang="en">Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</para>
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
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
