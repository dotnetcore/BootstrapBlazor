// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Checkboxs
/// </summary>
public sealed partial class Checkboxs
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem() {
                    Name = "ShowLabel",
                    Description = Localizer["Att1"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowAfterLabel",
                    Description = Localizer["Att2"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = Localizer["Att3"],
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "IsDisabled",
                    Description = Localizer["Att4"],
                    Type = "boolean",
                    ValueList = "true / false",
                    DefaultValue = "false"
                },
                new AttributeItem()
                {
                    Name = "State",
                    Description = Localizer["Att5"],
                    Type = "CheckboxState",
                    ValueList = "Mixed / Checked / UnChecked",
                    DefaultValue = "UnChecked"
                },
        };
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnStateChanged",
                Description = Localizer["Event1"],
                Type ="Action<CheckboxState, TItem>"
            },
            new EventItem()
            {
                Name = "StateChanged",
                Description = Localizer["Event2"],
                Type ="EventCallback<CheckboxState>"
            }
    };
}
