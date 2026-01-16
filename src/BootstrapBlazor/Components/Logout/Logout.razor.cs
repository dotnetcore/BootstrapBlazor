// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ListView 组件基类</para>
/// <para lang="en">ListView component基类</para>
/// </summary>
public partial class Logout
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Logout>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件当前用户头像</para>
    /// <para lang="en">Gets or sets component当前用户头像</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件当前用户显示名称</para>
    /// <para lang="en">Gets or sets component当前用户display名称</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DisplayName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件当前用户显示名称前置文本 默认 欢迎</para>
    /// <para lang="en">Gets or sets component当前用户display名称前置文本 Default is 欢迎</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PrefixDisplayNameText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件当前用户登录账号</para>
    /// <para lang="en">Gets or sets component当前用户登录账号</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? UserName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets 子component</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示用户名 默认 true 显示</para>
    /// <para lang="en">Gets or sets whetherdisplay用户名 Default is true display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowUserName { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 组件当前用户登录账号前置文本 默认 当前账号</para>
    /// <para lang="en">Gets or sets component当前用户登录账号前置文本 Default is 当前账号</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PrefixUserNameText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 HeaderTemplate</para>
    /// <para lang="en">Gets or sets component HeaderTemplate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 LinkTemplate</para>
    /// <para lang="en">Gets or sets component LinkTemplate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? LinkTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the avatar border radius. 默认为 null.</para>
    /// <para lang="en">Gets or sets the avatar border radius. Default is null.</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized 方法</para>
    /// </summary>
    protected override void OnInitialized()
    {
        PrefixDisplayNameText ??= Localizer[nameof(PrefixDisplayNameText)];
        PrefixUserNameText ??= Localizer[nameof(PrefixUserNameText)];
    }
}
