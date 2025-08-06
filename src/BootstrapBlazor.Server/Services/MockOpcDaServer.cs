// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 模拟 OpcDa Server 实现类
/// </summary>
sealed class MockOpcDaServer : IOpcServer
{
    public bool IsConnected { get; set; }

    public string? ServerName { get; set; }

    public bool Connect(string serverName)
    {
        ServerName = serverName;
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        ServerName = null;
    }

    public ISubscription CreateSubscription(string name, int updateRate = 1000, bool active = true)
    {

    }

    public void CancelSubscription(ISubscription subscription)
    {

    }

    public HashSet<OpcReadItem> Read(params HashSet<string> items)
    {

    }

    public HashSet<OpcWriteItem> Write(params HashSet<OpcWriteItem> items)
    {

    }

    public void Dispose()
    {

    }
}

class MockOpcDaSubscription : ISubscription
{
    public bool KeepLastValue { get; set; }

    public Action<List<OpcReadItem>>? DataChanged { get; set; }

    public Opc.Da.ISubscription GetSubscription()
    {
        throw new NotImplementedException();
    }

    public void AddItems(IEnumerable<string> items)
    {
        throw new NotImplementedException();
    }
}
