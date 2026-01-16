// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">摄像头信息类</para>
///  <para lang="en">摄像头信息类</para>
/// </summary>
public class DeviceItem
{
    /// <summary>
    ///  <para lang="zh">获得/设置 设备 ID</para>
    ///  <para lang="en">Gets or sets 设备 ID</para>
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    ///  <para lang="zh">获得/设置 设备标签</para>
    ///  <para lang="en">Gets or sets 设备标签</para>
    /// </summary>
    public string Label { get; set; } = "";
}
