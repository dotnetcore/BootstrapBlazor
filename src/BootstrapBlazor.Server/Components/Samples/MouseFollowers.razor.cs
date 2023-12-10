// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MouseFollowers
/// </summary>
public partial class MouseFollowers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<MouseFollowers>? Localizer { get; set; }

    [NotNull]
    private static string NugetPackageName => "BootstrapBlazor.MouseFollower";

    private readonly MouseFollowerOptions FollowerOptions = new() { ClassName = "mf-cursor bb-cursor" };

    private readonly MouseFollowerOptions FollowerImageOptions = new MouseFollowerOptions()
    {
        ClassName = "mf-cursor bb-cursor",
        MediaClassName = "mf-cursor-media bb-cursor-media"
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "FollowerMode",
            Description = Localizer["MouseFollowersFollowerMode"],
            Type = "Enum",
            ValueList = " — ",
            DefaultValue = "MouseFollowerMode.Normal"
        },
        new()
        {
            Name = "GlobalMode",
            Description = Localizer["MouseFollowersGlobalMode"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Content",
            Description = Localizer["MouseFollowersContent"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
