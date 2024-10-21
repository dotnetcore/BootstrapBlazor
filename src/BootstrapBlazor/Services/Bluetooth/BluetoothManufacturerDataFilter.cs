// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// BluetoothManufacturerDataFilter 配置类
/// </summary>
public class BluetoothManufacturerDataFilter
{
    /// <summary>
    /// A mandatory number identifying the manufacturer of the device. Company identifiers are listed in the Bluetooth specification Assigned numbers, Section 7. For example, to match against devices manufactured by "Digianswer A/S", with assigned hex number 0x000C, you would specify 12.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CompanyIdentifier { get; set; }

    /// <summary>
    /// The data prefix. A buffer containing values to match against the values at the start of the advertising manufacturer data.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataPrefix { get; set; }

    /// <summary>
    /// This allows you to match against bytes within the manufacturer data, by masking some bytes of the service data dataPrefix.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mask { get; set; }
}
