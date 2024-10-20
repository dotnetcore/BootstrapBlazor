// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

    /// <summary>
    /// 获得 设备电量方法
    /// </summary>
    /// <param name="blueDevice"></param>
    /// <returns></returns>
    public static async Task<string?> GetHeartRateValue(this IBluetoothDevice blueDevice)
    {
        string? value = null;
        var data = await blueDevice.ReadValue("heart_rate", "heart_rate_measurement");
        if (data is { Length: > 0 })
        {
            value = $"{data[0]}%";
        }
        return value;
    }
}
