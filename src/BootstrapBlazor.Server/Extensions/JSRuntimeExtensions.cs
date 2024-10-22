// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
