// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Tooltips
/// </summary>
public partial class Tooltips
{
    private static string TopString => "Tooltip on top";

    private static string LeftString => "Tooltip on left";

    private static string RightString => "Tooltip on right";

    private static string BottomString => "Tooltip on bottom";

    private static string HtmlString => "This is <a href=\"www.blazor.zone\">Blazor</a> tooltip";

    private Tooltip? _tooltip;

    private async Task ToggleShow()
    {
        if (_tooltip != null)
        {
            await _tooltip.Toggle();
        }
    }
}
