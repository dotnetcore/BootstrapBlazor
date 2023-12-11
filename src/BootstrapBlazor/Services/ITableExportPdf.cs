// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件 Pdf 导出接口
/// </summary>
public interface ITableExportPdf
{
    /// <summary>
    /// 导出 Pdf 数据
    /// </summary>
    /// <returns></returns>
    Task<byte[]> PdfDataAsync(string content);

    /// <summary>
    /// 导出 Pdf 流
    /// </summary>
    /// <returns></returns>
    Task<Stream> PdfStreamAsync(string content);
}
