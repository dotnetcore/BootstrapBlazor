// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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
    private static AttributeItem[] GetAttributes() =>
    [
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
    ];
}
