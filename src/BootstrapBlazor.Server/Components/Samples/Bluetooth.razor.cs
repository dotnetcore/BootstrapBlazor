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

    private async Task RequestDevice()
    {
        _blueDevice = await BluetoothService.RequestDevice(["battery_service"]);
        if (BluetoothService.IsSupport == false)
        {
            await ToastService.Error(Localizer["NotSupportBluetoothTitle"], Localizer["NotSupportBluetoothContent"]);
            return;
        }

        if (_blueDevice == null && !string.IsNullOrEmpty(BluetoothService.ErrorMessage))
        {
            await ToastService.Error("Request", BluetoothService.ErrorMessage);
        }
    }

    private async Task Connect()
    {
        if (_blueDevice != null)
        {
            var ret = await _blueDevice.Connect();
            if (ret == false && !string.IsNullOrEmpty(_blueDevice.ErrorMessage))
            {
                await ToastService.Error("Connect", _blueDevice.ErrorMessage);
            }
        }
    }

    private async Task Disconnect()
    {
        if (_blueDevice != null)
        {
            var ret = await _blueDevice.Disconnect();
            if (ret == false && !string.IsNullOrEmpty(_blueDevice.ErrorMessage))
            {
                await ToastService.Error("Disconnect", _blueDevice.ErrorMessage);
            }
        }
    }

    private async Task GetBatteryValue()
    {
        if (_blueDevice != null)
        {
            _batteryValue = await _blueDevice.GetBatteryValue();
            if (string.IsNullOrEmpty(_batteryValue) && !string.IsNullOrEmpty(_blueDevice.ErrorMessage))
            {
                await ToastService.Error("Battery Value", _blueDevice.ErrorMessage);
            }
        }
    }
}
