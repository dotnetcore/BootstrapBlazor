// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// SwitchButtons
/// </summary>
public partial class SwitchButtons
{
    private bool ToggleState { get; set; } = true;

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private void OnClick()
    {
        Logger.Log("Clicked");
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "OnText",
            Description = "On 状态显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OffText",
            Description = "Off 状态显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ToggleState",
            Description = "当前状态",
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ToggleStateChanged",
            Description = "状态切换回调方法",
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClick",
            Description = "点击回调方法",
            Type = "EventCallback<MouseEventArgs>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
