// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Pages;

/// <summary>
/// Index 组件
/// </summary>
public partial class Index
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Index>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private PackageVersionService? PackageVersionService { get; set; }

    private string _versionString = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _versionString = $"v{PackageVersionService.Version}";
    }
}
