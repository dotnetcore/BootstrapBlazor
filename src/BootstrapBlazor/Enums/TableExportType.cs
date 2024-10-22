// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件导出类型枚举
/// </summary>
public enum TableExportType
{
    /// <summary>
    /// 未知格式
    /// </summary>
    Unknown,
    /// <summary>
    /// Excel 格式
    /// </summary>
    Excel,
    /// <summary>
    /// Csv 格式
    /// </summary>
    Csv,
    /// <summary>
    /// Pdf 格式
    /// </summary>
    Pdf
}
