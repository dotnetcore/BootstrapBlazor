// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Rates
{
    private int BindValue { get; set; } = 3;

    private int BindValue1 { get; set; } = 2;

    private bool IsDisable { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? Trace { get; set; }

    private void OnValueChanged(int val)
    {
        BindValue = val;
        Trace?.Log($"{Localizer["Log"]} {val}");
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "ValueChanged",
                Description =Localizer["Event1"],
                Type ="EventCallback<int>"
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
                Description = Localizer["Att1"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = Localizer["Att2"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
    };
}
