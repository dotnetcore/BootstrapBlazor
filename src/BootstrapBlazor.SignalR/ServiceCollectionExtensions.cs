// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Components;

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
    public static IServiceCollection AddBootstrapBlazorDispatchClient(this IServiceCollection services, string hubPath = "dispatch.hub")
    {
        ArgumentException.ThrowIfNullOrEmpty(hubPath);
        services.AddSingleton(provider => new SignalRDispatchConnection(
            provider.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>(),
            provider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<SignalRDispatchConnection>>(),
            hubPath));
        services.AddSingleton(typeof(IDispatchService<>), typeof(DefaultDispatchService<>));
        return services;
    }
}
