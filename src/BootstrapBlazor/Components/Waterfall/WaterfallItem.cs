// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Waterfall 组件数据类</para>
///  <para lang="en">Waterfall componentdata类</para>
/// </summary>
public class WaterfallItem
{
    /// <summary>
    ///  <para lang="zh">获得/设置 id</para>
    ///  <para lang="en">Gets or sets id</para>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    ///  <para lang="zh">the url of image element</para>
    ///  <para lang="en">the url of image element</para>
    /// </summary>
    public string? Url { get; set; }
}
