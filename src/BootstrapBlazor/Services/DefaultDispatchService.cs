﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
