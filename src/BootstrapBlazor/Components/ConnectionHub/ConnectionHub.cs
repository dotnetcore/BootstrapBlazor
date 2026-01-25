// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">客户端链接组件</para>
/// <para lang="en">Client connection component</para>
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "hub", JSObjectReference = true)]
public class ConnectionHub : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private IIpLocatorFactory? IpLocatorFactory { get; set; }

    [Inject]
    [NotNull]
    private IThrottleDispatcherFactory? ThrottleDispatcherFactory { get; set; }

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    private IIpLocatorProvider? _ipLocatorProvider;

    private ThrottleOptions? _throttleOptions;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task InvokeInitAsync()
    {
        var options = BootstrapBlazorOptions.Value.ConnectionHubOptions;
        if (options.Enable)
        {
            _throttleOptions = new ThrottleOptions() { Interval = options.BeatInterval };
            await InvokeVoidAsync("init", Id, new
            {
                Invoke = Interop,
                Method = nameof(Callback),
                ConnectionId = Guid.NewGuid(),
                Interval = options.BeatInterval.TotalMilliseconds,
                Url = "ip.axd"
            });
        }
    }

    /// <summary>
    /// <para lang="zh">JSInvoke 回调方法</para>
    /// <para lang="en">JSInvoke callback method</para>
    /// </summary>
    /// <param name="client"></param>
    [JSInvokable]
    public async Task Callback(ClientInfo client)
    {
        var code = client.Id;
        if (!string.IsNullOrEmpty(code))
        {
            var dispatch = ThrottleDispatcherFactory.GetOrCreate(code, _throttleOptions);
            await dispatch.ThrottleAsync(async () =>
            {
                client.RequestUrl = NavigationManager.Uri;

                if (BootstrapBlazorOptions.Value.ConnectionHubOptions.EnableIpLocator)
                {
                    _ipLocatorProvider ??= IpLocatorFactory.Create(BootstrapBlazorOptions.Value.IpLocatorOptions.ProviderName);
                    client.City = await _ipLocatorProvider.Locate(client.Ip);
                }
                ConnectionService.AddOrUpdate(client);
            });
        }
    }
}
