// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IP 地址定位服务接口</para>
///  <para lang="en">IP Address Location Service Interface</para>
/// </summary>
public interface IIpLocatorProvider
{
    /// <summary>
    ///  <para lang="zh">获得/设置 定位器 Key 默认 null 使用已注册的最后一个 Provider</para>
    ///  <para lang="en">Get/Set Locator Key, default null, use the last registered Provider</para>
    /// </summary>
    string? Key { get; set; }

    /// <summary>
    ///  <para lang="zh">通过 IP 地址定位地理位置信息</para>
    ///  <para lang="en">Locate Geolocation Info by IP Address</para>
    /// </summary>
    /// <param name="ip"></param>
    Task<string?> Locate(string? ip);
}
