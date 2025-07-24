// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 更新日志介绍组件
/// </summary>
public partial class UpdateIntro
{
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private PackageVersionService? PackageVersionService { get; set; }

    private string UpdateLogUrl => $"{WebsiteOption.Value.GiteeRepositoryUrl}/wikis/%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97?sort_id=4062034";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task InvokeInitAsync()
    {
#if DEBUG
        await InvokeVoidAsync("init", "");
#else
        await InvokeVoidAsync("init", Id, PackageVersionService.Version);
#endif
    }
}
