// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 自动断帧方式
/// </summary>
public enum AutoFrameBreakType
{
    /// <summary>
    /// 未启用自动断帧
    /// </summary>
    None,

    /// <summary>
    /// 字符断帧
    /// </summary>
    Character,

    /// <summary>
    /// 空闲中断 (未完成)
    /// </summary>
    Timeout,

    /// <summary>
    /// 帧头、帧尾 (未实现)
    /// <para></para>例如: 帧头（AA 、BB） + 数据长度 + 数据  + CRC校验 + 帧尾（CC、DD）
    /// </summary>
    FrameTail,

    /// <summary>
    /// 字符间隔 (未实现)
    /// </summary>
    CharacterInterval,
}
