// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.Collections;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// Console 组件消息辅助类
/// </summary>
public class ConsoleMessageCollection(int maxCount = 2000) : IEnumerable<ConsoleMessageItem>, IDisposable
{
    /// <summary>
    /// 获得/设置 最大记录数 默认 2000
    /// </summary>
    public int MaxCount { get; set; } = maxCount;

    private readonly AutoResetEvent _locker = new(true);

    private readonly ConcurrentQueue<ConsoleMessageItem> _messages = new();

    /// <summary>
    /// 添加方法
    /// </summary>
    /// <param name="item"></param>
    public void Add(ConsoleMessageItem item)
    {
        _locker.WaitOne();
        _messages.Enqueue(item);
        if (_messages.Count > MaxCount)
        {
            _messages.TryDequeue(out var _);
        }
        _locker.Set();
    }

    /// <summary>
    /// 清空集合方法
    /// </summary>
    public void Clear()
    {
        _locker.WaitOne();
        _messages.Clear();
        _locker.Set();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public IEnumerator<ConsoleMessageItem> GetEnumerator() => _messages.GetEnumerator();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _locker.Dispose();
        }
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
