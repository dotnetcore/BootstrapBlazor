// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.AspNetCore.Components.Web;

/// <summary>
/// ReconnectorOutlet 组件
/// </summary>
public class ReconnectorOutlet : ComponentBase
{
    /// <summary>
    /// 获得/设置 是否自动尝试重连 默认 true
    /// </summary>
    [Parameter]
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动重连间隔 默认 5000 毫秒 最小值为 1000 毫秒
    /// </summary>
    [Parameter]
    public int ReconnectInterval { get; set; } = 5000;

    /// <summary>
    /// BuildRenderTree 方法
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
