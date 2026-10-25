// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

sealed class DefaultConnectionHeartbeatService(
    IConnectionService connectionService,
    IOptions<BootstrapBlazorOptions> options,
    IIpLocatorFactory ipLocatorFactory,
    IServiceProvider provider) : IConnectionHeartbeatService
{
    private IIpLocatorProvider? _ipLocatorProvider;

    public async Task<bool> ReportHeartbeatAsync(ClientInfo client, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (!options.Value.ConnectionHubOptions.Enable)
        {
            return false;
        }

        if (OperatingSystem.IsBrowser())
        {
            throw new InvalidOperationException("WebAssembly connection heartbeats require BootstrapBlazor.SignalR. Call AddBootstrapBlazorConnectionClient after AddBootstrapBlazor.");
        }

        if (options.Value.ConnectionHubOptions.EnableIpLocator)
        {
            _ipLocatorProvider ??= ipLocatorFactory.Create(options.Value.IpLocatorOptions.ProviderName);
            client.City = await _ipLocatorProvider.Locate(client.Ip);
        }

        client.UserName = null;
        var authenticationStateProvider = provider.GetService<AuthenticationStateProvider>();
        if (authenticationStateProvider != null)
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            var identity = state.User.Identity;
            if (identity is { IsAuthenticated: true })
            {
                client.UserName = identity.Name;
            }
        }

        cancellationToken.ThrowIfCancellationRequested();
        await connectionService.AddOrUpdateAsync(client, cancellationToken);
        return true;
    }
}
