// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">限流器泛型类</para>
/// <para lang="en">Throttle Dispatcher Generic Class</para>
/// </summary>
public class ThrottleDispatcher(ThrottleOptions options)
{
    private DateTime? _invokeTime;

    /// <summary>
    /// <para lang="zh">判断是否等待方法</para>
    /// <para lang="en">Check if Should Wait Method</para>
    /// </summary>
    protected virtual bool ShouldWait() => _invokeTime.HasValue && (DateTime.UtcNow - _invokeTime.Value) < options.Interval;

    /// <summary>
    /// <para lang="zh">异步限流方法</para>
    /// <para lang="en">Throttle Async Method</para>
    /// </summary>
    /// <param name="function"><para lang="zh">异步回调方法</para><para lang="en">Async Callback Function</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌</para><para lang="en">Cancellation Token</para></param>
    public Task ThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default) => InternalThrottleAsync(function, cancellationToken);

    /// <summary>
    /// <para lang="zh">同步限流方法</para>
    /// <para lang="en">Throttle Method</para>
    /// </summary>
    /// <param name="action"><para lang="zh">同步回调方法</para><para lang="en">Sync Callback Action</para></param>
    /// <param name="token"><para lang="zh">取消令牌</para><para lang="en">Cancellation Token</para></param>
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
    /// <para lang="zh">限流异步方法</para>
    /// <para lang="en">Internal Throttle Async Method</para>
    /// </summary>
    /// <param name="function"><para lang="zh">异步回调方法</para><para lang="en">异步callback method</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌</para><para lang="en">取消令牌</para></param>
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
