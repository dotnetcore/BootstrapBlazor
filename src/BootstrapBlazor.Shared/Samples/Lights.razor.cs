// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Lights
{
    private static IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
                DefaultValue = "Success"
            },
            new AttributeItem() {
                Name = "IsFlash",
                Description = "是否闪烁",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Title",
                Description = "指示灯 Tooltip 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
