// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">串口通讯参数</para>
/// <para lang="en">串口通讯参数</para>
/// </summary>
public class SerialPortOptions
{
    /// <summary>
    /// <para lang="zh">波特率 默认 9600</para>
    /// <para lang="en">波特率 Default is 9600</para>
    /// </summary>
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// <para lang="zh">数据位 7 或 8 默认 8</para>
    /// <para lang="en">data位 7 或 8 Default is 8</para>
    /// </summary>
    public int DataBits { get; set; } = 8;

    /// <summary>
    /// <para lang="zh">停止位 1 或 2 默认为 1</para>
    /// <para lang="en">停止位 1 或 2 Default is为 1</para>
    /// </summary>
    public int StopBits { get; set; } = 1;

    /// <summary>
    /// <para lang="zh">校验位 none、even、odd 默认 "none"</para>
    /// <para lang="en">校验位 none、even、odd Default is "none"</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortParityType ParityType { get; set; }

    /// <summary>
    /// <para lang="zh">读写缓冲区 默认 255</para>
    /// <para lang="en">读写缓冲区 Default is 255</para>
    /// </summary>
    public int BufferSize { get; set; } = 255;

    /// <summary>
    /// <para lang="zh">流控制 "none"或"hardware" 默认值为"none"</para>
    /// <para lang="en">流控制 "none"或"hardware" Default is值为"none"</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public SerialPortFlowControlType FlowControlType { get; set; }
}
