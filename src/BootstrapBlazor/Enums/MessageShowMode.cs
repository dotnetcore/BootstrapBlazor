// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">消息显示模式
///</para>
/// <para lang="en">消息display模式
///</para>
/// </summary>
public enum MessageShowMode
{
    /// <summary>
    /// <para lang="zh">单个模式，始终显示一个消息弹窗
    ///</para>
    /// <para lang="en">单个模式，始终display一个消息弹窗
    ///</para>
    /// </summary>
    Single,

    /// <summary>
    /// <para lang="zh">多个模式，消息均显示
    ///</para>
    /// <para lang="en">多个模式，消息均display
    ///</para>
    /// </summary>
    Multiple
}
