// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// PlayerTracks 类
/// </summary>
public class PlayerTracks
{
    /// <summary>
    /// 获得/设置 轨道类型 默认 subtitles
    /// </summary>
    public string? Kind { get; set; } = "captions";

    /// <summary>
    /// 获得/设置 轨道语言
    /// </summary>
    [JsonPropertyName("scrlang")]
    public string? SrcLang { get; set; }

    /// <summary>
    /// 获得/设置 轨道标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 获得/设置 轨道地址
    /// </summary>
    public string? Src { get; set; }

    /// <summary>
    /// 获得/设置 默认轨道
    /// </summary>
    public bool Default { get; set; }
}
