﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoRedirect component
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "autoredirect", JSObjectReference = true)]
public class AutoRedirect : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得/设置 重定向地址
    /// </summary>
    [Parameter]
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// 获得/设置 是否强制导航 默认 false
    /// </summary>
    [Parameter]
    public bool IsForceLoad { get; set; }

    /// <summary>
    /// 获得/设置 自动锁屏间隔单位 秒 默认 60000 毫秒
    /// </summary>
    [Parameter]
    public int Interval { get; set; } = 60000;

    /// <summary>
    /// 获得/设置 地址跳转前回调方法 返回 true 时中止跳转
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnBeforeRedirectAsync { get; set; }

    /// <summary>
    /// 获得/设置 NavigationManager 实例
    /// </summary>
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Interval, nameof(Lock));

    /// <summary>
    /// 锁屏操作由 JS 调用
    /// </summary>
    [JSInvokable]
    public async Task Lock()
    {
        var interrupt = false;
        if (OnBeforeRedirectAsync != null)
        {
            interrupt = await OnBeforeRedirectAsync();
        }
        if (!interrupt && !string.IsNullOrEmpty(RedirectUrl))
        {
            NavigationManager.NavigateTo(RedirectUrl, IsForceLoad);
        }
    }
}
