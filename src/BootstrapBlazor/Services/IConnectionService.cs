// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 当前链接服务
/// </summary>
public interface IConnectionService
{
    /// <summary>
    /// 增加或更新当前 Key
    /// </summary>
    /// <param name="client">ClientInfo 实例</param>
    void AddOrUpdate(ClientInfo client);

    /// <summary>
    /// 获得指定 key 的连接信息
    /// </summary>
    /// <param name="key">键值</param>
    /// <param name="value">连接信息</param>
    /// <returns></returns>
    bool TryGetValue(string key, out ConnectionItem? value);

    /// <summary>
    /// 获得 链接集合
    /// </summary>
    ICollection<ConnectionItem> Connections { get; }

    /// <summary>
    /// 获得在线连接数
    /// </summary>
    long Count { get; }
}
