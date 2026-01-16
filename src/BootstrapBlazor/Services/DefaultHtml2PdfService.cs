// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">默认 Html to Pdf 实现 </para>
/// <para lang="en">Default Html to Pdf Implementation</para>
/// </summary>
class DefaultHtml2PdfService : IHtml2Pdf
{
    private const string ErrorMessage = "请增加依赖包 BootstrapBlazor.Html2Pdf 通过 AddBootstrapBlazorHtml2PdfService 进行服务注入; Please add BootstrapBlazor.Html2Pdf package and use AddBootstrapBlazorHtml2PdfService inject service";

    public Task<byte[]> PdfDataAsync(string url, PdfOptions? options = null) => throw new NotImplementedException(ErrorMessage);

    public Task<Stream> PdfStreamAsync(string url, PdfOptions? options = null) => throw new NotImplementedException(ErrorMessage);

    public Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null) => throw new NotImplementedException(ErrorMessage);

    public Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null) => throw new NotImplementedException(ErrorMessage);
}
