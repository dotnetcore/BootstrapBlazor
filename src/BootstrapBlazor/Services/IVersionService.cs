// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 组件版本号服务
/// </summary>
public interface IVersionService
{
    /// <summary>
    /// 获得 版本号
    /// </summary>
    /// <returns></returns>
    string GetVersion();

    /// <summary>
    /// 获得 版本号
    /// </summary>
    /// <param name="url">当前资源 相对路径 如 ./css/site.css</param>
    /// <returns></returns>
    string GetVersion(string? url) => GetVersion();
}
