// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Dividers 组件示例文档
/// </summary>
public sealed partial class Dividers
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Text",
                Description = Localizer["Desc1"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Icon",
                Description = Localizer["Desc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Alignment",
                Description = Localizer["Desc3"],
                Type = "Aligment",
                ValueList = "Left|Center|Right|Top|Bottom",
                DefaultValue = "Center"
            },
            new AttributeItem() {
                Name = "IsVertical",
                Description = Localizer["Desc4"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["Desc5"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
