// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">列宽设置类
///</para>
/// <para lang="en">列宽Sets类
///</para>
/// </summary>
struct ColumnWidth
{
    /// <summary>
    /// <para lang="zh">获得/设置 列名称
    ///</para>
    /// <para lang="en">Gets or sets 列名称
    ///</para>
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列宽
    ///</para>
    /// <para lang="en">Gets or sets 列宽
    ///</para>
    /// </summary>
    public int Width { get; set; }
}
