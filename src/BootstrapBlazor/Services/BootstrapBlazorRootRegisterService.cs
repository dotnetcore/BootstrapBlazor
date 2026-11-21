// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">管理根内容提供者与内容出口的注册、订阅和更新通知的服务</para>
/// <para lang="en">Service that manages registration, subscriptions, and update notifications for root content providers and outlets.</para>
/// </summary>
public class BootstrapBlazorRootRegisterService
{
    private readonly Dictionary<object, BootstrapBlazorRootOutlet> _subscribersByIdentifier = [];
    private readonly Dictionary<object, List<BootstrapBlazorRootContent>> _providersByIdentifier = [];

    /// <summary>
    /// <para lang="zh">为指定标识注册内容提供者，此操作不通知内容出口</para>
    /// <para lang="en">Registers a content provider for the specified identifier without notifying the content outlet.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">用于匹配内容提供者与内容出口的标识</para>
    /// <para lang="en">Identifier used to match content providers with an outlet.</para>
    /// </param>
    /// <param name="provider">
    /// <para lang="zh">要注册的内容提供者</para>
    /// <para lang="en">Content provider to register.</para>
    /// </param>
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
    /// <para lang="zh">移除指定标识下的内容提供者；移除最后注册的提供者时通知内容出口更新</para>
    /// <para lang="en">Removes a content provider for the specified identifier and notifies the outlet when the last registered provider is removed.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">内容提供者注册时使用的标识</para>
    /// <para lang="en">Identifier used to register the content provider.</para>
    /// </param>
    /// <param name="provider">
    /// <para lang="zh">要移除的内容提供者</para>
    /// <para lang="en">Content provider to remove.</para>
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// <para lang="zh">指定标识没有注册内容提供者，或待移除的提供者未在该标识下注册</para>
    /// <para lang="en">No content providers are registered for the identifier, or the provider to remove is not registered under it.</para>
    /// </exception>
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
            // 移除最后注册的内容提供者时通知出口重新渲染。
            // Notify the outlet to re-render when the last registered content provider is removed.
            var contentProvider = GetCurrentProviderContentOrDefault(providers);
            NotifyContentChangedForSubscriber(identifier, contentProvider);
        }
    }

    /// <summary>
    /// <para lang="zh">获取指定标识下按注册顺序排列的全部内容提供者</para>
    /// <para lang="en">Gets all content providers for the specified identifier in registration order.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">内容提供者注册时使用的标识</para>
    /// <para lang="en">Identifier used to register the content providers.</para>
    /// </param>
    /// <returns>
    /// <para lang="zh">已注册的内容提供者列表；标识不存在时返回空列表</para>
    /// <para lang="en">The registered content provider list, or an empty list if the identifier does not exist.</para>
    /// </returns>
    public List<BootstrapBlazorRootContent> GetProviders(object identifier)
    {
        _providersByIdentifier.TryGetValue(identifier, out var providers);
        return providers ?? [];
    }

    /// <summary>
    /// <para lang="zh">为指定标识订阅内容出口；已有订阅时保留原出口</para>
    /// <para lang="en">Subscribes a content outlet for the specified identifier, retaining the existing outlet if already subscribed.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">内容出口要订阅的标识</para>
    /// <para lang="en">Identifier the content outlet subscribes to.</para>
    /// </param>
    /// <param name="subscriber">
    /// <para lang="zh">接收内容更新通知的出口</para>
    /// <para lang="en">Outlet that receives content update notifications.</para>
    /// </param>
    public void Subscribe(object identifier, BootstrapBlazorRootOutlet subscriber)
    {
        if (_subscribersByIdentifier.ContainsKey(identifier))
        {
            return;
        }

        _subscribersByIdentifier.Add(identifier, subscriber);
    }

    /// <summary>
    /// <para lang="zh">解除指定标识下内容出口的订阅</para>
    /// <para lang="en">Unsubscribes the content outlet for the specified identifier.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">要解除订阅的标识</para>
    /// <para lang="en">Identifier to unsubscribe from.</para>
    /// </param>
    public void Unsubscribe(object identifier)
    {
        _subscribersByIdentifier.Remove(identifier);
    }

    /// <summary>
    /// <para lang="zh">通知指定标识的内容出口重新渲染，以反映内容提供者的更新</para>
    /// <para lang="en">Notifies the content outlet for the specified identifier to re-render after a content provider update.</para>
    /// </summary>
    /// <param name="identifier">
    /// <para lang="zh">用于查找内容出口的标识</para>
    /// <para lang="en">Identifier used to find the content outlet.</para>
    /// </param>
    /// <param name="provider">
    /// <para lang="zh">发生更新的内容提供者</para>
    /// <para lang="en">Content provider that was updated.</para>
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// <para lang="zh">指定标识尚未注册内容提供者</para>
    /// <para lang="en">No content providers have been registered for the identifier.</para>
    /// </exception>
    public void NotifyContentProviderChanged(object identifier, BootstrapBlazorRootContent provider)
    {
        if (!_providersByIdentifier.TryGetValue(identifier, out _))
        {
            throw new InvalidOperationException($"There are no content providers with the given root ID '{identifier}'.");
        }

        NotifyContentChangedForSubscriber(identifier, provider);
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
