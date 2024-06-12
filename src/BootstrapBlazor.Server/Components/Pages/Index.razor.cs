// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Pages;

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
