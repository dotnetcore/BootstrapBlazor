﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Data;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Pages;

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
