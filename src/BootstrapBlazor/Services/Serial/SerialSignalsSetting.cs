// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 串口信号设置
/// </summary>
public class SerialSignalsSetting
{
    /// <summary>
    /// 中断
    /// <para></para>如果 Break 为 true，则表示已中断。如果 Break 为 false，则表示未中断。
    /// </summary>
    public bool? Break { get; set; }

    /// <summary>
    /// 数据终端准备就绪 DTR（Data Terminal Ready）
    /// <para></para>如果 DTR 为 true，则表示已准备好接收数据。如果 DTR 为 false，则表示未准备好接收数据。Pin 4
    /// </summary>
    [JsonPropertyName("DTR")]
    public bool? DTR { get; set; }

    /// <summary>
    /// 请求发送 RTS（Request To Send）
    /// <para></para>如果 RTS 为 true，则表示已准备好发送数据。如果 RTS 为 false，则表示未准备好发送数据。Pin 7
    /// </summary>
    [JsonPropertyName("RTS")]
    public bool? RTS { get; set; }
}
