// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Tags
/// </summary>
public sealed partial class Tags
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task DismissClick()
    {
        Logger.Log($"Tag Dismissed");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnDismiss",
            Description = Localizer["TagsOnDismiss"],
            Type ="EventCallback<MouseEventArgs>"
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
            Name = "ChildContent",
            Description = Localizer["TagsChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = Localizer["TagsClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = Localizer["TagsColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["TagsIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowDismiss",
            Description = Localizer["TagsShowDismiss"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        }
    ];
}
