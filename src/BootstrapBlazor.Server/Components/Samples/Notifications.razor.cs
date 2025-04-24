// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Notifications 通知
/// </summary>
public partial class Notifications
{
    [Inject]
    [NotNull]
    private NotificationService? BrowserNotification { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool Permission { get; set; }

    [NotNull]
    private NotificationItem? Model { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = new()
        {
            Icon = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo-C.png",
            Title = Localizer["NotificationsNormalTitleSampleText"],
            Message = Localizer["NotificationsNormalMessageSampleText"],
            OnClick = OnClickNotificationCallback
        };
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
        new()
        {
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
