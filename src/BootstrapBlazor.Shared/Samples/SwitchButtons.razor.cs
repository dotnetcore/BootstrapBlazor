// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// SwitchButtons
/// </summary>
public partial class SwitchButtons
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "OnText",
            Description = "On 状态显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OffText",
            Description = "Off 状态显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ToggleState",
            Description = "当前状态",
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ToggleStateChanged",
            Description = "状态切换回调方法",
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnClick",
            Description = "点击回调方法",
            Type = "EventCallback<MouseEventArgs>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
