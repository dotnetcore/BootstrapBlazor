// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Usb 串口设备信息类
/// </summary>
public class SerialPortUsbInfo
{
    /// <summary>
    /// 厂商 Id
    /// </summary>
    public string? UsbVendorId { get; set; }

    /// <summary>
    /// 产品 Id
    /// </summary>
    public string? UsbProductId { get; set; }
}
