// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IP 地址定位服务
/// </summary>
public interface IIPLocatorProvider
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<string?> Locate(string ip);
}
