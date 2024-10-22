// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 通知信息类
/// </summary>
public class NotificationItem
{
    /// <summary>
    /// 获得/设置 消息键值 未赋值时系统自动生成
    /// </summary>
    public string? Id { get; set; }

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
    /// <remarks>点击通知后自动关闭</remarks>
    [JsonIgnore]
    public Func<Task>? OnClick { get; set; }
}
