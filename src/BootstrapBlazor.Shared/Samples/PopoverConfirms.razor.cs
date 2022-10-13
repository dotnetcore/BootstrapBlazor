// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class PopoverConfirms
{
    [NotNull]
    private Foo? Model { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    private BlockLogger? Trace1 { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };
    }

    /// <summary>
    /// 
    /// </summary>
    private Task OnClose()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Trace.Log("OnClose Trigger");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    private Task OnConfirm()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Trace.Log("OnConfirm Trigger");
        return Task.CompletedTask;
    }

    private static Task OnAsyncConfirm() => Task.Delay(3000);

    private async Task OnAsyncSubmit()
    {
        Trace1.Log("异步提交");
        await Task.Delay(3000);
    }

    private async Task OnValidSubmit(EditContext context)
    {
        await Task.Delay(3000);
        Trace1.Log("数据合规");
    }

    private Task OnInValidSubmit(EditContext context)
    {
        Trace1.Log("数据非法");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(PopConfirmButton.IsLink),
            Description = "Whether to render the component for the A tag",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Text",
            Description = "Show title",
            Type = "string",
            ValueList = "",
            DefaultValue = "删除"
        },
        new AttributeItem() {
            Name = "Icon",
            Description = "Button icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-xmark"
        },
        new AttributeItem() {
            Name = "CloseButtonText",
            Description = "Close button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "关闭"
        },
        new AttributeItem() {
            Name = "CloseButtonColor",
            Description = "Confirm button color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Secondary"
        },
        new AttributeItem() {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "ConfirmButtonText",
            Description = "Confirm button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "确定"
        },
        new AttributeItem() {
            Name = "ConfirmButtonColor",
            Description = "Confirm button color",
            Type = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            ValueList = "",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "ConfirmIcon",
            Description = "Confirmation box icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-circle-exclamation text-info"
        },
        new AttributeItem() {
            Name = "Content",
            Description = "Display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Confirm delete?"
        },
        new AttributeItem() {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popover Popup title",
            Type = "string",
            ValueList = "",
            DefaultValue = " "
        }
    };

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnConfirm",
            Description="Callback method when confirm is clicked",
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnClose",
            Description="Callback method when click close",
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnBeforeClick",
            Description="Click the callback method before confirming the pop-up window",
            Type ="Func<Task<bool>>"
        }
    };
}
