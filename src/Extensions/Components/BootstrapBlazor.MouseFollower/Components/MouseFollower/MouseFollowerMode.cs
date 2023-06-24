// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// MouseFollower 显示模式枚举
/// </summary>
public enum MouseFollowerMode
{
    /// <summary>
    /// Normal 默认模式
    /// </summary>
    Normal,

    /// <summary>
    /// Text 文本模式
    /// </summary>
    [Description("text")]
    Text,

    /// <summary>
    /// Image 图片模式
    /// </summary>
    [Description("image")]
    Image,

    /// <summary>
    /// Video 视频模式
    /// </summary>
    [Description("video")]
    Video
}
