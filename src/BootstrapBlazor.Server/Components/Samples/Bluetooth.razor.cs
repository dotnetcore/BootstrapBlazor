// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    private string? BluetoothDeviceName => _blueDevice?.Name ?? "Unknown or Unsupported Device";

    private IBluetoothDevice? _blueDevice;

    private string? _batteryValue = null;

    private string? _batteryValueString = null;

    private async Task RequestDevice()
    {
        _blueDevice = await BluetoothService.RequestDevice(["device_information"]);
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
            else
            {
                _batteryValue = null;
                _batteryValueString = null;
            }
        }
    }

    private async Task GetBatteryValue()
    {
        _batteryValue = null;
        _batteryValueString = null;

        if (_blueDevice != null)
        {
            var val = await _blueDevice.GetBatteryValue();
            if (val == 0 && !string.IsNullOrEmpty(_blueDevice.ErrorMessage))
            {
                await ToastService.Error("Battery Value", _blueDevice.ErrorMessage);
                return;
            }

            _batteryValue = $"{val}";
            _batteryValueString = $"{_batteryValue} %";
        }
    }

    private readonly List<string> _deviceInfoList = [];

    private async Task GetDeviceInfoValue()
    {
        _deviceInfoList.Clear();
        if (_blueDevice != null)
        {
            var info = await _blueDevice.GetDeviceInfo();
            _deviceInfoList.Add($"ManufacturerName: {info?.ManufacturerName}");
            _deviceInfoList.Add($"ModuleNumber: {info?.ModelNumber}");
        }
    }
}
