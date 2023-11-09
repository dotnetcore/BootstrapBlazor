﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
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
    public int Step { get; set; } = 30;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("bar_height")]
    public int BarHeight { get; set; } = 20;

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
    public int Padding { get; set; } = 18;

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
    public string? Language { get; set; } = "zh";
}
