// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Components;

sealed class DefaultDispatchInteractiveServerService<TEntry> : IDispatchService<TEntry>, IAsyncDisposable
{
    private static readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web);

    private static readonly string _entryType = typeof(TEntry).FullName ?? typeof(TEntry).Name;

    private readonly List<Func<DispatchEntry<TEntry>, Task>> _cache = new(50);

#if NET9_0_OR_GREATER
    private readonly Lock _locker = new();
#else
    private readonly object _locker = new();
#endif

    private readonly DispatchServiceCache _dispatchCache;

    public DefaultDispatchInteractiveServerService(DispatchServiceCache dispatchCache)
    {
        _dispatchCache = dispatchCache;
        _dispatchCache.OnDispatchAsync += OnDispatchAsync;
    }

    private Task OnDispatchAsync(string entryType, JsonElement entry)
    {
        if (!string.Equals(entryType, _entryType, StringComparison.Ordinal))
        {
            return Task.CompletedTask;
        }

        var dispatchEntry = entry.Deserialize<DispatchEntry<TEntry>>(_serializerOptions)
            ?? throw new JsonException("The dispatch entry must not be null.");
        return DispatchCoreAsync(dispatchEntry, CancellationToken.None);
    }

    public async Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // 增加到缓存中，供 WebAssembly 客户端拉取
        _dispatchCache.Set(entry, triggerDispatch: false);

        await DispatchCoreAsync(entry, cancellationToken);
    }

    private async Task DispatchCoreAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken)
    {
        Func<DispatchEntry<TEntry>, Task>[] callbacks;
        lock (_locker)
        {
            callbacks = _cache.ToArray();
        }

        foreach (var callback in callbacks)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await callback(entry);
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

    public ValueTask DisposeAsync()
    {
        _dispatchCache.OnDispatchAsync -= OnDispatchAsync;
        return ValueTask.CompletedTask;
    }
}
