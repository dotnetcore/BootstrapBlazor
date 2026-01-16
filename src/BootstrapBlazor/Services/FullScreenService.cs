// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FullScreen 服务</para>
/// <para lang="en">FullScreen Service</para>
/// </summary>
public class FullScreenService(IJSRuntime jSRuntime)
{
    [NotNull]
    private JSModule? _module = null;

    /// <summary>
    /// <para lang="zh">全屏方法，已经全屏时再次调用后退出全屏</para>
    /// <para lang="en">Fullscreen method, exit fullscreen if called again when already in fullscreen</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task Toggle(FullScreenOption? option = null, CancellationToken token = default)
    {
        _module ??= await jSRuntime.LoadModuleByName("fullscreen");
        await _module.InvokeVoidAsync("toggle", token, option);
    }
}
