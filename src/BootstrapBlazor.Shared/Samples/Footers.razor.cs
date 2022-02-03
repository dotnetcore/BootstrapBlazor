// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Footers
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    public IStringLocalizer<Footers>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Text",
                Description = Localizer["Desc1"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Target",
                Description = Localizer["Desc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
