// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 消息显示模式
/// </summary>
public enum MessageShowMode
{
    /// <summary>
    /// 单个模式，始终显示一个消息弹窗
    /// </summary>
    Single,

    /// <summary>
    /// 多个模式，消息均显示
    /// </summary>
    Multiple
}
