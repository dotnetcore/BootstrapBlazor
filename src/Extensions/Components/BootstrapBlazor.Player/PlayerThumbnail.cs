// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Player 组件预览图类
/// </summary>
public class PlayerThumbnail
{
    /// <summary>
    /// 
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("pic_num")]
    public int PicNum { get; set; } = 184;

    /// <summary>
    /// 
    /// </summary>
    public int Width { get; set; } = 178;

    /// <summary>
    /// 
    /// </summary>
    public int Height { get; set; } = 100;

    /// <summary>
    /// 
    /// </summary>
    public int Col { get; set; } = 7;

    /// <summary>
    /// 
    /// </summary>
    public int Row { get; set; } = 7;

    /// <summary>
    /// 
    /// </summary>
    public int OffsetX { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public int OffsetY { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public List<string> Urls { get; } = [];
}
