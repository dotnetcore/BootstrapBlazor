// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal class DefaultDispatchService<TEntry> : IDispatchService<TEntry>
{
    public Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        lock (_locker)
        {
            Cache.ForEach(cb =>
            {
                cb(entry);
            });
        }

        return Task.CompletedTask;
    }

    public Task SubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        lock (_locker)
        {
            Cache.Add(callback);
        }

        return Task.CompletedTask;
    }

    public Task UnSubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        lock (_locker)
        {
            Cache.Remove(callback);
        }

        return Task.CompletedTask;
    }

    private List<Func<DispatchEntry<TEntry>, Task>> Cache { get; } = new(50);

#if NET9_0_OR_GREATER
    private readonly Lock _locker = new();
#else
    private readonly object _locker = new();
#endif
}
