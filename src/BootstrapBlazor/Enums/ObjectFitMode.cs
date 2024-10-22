// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Image 组件图片填充模式
/// </summary>
public enum ObjectFitMode
{
    /// <summary>
    /// 填充 内容拉伸填满
    /// </summary>
    [Description("fill")]
    Fill,

    /// <summary>
    /// 保持原有尺寸比例 会留白
    /// </summary>
    [Description("contain")]
    Contain,

    /// <summary>
    /// 覆盖 保持原有尺寸比例 宽度和高度至少有一个和容器一致
    /// </summary>
    [Description("cover")]
    Cover,

    /// <summary>
    /// 保持原有尺寸比例
    /// </summary>
    [Description("none")]
    None,

    /// <summary>
    /// 依次设置了 none 或 contain, 最终呈现的是尺寸比较小的那个
    /// </summary>
    [Description("scale-down")]
    ScaleDown
}
