// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Bluetooth 扩展方法</para>
/// <para lang="en">Bluetooth Extensions</para>
/// </summary>
public static class BluetoothExtensions
{
    /// <summary>
    /// <para lang="zh">获得指定蓝牙服务字符串集合</para>
    /// <para lang="en">Get Bluetooth Service List</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static List<string> GetServicesList(this IEnumerable<BluetoothServicesEnum> services) => services.Select(i =>
    {
        var v = i.ToString();
        var attributes = typeof(BluetoothServicesEnum).GetField(v)!.GetCustomAttribute<JsonPropertyNameAttribute>(false)!;
        return attributes.Name;
    }).ToList();
}
