// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">NotificationItem 类用于表示通知信息</para>
/// <para lang="en">Notification item class</para>
/// </summary>
public class NotificationItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 消息键值，未赋值时系统自动生成</para>
    /// <para lang="en">Gets or sets the message ID. Auto-generated if not set</para>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标题</para>
    /// <para lang="en">Gets or sets the title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 信息</para>
    /// <para lang="en">Gets or sets the message</para>
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否静默</para>
    /// <para lang="en">Gets or sets whether silent</para>
    /// </summary>
    public bool Silent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 通知触发时要播放的音频文件的 URL</para>
    /// <para lang="en">Gets or sets the URL of the audio file to play when notification is triggered</para>
    /// </summary>
    public string? Sound { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 通知点击后的回调</para>
    /// <para lang="en">Gets or sets 通知点击后的回调</para>
    /// </summary>
    /// <remarks>点击通知后自动关闭</remarks>
    [JsonIgnore]
    public Func<Task>? OnClick { get; set; }
}
