// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Watermarks 组件
/// </summary>
public partial class Watermarks
{
    private string _text = "Bootstrap\\nBlazor";

    private Alignment AlignmentValue { get; set; } = Alignment.Center;

    private List<SelectedItem> AlignmentItems =>
    [
        new(nameof(Alignment.Left), Localizer["AlignmentLeft"]),
        new(nameof(Alignment.Center), Localizer["AlignmentCenter"]),
        new(nameof(Alignment.Right), Localizer["AlignmentRight"])
    ];

    private int _fontSize = 16;

    private int _gap = 40;

    private int _rotate = -40;

    private string _color = "#0000004d";
}
