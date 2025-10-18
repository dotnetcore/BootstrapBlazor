// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ListView 组件基类
/// </summary>
public partial class Logout
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Logout>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户头像
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户显示名称
    /// </summary>
    [Parameter]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户显示名称前置文本 默认 欢迎
    /// </summary>
    [Parameter]
    public string? PrefixDisplayNameText { get; set; }

    /// <summary>
    /// 获得/设置 组件当前用户登录账号
    /// </summary>
    [Parameter]
    public string? UserName { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示用户名 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowUserName { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件当前用户登录账号前置文本 默认 当前账号
    /// </summary>
    [Parameter]
    public string? PrefixUserNameText { get; set; }

    /// <summary>
    /// 获得/设置 组件 HeaderTemplate
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件 LinkTemplate
    /// </summary>
    [Parameter]
    public RenderFragment? LinkTemplate { get; set; }

    /// <summary>
    /// Gets or sets the avatar border radius. Default is null.
    /// </summary>
    [Parameter]
    public string? AvatarRadius { get; set; }

    private string? LogoutClassString => CssBuilder.Default("dropdown dropdown-logout")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? AvatarStyleString => CssBuilder.Default()
        .AddClass($"--bb-logout-user-avatar-border-radius: {AvatarRadius};", !string.IsNullOrEmpty(AvatarRadius))
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        PrefixDisplayNameText ??= Localizer[nameof(PrefixDisplayNameText)];
        PrefixUserNameText ??= Localizer[nameof(PrefixUserNameText)];
    }
}
