// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器指纹服务
/// </summary>
class DefaultBrowserFingerService(IJSRuntime jSRuntime) : IBrowserFingerService
{
    [NotNull]
    private JSModule? _module = null;

    /// <summary>
    /// 获取剪切板数据方法
    /// </summary>
    public async Task<string?> GetFingerCodeAsync(CancellationToken token = default)
    {
        _module ??= await jSRuntime.LoadUtility();
        return await _module.InvokeAsync<string?>("getFingerCode", token);
    }
}
