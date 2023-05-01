// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// JSInterop 类
/// </summary>
[ExcludeFromCodeCoverage]
public class JSInterop<TValue> : IDisposable where TValue : class
{
    private IJSRuntime JSRuntime { get; }

    private DotNetObjectReference<TValue>? _objRef;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public JSInterop(IJSRuntime jsRuntime)
    {
        JSRuntime = jsRuntime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal ValueTask CheckNotifyPermissionAsync(TValue value, string? callbackMethodName = null, bool requestPermission = true)
    {
        _objRef = DotNetObjectReference.Create(value);
        return JSRuntime.InvokeVoidAsync("$.bb_notify_checkPermission", _objRef, callbackMethodName ?? "", requestPermission);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal ValueTask<bool> Dispatch(TValue value, NotificationItem model, string? callbackMethodName = null)
    {
        _objRef = DotNetObjectReference.Create(value);
        return JSRuntime.InvokeAsync<bool>("$.bb_notify_display", _objRef, callbackMethodName, model);
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_objRef != null)
            {
                _objRef.Dispose();
                _objRef = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
