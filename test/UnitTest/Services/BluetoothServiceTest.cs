// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Services;

public class BluetoothServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task RequestDevice_Ok()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<bool>("getAvailability", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments.Count == 4 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments.Count == 3 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<byte[]>("readValue", matcher => matcher.Arguments.Count == 5 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult([0x31]);
        Context.JSInterop.Setup<bool>("disconnect", matcher => matcher.Arguments.Count == 3 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(true);

        var bluetoothService = Context.Services.GetRequiredService<IBluetoothService>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);
        Assert.Equal("test", device.Name);
        Assert.Equal("id_1234", device.Id);
        Assert.Null(device.ErrorMessage);

        await device.Connect();
        Assert.True(device.Connected);

        var val = await device.ReadValue("battery_service", "battery_level");
        Assert.Equal([0x31], val);

        var v = await device.GetBatteryValue();
        Assert.Equal(0x31, v);

        var mi = device.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(device, ["test"]);
        Assert.Equal("test", device.ErrorMessage);

        await device.Disconnect();
        Assert.False(device.Connected);

        await device.DisposeAsync();
    }

    [Fact]
    public async Task ReadValue_null()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments.Count == 4 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<byte[]?>("readValue", matcher => matcher.Arguments.Count == 5 && (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(null);
        var bluetoothService = Context.Services.GetRequiredService<IBluetoothService>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);
        var v = await device.GetBatteryValue();
        Assert.Equal(0x0, v);
    }

    [Fact]
    public async Task GetAvailability_Ok()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<bool>("getAvailability", matcher => matcher.Arguments.Count == 0).SetResult(true);
        var bluetoothService = Context.Services.GetRequiredService<IBluetoothService>();

        await bluetoothService.GetAvailability();
        Assert.True(bluetoothService.IsSupport);
        Assert.True(bluetoothService.IsAvailable);
        Assert.Null(bluetoothService.ErrorMessage);

        var mi = bluetoothService.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(bluetoothService, ["test"]);
        Assert.Equal("test", bluetoothService.ErrorMessage);
    }

    [Fact]
    public void Filter_Ok()
    {
        var filter = new BluetoothRequestOptions()
        {
            Filters =
            [
                 new() {
                      ManufacturerData =
                      [
                          new()
                          {
                               CompanyIdentifier = 0x004C,
                               DataPrefix = "Apple",
                               Mask = "test"
                          }
                      ],
                      Name = "test-Name",
                      NamePrefix = "test-NamePrefix",
                      Services = ["test-service"],
                      ServiceData = [new BluetoothServiceDataFilter()
                      {
                           DataPrefix = "test-data-prefix",
                           Service = "test-data-service",
                           Mask = "test-data-mask"
                      }]
                 }
            ],
            AcceptAllDevices = false,
            ExclusionFilters = [],
            OptionalManufacturerData = ["test-manufacturer-data"],
            OptionalServices = ["test-optional-service"]
        };

        Assert.NotNull(filter.Filters);

        var data = filter.Filters[0].ManufacturerData;
        Assert.NotNull(data);
        Assert.Equal(0x004C, data[0].CompanyIdentifier);
        Assert.Equal("Apple", data[0].DataPrefix);
        Assert.Equal("test", data[0].Mask);

        Assert.Equal("test-Name", filter.Filters[0].Name);
        Assert.Equal("test-NamePrefix", filter.Filters[0].NamePrefix);

        var services = filter.Filters[0].Services;
        Assert.NotNull(services);
        Assert.Equal("test-service", services[0]);

        var serviceData = filter.Filters[0].ServiceData;
        Assert.NotNull(serviceData);
        Assert.Equal("test-data-prefix", serviceData[0].DataPrefix);
        Assert.Equal("test-data-service", serviceData[0].Service);
        Assert.Equal("test-data-mask", serviceData[0].Mask);

        Assert.False(filter.AcceptAllDevices);
        Assert.Empty(filter.ExclusionFilters);
        Assert.Equal(["test-manufacturer-data"], filter.OptionalManufacturerData);
        Assert.Equal(["test-optional-service"], filter.OptionalServices);
    }
}
