// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// BluetoothRequestOptions 参数类
/// </summary>
public class BluetoothRequestOptions
{
    /// <summary>
    /// An array of filter objects indicating the properties of devices that will be matched. To match a filter object, a device must match all the values of the filter: all its specified services, name, namePrefix, and so on
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<BluetoothFilter>? Filters { get; set; }

    /// <summary>
    /// An array of filter objects indicating the characteristics of devices that will be excluded from matching. The properties of the array elements are the same as for <see cref="Filters"/>.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<BluetoothFilter>? ExclusionFilters { get; set; }

    /// <summary>
    /// An array of optional service identifiers.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? OptionalServices { get; set; }

    /// <summary>
    /// An optional array of integer manufacturer codes. This takes the same values as companyIdentifier.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? OptionalManufacturerData { get; set; }

    /// <summary>
    /// A boolean value indicating that the requesting script can accept all Bluetooth devices. The default is false.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AcceptAllDevices { get; set; }
}
