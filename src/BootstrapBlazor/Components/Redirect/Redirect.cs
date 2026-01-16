// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Redirect 组件</para>
/// <para lang="en">Redirect Component</para>
/// </summary>
public class Redirect : ComponentBase
{
    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 登录地址 默认 Account/Login</para>
    /// <para lang="en">Get/Set Login URL. Default Account/Login</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string Url { get; set; } = "Account/Login";

    /// <summary>
    /// <para lang="zh">获得/设置 是否强制导航 默认 true</para>
    /// <para lang="en">Get/Set Whether to force load. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">设置 false 时适用 SPA 程序不会强制页面重新加载</para>
    /// <para lang="en">Applicable to SPA programs when set to false, will not force page reload</para>
    /// </remarks>
    [Parameter]
    public bool ForceLoad { get; set; } = true;

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        Navigation.NavigateTo(Url, ForceLoad);
    }
}
