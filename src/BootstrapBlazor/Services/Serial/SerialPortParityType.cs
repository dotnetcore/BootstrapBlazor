// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">校验位枚举
///</para>
/// <para lang="en">校验位enum
///</para>
/// </summary>
[JsonEnumConverter(true)]
public enum SerialPortParityType
{
    /// <summary>
    /// <para lang="zh">每个数据字不发送奇偶校验位
    ///</para>
    /// <para lang="en">每个data字不发送奇偶校验位
    ///</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">数据字加上奇偶校验位具有偶奇偶校验
    ///</para>
    /// <para lang="en">data字加上奇偶校验位具有偶奇偶校验
    ///</para>
    /// </summary>
    Even,

    /// <summary>
    /// <para lang="zh">数据字加奇偶校验位具有奇校验
    ///</para>
    /// <para lang="en">data字加奇偶校验位具有奇校验
    ///</para>
    /// </summary>
    Odd
}
