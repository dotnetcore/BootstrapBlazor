// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 播放器选项类
/// </summary>
public class PlayerOptions
{
    /// <summary>
    /// 获得/设置 是否为 Hls 播放资源 默认 false
    /// </summary>
    public bool IsHls { get; set; }

    /// <summary>
    /// 获得/设置 宽度
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// 获得/设置 高度
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 显示控制条 默认 true
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Controls { get; set; }

    /// <summary>
    /// 获得/设置 自动播放 默认 true
    /// </summary>
    [JsonPropertyName("autoplay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AutoPlay { get; set; }

    /// <summary>
    /// 获得/设置 预载 默认 auto
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Preload { get; set; }

    /// <summary>
    /// 获得/设置 设置封面资源 相对或者绝对路径
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Poster { get; set; }

    /// <summary>
    /// 获得/设置 界面语言, 默认 zh-CN
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Language { get; set; }

    /// <summary>
    /// 获得 视频标记点实例 <see cref="PlayerMarkers"/>
    /// </summary>
    public PlayerMarkers Markers { get; } = new();

    /// <summary>
    /// 获得 播放资源预览图实例 <see cref="PlayerThumbnail"/>
    /// </summary>
    public PlayerThumbnail Thumbnail { get; } = new();

    /// <summary>
    /// 获得/设置 播放资源
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PlayerSource Source { get; } = new();
}
