// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {

        new() {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        }
    };
}
