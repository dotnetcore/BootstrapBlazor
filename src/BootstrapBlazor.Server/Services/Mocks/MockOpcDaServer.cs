// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.OpcDa;

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

    /// <summary>
    /// 浏览 OPC Server 中的位号 (即数据项或者标签)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="filters"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public OpcBrowseElement[] Browse(string name, OpcBrowseFilters filters, out OpcBrowsePosition? position)
    {
        position = null;
        if (string.IsNullOrEmpty(name))
        {
            return [
                new OpcBrowseElement()
                {
                    Name ="Channel1",
                    ItemName = "Channel1",
                    IsItem = false,
                    HasChildren = true
                },
                new OpcBrowseElement()
                {
                    Name ="Channel2",
                    ItemName = "Channel2",
                    IsItem = false,
                    HasChildren = true
                }
            ];
        }

        if (name == "Channel1")
        {
            return [
                new OpcBrowseElement()
                {
                    Name ="Device1",
                    ItemName = "Channel1.Device1",
                    IsItem = false,
                    HasChildren = true
                }
            ];
        }

        if (name == "Channel1.Device1")
        {
            return [
                new OpcBrowseElement()
                {
                    Name ="Tag1",
                    ItemName = "Channel1.Device1.Tag1",
                    IsItem = true,
                    HasChildren = false
                },
                new OpcBrowseElement()
                {
                    Name ="Tag2",
                    ItemName = "Channel1.Device1.Tag2",
                    IsItem = true,
                    HasChildren = false
                }
            ];
        }

        return [];
    }

    /// <summary>
    /// 浏览 OPC Server 中的位号 (即数据项或者标签)
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public OpcBrowseElement[] BrowseNext(OpcBrowsePosition position)
    {
        return [];
    }

    public void Dispose()
    {

    }
}
