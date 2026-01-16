// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">组件版本号服务</para>
///  <para lang="en">Component Version Service</para>
/// </summary>
public interface IVersionService
{
    /// <summary>
    ///  <para lang="zh">获得 版本号</para>
    ///  <para lang="en">Get Version</para>
    /// </summary>
    /// <returns></returns>
    string GetVersion();

    /// <summary>
    ///  <para lang="zh">获得 版本号</para>
    ///  <para lang="en">Get Version</para>
    /// </summary>
    /// <param name="url"><para lang="zh">当前资源 相对路径 如 ./css/site.css</para><para lang="en">Current resource relative path, e.g. ./css/site.css</para></param>
    /// <returns></returns>
    string GetVersion(string? url) => GetVersion();
}
