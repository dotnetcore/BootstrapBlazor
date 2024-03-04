// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BootstrapBlazor.Maui;

public partial class Main
{
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOptions { get; set; }

    [NotNull]
    private IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        AdditionalAssemblies = WebsiteOptions.Value.AdditionalAssemblies;
    }
}
