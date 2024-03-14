﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FlipClock 示例代码
/// </summary>
public partial class FlipClocks
{
    private bool _isCompleted;

    private Task OnCompletedAsync()
    {
        _isCompleted = true;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
