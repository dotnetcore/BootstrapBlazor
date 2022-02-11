// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Geolocation 地理定位/移动距离追踪
/// </summary>
public partial class Geolocations : IDisposable
{
    private JSInterop<Geolocations>? Interop { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Geolocations>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    private GeolocationItem? Model { get; set; }

    private async Task GetLocation()
    {
        Interop ??= new JSInterop<Geolocations>(JSRuntime);
        var ret = await Geolocation.GetLocaltion(Interop, this, nameof(GetLocationCallback));
        Trace.Log(ret ? Localizer["GetLocationResultSuccess"] : Localizer["GetLocationResultFailed"]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    [JSInvokable]
    public void GetLocationCallback(GeolocationItem item)
    {
        Model = item;
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
