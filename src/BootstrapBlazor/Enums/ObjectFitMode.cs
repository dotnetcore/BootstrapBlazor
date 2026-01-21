// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Image 组件图片填充模式</para>
/// <para lang="en">Image component图片填充模式</para>
/// </summary>
public enum ObjectFitMode
{
    /// <summary>
    /// <para lang="zh">填充 内容拉伸填满</para>
    /// <para lang="en">填充 content拉伸填满</para>
    /// </summary>
    [Description("fill")]
    Fill,

    /// <summary>
    /// <para lang="zh">保持原有尺寸比例 会留白</para>
    /// <para lang="en">保持原有尺寸比例 会留白</para>
    /// </summary>
    [Description("contain")]
    Contain,

    /// <summary>
    /// <para lang="zh">覆盖 保持原有尺寸比例 宽度和高度至少有一个和容器一致</para>
    /// <para lang="en">覆盖 保持原有尺寸比例 width和height至少有一个和容器一致</para>
    /// </summary>
    [Description("cover")]
    Cover,

    /// <summary>
    /// <para lang="zh">保持原有尺寸比例</para>
    /// <para lang="en">保持原有尺寸比例</para>
    /// </summary>
    [Description("none")]
    None,

    /// <summary>
    /// <para lang="zh">依次设置了 none 或 contain, 最终呈现的是尺寸比较小的那个</para>
    /// <para lang="en">依次Sets了 none 或 contain, 最终呈现的是尺寸比较小的那个</para>
    /// </summary>
    [Description("scale-down")]
    ScaleDown
}
