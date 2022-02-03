// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Calendars
{
    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? Trace { get; set; }

    private DateTime BindValue { get; set; } = DateTime.Today;

    private void OnValueChanged(DateTime ts)
    {
        Trace?.Log($"{ts:yyyy-MM-dd}");
    }

    private static string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "ValueChanged",
                Description = Localizer["ValueChanged"],
                Type ="EventCallback<DateTime>"
            }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Value",
                Description = Localizer["Value"],
                Type = "DateTime",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["ChildContent"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
    };
}
