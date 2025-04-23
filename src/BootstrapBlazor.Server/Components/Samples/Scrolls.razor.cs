// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Scrolls
/// </summary>
public sealed partial class Scrolls
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ChildContent",
            Description = Localizer["Desc1"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = Localizer["Desc2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    [NotNull]
    private Scroll? _scroll = null;

    private Task ScrollToBottom() => _scroll.ScrollToBottom();
}
