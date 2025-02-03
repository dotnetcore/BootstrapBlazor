// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// Ajax 服务类
/// </summary>
public class AjaxService(IJSRuntime jSRuntime)
{
    [NotNull]
    private JSModule? _module = null;

    private Task<JSModule> LoadModule() => jSRuntime.LoadModuleByName("ajax");

    /// <summary>
    /// 调用Ajax方法发送请求
    /// </summary>
    public async Task<JsonDocument?> InvokeAsync(AjaxOption option, CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<JsonDocument?>("execute", token, option);
    }

    /// <summary>
    /// 调用 Goto 方法跳转其他页面
    /// </summary>
    /// <param name="url"></param>
    /// <param name="token"></param>
    public async Task Goto(string url, CancellationToken token = default)
    {
        _module ??= await LoadModule();
        await _module.InvokeVoidAsync("goto", token, url);
    }
}
