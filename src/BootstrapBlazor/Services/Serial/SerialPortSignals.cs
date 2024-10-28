﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

//RS-232C接口定义(DB9)
//引脚 定义 符号
//1 载波检测 DCD（Data Carrier Detect）
//2 接收数据 RXD（Received Data）
//3 发送数据 TXD（Transmit Data）
//4 数据终端准备就绪 DTR（Data Terminal Ready）
//5 信号地 SG（Signal Ground）
//6 数据准备就绪 DSR（Data Set Ready）
//7 请求发送 RTS（Request To Send）
//8 清除发送 CTS（Clear To Send）
//9 振铃提示 RI（Ring Indicator）

/// <summary>
/// 串口信号
/// </summary>
public class SerialPortSignals
{
    /// <summary>
    /// 振铃提示 RI（Ring Indicator）
    /// 如果 RI 为 true，则表示已检测到振铃。如果 RI 为 false，则表示未检测到振铃。Pin 9
    /// </summary>
    [JsonPropertyName("ringIndicator")]
    public bool RING { get; set; }

    /// <summary>
    /// 数据准备就绪 DSR（Data Set Ready）
    /// 如果 DSR 为 true，则表示已准备好接收数据。如果 DSR 为 false，则表示未准备好接收数据。Pin 6
    /// </summary>
    [JsonPropertyName("dataSetReady")]
    public bool DSR { get; set; }

    /// <summary>
    /// 清除发送 CTS（Clear To Send）
    /// 如果 CTS 为 true，则表示已准备好发送数据。如果 CTS 为 false，则表示未准备好发送数据。Pin 8
    /// </summary>
    [JsonPropertyName("clearToSend")]
    public bool CTS { get; set; }

    /// <summary>
    /// 载波检测 DCD（Data Carrier Detect）
    /// 如果 DCD 为 true，则表示已检测到载波。如果 DCD 为 false，则表示未检测到载波。Pin 1
    /// </summary>
    [JsonPropertyName("dataCarrierDetect")]
    public bool DCD { get; set; }
}
