// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BootstrapBlazor.Components;

class DefaultConnectionWebAssemblyService : IConnectionService, IAsyncDisposable
{
    private readonly ILogger<DefaultConnectionWebAssemblyService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Uri _url;
    private readonly CancellationTokenSource _pollingCancellation = new();
    private readonly Task _pollingTask;
    private ConnectionSnapshot _snapshot = new();

    public DefaultConnectionWebAssemblyService(NavigationManager navigationManager,
        IHttpClientFactory httpClientFactory,
        ILogger<DefaultConnectionWebAssemblyService> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _url = new Uri(new Uri(navigationManager.BaseUri), "connection.axd/snapshot");
        _pollingTask = PollAsync(_pollingCancellation.Token);
    }

    public long Count => _snapshot.Count;

    public ICollection<ConnectionItem> Connections => _snapshot.Connections;

    public bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value)
    {
        value = _snapshot.Connections.FirstOrDefault(item => item.Id == key);
        return value != null;
    }

    public void AddOrUpdate(ClientInfo client) =>
        throw new PlatformNotSupportedException("Use ConnectionHub to report WebAssembly heartbeats to the server.");

    private async Task PollAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var response = await client.GetAsync(_url, cancellationToken);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync(cancellationToken);
                        _logger.LogError("Connection snapshot request failed with HTTP {StatusCode}: {Error}", (int)response.StatusCode, error);
                    }
                    else
                    {
                        var snapshot = await response.Content.ReadFromJsonAsync<ConnectionSnapshot>(cancellationToken: cancellationToken);
                        if (snapshot == null)
                        {
                            _logger.LogError("Connection snapshot request returned null.");
                            continue;
                        }

                        // Update the snapshot
                        _snapshot = snapshot;
                    }
                }
                catch (Exception ex) when (ex is HttpRequestException or JsonException ||
                    ex is OperationCanceledException && !cancellationToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "Unable to refresh connection snapshot from {Url}.", _url);
                }
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
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
