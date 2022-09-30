// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class AnchorLinks
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AnchorLink>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(AnchorLink.Id),
            Description = "组件 Id 必填项",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AnchorLink.Icon),
            Description = "组件锚点图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-link"
        },
        new AttributeItem() {
            Name = nameof(AnchorLink.Text),
            Description = "组件 Text 显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AnchorLink.TooltipText),
            Description = "拷贝成功后 显示文字",
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer[nameof(AnchorLink.TooltipText)]
        }
    };
}
