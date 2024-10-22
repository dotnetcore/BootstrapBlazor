// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 分发服务接口
/// </summary>
public interface IDispatchService<TEntry>
{
    /// <summary>
    /// 分发方法
    /// </summary>
    /// <param name="entry"></param>
    void Dispatch(DispatchEntry<TEntry> entry);

    /// <summary>
    /// 订阅分发
    /// </summary>
    /// <param name="callback"></param>
    void Subscribe(Func<DispatchEntry<TEntry>, Task> callback);

    /// <summary>
    /// 取消订阅分发
    /// </summary>
    /// <param name="callback"></param>
    /// <exception cref="NotImplementedException"></exception>
    void UnSubscribe(Func<DispatchEntry<TEntry>, Task> callback);
}
