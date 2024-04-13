// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
