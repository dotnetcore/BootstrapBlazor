// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Messages
/// </summary>
public sealed partial class Messages
{
    [NotNull]
    private Message? Message { get; set; }

    [NotNull]
    private Message? Message1 { get; set; }

    private readonly MessageOption _option = new();

    private long _count = 0;

    private string _placement = "Top";

    private readonly List<SelectedItem> _items = [new SelectedItem("Top", "Top"), new SelectedItem("Bottom", "Bottom")];

    private Placement Placement => _placement == "Top" ? Placement.Top : Placement.Bottom;

    private async Task ShowMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = $"This is a reminder message {_count++}"
        }, Message);
    }

    private async Task ShowAsyncMessage()
    {
        _option.ForceDelay = true;
        _option.IsAutoHide = false;
        _option.Delay = 3000;
        _option.Content = Localizer["MessagesAsyncDemoStep1Text"];
        _option.Color = Color.Primary;
        await MessageService.Show(_option);

        await Task.Delay(3000);
        _option.Content = Localizer["MessagesAsyncDemoStep2Text"];
        _option.Color = Color.Info;
        await MessageService.Show(_option);

        await Task.Delay(2000);
        _option.IsAutoHide = true;
        _option.Content = Localizer["MessagesAsyncDemoStep3Text"];
        _option.Color = Color.Success;

        await MessageService.Show(_option);
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
            IsAutoHide = false,
            OnDismiss = () =>
            {
                return Task.CompletedTask;
            }
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
            Content = $"This is a reminder message - {_count++}",
            Icon = "fa-solid fa-circle-info",
        }, Message1);
    }

    private async Task ShowTemplateMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Icon = "fa-solid fa-circle-info",
            ChildContent = RenderContent
        });
    }

    private Task ShowLastOnlyMessage() => MessageService.Show(new MessageOption()
    {
        ShowShadow = true,
        ShowMode = MessageShowMode.Single,
        Content = $"This is a reminder message - {_count++}"
    });

    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Placement",
            Description = "message popup location",
            Type = "Placement",
            ValueList = "Top|Bottom",
            DefaultValue = "Top"
        },
        new()
        {
            Name = "ShowMode",
            Description = "Display mode",
            Type = "MessageShowMode",
            ValueList = "Single|Multiple",
            DefaultValue = "Multiple"
        }
    ];

    /// <summary>
    /// get property method
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetMessageItemAttributes() =>
    [
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
        },
        new()
        {
            Name = "ShowShadow",
            Description = "Whether to show the shadow",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowBorder",
            Description = "Whether to show the border",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnDismiss",
            Description = "The callback when click close button",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
