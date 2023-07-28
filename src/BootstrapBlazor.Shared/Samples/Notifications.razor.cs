// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Notifications 通知
/// </summary>
public partial class Notifications
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool Permission { get; set; }

    private NotificationItem Model { get; set; } = new NotificationItem()
    {
        Icon = "./images/Argo-C.png"
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Model.Title ??= Localizer["NotificationsNormalTitleSampleText"];
        Model.Message ??= Localizer["NotificationsNormalMessageSampleText"];
        Model.OnClick = OnClickNotificationCallback;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await BrowserNotification.CheckPermission(false);
        }
    }

    private async Task CheckPermission()
    {
        Permission = await BrowserNotification.CheckPermission();
        Logger.Log(Localizer["NotificationsNormalGetPermissionCallbackText"] + (Permission ? "OK" : "No permission"));
    }

    private async Task Dispatch()
    {
        await BrowserNotification.Dispatch(Model);
    }

    private Task OnClickNotificationCallback()
    {
        Logger.Log($"{Localizer["NotificationsNormalOnClickText"]}");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetNotificationItem() => new AttributeItem[]
    {
        new()
        {
            Name = "Title",
            Description = Localizer["NotificationsNormalTitleText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Message",
            Description = Localizer["NotificationsNormalMessageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Icon",
            Description = Localizer["NotificationsIconText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Silent",
            Description = Localizer["NotificationsNormalSilentText"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Sound",
            Description = Localizer["NotificationsSoundText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClick",
            Description = Localizer["NotificationsNormalOnClickText"],
            Type = "Methods",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };
}
