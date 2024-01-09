// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        Navigation.NavigateTo(Url, ForceLoad);
    }
}
