// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 客户端链接组件
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "net", JSObjectReference = true)]
public class NetworkMonitor : BootstrapModuleComponentBase
{
    /// <summary>
    /// Gets or sets the callback function that is invoked when the network state changes.
    /// </summary>
    [Parameter]
    public Func<NetworkMonitorState, Task>? OnNetworkStateChanged { get; set; }

    /// <summary>
    /// Gets or sets the list of indicators used for display info.
    /// </summary>
    [Parameter]
    public List<string>? Indicators { get; set; }

    private NetworkMonitorState _state = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new
    {
        Invoke = Interop,
        OnlineStateChangedCallback = nameof(TriggerOnlineStateChanged),
        OnNetworkStateChangedCallback = nameof(TriggerNetworkStateChanged),
        Indicators
    });

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerOnlineStateChanged(bool online)
    {
        _state.IsOnline = online;
        if (OnNetworkStateChanged != null)
        {
            await OnNetworkStateChanged(_state);
        }
    }

    /// <summary>
    /// JSInvoke 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerNetworkStateChanged(NetworkMonitorState state)
    {
        // 网络状态变化回调方法
        _state = state;
        _state.IsOnline = true;
        if (OnNetworkStateChanged != null)
        {
            await OnNetworkStateChanged(_state);
        }
    }
}
