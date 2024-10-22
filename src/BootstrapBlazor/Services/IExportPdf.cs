// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
