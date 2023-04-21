// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Shared;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// Header 组件
/// </summary>
public partial class Header
{
    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<BaseLayout>? Localizer { get; set; }

    [NotNull]
    private string? HomeText { get; set; }

    [NotNull]
    private string? IntroductionText { get; set; }

    [NotNull]
    private string? ComponentsText { get; set; }

    [NotNull]
    private string? DownloadText { get; set; }

    private string DownloadUrl => $"{WebsiteOption.CurrentValue.BootstrapBlazorLink}/repository/archive/main.zip";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DownloadText ??= Localizer[nameof(DownloadText)];
        HomeText ??= Localizer[nameof(HomeText)];
        IntroductionText ??= Localizer[nameof(IntroductionText)];
    }
}
