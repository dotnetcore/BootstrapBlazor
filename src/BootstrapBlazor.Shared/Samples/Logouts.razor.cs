// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Logouts
{
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(Logout.ImageUrl),
            Description = "登出组件当前用户头像",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Logout.DisplayName),
            Description = "登出组件当前用户显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Logout.PrefixDisplayNameText),
            Description = "登出组件当前用户显示文字前置文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "欢迎"
        },
        new AttributeItem() {
            Name = nameof(Logout.ImageUrl),
            Description = "登出组件当前用户登录账号",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Logout.ImageUrl),
            Description = "登出组件当前用户登录账号前置文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "当前账号"
        },
        new AttributeItem() {
            Name = nameof(Logout.HeaderTemplate),
            Description = "账户信息模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Logout.LinkTemplate),
            Description = "导航信息模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
