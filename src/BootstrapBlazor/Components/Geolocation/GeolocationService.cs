// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 地理位置坐标服务
/// </summary>
public class GeolocationService
{
    /// <summary>
    /// 获取当前地理位置坐标信息
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GetLocaltion<TComponent>(JSInterop<TComponent> interop, TComponent component, string callbackMethodName) where TComponent : class
    {
        var ret = await interop.GetGeolocationItemAsync(component, callbackMethodName);
        return ret;
    }
}
