// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 列宽设置类
/// </summary>
struct ColumnWidth
{
    /// <summary>
    /// 获得/设置 列名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 列宽
    /// </summary>
    public int Width { get; set; }
}
