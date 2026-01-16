// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件导出类型枚举
///</para>
/// <para lang="en">Table component导出typeenum
///</para>
/// </summary>
public enum TableExportType
{
    /// <summary>
    /// <para lang="zh">未知格式
    ///</para>
    /// <para lang="en">未知格式
    ///</para>
    /// </summary>
    Unknown,
    /// <summary>
    /// <para lang="zh">Excel 格式
    ///</para>
    /// <para lang="en">Excel 格式
    ///</para>
    /// </summary>
    Excel,
    /// <summary>
    /// <para lang="zh">Csv 格式
    ///</para>
    /// <para lang="en">Csv 格式
    ///</para>
    /// </summary>
    Csv,
    /// <summary>
    /// <para lang="zh">Pdf 格式
    ///</para>
    /// <para lang="en">Pdf 格式
    ///</para>
    /// </summary>
    Pdf
}
