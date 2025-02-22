// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IHtml2Image 接口
/// </summary>
public interface IHtml2Image
{
    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <param name="options"></param>
    Task<string?> GetDataAsync(string selector, IHtml2ImageOptions? options = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="selector">选择器</param>
    /// <param name="options"></param>
    Task<Stream?> GetStreamAsync(string selector, IHtml2ImageOptions? options = null);
}
