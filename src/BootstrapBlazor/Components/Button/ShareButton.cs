// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">分享按钮</para>
///  <para lang="en">Share button</para>
/// </summary>
public class ShareButton : Button
{
    /// <summary>
    ///  <para lang="zh">获得/设置 分享内容上下文 默认 null</para>
    ///  <para lang="en">Gets or sets the share context. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ShareButtonContext? ShareContext { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task HandlerClick() => InvokeVoidAsync("share", ShareContext);
}
