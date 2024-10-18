// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 串口通讯参数
/// </summary>
public class SerialOptions
{
    /// <summary>
    /// 波特率 默认 9600
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BaudRate { get; set; }

    /// <summary>
    /// 数据位 7 或 8 默认 8
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DataBits { get; set; }

    /// <summary>
    /// 停止位 1 或 2 默认为 1
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StopBits { get; set; }

    /// <summary>
    /// 流控制 none、even、odd 默认 "none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SerialFlowControlType? ParityType { get; set; }

    /// <summary>
    /// 读写缓冲区 默认 255
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BufferSize { get; set; } = 255;

    /// <summary>
    /// 校验位 "none"或"hardware" 默认值为"none" 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SerialParityType? FlowControlType { get; set; }

    /// <summary>
    /// 自动连接设备
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AutoConnect { get; set; }

    /// <summary>
    /// 自动断帧方式
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AutoFrameBreakType? AutoFrameBreakType { get; set; }

    /// <summary>
    /// 断帧字符(默认\n)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FrameBreakChar { get; set; }

    /// <summary>
    /// 自动检查状态
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool AutoGetSignals { get; set; }
}
