// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Lights
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
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
                }
            };
        }
    }
}
