// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器通知服务
/// </summary>
public static class BrowserNotification
{
    /// <summary>
    /// 检查浏览器通知权限状态
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="interop">JSInterop 实例</param>
    /// <param name="component">当前页面</param>
    /// <param name="callbackMethodName">检查通知权限结果回调方法</param>
    /// <param name="requestPermission">是否请求权限 默认 true</param>
    /// <returns></returns>
    public static ValueTask CheckPermission<TComponent>(JSInterop<TComponent> interop, TComponent component, string? callbackMethodName = null, bool requestPermission = true) where TComponent : class => interop.CheckNotifyPermissionAsync(component, callbackMethodName, requestPermission);

    /// <summary>
    /// 发送浏览器通知
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="interop">JSInterop 实例</param>
    /// <param name="component">当前页面</param>
    /// <param name="model">NotificationItem 实例</param>
    /// <param name="callbackMethodName">发送结果回调方法</param>
    /// <returns></returns>
    public static async Task<bool> Dispatch<TComponent>(JSInterop<TComponent> interop, TComponent component, NotificationItem model, string? callbackMethodName) where TComponent : class
    {
        var ret = await interop.Dispatch(component, model, callbackMethodName);
        return ret;
    }
}
