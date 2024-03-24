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
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    private ClientInfo? _clientInfo;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var options = BootstrapBlazorOptions.Value.CollectionHubOptions ?? new();
        if (options.Enable)
        {
            _clientInfo = await WebClientService.GetClientInfo();
            await InvokeVoidAsync("init", new { Invoke = Interop, Method = nameof(Callback), Interval = options.BeatInterval });
        }
    }

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task Callback(string? code)
    {
        if (_clientInfo != null && !string.IsNullOrEmpty(code))
        {
            _clientInfo.Id = code;
            _clientInfo.RequestUrl = NavigationManager.Uri;
            ConnectionService.AddOrUpdate(_clientInfo);
        }
        return Task.CompletedTask;
    }
}
