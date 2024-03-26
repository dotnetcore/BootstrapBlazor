// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 同步限流器接口
/// </summary>
public interface IThrottleDispatcherFactory
{
    /// <summary>
    /// 获得或创建限流器
    /// </summary>
    /// <returns></returns>
    ThrottleDispatcher GetOrCreate(string key, ThrottleOptions? options = null);
}
