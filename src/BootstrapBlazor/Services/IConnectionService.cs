// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
