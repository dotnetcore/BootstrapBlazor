// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Avatars
    {
        private async Task<string> GetUrlAsync()
        {
            // 模拟异步获取图像地址
            await Task.Delay(500);
            return "_content/BootstrapBlazor.Shared/images/Argo-C.png";
        }
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Size",
                Description = "头像框大小",
                Type = "Size",
                ValueList = "ExtraSmall|Small|Medium|Large|ExtraLarge",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsBorder",
                Description = "是否显示边框",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCircle",
                Description = "是否为圆形",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsIcon",
                Description = "是否为图标",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsText",
                Description = "是否为显示为文字",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "头像框显示图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-user"
            },
            new AttributeItem() {
                Name = "Text",
                Description = "头像框显示文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Url",
                Description = "Image 头像路径地址",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "GetUrlAsync",
                Description = "获取 Image 头像路径地址异步回调委托",
                Type = "Func<Task<string>>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
