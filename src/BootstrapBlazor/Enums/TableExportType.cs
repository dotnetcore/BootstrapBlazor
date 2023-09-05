// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
