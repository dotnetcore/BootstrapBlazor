// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// GanttOption 配置类
/// </summary>
public class GanttOption
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("header_height")]
    public int HeaderHeight { get; set; } = 50;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("column_width")]
    public int ColumnWidth { get; set; } = 30;

    /// <summary>
    /// 
    /// </summary>
    public int Step { get; set; } = 24;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bar_height")]
    public int BarHeight { get; set; } = 35;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bar_corner_radius")]
    public int BarCornerRadius { get; set; } = 3;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("arrow_curve")]
    public int ArrowCurve { get; set; } = 5;

    /// <summary>
    /// 
    /// </summary>
    public int Padding { get; set; } = 25;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("view_mode")]
    public string ViewMode { get; set; } = GanttViewMode.QUARTER_DAY.ToDescriptionString();

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("date_format")]
    public string? DateFormat { get; set; } = "YYYY-MM-DD";

    /// <summary>
    /// 
    /// </summary>
    public string? Language { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
}
