// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Bootstrap blazor Javascript isoloation base class
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
    [NotNull]
    protected string? ModulePath { get; set; }

    /// <summary>
    /// The javascript dynamic module name
    /// </summary>
    [NotNull]
    protected string? ModuleName { get; set; }

    /// <summary>
    /// 获得/设置 路径是否为相对路径 默认 false
    /// </summary>
    protected bool Relative { get; set; }

    /// <summary>
    /// 获得/设置 是否继承父类 JSModuleAutoLoader 设置 默认 true
    /// </summary>
    protected bool Inherited { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否需要 javascript invoke 默认 false
    /// </summary>
    protected bool JSObjectReference { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnLoadJSModule();
    }

    /// <summary>
    /// 加载 JS Module 方法
    /// </summary>
    protected virtual void OnLoadJSModule()
    {
        if (Inherited)
        {
            var type = this.GetType();
            var attr = type.GetCustomAttribute<JSModuleAutoLoaderAttribute>();

            if (attr != null)
            {
                string? typeName = null;
                ModulePath = attr.Path ?? GetTypeName().ToLowerInvariant();
                ModuleName = attr.ModuleName ?? GetTypeName();
                JSObjectReference = attr.JSObjectReference;
                Relative = attr.Relative;

                string GetTypeName()
                {
                    typeName ??= type.GetTypeModuleName();
                    return typeName;
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !string.IsNullOrEmpty(ModulePath))
        {
            Module ??= JSObjectReference
                ? await JSRuntime.LoadModule(ModulePath, this, Relative)
                : await JSRuntime.LoadModule(ModulePath, Relative);
        }

        await ModuleInvokeVoidAsync(firstRender);
    }

    /// <summary>
    /// Load javascript module method
    /// </summary>
    /// <returns></returns>
    protected virtual async Task ModuleInvokeVoidAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ModuleInitAsync();
        }
        else
        {
            await ModuleExecuteAsync();
        }
    }

    /// <summary>
    /// call javascript init method
    /// </summary>
    /// <returns></returns>
    protected virtual async Task ModuleInitAsync()
    {
        if (Module != null)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.init", Id);
        }
    }

    /// <summary>
    /// call javascript execute method
    /// </summary>
    /// <returns></returns>
    protected virtual Task ModuleExecuteAsync() => Task.CompletedTask;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (Module != null && disposing)
        {
            await Module.InvokeVoidAsync($"{ModuleName}.dispose", Id);
            await Module.DisposeAsync();
            Module = null;
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
