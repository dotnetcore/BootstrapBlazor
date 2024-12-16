// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// PopoverConfirms
/// </summary>
public sealed partial class PopoverConfirms
{
    private static Task OnAsyncConfirm() => Task.Delay(3000);

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private ConsoleLogger? FormLogger { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };
    }

    private Task OnClose()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Logger.Log("OnClose Trigger");
        return Task.CompletedTask;
    }

    private Task OnConfirm()
    {
        // This method is called back when the confirm button is clicked, and this method will not be called when the cancel button is clicked
        Logger.Log("OnConfirm Trigger");
        return Task.CompletedTask;
    }

    private async Task OnAsyncSubmit()
    {
        FormLogger.Log("异步提交");
        await Task.Delay(3000);
    }

    private async Task OnValidSubmit(EditContext context)
    {
        await Task.Delay(3000);
        FormLogger.Log("数据合规");
    }

    private Task OnInValidSubmit(EditContext context)
    {
        FormLogger.Log("数据非法");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(PopConfirmButton.IsLink),
            Description = "Whether to render the component for the A tag",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Text",
            Description = "Show title",
            Type = "string",
            ValueList = "",
            DefaultValue = "Delete"
        },
        new()
        {
            Name = "Icon",
            Description = "Button icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-xmark"
        },
        new()
        {
            Name = "CloseButtonText",
            Description = "Close button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Close"
        },
        new()
        {
            Name = "CloseButtonColor",
            Description = "Confirm button color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Secondary"
        },
        new()
        {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "None"
        },
        new()
        {
            Name = "ConfirmButtonText",
            Description = "Confirm button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Ok"
        },
        new()
        {
            Name = "ConfirmButtonColor",
            Description = "Confirm button color",
            Type = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            ValueList = "",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "ConfirmIcon",
            Description = "Confirmation box icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-circle-exclamation text-info"
        },
        new()
        {
            Name = "Content",
            Description = "Display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Confirm delete?"
        },
        new()
        {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new()
        {
            Name = "Title",
            Description = "Popover Popup title",
            Type = "string",
            ValueList = "",
            DefaultValue = " "
        },
        new()
        {
            Name = nameof(PopConfirmButton.Trigger),
            Description = "How pop confirm is triggered",
            Type = "string",
            ValueList = "click|hover|focus",
            DefaultValue = "click"
        },
        new()
        {
            Name = nameof(PopConfirmButton.ShowCloseButton),
            Description = "Whether to display the close button",
            Type = "string",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(PopConfirmButton.ShowConfirmButton),
            Description = "Whether to display the confirm button",
            Type = "string",
            ValueList = "true/false",
            DefaultValue = "true"
        }
    ];

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private static EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnConfirm",
            Description="Callback method when confirm is clicked",
            Type ="Func<Task>"
        },
        new()
        {
            Name = "OnClose",
            Description="Callback method when click close",
            Type ="Func<Task>"
        },
        new()
        {
            Name = "OnBeforeClick",
            Description="Click the callback method before confirming the pop-up window",
            Type ="Func<Task<bool>>"
        }
    ];
}
