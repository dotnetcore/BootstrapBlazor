// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Timelines
/// </summary>
public sealed partial class Timelines
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["TimelinesItems"],
            Type = "IEnumerable<TimelineItem>",
            ValueList = "—",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsReverse",
            Description = Localizer["TimelinesIsReverse"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsLeft",
            Description = Localizer["TimelinesIsLeft"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAlternate",
            Description = Localizer["TimelinesIsAlternate"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetTimelineItemAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(TimelineItem.Color),
            Description = Localizer["TimelinesColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TimelineItem.Content),
            Description = Localizer["TimelinesContent"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TimelineItem.Icon),
            Description = Localizer["TimelinesIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TimelineItem.Description),
            Description = Localizer["TimelinesDescription"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(TimelineItem.Component),
            Description = Localizer["TimelinesComponent"],
            Type = nameof(BootstrapDynamicComponent),
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
