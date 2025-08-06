// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 模拟 OpcDa Server 实现类
/// </summary>
sealed class MockOpcDaServer : IOpcDaServer
{
    public bool IsConnected { get; set; }

    public string? ServerName { get; set; }

    private readonly Dictionary<string, IOpcSubscription> _subscriptions = [];

    public bool Connect(string serverName)
    {
        ServerName = serverName;
        IsConnected = true;
        return true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        ServerName = null;
    }

    public IOpcSubscription CreateSubscription(string name, int updateRate = 1000, bool active = true)
    {
        if (_subscriptions.TryGetValue(name, out var subscription))
        {
            CancelSubscription(subscription);
        }

        subscription = new MockOpcDaSubscription(name, updateRate, active);
        _subscriptions.Add(name, subscription);
        return subscription;
    }

    public void CancelSubscription(IOpcSubscription subscription)
    {
        _subscriptions.Remove(subscription.Name);
        if (subscription is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public HashSet<OpcReadItem> Read(params HashSet<string> items)
    {
        return items.Select(i => new OpcReadItem(i, Quality.Good, DateTime.Now, Random.Shared.Next(1000, 2000)))
            .ToHashSet(OpcItemEqualityComparer<OpcReadItem>.Default);
    }

    public HashSet<OpcWriteItem> Write(params HashSet<OpcWriteItem> items)
    {
        return items.Select(i => new OpcWriteItem(i.Name, i.Value) { Result = true })
            .ToHashSet(OpcItemEqualityComparer<OpcWriteItem>.Default);
    }

    public void Dispose()
    {
    }
}

class MockOpcDaSubscription : IOpcSubscription, IDisposable
{
    private readonly int _updateRate;
    private readonly bool _active;
    private readonly List<string> _items = [];
    private CancellationTokenSource? _cts;

    public MockOpcDaSubscription(string name, int updateRate = 1000, bool active = true)
    {
        Name = name;
        _updateRate = updateRate;
        _active = active;

        _cts = new CancellationTokenSource();
        _ = Task.Run(() => DoTask(_cts.Token));
    }

    public string Name { get; }

    public bool KeepLastValue { get; set; }

    public Action<List<OpcReadItem>>? DataChanged { get; set; }

    public void AddItems(IEnumerable<string> items)
    {
        _items.AddRange(items);
    }

    private void UpdateValues()
    {
        if (DataChanged != null)
        {
            var values = _items.Select(i => new OpcReadItem(i, Quality.Good, DateTime.Now, Random.Shared.Next(1000, 2000))).ToList();
            DataChanged.Invoke(values);
        }
    }

    private async Task DoTask(CancellationToken token)
    {
        do
        {
            try
            {
                if (_active)
                {
                    UpdateValues();
                }

                await Task.Delay(_updateRate, token);
            }
            catch (OperationCanceledException) { }
            catch (Exception)
            {
                // ignored
            }
        }
        while (!token.IsCancellationRequested);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
