// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 分享按钮
/// </summary>
public class ShareButton : Button
{
    /// <summary>
    /// 获得/设置 分享内容上下文 默认 null
    /// </summary>
    [Parameter]
    public ShareButtonContext? ShareContext { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task HandlerClick() => InvokeVoidAsync("share", ShareContext);
}
