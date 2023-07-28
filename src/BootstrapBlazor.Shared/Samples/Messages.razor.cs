// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Messages
/// </summary>
public sealed partial class Messages
{
    [NotNull]
    private Message? Message { get; set; }

    [NotNull]
    private Message? Message1 { get; set; }

    private async Task ShowMessage()
    {
        Message.SetPlacement(Placement.Top);
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a reminder message"
        });
    }

    private async Task ShowIconMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a reminder message",
            Icon = "fa-solid fa-circle-info"
        });
    }

    private async Task ShowCloseMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a reminder message",
            Icon = "fa-solid fa-circle-info",
            ShowDismiss = true,
        });
    }

    private async Task ShowBarMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a reminder message",
            Icon = "fa-solid fa-circle-info",
            ShowBar = true,
        });
    }

    private async Task ShowColorMessage(Color color)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a colored message",
            Icon = "fa-solid fa-circle-info",
            Color = color
        });
    }

    private async Task ShowBottomMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "This is a reminder message",
            Icon = "fa-solid fa-circle-info",
        }, Message1);
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Placement",
            Description = "message popup location",
            Type = "Placement",
            ValueList = "Top|Bottom",
            DefaultValue = "Top"
        }
    };

    /// <summary>
    /// get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetMessageItemAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "ChildContent",
            Description = "Content",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = "Style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "Icon",
            Description = "Icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowDismiss",
            Description = "Show close button",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowBar",
            Description = "Whether to show the left Bar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
