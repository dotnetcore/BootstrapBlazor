// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultDispatchService<TEntry> : IDispatchService<TEntry>
{
    public void Dispatch(DispatchEntry<TEntry> payload)
    {
        lock (locker)
        {
            Cache.ForEach(cb =>
            {
                cb(payload);
            });
        }
    }

    public void Subscribe(Func<DispatchEntry<TEntry>, Task> callback)
    {
        lock (locker)
        {
            Cache.Add(callback);
        }
    }

    public void UnSubscribe(Func<DispatchEntry<TEntry>, Task> callback)
    {
        lock (locker)
        {
            Cache.Remove(callback);
        }
    }

    private List<Func<DispatchEntry<TEntry>, Task>> Cache { get; } = new(50);

    private readonly object locker = new();
}
