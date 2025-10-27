// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// <param name="options"></param>
    Task<byte[]> PdfDataAsync(string url, PdfOptions? options = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="options"></param>
    Task<Stream> PdfStreamAsync(string url, PdfOptions? options = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    /// <param name="options"></param>
    Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    /// <param name="options"></param>
    Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null);
}
