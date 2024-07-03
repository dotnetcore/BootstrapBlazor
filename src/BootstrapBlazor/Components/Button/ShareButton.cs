// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
