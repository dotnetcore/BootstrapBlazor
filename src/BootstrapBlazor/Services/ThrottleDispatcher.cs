// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 限流器泛型类
/// </summary>
public class ThrottleDispatcher(ThrottleOptions? options = null)
{
    private readonly object _locker = new();
    private Task? _lastTask;
    private DateTime? _invokeTime;
    private bool _busy;
    private readonly ThrottleOptions _options = options ?? new();

    private bool ShouldWait() => _invokeTime.HasValue && (DateTime.UtcNow - _invokeTime.Value).TotalMilliseconds < _options.Interval;

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

    /// <summary>
    /// 限流异步方法
    /// </summary>
    /// <param name="function">异步回调方法</param>
    /// <param name="cancellationToken">取消令牌</param>
    private Task InternalThrottleAsync(Func<Task> function, CancellationToken cancellationToken = default)
    {
        lock (_locker)
        {
            if (_lastTask != null && (_busy || ShouldWait()))
            {
                return _lastTask;
            }

            _busy = true;
            _invokeTime = DateTime.UtcNow;
            _lastTask = function();

            _lastTask.ContinueWith(task =>
            {
                if (_options.DelayAfterExecution)
                {
                    _invokeTime = DateTime.UtcNow;
                }
                _busy = false;
            }, cancellationToken);

            if (_options.ResetIntervalOnException)
            {
                _lastTask.ContinueWith((task, obj) =>
                {
                    _lastTask = null;
                    _invokeTime = null;
                }, cancellationToken, TaskContinuationOptions.OnlyOnFaulted);
            }
            return _lastTask;
        }
    }
}
