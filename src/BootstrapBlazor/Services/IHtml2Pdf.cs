// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    Task<byte[]> PdfDataAsync(string url);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="url">url</param>
    Task<Stream> PdfStreamAsync(string url);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null);
}
