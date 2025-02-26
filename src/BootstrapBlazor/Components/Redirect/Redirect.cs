// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class Redirect : ComponentBase
{
    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }

    /// <summary>
    /// 获得/设置 登录地址 默认 Account/Login
    /// </summary>
    [Parameter]
    public string Url { get; set; } = "Account/Login";

    /// <summary>
    /// 获得/设置 是否强制导航 默认 true
    /// </summary>
    /// <remarks>设置 false 时适用 SPA 程序不会强制页面重新加载</remarks>
    [Parameter]
    public bool ForceLoad { get; set; } = true;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Navigation.NavigateTo(Url, ForceLoad);
    }
}
