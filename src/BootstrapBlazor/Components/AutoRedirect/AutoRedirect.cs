// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoRedirect component
/// </summary>
[JSModuleAutoLoader(JSObjectReference = true)]
public class AutoRedirect : BootstrapModule2ComponentBase
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
    protected override Task ModuleInitAsync() => InvokeInitAsync(Id, Interval, nameof(Lock));

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
