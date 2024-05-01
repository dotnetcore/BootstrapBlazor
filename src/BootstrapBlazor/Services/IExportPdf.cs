// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 导出 Pdf 接口
/// </summary>
[Obsolete("已弃用，删除即可，组件内部已不使用，直接调用 IHtml2Pdf 接口方法")]
public interface IExportPdf
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
