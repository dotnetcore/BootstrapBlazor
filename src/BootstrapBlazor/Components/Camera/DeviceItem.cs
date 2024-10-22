// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 摄像头信息类
/// </summary>
public class DeviceItem
{
    /// <summary>
    /// 获得/设置 设备 ID
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    /// 获得/设置 设备标签
    /// </summary>
    public string Label { get; set; } = "";
}
