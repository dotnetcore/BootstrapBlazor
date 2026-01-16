// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">物体偏移量类</para>
/// <para lang="en">Object offset class</para>
/// </summary>
public class Offset
{
    /// <summary>
    /// <para lang="zh">获得/设置 X 轴偏移量</para>
    /// <para lang="en">Get/Set global x-coordinate</para>
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Y 轴偏移量</para>
    /// <para lang="en">Get/Set global y-coordinate</para>
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 宽度</para>
    /// <para lang="en">Get/Set width</para>
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 高度</para>
    /// <para lang="en">Get/Set height</para>
    /// </summary>
    public int Height { get; set; }
}
