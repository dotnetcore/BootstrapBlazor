// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.OpcUa;

sealed class MockOpcUaSubscription : IOpcUaSubscription, IDisposable
{
    private readonly int _publishingInterval;
    private readonly bool _active;
    private readonly List<string> _items = [];
    private readonly Dictionary<string, object?> _lastValues = [];
    private CancellationTokenSource? _cancellationTokenSource;

    public MockOpcUaSubscription(string name, int publishingInterval, bool active)
    {
        Name = name;
        _publishingInterval = Math.Max(1, publishingInterval);
        _active = active;
        _cancellationTokenSource = new CancellationTokenSource();
        _ = Task.Run(() => RunAsync(_cancellationTokenSource.Token));
    }

    public string Name { get; }

    public bool KeepLastValue { get; set; }

    public Action<IReadOnlyList<OpcUaReadItem>>? DataChanged { get; set; }

    public Task AddItemsAsync(IEnumerable<string> nodeIds, int samplingInterval = -1, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _items.AddRange(nodeIds);
        return Task.CompletedTask;
    }

    private void UpdateValues()
    {
        var timestamp = DateTime.UtcNow;
        var values = _items.Select(nodeId =>
        {
            var value = Random.Shared.Next(1000, 2000);
            _lastValues.TryGetValue(nodeId, out var lastValue);
            _lastValues[nodeId] = value;
            return new OpcUaReadItem(nodeId, value, Opc.Ua.StatusCodes.Good, timestamp, timestamp)
            {
                LastValue = KeepLastValue ? lastValue : null
            };
        }).ToList();
        DataChanged?.Invoke(values);
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (_active)
                {
                    UpdateValues();
                }
                await Task.Delay(_publishingInterval, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing && _cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
