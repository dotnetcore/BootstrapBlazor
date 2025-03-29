// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

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
    private PackageVersionService? PackageVersionService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Header>? Localizer { get; set; }

    [NotNull]
    private string? HomeText { get; set; }

    [NotNull]
    private string? IntroductionText { get; set; }

    [NotNull]
    private string? ComponentsText { get; set; }

    [NotNull]
    private string? DownloadText { get; set; }

    [NotNull]
    private string? TutorialsText { get; set; }

    private const string DownloadUrl = "https://github.com/dotnetcore/BootstrapBlazor/releases?wt.mc_id=DT-MVP-5004174";

    private string _versionString = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DownloadText ??= Localizer[nameof(DownloadText)];
        HomeText ??= Localizer[nameof(HomeText)];
        IntroductionText ??= Localizer[nameof(IntroductionText)];
        ComponentsText ??= Localizer[nameof(ComponentsText)];
        TutorialsText ??= Localizer[nameof(TutorialsText)];
        _versionString = $"v{PackageVersionService.Version}";
    }
}
