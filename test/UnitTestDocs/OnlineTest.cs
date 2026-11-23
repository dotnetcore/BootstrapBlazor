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
            new ConnectionItem { Id = "active", LastBeatTime = now },
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

        type.GetMethod("BuildContext", BindingFlags.NonPublic | BindingFlags.Instance)!.Invoke(page, null);

        var items = Assert.IsType<List<ConnectionItem>>(type.GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(page));
        Assert.Equal("active", Assert.Single(items).Id);
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
