// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 串口信号设置
/// </summary>
public class SerialPortSignalsOptions
{
    /// <summary>
    /// 中断
    /// <para></para>如果 Break 为 true，则表示已中断。如果 Break 为 false，则表示未中断。
    /// </summary>
    [JsonPropertyName("break")]
    public bool Break { get; set; }

    /// <summary>
    /// 数据终端准备就绪 DTR（Data Terminal Ready）
    /// <para></para>如果 DTR 为 true，则表示已准备好接收数据。如果 DTR 为 false，则表示未准备好接收数据。Pin 4
    /// </summary>
    [JsonPropertyName("dataTerminalReady")]
    public bool DTR { get; set; }

    /// <summary>
    /// 请求发送 RTS（Request To Send）
    /// <para></para>如果 RTS 为 true，则表示已准备好发送数据。如果 RTS 为 false，则表示未准备好发送数据。Pin 7
    /// </summary>
    [JsonPropertyName("requestToSend")]
    public bool RTS { get; set; }
}
