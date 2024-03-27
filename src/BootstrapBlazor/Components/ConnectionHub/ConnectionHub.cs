// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 客户端链接组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "hub", JSObjectReference = true, AutoInvokeInit = true, AutoInvokeDispose = false)]
public class ConnectionHub : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private IConnectionService? ConnectionService { get; set; }

    [Inject]
    [NotNull]
    private WebClientService? WebClientService { get; set; }

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

    private ClientInfo? _clientInfo;

    private IIpLocatorProvider? _ipLocatorProvider;

    private ThrottleOptions? _throttleOptions = default!;

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
            await InvokeVoidAsync("init", new { Invoke = Interop, Method = nameof(Callback), Interval = options.BeatInterval });
        }
    }

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Callback(string? code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            var dispatch = ThrottleDispatcherFactory.GetOrCreate(code, _throttleOptions);
            await dispatch.ThrottleAsync(async () =>
            {
                _clientInfo ??= await WebClientService.GetClientInfo();
                _clientInfo.Id = code;
                _clientInfo.RequestUrl = NavigationManager.Uri;

                if (!string.IsNullOrEmpty(_clientInfo.Ip))
                {
                    _ipLocatorProvider ??= IpLocatorFactory.Create();
                    _clientInfo.City = await _ipLocatorProvider.Locate(_clientInfo.Ip);
                }
                ConnectionService.AddOrUpdate(_clientInfo);
            });
        }
    }
}
