// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IIpLocatorFactory 接口
/// </summary>
public interface IIpLocatorFactory
{
    /// <summary>
    /// 创建 IIPLocator 实例方法
    /// </summary>
    /// <param name="key">注入时使用的 key 值</param>
    /// <returns></returns>
    IIpLocatorProvider Create(string? key = null);
}
