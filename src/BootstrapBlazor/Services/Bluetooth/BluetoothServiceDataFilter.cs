// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">BluetoothServiceDataFilter 配置类</para>
///  <para lang="en">BluetoothServiceDataFilter Configuration Class</para>
/// </summary>
public class BluetoothServiceDataFilter
{
    /// <summary>
    ///  <para lang="zh">GATT service name, the service UUID, or the UUID 16-bit or 32-bit form. This takes the same values as the elements of the services array.</para>
    ///  <para lang="en">The GATT service name, the service UUID, or the UUID 16-bit or 32-bit form. This takes the same values as the elements of the services array.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Service { get; set; }

    /// <summary>
    ///  <para lang="zh">数据 prefix. A buffer containing values to match against the values at the start of the advertising service 数据.</para>
    ///  <para lang="en">The data prefix. A buffer containing values to match against the values at the start of the advertising service data.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataPrefix { get; set; }

    /// <summary>
    ///  <para lang="zh">This allows you to match against bytes within the manufacturer 数据, by masking some bytes of the service 数据 数据Prefix.</para>
    ///  <para lang="en">This allows you to match against bytes within the manufacturer data, by masking some bytes of the service data dataPrefix.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mask { get; set; }
}
