// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace UnitTest.Components;

public class GeolocationTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Geolocation_Ok()
    {
        var cut = Context.RenderComponent<MockGeoTest>();
        var instance = cut.Instance;
        await instance.GetLocaltion();
        await instance.WatchPosition();
        await instance.ClearWatchPosition(1);
    }

    [Fact]
    public void GeolocationItem_Ok()
    {
        var item = new GeolocationItem()
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

    private class MockGeoTest : ComponentBase
    {
        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        [NotNull]
        private JSInterop<MockGeoTest>? Interop { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Interop = new JSInterop<MockGeoTest>(JSRuntime);
        }

        public ValueTask<bool> GetLocaltion() => Geolocation.GetLocaltion(Interop, this, "Test");

        public ValueTask<long> WatchPosition() => Geolocation.WatchPosition(Interop, this, "Test");

        public ValueTask<bool> ClearWatchPosition(long watchId) => Geolocation.ClearWatchPosition(Interop, watchId);
    }
}
