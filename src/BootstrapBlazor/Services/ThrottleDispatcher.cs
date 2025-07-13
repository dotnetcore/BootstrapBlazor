// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 限流器泛型类
/// </summary>
public class ThrottleDispatcher(ThrottleOptions options)
{
    private DateTime? _invokeTime;

    /// <summary>
    /// 判断是否等待方法
    /// </summary>
    /// <returns></returns>
    protected virtual bool ShouldWait() => _invokeTime.HasValue && (DateTime.UtcNow - _invokeTime.Value) < options.Interval;

    /// <summary>
    /// 异步限流方法
    /// </summary>
    /// <param name="function">异步回调方法</param>
    /// <param name="cancellationToken">取消令牌</param>
    public Task ThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default) => InternalThrottleAsync(function, cancellationToken);

    /// <summary>
    /// 同步限流方法
    /// </summary>
    /// <param name="action">同步回调方法</param>
    /// <param name="token">取消令牌</param>
    public void Throttle(Action action, CancellationToken token = default)
    {
        var task = InternalThrottleAsync(() =>
        {
            action();
            return Task.CompletedTask;
        }, token);
        task.Wait(token);
        return;
    }

    /// <summary>
    /// 限流异步方法
    /// </summary>
    /// <param name="function">异步回调方法</param>
    /// <param name="cancellationToken">取消令牌</param>
    private async Task InternalThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default)
    {
        if (ShouldWait())
        {
            return;
        }

        _invokeTime = DateTime.UtcNow;

        try
        {
            await function();
            if (options.DelayAfterExecution)
            {
                _invokeTime = DateTime.UtcNow;
            }
        }
        catch
        {
            if (options.ResetIntervalOnException)
            {
                _invokeTime = null;
            }
        }
    }
}
