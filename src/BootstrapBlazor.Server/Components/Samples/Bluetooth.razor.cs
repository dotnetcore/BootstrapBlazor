// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Bluetooth
/// </summary>
public partial class Bluetooth
{
    [Inject, NotNull]
    private IBluetooth? BluetoothService { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private string? BluetoothDeviceName => _blueDevice?.Name ?? "Unknown or Unsupported Device";

    private IBluetoothDevice? _blueDevice;

    private string? _batteryValue = null;

    private string? _batteryValueString = null;

    private string? _currentTimeValueString = null;

    private string? _readValueString = null;

    private List<IBluetoothService> _bluetoothServices = [];

    private List<IBluetoothCharacteristic> _bluetoothCharacteristics = [];

    private string? _selectedService;

    private string? _selectedCharacteristic;

    private List<SelectedItem> ServicesList => _bluetoothServices.Select(i => new SelectedItem(i.UUID, FormatServiceName(i))).ToList();

    private List<SelectedItem> CharacteristicsList => _bluetoothCharacteristics.Select(i => new SelectedItem(i.UUID, FormatCharacteristicsName(i))).ToList();

    private Dictionary<string, string> ServiceUUIDList = [];

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ServiceUUIDList = Enum.GetNames(typeof(BluetoothServicesEnum)).Select(i =>
        {
            var attributes = typeof(BluetoothServicesEnum).GetField(i)!.GetCustomAttribute<BluetoothUUIDAttribute>(false)!;
            return new KeyValuePair<string, string>(attributes.Name.ToUpperInvariant(), i);
        }).ToDictionary();
    }

    private string FormatServiceName(IBluetoothService service)
    {
        var uuId = service.UUID.ToUpperInvariant();
        return ServiceUUIDList.TryGetValue(uuId, out var serviceName)
            ? $"{serviceName}({uuId})" : uuId;
    }

    private string FormatCharacteristicsName(IBluetoothCharacteristic characteristic) => characteristic.UUID.ToUpperInvariant();

    private async Task RequestDevice()
    {
        var options = new BluetoothRequestOptions()
        {
            AcceptAllDevices = true,
            OptionalServices = ["device_information", "current_time", "battery_service"]
        };
        _blueDevice = await BluetoothService.RequestDevice(options);
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
                _deviceInfoList.Clear();
                _bluetoothServices.Clear();
                _bluetoothCharacteristics.Clear();
                _readValueString = null;
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

    private async Task GetTimeValue()
    {
        _currentTimeValueString = null;

        if (_blueDevice != null)
        {
            var val = await _blueDevice.GetCurrentTime();
            if (val.HasValue && !string.IsNullOrEmpty(_blueDevice.ErrorMessage))
            {
                await ToastService.Error("Current Time", _blueDevice.ErrorMessage);
                return;
            }
            _currentTimeValueString = val.ToString();
        }
    }

    private readonly List<string> _deviceInfoList = [];

    private async Task GetDeviceInfoValue()
    {
        _deviceInfoList.Clear();
        if (_blueDevice != null)
        {
            var info = await _blueDevice.GetDeviceInfo();
            _deviceInfoList.Add($"Manufacturer Name: {info?.ManufacturerName}");
            _deviceInfoList.Add($"Module Number: {info?.ModelNumber}");
            _deviceInfoList.Add($"Firmware Revision: {info?.FirmwareRevision}");
            _deviceInfoList.Add($"Hardware Revision: {info?.HardwareRevision}");
            _deviceInfoList.Add($"Software Revision: {info?.SoftwareRevision}");
        }
    }

    private async Task GetServices()
    {
        if (_blueDevice != null)
        {
            _bluetoothServices = await _blueDevice.GetPrimaryServices();
        }
    }

    private async Task GetCharacteristics()
    {
        if (_blueDevice != null && !string.IsNullOrEmpty(_selectedService))
        {
            _bluetoothCharacteristics.Clear();
            var service = _bluetoothServices.Find(i => i.UUID == _selectedService);
            if (service != null)
            {
                _bluetoothCharacteristics = await service.GetCharacteristics();
            }
        }
    }

    private async Task ReadValue()
    {
        _readValueString = null;
        if (_blueDevice != null && !string.IsNullOrEmpty(_selectedService) && !string.IsNullOrEmpty(_selectedCharacteristic))
        {
            var characteristics = _bluetoothCharacteristics.Find(i => i.UUID == _selectedCharacteristic);
            if (characteristics != null)
            {
                var data = await characteristics.ReadValue();
                if (data != null)
                {
                    _readValueString = string.Join(" ", data.Select(i => Convert.ToString(i, 16).PadLeft(2, '0').ToUpperInvariant()));
                }
            }
        }
    }
}
