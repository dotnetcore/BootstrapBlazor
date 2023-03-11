// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Sliders
/// </summary>
public sealed partial class Sliders
{
    private double BindValue { get; set; } = 50;

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Class",
            Description = Localizer["SlidersClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["SlidersIsDisabled"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["SlidersValue"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["SlidersValueChanged"],
            Type ="EventCallback<int>"
        }
    };
}
