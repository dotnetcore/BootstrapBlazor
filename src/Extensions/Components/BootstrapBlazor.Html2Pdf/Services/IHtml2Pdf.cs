// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using PuppeteerSharp;

namespace BootstrapBlazor.Components;

/// <summary>
/// Html export PDF service
/// </summary>
public interface IHtml2Pdf
{
    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="options">the instance of PdfOptions</param>
    Task<byte[]> ExportDataAsync(string url, PdfOptions? options = null);

    /// <summary>
    /// 导出流
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="options">the instance of PdfOptions</param>
    Task<Stream> ExportStreamAsync(string url, PdfOptions? options = null);
}
