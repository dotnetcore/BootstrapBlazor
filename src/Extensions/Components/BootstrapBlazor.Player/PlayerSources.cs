// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 播放媒体资源类
/// </summary>
public class VideoPlayerSources
{
    /// <summary>
    /// 获得/设置 资源地址
    /// </summary>
    public string? Src { get; set; }

    /// <summary>
    /// 获得/设置 资源类型
    /// </summary>
    public string? Type { get; set; } = "application/x-mpegURL";
}
