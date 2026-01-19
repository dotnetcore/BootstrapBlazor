// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Dividers 组件示例文档
/// </summary>
public sealed partial class Dividers
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
            new() {
                Name = "Text",
                Description = Localizer["Desc1"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new() {
                Name = "Icon",
                Description = Localizer["Desc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new() {
                Name = "Alignment",
                Description = Localizer["Desc3"],
                Type = "Aligment",
                ValueList = "Left|Center|Right|Top|Bottom",
                DefaultValue = "Center"
            },
            new() {
                Name = "IsVertical",
                Description = Localizer["Desc4"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new() {
                Name = "ChildContent",
                Description = Localizer["Desc5"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
    ];
}
