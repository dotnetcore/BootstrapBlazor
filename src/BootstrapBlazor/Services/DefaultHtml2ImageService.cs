// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html2Image 实现
/// </summary>
class DefaultHtml2ImageService : IHtml2Image
{
    private const string ErrorMessage = "请增加依赖包 BootstrapBlazor.Html2Image 通过 AddBootstrapBlazorHtml2ImageService 进行服务注入; Please add BootstrapBlazor.Html2Image package and use AddBootstrapBlazorHtml2ImageService inject service";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<string?> GetDataAsync(string selector, IHtml2ImageOptions? options = null) => throw new NotImplementedException(ErrorMessage);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Task<Stream?> GetStreamAsync(string selector, IHtml2ImageOptions? options = null) => throw new NotImplementedException(ErrorMessage);
}
