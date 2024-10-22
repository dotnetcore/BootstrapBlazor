// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IBluetoothDevice 扩展方法
/// </summary>
public static class IBluetoothDeviceExtensions
{
    /// <summary>
    /// 获得 设备电量方法
    /// </summary>
    /// <param name="blueDevice"></param>
    /// <returns></returns>
    public static async Task<byte> GetBatteryValue(this IBluetoothDevice blueDevice)
    {
        byte value = 0;
        var data = await blueDevice.ReadValue("battery_service", "battery_level");
        if (data is { Length: > 0 })
        {
            value = data[0];
        }
        return value;
    }
}
