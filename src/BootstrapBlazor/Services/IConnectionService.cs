// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">当前链接服务</para>
/// <para lang="en">Current Connection Service</para>
/// </summary>
public interface IConnectionService
{
    /// <summary>
    /// <para lang="zh">增加或更新当前 Key</para>
    /// <para lang="en">Add or Update Current Key</para>
    /// </summary>
    /// <param name="client"><para lang="zh">ClientInfo 实例</para><para lang="en">ClientInfo instance</para></param>
    void AddOrUpdate(ClientInfo client);

    /// <summary>
    /// <para lang="zh">获得指定 key 的连接信息</para>
    /// <para lang="en">Get connection information for specified key</para>
    /// </summary>
    /// <param name="key"><para lang="zh">键值</para><para lang="en">键value</para></param>
    /// <param name="value"><para lang="zh">连接信息</para><para lang="en">连接info</para></param>
    bool TryGetValue(string key, out ConnectionItem? value);

    /// <summary>
    /// <para lang="zh">获得 链接集合</para>
    /// <para lang="en">Get Connections Collection</para>
    /// </summary>
    ICollection<ConnectionItem> Connections { get; }

    /// <summary>
    /// <para lang="zh">获得在线连接数</para>
    /// <para lang="en">Get Online Connection Count</para>
    /// </summary>
    long Count { get; }
}
