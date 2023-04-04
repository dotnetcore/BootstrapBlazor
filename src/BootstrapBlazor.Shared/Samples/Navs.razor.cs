// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Navs
/// </summary>
public sealed partial class Navs
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["NavsChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Alignment",
            Description = Localizer["NavsAlignment"],
            Type = "Alignment",
            ValueList = "Left|Center|Right",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsVertical",
            Description = Localizer["NavsIsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsPills",
            Description = Localizer["NavsIsPills"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsFill",
            Description = Localizer["NavsIsFill"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsJustified",
            Description = Localizer["NavsIsJustified"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
