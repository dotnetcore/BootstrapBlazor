// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
