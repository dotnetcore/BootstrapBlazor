// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.AspNetCore.Components.Web;

/// <summary>
/// <para lang="zh">ReconnectorOutlet 组件</para>
/// <para lang="en">ReconnectorOutlet Component</para>
/// </summary>
public class ReconnectorOutlet : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否自动尝试重连 默认 true</para>
    /// <para lang="en">Gets or sets Whether to auto reconnect. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 自动重连间隔 默认 5000 毫秒 最小值为 1000 毫秒</para>
    /// <para lang="en">Gets or sets Auto reconnect interval. Default 5000ms. Min 1000ms</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int ReconnectInterval { get; set; } = 5000;

    /// <summary>
    /// <para lang="zh">BuildRenderTree 方法</para>
    /// <para lang="en">BuildRenderTree Method</para>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<ReconnectorContent>(0);
        builder.AddAttribute(1, nameof(ReconnectorContent.AutoReconnect), AutoReconnect);
        builder.AddAttribute(2, nameof(ReconnectorContent.ReconnectInterval), ReconnectInterval);
        builder.CloseComponent();
    }
}
