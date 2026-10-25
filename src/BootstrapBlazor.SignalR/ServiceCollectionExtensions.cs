// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Optional client-side SignalR dispatch registration.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Use one shared Hub connection for all dispatch types.
    /// Call after AddBootstrapBlazor in the WebAssembly host.
    /// Offline sends fail; messages are not queued or replayed.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hubPath"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorDispatchClient(this IServiceCollection services, string hubPath = "bb.hub")
    {
        ArgumentException.ThrowIfNullOrEmpty(hubPath);
        services.AddSignalRConnection(hubPath);
        services.AddSingleton(typeof(IDispatchService<>), typeof(DefaultDispatchService<>));
        return services;
    }

    /// <summary>
    /// Register WebAssembly heartbeat reporting and optional connection snapshot polling.
    /// Shares the Hub connection with dispatch. Call after AddBootstrapBlazor.
    /// Snapshot queries must also be enabled and authorized on the server.
    /// Use the same hubPath for connection and dispatch registration.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="hubPath"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorConnectionClient(this IServiceCollection services, string hubPath = "bb.hub")
    {
        ArgumentException.ThrowIfNullOrEmpty(hubPath);
        services.AddSignalRConnection(hubPath);
        services.AddSingleton<DefaultConnectionService>();
        services.AddSingleton<IConnectionService>(provider => provider.GetRequiredService<DefaultConnectionService>());
        return services;
    }

    private static void AddSignalRConnection(this IServiceCollection services, string hubPath)
        => services.TryAddSingleton(provider => new SignalRConnection(
            provider.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>(),
            provider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<SignalRConnection>>(),
            hubPath));
}
