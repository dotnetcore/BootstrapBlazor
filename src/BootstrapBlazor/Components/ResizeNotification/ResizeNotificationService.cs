// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// 网页尺寸变化通知服务
/// </summary>
public class ResizeNotificationService
{
    private ConcurrentDictionary<object, Func<BreakPoint, Task>> Cache { get; } = new();

    /// <summary>
    /// 订阅网页尺寸变化通知
    /// </summary>
    /// <param name="target"></param>
    /// <param name="callback"></param>
    public void Subscribe(object target, Func<BreakPoint, Task> callback) => Cache.AddOrUpdate(target, k => callback, (k, v) => callback);

    /// <summary>
    /// 取消网页尺寸变化通知
    /// </summary>
    /// <param name="target"></param>
    public void Unsubscribe(object target) => Cache.TryRemove(target, out _);

    /// <summary>
    /// 获得 当前值
    /// </summary>
    public BreakPoint CurrentValue { get; private set; }

    /// <summary>
    /// 内部调用
    /// </summary>
    /// <param name="breakPoint"></param>
    /// <returns></returns>
    internal async Task InvokeAsync(BreakPoint breakPoint)
    {
        CurrentValue = breakPoint;

        foreach (var cb in Cache.Values)
        {
            await cb(breakPoint);
        }
    }
}
