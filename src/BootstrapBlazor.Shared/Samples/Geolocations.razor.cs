// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Geolocation 地理定位/移动距离追踪
/// </summary>
public partial class Geolocations : IAsyncDisposable
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

    /// <summary>
    /// 获得/设置 获取持续定位监听器ID
    /// </summary>
    private long WatchID { get; set; }

    private async Task GetLocation()
    {
        Interop ??= new JSInterop<Geolocations>(JSRuntime);
        var ret = await Geolocation.GetLocaltion(Interop, this, nameof(GetLocationCallback));
        Trace.Log(ret ? Localizer["GetLocationResultSuccess"] : Localizer["GetLocationResultFailed"]);
    }
    private async Task WatchPosition()
    {
        try
        {
            Interop ??= new JSInterop<Geolocations>(JSRuntime);
            WatchID = await Geolocation.WatchPosition(Interop, this, nameof(GetLocationCallback));
            Trace.Log(WatchID != 0 ? Localizer["WatchPositionResultSuccess"] : Localizer["WatchPositionResultFailed"]);
            Trace.Log($"WatchID : {WatchID}");
        }
        catch (Exception)
        {
            Trace.Log(Localizer["WatchPositionResultFailed"]);
        }
    }

    private async Task ClearWatchPosition()
    {
        if (WatchID != 0)
        {
            Interop ??= new JSInterop<Geolocations>(JSRuntime);
            var ret = await Geolocation.ClearWatchPosition(Interop, WatchID);
            if (ret)
            {
                WatchID = 0;
            }
            Trace.Log(ret ? Localizer["ClearWatchPositionResultSuccess"] : Localizer["ClearWatchPositionResultFailed"]);
        }
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
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                if (WatchID != 0)
                {
                    await Geolocation.ClearWatchPosition(Interop, WatchID);
                }

                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
