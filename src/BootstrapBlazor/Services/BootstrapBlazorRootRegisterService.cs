// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorRootRegisterService
/// </summary>
public class BootstrapBlazorRootRegisterService
{
    private readonly Dictionary<object, BootstrapBlazorRootOutlet> _subscribersByIdentifier = [];
    private readonly Dictionary<object, List<BootstrapBlazorRootContent>> _providersByIdentifier = [];

    /// <summary>
    /// Add provider
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="provider"></param>
    public void AddProvider(object identifier, BootstrapBlazorRootContent provider)
    {
        if (!_providersByIdentifier.TryGetValue(identifier, out var providers))
        {
            providers = [];
            _providersByIdentifier.Add(identifier, providers);
        }

        providers.Add(provider);
    }

    /// <summary>
    /// Remove provider
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="provider"></param>
    public void RemoveProvider(object identifier, BootstrapBlazorRootContent provider)
    {
        if (!_providersByIdentifier.TryGetValue(identifier, out var providers))
        {
            throw new InvalidOperationException($"There are no content providers with the given root ID '{identifier}'.");
        }

        var index = providers.LastIndexOf(provider);
        if (index < 0)
        {
            throw new InvalidOperationException($"The provider was not found in the providers list of the given root ID '{identifier}'.");
        }

        providers.RemoveAt(index);
        if (index == providers.Count)
        {
            // We just removed the most recently added provider, meaning we need to change
            // the current content to that of second most recently added provider.
            var contentProvider = GetCurrentProviderContentOrDefault(providers);
            NotifyContentChangedForSubscriber(identifier, contentProvider);
        }
    }

    /// <summary>
    /// Get all providers by identifier
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public List<BootstrapBlazorRootContent> GetProviders(object identifier)
    {
        _providersByIdentifier.TryGetValue(identifier, out var providers);
        return providers ?? [];
    }

    /// <summary>
    /// Subscribe
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="subscriber"></param>
    public void Subscribe(object identifier, BootstrapBlazorRootOutlet subscriber)
    {
        if (_subscribersByIdentifier.ContainsKey(identifier))
        {
            return;
        }

        _subscribersByIdentifier.Add(identifier, subscriber);
    }

    /// <summary>
    /// Unsubscribe
    /// </summary>
    /// <param name="identifier"></param>
    public void Unsubscribe(object identifier)
    {
        _subscribersByIdentifier.Remove(identifier);
    }

    /// <summary>
    /// Notify content provider changed
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="provider"></param>
    public void NotifyContentProviderChanged(object identifier, BootstrapBlazorRootContent provider)
    {
        if (!_providersByIdentifier.TryGetValue(identifier, out var providers))
        {
            throw new InvalidOperationException($"There are no content providers with the given root ID '{identifier}'.");
        }

        // We only notify content changed for subscribers when the content of the
        // most recently added provider changes.
        if (providers.Count != 0 && providers[^1] == provider)
        {
            NotifyContentChangedForSubscriber(identifier, provider);
        }
    }

    private static BootstrapBlazorRootContent? GetCurrentProviderContentOrDefault(List<BootstrapBlazorRootContent> providers)
        => providers.Count != 0
            ? providers[^1]
            : null;

    private void NotifyContentChangedForSubscriber(object identifier, BootstrapBlazorRootContent? provider)
    {
        if (_subscribersByIdentifier.TryGetValue(identifier, out var subscriber))
        {
            subscriber.ContentUpdated(provider);
        }
    }
}
