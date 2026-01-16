// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">分享按钮上下文类</para>
///  <para lang="en">ShareButton context class</para>
/// </summary>
public class ShareButtonContext
{
    /// <summary>
    ///  <para lang="zh">获得/设置 分享标题</para>
    ///  <para lang="en">Gets or sets the share title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 分享描述文字</para>
    ///  <para lang="en">Gets or sets the share text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 分享链接地址</para>
    ///  <para lang="en">Gets or sets the share URL</para>
    /// </summary>
    public string? Url { get; set; }
}
