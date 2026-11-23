// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components.Pages;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace UnitTestDocs;

public class OnlineTest
{
    [Fact]
    public void Online_UsesConfiguredTimeoutWithCustomService()
    {
        var now = DateTimeOffset.Now;
        var service = new CustomConnectionService(
        [
            new ConnectionItem { Id = "active", LastBeatTime = now.AddSeconds(-20) },
            new ConnectionItem { Id = "expired", LastBeatTime = now.AddMinutes(-2) }
        ]);
        var page = new Online();
        var type = typeof(Online);
        type.GetProperty("ConnectionService", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(page, service);
        type.GetProperty("Options", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(page,
            Options.Create(new BootstrapBlazorOptions
            {
                ConnectionHubOptions = new() { TimeoutInterval = TimeSpan.FromMinutes(1) }
            }));

        var buildContext = type.GetMethod("BuildContext", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var getNextRefreshDelay = type.GetMethod("GetNextRefreshDelay", BindingFlags.NonPublic | BindingFlags.Instance)!;
        buildContext.Invoke(page, [now]);

        var items = Assert.IsType<List<ConnectionItem>>(type.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(page));
        var active = Assert.Single(items);
        Assert.Equal("active", active.Id);
        var getTimeUntilRemoval = type.GetMethod("GetTimeUntilRemoval", BindingFlags.NonPublic | BindingFlags.Instance)!;
        Assert.Equal("00:00:40", getTimeUntilRemoval.Invoke(page, [active]));
        Assert.Equal(TimeSpan.FromSeconds(10), getNextRefreshDelay.Invoke(page, [now]));

        var justBeforeTimeout = now.AddMilliseconds(39500);
        buildContext.Invoke(page, [justBeforeTimeout]);
        Assert.Same(active, Assert.Single(items));
        Assert.Equal("00:00:01", getTimeUntilRemoval.Invoke(page, [active]));
        Assert.Equal(TimeSpan.FromSeconds(1), getNextRefreshDelay.Invoke(page, [justBeforeTimeout]));

        buildContext.Invoke(page, [justBeforeTimeout.AddSeconds(1)]);
        Assert.Empty(items);
    }

    [Fact]
    public void Online_LargeTimeoutDoesNotOverflow()
    {
        var now = DateTimeOffset.Now;
        var service = new CustomConnectionService([new ConnectionItem { Id = "active", LastBeatTime = now.AddSeconds(1) }]);
        var page = new Online();
        var type = typeof(Online);
        type.GetProperty("ConnectionService", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(page, service);
        type.GetProperty("Options", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(page,
            Options.Create(new BootstrapBlazorOptions { ConnectionHubOptions = new() { TimeoutInterval = TimeSpan.MaxValue } }));

        type.GetMethod("BuildContext", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(page, [now]);

        var items = Assert.IsType<List<ConnectionItem>>(type.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(page));
        var active = Assert.Single(items);
        Assert.Equal("active", active.Id);
        var remaining = type.GetMethod("GetTimeUntilRemoval", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(page, [active]);
        Assert.Equal("10675199.02:48:05", remaining);
    }

    private sealed class CustomConnectionService(ICollection<ConnectionItem> connections) : IConnectionService
    {
        public long Count => connections.Count;
        public ICollection<ConnectionItem> Connections => connections;
        public Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default) => Task.CompletedTask;
        public bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value)
        {
            value = connections.FirstOrDefault(item => item.Id == key);
            return value != null;
        }
    }
}
