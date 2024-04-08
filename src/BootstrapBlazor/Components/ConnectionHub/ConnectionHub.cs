// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 客户端链接组件
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
    /// <returns></returns>
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
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
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

                if (!string.IsNullOrEmpty(client.Ip))
                {
                    _ipLocatorProvider ??= IpLocatorFactory.Create(BootstrapBlazorOptions.Value.IpLocatorOptions.ProviderName);
                    client.City = await _ipLocatorProvider.Locate(client.Ip);
                }
                ConnectionService.AddOrUpdate(client);
            });
        }
    }
}
