// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// FullScreens 全屏示例代码
/// </summary>
public partial class FullScreens
{
    [Inject]
    [NotNull]
    private FullScreenService? FullScreenService { get; set; }

    private async Task ToggleFullScreen()
    {
        await FullScreenService.Toggle();
    }
}
