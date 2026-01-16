// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"></para>
/// <para lang="en"></para>
/// </summary>
public partial class LogoutLink
{
    [Inject]
    [NotNull]
    private IStringLocalizer<LogoutLink>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮文字</para>
    /// <para lang="en">Gets or sets button文字</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮文字</para>
    /// <para lang="en">Gets or sets button文字</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Url { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.LogoutLinkIcon);

        Url ??= "/Account/Logout";
    }
}
