// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Splits
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "IsVertical",
                Description = Localizer["Desc1"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Basis",
                Description = Localizer["Desc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "50%"
            },
            new AttributeItem() {
                Name = "FirstPaneTemplate",
                Description = Localizer["Desc3"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SecondPaneTemplate",
                Description = Localizer["Desc4"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
