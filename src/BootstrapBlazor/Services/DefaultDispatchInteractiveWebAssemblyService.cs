// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

sealed class DefaultDispatchInteractiveWebAssemblyService<TEntry> : IDispatchService<TEntry>, IAsyncDisposable
{
    private static readonly TimeSpan _requestTimeout = TimeSpan.FromSeconds(10);

    private static readonly TimeSpan _pollingInterval = TimeSpan.FromSeconds(3);

    private const string DispatchIdHeaderName = "X-Dispatch-Id";

    private string? _lastDispatchId;

    private readonly SemaphoreSlim _requestLocker = new(1, 1);

    private readonly List<Func<DispatchEntry<TEntry>, Task>> _cache = new(50);

#if NET9_0_OR_GREATER
    private readonly Lock _locker = new();
#else
    private readonly object _locker = new();
#endif

    private readonly ILogger<DefaultDispatchInteractiveWebAssemblyService<TEntry>> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly CancellationTokenSource _pollingCancellation = new();
    private readonly Task _pollingTask;
    private readonly string _entryType = Uri.EscapeDataString(typeof(TEntry).FullName ?? typeof(TEntry).Name);
    private ConnectionSnapshot _snapshot = new();
    private NavigationManager _navigationManager;

    public DefaultDispatchInteractiveWebAssemblyService(NavigationManager navigationManager,
        IHttpClientFactory httpClientFactory,
        ILogger<DefaultDispatchInteractiveWebAssemblyService<TEntry>> logger)
    {
        _navigationManager = navigationManager;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _pollingTask = PollAsync(_pollingCancellation.Token);
    }

    private async Task PollAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient();
            client.Timeout = _requestTimeout;
            client.BaseAddress = new Uri(_navigationManager.BaseUri);
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    DispatchEntry<TEntry>? dispatchEntry = null;
                    await _requestLocker.WaitAsync(cancellationToken);
                    try
                    {
                        using var response = await client.GetAsync($"dispatch.axd/fetch?type={_entryType}", cancellationToken);
                        var dispatchId = GetDispatchId(response);
                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // No cached entry on the server.
                        }
                        else if (!response.IsSuccessStatusCode)
                        {
                            var error = await response.Content.ReadAsStringAsync(cancellationToken);
                            _logger.LogError("Dispatch fetch request failed with HTTP {StatusCode}: {Error}", (int)response.StatusCode, error);
                        }
                        else if (!string.IsNullOrEmpty(dispatchId) && dispatchId == _lastDispatchId)
                        {
                            // The cached entry was already handled locally or by a previous fetch.
                        }
                        else
                        {
                            dispatchEntry = await response.Content.ReadFromJsonAsync<DispatchEntry<TEntry>>(cancellationToken: cancellationToken);
                            if (dispatchEntry == null)
                            {
                                _logger.LogError("Dispatch fetch request returned null.");
                            }
                            else
                            {
                                _lastDispatchId = dispatchId;
                            }
                        }
                    }
                    finally
                    {
                        _requestLocker.Release();
                    }

                    if (dispatchEntry != null)
                    {
                        await DispatchCoreAsync(dispatchEntry);
                    }
                }
                catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "Unable to fetch dispatch items");
                }
                await Task.Delay(_pollingInterval, cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private static string? GetDispatchId(HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues(DispatchIdHeaderName, out var values))
        {
            return null;
        }

        return values.FirstOrDefault();
    }

    public async Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient();
            client.Timeout = _requestTimeout;
            client.BaseAddress = new Uri(_navigationManager.BaseUri);
            await _requestLocker.WaitAsync(cancellationToken);
            try
            {
                using var response = await client.PostAsJsonAsync($"dispatch.axd?type={_entryType}", entry, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new HttpRequestException($"Dispatch request failed with HTTP {(int)response.StatusCode}: {error}", null, response.StatusCode);
                }

                _lastDispatchId = GetDispatchId(response)
                    ?? throw new HttpRequestException("Dispatch response did not include a dispatch identifier.");
            }
            finally
            {
                _requestLocker.Release();
            }

            await DispatchCoreAsync(entry);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to dispatch items.");
            throw;
        }
    }

    private async Task DispatchCoreAsync(DispatchEntry<TEntry> entry)
    {
        Func<DispatchEntry<TEntry>, Task>[] callbacks;
        lock (_locker)
        {
            callbacks = _cache.ToArray();
        }

        foreach (var callback in callbacks)
        {
            try
            {
                await callback(entry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A dispatch subscriber failed for entry type {EntryType}.", typeof(TEntry));
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

    public async ValueTask DisposeAsync()
    {
        _pollingCancellation.Cancel();
        try
        {
            await _pollingTask;
        }
        finally
        {
            _pollingCancellation.Dispose();
        }
    }
}
