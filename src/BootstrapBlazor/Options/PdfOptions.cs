// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// PdfOptions 实例用于设置导出 Pdf 相关选项
/// </summary>
public class PdfOptions
{
    /// <summary>
    /// 获得/设置 是否横向打印 默认 false
    /// </summary>
    public bool Landscape { get; set; }
}
