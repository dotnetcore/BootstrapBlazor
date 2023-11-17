// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Extensions;

/// <summary>
/// 
/// </summary>
public static class JSRuntimeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsRuntime"></param>
    /// <param name="cultureName"></param>
    public static ValueTask SetCulture(this IJSRuntime jsRuntime, string cultureName) => jsRuntime.InvokeVoidAsync("BB.blazorCulture.set", cultureName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsRuntime"></param>
    public static ValueTask<string> GetCulture(this IJSRuntime jsRuntime) => jsRuntime.InvokeAsync<string>("BB.blazorCulture.get");
}
