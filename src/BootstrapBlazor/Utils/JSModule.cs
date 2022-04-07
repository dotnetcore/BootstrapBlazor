// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 模块加载器
/// </summary>
public class JSModule : IAsyncDisposable
{
    [NotNull]
    private IJSObjectReference? Module { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jSObjectReference"></param>
    public JSModule(IJSObjectReference? jSObjectReference)
    {
        Module = jSObjectReference ?? throw new ArgumentNullException(nameof(jSObjectReference));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ValueTask InvokeVoidAsync(string identifier, params object[] args) => Module.InvokeVoidAsync(identifier, args);

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            if (Module != null)
            {
                await Module.DisposeAsync().ConfigureAwait(false);
                Module = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}
