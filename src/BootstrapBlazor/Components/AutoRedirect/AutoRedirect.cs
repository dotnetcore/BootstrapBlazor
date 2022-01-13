// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 自动锁定组件
/// </summary>
public class AutoRedirect : BootstrapComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 登出 Controller Url
    /// </summary>
    [Parameter]
    public string? LogoutUrl { get; set; }

    /// <summary>
    /// 获得/设置 自动锁屏间隔单位 秒 默认 60 秒
    /// </summary>
    [Parameter]
    public int Interval { get; set; } = 60;

    /// <summary>
    /// 获得/设置 NavigationManager 实例
    /// </summary>
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    private JSInterop<AutoRedirect>? Interop { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Interop = new JSInterop<AutoRedirect>(JSRuntime);
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Interop != null)
            {
                await Interop.InvokeVoidAsync(this, null, "bb_auto_redirect", Interval, nameof(Lock));
            }
        }
    }

    /// <summary>
    /// 锁屏操作由 JS 调用
    /// </summary>
    [JSInvokable]
    public void Lock()
    {
        if (!string.IsNullOrEmpty(LogoutUrl))
        {
            NavigationManager.NavigateTo(LogoutUrl, true);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
