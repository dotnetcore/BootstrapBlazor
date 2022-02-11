// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 通知信息类
/// </summary>
public class NotificationItem
{
    /// <summary>
    /// 获得/设置 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 信息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 获得/设置 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 静默
    /// </summary>
    public bool Silent { get; set; }

    /// <summary>
    /// 获得/设置 通知触发时要播放的音频文件的 URL
    /// </summary>
    public string? Sound { get; set; }

    /// <summary>
    /// 获得/设置 通知点击后的回调
    /// </summary>
    public string? OnClick { get; set; }
}
