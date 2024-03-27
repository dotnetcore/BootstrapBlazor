// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 限流器泛型类
/// </summary>
public class ThrottleDispatcher(ThrottleOptions options)
{
    private readonly object _locker = new();
    private Task? _lastTask;
    private DateTime? _invokeTime;
    private bool _busy;

    /// <summary>
    /// 判断是否等待方法
    /// </summary>
    /// <returns></returns>
    protected bool ShouldWait() => _busy || _invokeTime.HasValue && (DateTime.UtcNow - _invokeTime.Value).TotalMilliseconds < options.Interval;

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
    /// <param name="cancellationToken">取消令牌</param>
    public void Throttle(Action action, CancellationToken cancellationToken = default) => InternalThrottleAsync(() =>
    {
        action();
        return Task.CompletedTask;
    }, cancellationToken);

    private Task LastTask => _lastTask ?? Task.CompletedTask;

    /// <summary>
    /// 限流异步方法
    /// </summary>
    /// <param name="function">异步回调方法</param>
    /// <param name="cancellationToken">取消令牌</param>
    private Task InternalThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default)
    {
        if (ShouldWait())
        {
            return LastTask;
        }

        lock (_locker)
        {
            if (ShouldWait())
            {
                return LastTask;
            }

            _busy = true;
            _invokeTime = DateTime.UtcNow;
            _lastTask = function();
            _lastTask.ContinueWith(_ =>
            {
                if (options.DelayAfterExecution)
                {
                    _invokeTime = DateTime.UtcNow;
                }
                _busy = false;
            }, cancellationToken);
            return _lastTask;
        }
    }
}
