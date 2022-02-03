// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Buttons
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private Button? ButtonDisableDemo { get; set; }

    private bool IsDisable { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void ButtonClick(MouseEventArgs e)
    {
        Trace.Log($"Button Clicked");
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
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnClick",
                Description = Localizer["EventDesc1"],
                Type ="EventCallback<MouseEventArgs>"
            },
            new EventItem()
            {
                Name = "OnClickWithoutRender",
                Description = Localizer["EventDesc2"],
                Type ="Func<Task>"
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
                Name = "Color",
                Description = Localizer["Att1"],
                Type = "Color",
                ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = Localizer["Att2"],
                Type = "string",
                ValueList = "",
                DefaultValue = ""
            },
            new AttributeItem() {
                Name = "LoadingIcon",
                Description = Localizer["Att3"],
                Type = "string",
                ValueList = "",
                DefaultValue = "fa fa-fw fa-spin fa-spinner"
            },
            new AttributeItem() {
                Name = "Text",
                Description = Localizer["Att4"],
                Type = "string",
                ValueList = "",
                DefaultValue = ""
            },
            new AttributeItem() {
                Name = "Size",
                Description = Localizer["Att5"],
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "Class",
                Description = Localizer["Att6"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsBlock",
                Description = Localizer["Att7"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = Localizer["Att8"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsOutline",
                Description = Localizer["Att9"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAsync",
                Description = Localizer["Att10"],
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["Att11"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ButtonStyle",
                Description = Localizer["Att12"],
                Type = "ButtonStyle",
                ValueList = "None / Circle / Round",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "ButtonType",
                Description = Localizer["Att13"],
                Type = "ButtonType",
                ValueList = "Button / Submit / Reset",
                DefaultValue = "Button"
            }
    };

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
            new MethodItem()
            {
                Name = "SetDisable",
                Description = Localizer["MethodDesc1"],
                Parameters = "disable",
                ReturnValue = " — "
            }
    };
}
