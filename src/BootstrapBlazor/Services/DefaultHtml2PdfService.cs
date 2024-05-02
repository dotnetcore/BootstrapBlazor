// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html to Pdf 实现 
/// </summary>
class DefaultHtml2PdfService : IHtml2Pdf
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<byte[]> PdfDataAsync(string url) => Task.FromResult(Array.Empty<byte>());

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<Stream> PdfStreamAsync(string url) => Task.FromResult<Stream>(new MemoryStream());

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    public Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null) => Task.FromResult(Array.Empty<byte>());

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    public Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null) => Task.FromResult<Stream>(new MemoryStream());
}
