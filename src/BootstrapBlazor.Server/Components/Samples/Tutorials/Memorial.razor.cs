// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// 追悼模式
/// </summary>
public partial class Memorial
{
    [Inject, NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    private bool _isMemorial = false;

    private async Task OnToggle()
    {
        var module = await JSRuntime.LoadUtility();

        _isMemorial = !_isMemorial;
        await module.SetMemorialModeAsync(_isMemorial);
    }
}
