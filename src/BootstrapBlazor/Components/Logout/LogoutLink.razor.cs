// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class LogoutLink
{
    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<LogoutLink>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    [Parameter]
    public string Icon { get; set; } = "fa fa-key";

    /// <summary>
    /// 获得/设置 按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 按钮文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string Url { get; set; } = "/Account/Logout";

    /// <summary>
    /// 获得/设置 是否强制加载导航页面 默认 true 
    /// </summary>
    /// <remarks>此参数用于 NavigateTo 第二个参数 forceLoad</remarks>
    [Parameter]
    public bool ForceLoad { get; set; } = true;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Text ??= Localizer[nameof(Text)];
    }

    private void OnLogout() => NavigationManager.NavigateTo(Url, ForceLoad);
}
