// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 分享按钮上下文类
/// </summary>
public class ShareButtonContext
{
    /// <summary>
    /// 获得/设置 分享标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 分享描述文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 分享链接地址
    /// </summary>
    public string? Url { get; set; }
}
