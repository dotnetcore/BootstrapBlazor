// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表格列状态类</para>
/// <para lang="en">Table Column Status Class</para>
/// </summary>
public class TableColumnClientStatus
{
    /// <summary>
    /// <para lang="zh">列状态集合</para>
    /// <para lang="en">Column State Collection</para>
    /// </summary>
    [JsonPropertyName("cols")]
    public List<TableColumnState> Columns { get; set; } = [];

    /// <summary>
    /// <para lang="zh">表格宽度，当值为 0 时表示表格宽度自适应</para>
    /// <para lang="en">Table Width, when the value is 0, it means the table width is auto</para>
    /// </summary>
    [JsonPropertyName("table")]
    public int TableWidth { get; set; }
}
