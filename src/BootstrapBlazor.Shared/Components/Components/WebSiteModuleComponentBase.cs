// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// WebSiteModuleComponentBase 组件
/// </summary>
public abstract class WebSiteModuleComponentBase : BootstrapModuleComponentBase
{
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? Options { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnLoadJSModule()
    {
        base.OnLoadJSModule();

        if (!string.IsNullOrEmpty(ModulePath))
        {
            ModulePath = $"{Options.Value.JSModuleRootPath}{ModulePath}";
        }
    }
}
