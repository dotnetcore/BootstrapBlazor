// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// 播放媒体资源类
/// </summary>
public class PlayerSource
{
    /// <summary>
    /// 获得/设置 资源类型 默认 video
    /// </summary>
    [JsonEnumConverter(true)]
    public PlayerMode Type { get; set; }

    /// <summary>
    /// 获得/设置 封面地址
    /// </summary>
    public string? Poster { get; set; }

    /// <summary>
    /// 获得 资源集合
    /// </summary>
    public List<PlayerSources> Sources { get; } = [];

    /// <summary>
    /// 获得 资源集合
    /// </summary>
    public List<PlayerTracks> Tracks { get; } = [];
}
