// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Console 组件消息辅助类</para>
/// <para lang="en">Console component message helper class</para>
/// </summary>
public class ConsoleMessageCollection(int maxCount = 2000) : IEnumerable<ConsoleMessageItem>, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 最大记录数 默认 2000</para>
    /// <para lang="en">Get/Set max record count, default is 2000</para>
    /// </summary>
    public int MaxCount { get; set; } = maxCount;

    private readonly AutoResetEvent _locker = new(true);

    private readonly ConcurrentQueue<ConsoleMessageItem> _messages = new();

    /// <summary>
    /// <para lang="zh">添加方法</para>
    /// <para lang="en">Add method</para>
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
    /// <para lang="zh">清空集合方法</para>
    /// <para lang="en">Clear collection method</para>
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
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _locker.Dispose();
        }
    }

    /// <summary>
    /// <para lang="zh">销毁资源</para>
    /// <para lang="en">Dispose resources</para>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
