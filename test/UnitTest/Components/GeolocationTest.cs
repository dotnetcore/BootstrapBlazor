// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class GeolocationTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetPositionAsync_Ok()
    {
        var server = Context.Services.GetRequiredService<IGeoLocationService>();
        Context.JSInterop.Setup<GeolocationPosition>("getPosition", v => true).SetResult(new GeolocationPosition() { Latitude = 100, LastLat = 50 });
        ResetModule(server);
        var pos = await server.GetPositionAsync();
        Assert.NotNull(pos);
        Assert.Equal(100, pos.Latitude);
        Assert.Equal(50, pos.LastLat);
    }

    [Fact]
    public async Task WatchPositionAsync_Ok()
    {
        var called = false;
        var server = Context.Services.GetRequiredService<IGeoLocationService>();
        Context.JSInterop.Setup<long>("watchPosition", v => true).SetResult(1);
        ResetModule(server);
        var id = await server.WatchPositionAsync(p =>
        {
            called = true;
            return Task.CompletedTask;
        });

        // test callback
        // WatchlocationPositionCallback
        var mi = server.GetType().GetMethod("WatchlocationPositionCallback")!;
        mi.Invoke(server, new object[] { new GeolocationPosition() { Latitude = 100, LastLat = 50 } });
        Assert.True(called);

        // test call watch again
        id = await server.WatchPositionAsync(p =>
        {
            return Task.CompletedTask;
        });
        ResetModule(server);
        await server.ClearWatchPositionAsync(id);
    }

    [Fact]
    public void GeolocationPosition_Ok()
    {
        var item = new GeolocationPosition()
        {
            Latitude = 10,
            Accuracy = 10,
            Altitude = 10,
            AltitudeAccuracy = 10,
            CurrentDistance = 10,
            Heading = 10,
            LastLat = 10,
            LastLong = 10,
            Longitude = 10,
            Speed = 10,
            Timestamp = 10,
            TotalDistance = 10
        };
        Assert.Equal(10, item.Latitude);
        Assert.Equal(10, item.Accuracy);
        Assert.Equal(10, item.Altitude);
        Assert.Equal(10, item.AltitudeAccuracy);
        Assert.Equal(10, item.CurrentDistance);
        Assert.Equal(10, item.Heading);
        Assert.Equal(10, item.LastLong);
        Assert.Equal(10, item.LastLat);
        Assert.Equal(10, item.Longitude);
        Assert.Equal(10, item.Speed);
        Assert.Equal(10, item.Timestamp);
        Assert.Equal(10, item.TotalDistance);
        Assert.NotEqual(1, item.LastUpdateTime.Year);
    }

    private static void ResetModule(IGeoLocationService service)
    {
        var pi = service.GetType().GetProperty("Module", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;
        pi.SetValue(service, null);
    }
}
