// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Geolocations
/// </summary>
public partial class Geolocations
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private GeolocationPosition? Model { get; set; }

    /// <summary>
    /// 获得/设置 获取持续定位监听器ID
    /// </summary>
    private long WatchID { get; set; }

    private async Task GetLocation()
    {
        Model = await GeoLocationService.GetPositionAsync();
    }

    private async Task WatchPosition()
    {
        try
        {
            WatchID = await GeoLocationService.WatchPositionAsync(p =>
            {
                Model = p;
                StateHasChanged();
                return Task.CompletedTask;
            });
            Logger.Log(WatchID != 0 ? Localizer["WatchPositionResultSuccess"] : Localizer["WatchPositionResultFailed"]);
            Logger.Log($"WatchID : {WatchID}");
        }
        catch (Exception)
        {
            Logger.Log(Localizer["WatchPositionResultFailed"]);
        }
    }

    private async Task ClearWatchPosition()
    {
        if (WatchID != 0)
        {
            var ret = await GeoLocationService.ClearWatchPositionAsync(WatchID);
            if (ret)
            {
                WatchID = 0;
            }

            Logger.Log(ret ? Localizer["ClearWatchPositionResultSuccess"] : Localizer["ClearWatchPositionResultFailed"]);
        }
    }

    /// <summary>
    /// DisposeAsync
    /// </summary>
    /// <param name = "disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (WatchID != 0)
            {
                await GeoLocationService.ClearWatchPositionAsync(WatchID);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
