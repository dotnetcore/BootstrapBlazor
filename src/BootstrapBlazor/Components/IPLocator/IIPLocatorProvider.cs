// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IP 地址定位服务</para>
///  <para lang="en">IP Address Locator Service</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
public interface IIPLocatorProvider
{
    /// <summary>
    ///  <para lang="zh"></para>
    ///  <para lang="en"></para>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    Task<string?> Locate(string ip);
}
