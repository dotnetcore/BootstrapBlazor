// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

class ColumnWidthState
{
    [JsonPropertyName("cols")]
    public List<ColumnWidth> ColumnWidths { get; set; } = [];

    [JsonPropertyName("table")]
    public int TableWidth { get; set; }
}
