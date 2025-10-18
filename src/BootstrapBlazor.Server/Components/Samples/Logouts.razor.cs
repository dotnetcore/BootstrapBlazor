// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Logout 组件示例
/// </summary>
public partial class Logouts
{
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(Logout.ImageUrl),
            Description = "登出组件当前用户头像",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Logout.AvatarRadius),
            Description = "登出组件当前用户头像圆角半径",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Logout.DisplayName),
            Description = "登出组件当前用户显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Logout.PrefixDisplayNameText),
            Description = "登出组件当前用户显示文字前置文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "欢迎"
        },
        new()
        {
            Name = nameof(Logout.UserName),
            Description = "登出组件当前用户登录账号",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(Logout.ShowUserName),
            Description = "是否显示用户名",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Logout.PrefixUserNameText),
            Description = "登出组件当前用户登录账号前置文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "当前账号"
        },
        new()
        {
            Name = nameof(Logout.HeaderTemplate),
            Description = "账户信息模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Logout.LinkTemplate),
            Description = "导航信息模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
