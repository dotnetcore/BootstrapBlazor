// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
