﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 串口通讯参数
/// </summary>
public class SerialPortOptions
{
    /// <summary>
    /// 波特率 默认 9600
    /// </summary>
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// 数据位 7 或 8 默认 8
    /// </summary>
    public int DataBits { get; set; } = 8;

    /// <summary>
    /// 停止位 1 或 2 默认为 1
    /// </summary>
    public int StopBits { get; set; } = 1;

    /// <summary>
    /// 校验位 none、even、odd 默认 "none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortParityType ParityType { get; set; }

    /// <summary>
    /// 读写缓冲区 默认 255
    /// </summary>
    public int BufferSize { get; set; } = 255;

    /// <summary>
    /// 流控制 "none"或"hardware" 默认值为"none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortFlowControlType FlowControlType { get; set; }
}
