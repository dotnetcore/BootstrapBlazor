// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// BluetoothServiceDataFilter 配置类
/// </summary>
public class BluetoothServiceDataFilter
{
    /// <summary>
    /// The GATT service name, the service UUID, or the UUID 16-bit or 32-bit form. This takes the same values as the elements of the services array.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Service { get; set; }

    /// <summary>
    /// The data prefix. A buffer containing values to match against the values at the start of the advertising service data.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataPrefix { get; set; }

    /// <summary>
    /// This allows you to match against bytes within the manufacturer data, by masking some bytes of the service data dataPrefix.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mask { get; set; }
}
