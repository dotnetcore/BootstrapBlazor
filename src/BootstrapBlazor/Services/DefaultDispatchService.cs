// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

sealed class DefaultDispatchService<TEntry>(ILogger<DefaultDispatchService<TEntry>> logger) : IDispatchService<TEntry>
{
    private readonly List<Subscription> _cache = new(50);

#if NET9_0_OR_GREATER
    private readonly Lock _locker = new();
#else
    private readonly object _locker = new();
#endif

    public async Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Subscription[] subscriptions;
        lock (_locker)
        {
            subscriptions = _cache.ToArray();
        }

        foreach (var subscription in subscriptions)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                Task callbackTask;
                lock (_locker)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (!_cache.Contains(subscription))
                    {
                        continue;
                    }

                    // Coordinate callback startup with unsubscription.
                    callbackTask = subscription.Callback(entry);
                }

                await callbackTask.WaitAsync(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
            {
                logger.LogError(ex, "A dispatch subscriber failed for entry type {EntryType}.", typeof(TEntry));
            }
        }
    }

    public Task SubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        lock (_locker)
        {
            _cache.Add(new Subscription(callback));
        }

        return Task.CompletedTask;
    }

    public Task UnSubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        lock (_locker)
        {
            var index = _cache.FindIndex(subscription => subscription.Callback == callback);
            if (index >= 0)
            {
                _cache.RemoveAt(index);
            }
        }

        return Task.CompletedTask;
    }

    private sealed class Subscription(Func<DispatchEntry<TEntry>, Task> callback)
    {
        public Func<DispatchEntry<TEntry>, Task> Callback { get; } = callback;
    }
}
