// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器通知服务
/// </summary>
public class NotificationService : IAsyncDisposable
{
    private IJSRuntime JSRuntime { get; }

    private JSModule? Module { get; set; }

    private DotNetObjectReference<WebClientService>? Interop { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="runtime"></param>
    public NotificationService(IJSRuntime runtime)
    {
        JSRuntime = runtime;
    }

    /// <summary>
    /// 检查浏览器通知权限状态
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="interop">JSInterop 实例</param>
    /// <param name="component">当前页面</param>
    /// <param name="callbackMethodName">检查通知权限结果回调方法</param>
    /// <param name="requestPermission">是否请求权限 默认 true</param>
    /// <returns></returns>
    public ValueTask CheckPermission<TComponent>(JSInterop<TComponent> interop, TComponent component, string? callbackMethodName = null, bool requestPermission = true) where TComponent : class => interop.CheckNotifyPermissionAsync(component, callbackMethodName, requestPermission);

    /// <summary>
    /// 发送浏览器通知
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="interop">JSInterop 实例</param>
    /// <param name="component">当前页面</param>
    /// <param name="model">NotificationItem 实例</param>
    /// <param name="callbackMethodName">发送结果回调方法</param>
    /// <returns></returns>
    public async Task<bool> Dispatch<TComponent>(JSInterop<TComponent> interop, TComponent component, NotificationItem model, string? callbackMethodName = null) where TComponent : class
    {
        var ret = await interop.Dispatch(component, model, callbackMethodName);
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
