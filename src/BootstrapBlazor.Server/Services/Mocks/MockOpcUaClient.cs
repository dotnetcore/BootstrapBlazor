// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Opc.Ua;

namespace BootstrapBlazor.OpcUa;

/// <summary>
/// 模拟 OpcUa Client 实现类
/// </summary>
sealed class MockOpcUaClient : IOpcUaClient
{
    private readonly Dictionary<string, IOpcUaSubscription> _subscriptions = [];

    public bool IsConnected { get; private set; }

    public string? EndpointUrl { get; private set; }

    public Task<bool> ConnectAsync(string endpointUrl, OpcUaConnectionOptions? options = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        EndpointUrl = endpointUrl;
        IsConnected = true;
        return Task.FromResult(true);
    }

    public Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        foreach (var subscription in _subscriptions.Values)
        {
            DisposeSubscription(subscription);
        }
        _subscriptions.Clear();
        EndpointUrl = null;
        IsConnected = false;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<OpcUaReadItem>> ReadAsync(IEnumerable<string> nodeIds, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var timestamp = DateTime.UtcNow;
        IReadOnlyList<OpcUaReadItem> items = [.. nodeIds.Select(nodeId => new OpcUaReadItem(
            nodeId,
            Random.Shared.Next(1000, 2000),
            Opc.Ua.StatusCodes.Good,
            timestamp,
            timestamp))];
        return Task.FromResult(items);
    }

    public Task<IReadOnlyList<OpcUaWriteItem>> WriteAsync(IEnumerable<OpcUaWriteItem> items, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<OpcUaWriteItem>>([.. items]);
    }

    public Task<IReadOnlyList<OpcUaBrowseElement>> BrowseAsync(string nodeId, OpcUaBrowseOptions? options = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IReadOnlyList<OpcUaBrowseElement> items = nodeId switch
        {
            "i=85" =>
            [
                new("ns=2;s=Channel1", "Channel1", "Channel1", NodeClass.Object, "i=35", "i=61"),
                new("ns=2;s=Channel2", "Channel2", "Channel2", NodeClass.Object, "i=35", "i=61")
            ],
            "ns=2;s=Channel1" =>
            [
                new("ns=2;s=Channel1.Device1", "Device1", "Device1", NodeClass.Object, "i=35", "i=61")
            ],
            "ns=2;s=Channel1.Device1" =>
            [
                new("ns=2;s=Channel1.Device1.Tag1", "Tag1", "Tag1", NodeClass.Variable, "i=47", "i=63"),
                new("ns=2;s=Channel1.Device1.Tag2", "Tag2", "Tag2", NodeClass.Variable, "i=47", "i=63")
            ],
            _ => []
        };
        return Task.FromResult(items);
    }

    public Task<IOpcUaSubscription> CreateSubscriptionAsync(string name, int publishingInterval = 1000, bool active = true, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_subscriptions.TryGetValue(name, out var subscription))
        {
            DisposeSubscription(subscription);
        }

        subscription = new MockOpcUaSubscription(name, publishingInterval, active);
        _subscriptions[name] = subscription;
        return Task.FromResult(subscription);
    }

    public Task CancelSubscriptionAsync(IOpcUaSubscription subscription, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _subscriptions.Remove(subscription.Name);
        DisposeSubscription(subscription);
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
        GC.SuppressFinalize(this);
    }

    private static void DisposeSubscription(IOpcUaSubscription subscription)
    {
        if (subscription is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
