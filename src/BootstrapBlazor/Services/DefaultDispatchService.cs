// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

sealed class DefaultDispatchService<TEntry>(ILogger<DefaultDispatchService<TEntry>> logger) : IDispatchService<TEntry>
{
    private readonly List<Func<DispatchEntry<TEntry>, Task>> _cache = new(50);

#if NET9_0_OR_GREATER
    private readonly Lock _locker = new();
#else
    private readonly object _locker = new();
#endif

    public async Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Func<DispatchEntry<TEntry>, Task>[] callbacks;
        lock (_locker)
        {
            callbacks = _cache.ToArray();
        }

        foreach (var callback in callbacks)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await callback(entry);
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
            _cache.Add(callback);
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
            _cache.Remove(callback);
        }

        return Task.CompletedTask;
    }
}
