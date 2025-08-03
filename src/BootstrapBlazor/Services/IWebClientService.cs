// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// WebClient 服务类
/// </summary>
public interface IWebClientService : IAsyncDisposable
{
    /// <summary>
    /// 获得 ClientInfo 实例方法
    /// </summary>
    /// <returns></returns>
    Task<ClientInfo> GetClientInfo();
    /// <summary>
    /// SetData 方法由 JS 调用
    /// </summary>
    /// <param name="client"></param> 
    void SetData(ClientInfo client);
}
