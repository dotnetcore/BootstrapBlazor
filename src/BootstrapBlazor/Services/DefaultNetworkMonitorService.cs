// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 网络状态服务
/// </summary>
class DefaultNetowrkMonitorService(IJSRuntime jSRuntime)
{
    [NotNull]
    private JSModule? _module = null;

    private Task<JSModule> LoadModule() => jSRuntime.LoadUtility();

    /// <summary>
    /// 获取剪切板数据方法
    /// </summary>
    public async Task<NetworkMonitorState> GetNetworkMonitorState(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<NetworkMonitorState?>("getNetworkInfo", token) ?? new();
    }
}
