// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Bluetooth
/// </summary>
public partial class Bluetooth
{
    [Inject, NotNull]
    private IBluetoothService? BluetoothService { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private IBluetoothDevice? _blueDevice;

    private string? _batteryValue = null;

    private async Task GetAvailability()
    {
        await BluetoothService.GetAvailability();
        if (BluetoothService.IsSupport == false)
        {
            await ToastService.Error(Localizer["NotSupportBluetoothTitle"], Localizer["NotSupportBluetoothContent"]);
        }
        if (BluetoothService.IsAvailable == false)
        {
            await ToastService.Error(Localizer["NoBluetoothAdapterTitle"], Localizer["NoBluetoothAdapterContent"]);
        }
    }

    private async Task RequestDevice()
    {
        if (BluetoothService.IsAvailable)
        {
            _blueDevice = await BluetoothService.RequestDevice(["battery_service"]);
        }
    }

    private async Task Connect()
    {
        if (_blueDevice != null)
        {
            await _blueDevice.Connect();
        }
    }

    private async Task Disconnect()
    {
        if (_blueDevice != null)
        {
            await _blueDevice.Disconnect();
        }
    }

    private async Task GetBatteryValue()
    {
        if (_blueDevice != null)
        {
            _batteryValue = await _blueDevice.GetBatteryValue();
        }
    }
}
