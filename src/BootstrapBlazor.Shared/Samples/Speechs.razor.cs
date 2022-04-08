// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

public partial class Speechs
{
    [Inject]
    [NotNull]
    private VersionService? VersionManager { get; set; }

    private string Version { get; set; } = "fetching";

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.azurespeech");
    }
}
