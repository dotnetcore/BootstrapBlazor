// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Html export PDF service
///</para>
/// <para lang="en">Html export PDF service
///</para>
/// </summary>
public interface IHtml2Pdf
{
    ///// <summary>
    /// <para lang="zh">获得/设置 WebProxy 对象用于网络请求代理
    ///// <para>Get or set the WebProxy object for network request proxy</para>
    /////</para>
    /// <para lang="en">Gets or sets WebProxy 对象用于网络请求代理
    ///// <para>Get or set the WebProxy object for network request proxy</para>
    /////</para>
    /// </summary>
    //public IWebProxy? WebProxy { get; set; }

    /// <summary>
    /// <para lang="zh">Export method
    ///</para>
    /// <para lang="en">Export method
    ///</para>
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="options"></param>
    Task<byte[]> PdfDataAsync(string url, PdfOptions? options = null);

    /// <summary>
    /// <para lang="zh">Export method
    ///</para>
    /// <para lang="en">Export method
    ///</para>
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="options"></param>
    Task<Stream> PdfStreamAsync(string url, PdfOptions? options = null);

    /// <summary>
    /// <para lang="zh">Export method
    ///</para>
    /// <para lang="en">Export method
    ///</para>
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    /// <param name="options"></param>
    Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null);

    /// <summary>
    /// <para lang="zh">Export method
    ///</para>
    /// <para lang="en">Export method
    ///</para>
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    /// <param name="options"></param>
    Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null);
}
