// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Alert
{
    /// <summary>
    /// 
    /// </summary>
    protected override string? ClassName => CssBuilder.Default(base.ClassName)
        .AddClass("d-none", !IsShown)
        .Build();

    private bool IsShown { get; set; } = true;

    private async Task OnClick()
    {
        IsShown = !IsShown;
        if (OnDismiss != null) await OnDismiss();
    }
}
