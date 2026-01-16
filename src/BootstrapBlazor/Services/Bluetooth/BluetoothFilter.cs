// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BluetoothFilter 类</para>
/// <para lang="en">BluetoothFilter Class</para>
/// </summary>
public class BluetoothFilter
{
    /// <summary>
    /// An array of values indicating the Bluetooth GATT (Generic Attribute Profile) services that a Bluetooth device must support. Each value can be a valid name from the GATT assigned services list, such as 'battery_service' or 'blood_pressure'. You can also pass a full service UUID such as '0000180F-0000-1000-8000-00805f9b34fb' or the short 16-bit (0x180F) or 32-bit alias. Note that these are the same values that can be passed to BluetoothUUID.getService().
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Services { get; set; }

    /// <summary>
    /// A string containing the precise name of the device to match against.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// A string containing the name prefix to match against. All devices that have a name starting with this string will be matched.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NamePrefix { get; set; }

    /// <summary>
    /// An array of objects matching against manufacturer data in the Bluetooth Low Energy (BLE) advertising packets.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<BluetoothManufacturerDataFilter>? ManufacturerData { get; set; }

    /// <summary>
    /// An array of objects matching against service data in the Bluetooth Low Energy (BLE) advertising packets.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<BluetoothServiceDataFilter>? ServiceData { get; set; }
}
