﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components.Pages;

/// <summary>
/// 
/// </summary>
public partial class Introduction
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<Introduction>? Localizer { get; set; }

    [NotNull]
    private string[]? LocalizerRules { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        LocalizerRules =
        [
            WebsiteOption.CurrentValue.BootstrapAdminLink,
            WebsiteOption.CurrentValue.BootstrapAdminLink + "/stargazers",
            WebsiteOption.CurrentValue.BootstrapAdminLink + "/badge/star.svg?theme=gvp",
            WebsiteOption.CurrentValue.BootstrapAdminLink
        ];
    }
}
