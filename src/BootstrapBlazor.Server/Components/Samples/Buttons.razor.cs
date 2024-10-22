﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Buttons
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="e"></param>
    private void ButtonClick(MouseEventArgs e)
    {
        NormalLogger.Log($"Button Clicked");
    }

    private bool IsDisable { get; set; }

    [NotNull]
    private Button? ButtonDisableDemo { get; set; }

    private void ClickButton1()
    {
        IsDisable = !IsDisable;
        StateHasChanged();
    }

    private Task ClickButton2()
    {
        IsDisable = false;
        ButtonDisableDemo.SetDisable(false);
        return Task.CompletedTask;
    }

    private string ButtonText { get; set; } = "";

    private Task ClickButtonShowText(string text)
    {
        ButtonText = text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static Task ClickAsyncButton() => Task.Delay(5000);

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnClick",
            Description = Localizer["EventDesc1"],
            Type ="EventCallback<MouseEventArgs>"
        },
        new()
        {
            Name = "OnClickWithoutRender",
            Description = Localizer["EventDesc2"],
            Type ="Func<Task>"
        }
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Color",
            Description = Localizer["Att1"],
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = "",
            DefaultValue = ""
        },
        new()
        {
            Name = "LoadingIcon",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-fw fa-spin fa-solid fa-spinner"
        },
        new()
        {
            Name = "Text",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = "",
            DefaultValue = ""
        },
        new()
        {
            Name = "Size",
            Description = Localizer["Att5"],
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "Class",
            Description = Localizer["Att6"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsBlock",
            Description = Localizer["Att7"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["Att8"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsOutline",
            Description = Localizer["Att9"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAsync",
            Description = Localizer["Att10"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["Att11"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ButtonStyle",
            Description = Localizer["Att12"],
            Type = "ButtonStyle",
            ValueList = "None / Circle / Round",
            DefaultValue = "None"
        },
        new()
        {
            Name = "ButtonType",
            Description = Localizer["Att13"],
            Type = "ButtonType",
            ValueList = "Button / Submit / Reset",
            DefaultValue = "Button"
        }
    ];

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "SetDisable",
            Description = Localizer["MethodDesc1"],
            Parameters = "disable",
            ReturnValue = " — "
        }
    ];
}
