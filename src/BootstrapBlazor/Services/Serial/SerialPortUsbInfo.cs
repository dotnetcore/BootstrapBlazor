// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Usb 串口设备信息类</para>
///  <para lang="en">Usb 串口设备信息类</para>
/// </summary>
public class SerialPortUsbInfo
{
    /// <summary>
    ///  <para lang="zh">厂商 Id</para>
    ///  <para lang="en">厂商 Id</para>
    /// </summary>
    public string? UsbVendorId { get; set; }

    /// <summary>
    ///  <para lang="zh">产品 Id</para>
    ///  <para lang="en">产品 Id</para>
    /// </summary>
    public string? UsbProductId { get; set; }
}
