// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Notifications 通知
/// </summary>
public partial class Notifications
{
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
