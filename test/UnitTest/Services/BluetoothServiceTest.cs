// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace UnitTest.Services;

public class BluetoothServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task RequestDevice_Ok()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<bool>("getAvailability").SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<byte[]>("readValue", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult([0x31]);
        Context.JSInterop.Setup<bool>("disconnect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);

        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();

        var services = new List<BluetoothServicesEnum>() { BluetoothServicesEnum.DeviceInformation };
        var device = await bluetoothService.RequestDevice(services.GetServicesList());
        Assert.NotNull(device);
        Assert.Equal("test", device.Name);
        Assert.Equal("id_1234", device.Id);
        Assert.Null(device.ErrorMessage);

        await device.Connect();
        Assert.True(device.Connected);

        var val = await device.ReadValue("battery_service", "battery_level");
        Assert.Equal([0x31], val);

        var v = await device.GetBatteryValue();
        Assert.Equal(0x31, v.Value);

        var mi = device.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(device, ["test"]);
        Assert.Equal("test", device.ErrorMessage);

        await device.Disconnect();
        Assert.False(device.Connected);

        await device.DisposeAsync();
    }

    [Fact]
    public async Task GetPrimaryServices_Ok()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<List<string>?>("getPrimaryServices", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["battery_service"]);
        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();
        var v = await device.GetPrimaryServices();
        Assert.Equal("battery_service", v[0].UUID);
    }

    [Fact]
    public async Task GetCharacteristics_Ok()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<string?>("getPrimaryService", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult("battery_service");
        Context.JSInterop.Setup<List<string>?>("getCharacteristics", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["battery_level"]);
        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();
        var service = await device.GetPrimaryService("battery_service");
        Assert.NotNull(service);

        var v = await service.GetCharacteristics();
        Assert.Equal("battery_level", v[0].UUID);
    }

    [Fact]
    public async Task ReadValue_null()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<string?>("getPrimaryService", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult("battery_service");
        Context.JSInterop.Setup<string?>("getCharacteristic", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult("battery_level");
        Context.JSInterop.Setup<byte[]?>("readValue", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(null);
        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();
        var v = await device.ReadValue("battery_service", "battery_level");
        Assert.Null(v);

        var data = await device.GetBatteryValue();
        Assert.Null(v);

        var service = await device.GetPrimaryService("battery_service");
        Assert.NotNull(service);

        var mi = service.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(service, ["test"]);
        Assert.Equal("test", service.ErrorMessage);

        var characteristic = await service.GetCharacteristic("battery_level");
        Assert.NotNull(characteristic);

        mi = characteristic.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(characteristic, ["test"]);
        Assert.Equal("test", characteristic.ErrorMessage);

        v = await characteristic.ReadValue();
        Assert.Null(v);
    }

    [Fact]
    public async Task GetAvailability_Ok()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<bool>("getAvailability", matcher => matcher.Arguments.Count == 0).SetResult(true);
        var bluetooth = Context.Services.GetRequiredService<IBluetooth>();

        await bluetooth.GetAvailability();
        Assert.True(bluetooth.IsSupport);
        Assert.True(bluetooth.IsAvailable);
        Assert.Null(bluetooth.ErrorMessage);

        var mi = bluetooth.GetType().GetMethod("OnError");
        Assert.NotNull(mi);
        mi.Invoke(bluetooth, ["test"]);
        Assert.Equal("test", bluetooth.ErrorMessage);
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

    [Fact]
    public void DateTimeOffset_Ok()
    {
        var val = "2018-12-04T13:53:42+07:00";
        Assert.True(DateTimeOffset.TryParseExact(val, "yyyy-MM-ddTHH:mm:sszzz", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var d));
    }

    [Fact]
    public async Task GetDeviceInfo_null()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<BluetoothDeviceInfo?>("getDeviceInfo", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(new BluetoothDeviceInfo() { ManufacturerName = "test" });

        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();
        var v = await device.GetDeviceInfo();
        Assert.Equal("test", v?.ManufacturerName);
    }

    [Fact]
    public async Task GetCurrentTime_null()
    {
        Context.JSInterop.Setup<bool>("init").SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<bool>("connect", matcher => (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult(true);
        Context.JSInterop.Setup<string?>("getCurrentTime", matcher => (matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false)).SetResult("2024-10-10T10:05:10+07:00");

        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();
        var v = await device.GetCurrentTime();
        Assert.Equal("2024-10-10 10:05:10", v.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.Equal(7, v.Value.Offset.TotalHours);
    }

    [Fact]
    public void BluetoothDeviceInfo_Ok()
    {
        var info = new BluetoothDeviceInfo()
        {
            FirmwareRevision = "test",
            HardwareRevision = "test",
            IEEERegulatoryCertificationDataList = "test",
            ManufacturerName = "test",
            ModelNumber = "test",
            SoftwareRevision = "test",
            SystemId = new SystemId()
            {
                ManufacturerIdentifier = "test",
                OrganizationallyUniqueIdentifier = "test",
            },
            PnPID = new PnPID()
            {
                ProductId = "test",
                ProductVersion = "test",
                VendorIdSource = "test",
            }
        };
        Assert.Equal("test", info.FirmwareRevision);
        Assert.Equal("test", info.HardwareRevision);
        Assert.Equal("test", info.SoftwareRevision);
        Assert.Equal("test", info.IEEERegulatoryCertificationDataList);
        Assert.Equal("test", info.ManufacturerName);
        Assert.Equal("test", info.ModelNumber);
        Assert.Equal("test", info.SystemId.ManufacturerIdentifier);
        Assert.Equal("test", info.SystemId.OrganizationallyUniqueIdentifier);
        Assert.Equal("test", info.PnPID.ProductId);
        Assert.Equal("test", info.PnPID.ProductVersion);
        Assert.Equal("test", info.PnPID.VendorIdSource);
    }

    [Fact]
    public void GetAllServices_Ok()
    {
        var services = BluetoothRequestOptions.GetAllServices();
        Assert.True(services.Count > 0);
    }

    [Fact]
    public void UUID_Ok()
    {
        var attr = new BluetoothUUIDAttribute("1234");
        Assert.Equal("1234", attr.Name);
    }

    [Fact]
    public async Task Notifications_Ok()
    {
        Context.JSInterop.Setup<bool>("init", matcher => matcher.Arguments.Count == 0).SetResult(true);
        Context.JSInterop.Setup<bool>("connect", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<string[]?>("requestDevice", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(["test", "id_1234"]);
        Context.JSInterop.Setup<string?>("getPrimaryService", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult("battery_service");
        Context.JSInterop.Setup<string?>("getCharacteristic", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult("battery_level");
        Context.JSInterop.Setup<bool?>("startNotifications", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<bool?>("stopNotifications", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        var bluetoothService = Context.Services.GetRequiredService<IBluetooth>();
        var device = await bluetoothService.RequestDevice();
        Assert.NotNull(device);

        await device.Connect();

        var service = await device.GetPrimaryService("battery_service");
        Assert.NotNull(service);

        var characteristic = await service.GetCharacteristic("battery_level");
        Assert.NotNull(characteristic);

        byte[]? buffer = null;
        var notification = await characteristic.StartNotifications(payload =>
        {
            buffer = payload;
            return Task.CompletedTask;
        });
        Assert.True(notification);
        Assert.True(characteristic.IsNotify);

        notification = await characteristic.StartNotifications(payload => { return Task.CompletedTask; });
        Assert.False(notification);

        // trigger notification
        var mi = characteristic.GetType().GetMethod("OnNotification");
        Assert.NotNull(mi);
        Assert.Null(buffer);
        mi.Invoke(characteristic, ["battery_level", "1"u8.ToArray()]);
        Assert.NotNull(buffer);

        notification = await characteristic.StopNotifications();
        Assert.True(notification);
        Assert.False(characteristic.IsNotify);

        Context.JSInterop.Setup<bool?>("startNotifications", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(null);
        await characteristic.StartNotifications(payload =>
        {
            return Task.CompletedTask;
        });
        notification = await characteristic.StopNotifications();
        Assert.True(notification);

        Context.JSInterop.Setup<bool?>("startNotifications", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(true);
        Context.JSInterop.Setup<bool?>("stopNotifications", matcher => matcher.Arguments[0]?.ToString()?.StartsWith("bb_bt_") ?? false).SetResult(null);
        await characteristic.StartNotifications(payload =>
        {
            return Task.CompletedTask;
        });
        notification = await characteristic.StopNotifications();
        Assert.False(notification);

    }
}
