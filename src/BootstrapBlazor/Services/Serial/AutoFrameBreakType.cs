// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">自动断帧方式
///</para>
/// <para lang="en">自动断帧方式
///</para>
/// </summary>
public enum AutoFrameBreakType
{
    /// <summary>
    /// <para lang="zh">未启用自动断帧
    ///</para>
    /// <para lang="en">未启用自动断帧
    ///</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">字符断帧
    ///</para>
    /// <para lang="en">字符断帧
    ///</para>
    /// </summary>
    Character,

    /// <summary>
    /// <para lang="zh">空闲中断 (未完成)
    ///</para>
    /// <para lang="en">空闲中断 (未完成)
    ///</para>
    /// </summary>
    Timeout,

    /// <summary>
    /// <para lang="zh">帧头、帧尾 (未实现)
    /// <para></para>例如: 帧头（AA 、BB） + 数据长度 + 数据  + CRC校验 + 帧尾（CC、DD）
    ///</para>
    /// <para lang="en">帧头、帧尾 (未实现)
    /// <para></para>例如: 帧头（AA 、BB） + data长度 + data  + CRC校验 + 帧尾（CC、DD）
    ///</para>
    /// </summary>
    FrameTail,

    /// <summary>
    /// <para lang="zh">字符间隔 (未实现)
    ///</para>
    /// <para lang="en">字符间隔 (未实现)
    ///</para>
    /// </summary>
    CharacterInterval,
}
