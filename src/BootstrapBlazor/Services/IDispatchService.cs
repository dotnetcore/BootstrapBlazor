// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">分发服务接口</para>
/// <para lang="en">Dispatcher Service Interface</para>
/// </summary>
public interface IDispatchService<TEntry>
{
    /// <summary>
    /// <para lang="zh">分发方法</para>
    /// <para lang="en">Dispatch Method</para>
    /// </summary>
    /// <param name="entry"></param>
    void Dispatch(DispatchEntry<TEntry> entry);

    /// <summary>
    /// <para lang="zh">订阅分发</para>
    /// <para lang="en">Subscribe Dispatch</para>
    /// </summary>
    /// <param name="callback"></param>
    void Subscribe(Func<DispatchEntry<TEntry>, Task> callback);

    /// <summary>
    /// <para lang="zh">取消订阅分发</para>
    /// <para lang="en">Unsubscribe Dispatch</para>
    /// </summary>
    /// <param name="callback"></param>
    /// <exception cref="NotImplementedException"></exception>
    void UnSubscribe(Func<DispatchEntry<TEntry>, Task> callback);
}
