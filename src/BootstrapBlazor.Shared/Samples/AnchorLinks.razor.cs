// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class AnchorLinks
{
    private static IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Id",
                Description = "组件 Id 必填项",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Text",
                Description = "组件 Text 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TooltipText",
                Description = "拷贝成功后 显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
}
