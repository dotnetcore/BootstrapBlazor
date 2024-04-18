// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
