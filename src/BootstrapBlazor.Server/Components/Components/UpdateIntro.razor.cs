// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// 更新日志介绍组件
/// </summary>
public partial class UpdateIntro
{
    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private PackageVersionService? PackageVersionService { get; set; }

    private string UpdateLogUrl => $"{WebsiteOption.CurrentValue.BootstrapBlazorLink}/wikis/%E6%9B%B4%E6%96%B0%E6%97%A5%E5%BF%97?sort_id=4062034";

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
