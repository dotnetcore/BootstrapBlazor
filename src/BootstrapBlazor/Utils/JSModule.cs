// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 模块加载器
/// </summary>
public class JSModule : IAsyncDisposable
{
    /// <summary>
    /// IJSObjectReference 实例
    /// </summary>
    [NotNull]
    protected IJSObjectReference? Module { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSObjectReference"></param>
    public JSModule(IJSObjectReference? jSObjectReference)
    {
        Module = jSObjectReference ?? throw new ArgumentNullException(nameof(jSObjectReference));
    }

    /// <summary>
    /// InvokeVoidAsync 方法
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask InvokeVoidAsync(string identifier, params object?[] args) => Module.InvokeVoidAsync(identifier, args);

    /// <summary>
    /// InvokeVoidAsync 方法
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="token"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask InvokeVoidAsync(string identifier, CancellationToken token, params object?[] args) => Module.InvokeVoidAsync(identifier, token, args);

    /// <summary>
    /// InvokeVoidAsync 方法
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object?[] args) => Module.InvokeAsync<TValue>(identifier, args);

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
                // TODO: 微软的代码这里加上 await 就会线程死锁
                try
                {
                    await Module.DisposeAsync().AsTask();
                }
                catch { }
                Module = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore(true);
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// 模块加载器
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class JSModule<TValue> : JSModule where TValue : class
{
    /// <summary>
    /// DotNetReference 实例
    /// </summary>
    protected DotNetObjectReference<TValue> DotNetReference { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jSObjectReference"></param>
    /// <param name="value"></param>
    public JSModule(IJSObjectReference? jSObjectReference, TValue value) : base(jSObjectReference)
    {
        DotNetReference = DotNetObjectReference.Create(value);
    }

    /// <summary>
    /// InvokeVoidAsync 方法
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override async ValueTask InvokeVoidAsync(string identifier, params object?[] args)
    {
        var paras = new List<object?>();
        if (args != null)
        {
            paras.AddRange(args);
        }
        paras.Add(DotNetReference);

        try
        {
            await Module.InvokeVoidAsync(identifier, paras.ToArray());
        }
        catch { }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            DotNetReference.Dispose();
        }
        return base.DisposeAsyncCore(disposing);
    }
}
