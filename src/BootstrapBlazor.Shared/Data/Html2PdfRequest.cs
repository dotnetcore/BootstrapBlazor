// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">浏览器提交的 HTML 转 PDF 请求。</para>
/// <para lang="en">An HTML-to-PDF request submitted by the browser.</para>
/// </summary>
public sealed class Html2PdfRequest
{
    /// <summary>
    /// <para lang="zh">待转换的 HTML。</para>
    /// <para lang="en">The HTML to convert.</para>
    /// </summary>
    public string Html { get; set; } = "";

    /// <summary>
    /// <para lang="zh">站点内置样式链接。</para>
    /// <para lang="en">Built-in site stylesheet links.</para>
    /// </summary>
    public List<string> Links { get; set; } = [];

    /// <summary>
    /// <para lang="zh">PDF 页面选项。</para>
    /// <para lang="en">PDF page options.</para>
    /// </summary>
    public PdfOptions? Options { get; set; }
}
