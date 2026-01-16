// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BluetoothRequestOptions 参数类</para>
/// <para lang="en">BluetoothRequestOptions Parameter Class</para>
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
    /// <remarks>This option is appropriate when devices have not advertised enough information for filtering to be useful. When acceptAllDevices is set to true you should omit all filters and exclusionFilters, and you must set optionalServices to be able to use the returned device.</remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool AcceptAllDevices { get; set; }

    /// <summary>
    /// <para lang="zh">获得所有蓝牙服务</para>
    /// <para lang="en">Get All Bluetooth Services</para>
    /// </summary>
    /// <returns></returns>
    public static List<string> GetAllServices() => typeof(BluetoothServicesEnum).GetEnumNames().Select(i =>
    {
        var v = i.ToString();
        var attributes = typeof(BluetoothServicesEnum).GetField(v)!.GetCustomAttribute<JsonPropertyNameAttribute>(false)!;
        return attributes.Name;
    }).ToList();
}
