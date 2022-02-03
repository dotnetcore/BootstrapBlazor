// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class Redirect : ComponentBase
{
    [Inject]
    [NotNull]
    private NavigationManager? Navigation { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string Url { get; set; } = "Account/Login";

#if DEBUG
    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        Navigation.NavigateTo(Url, true);
    }
#else
    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Navigation.NavigateTo(Url, true);
    }
#endif
}
