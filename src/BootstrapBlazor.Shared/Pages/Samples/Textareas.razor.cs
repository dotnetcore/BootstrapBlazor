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
    public partial class Textareas
    {
        private string Text { get; set; } = "";

        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "ShowLabel",
                    Description = "是否显示前置标签",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = "前置标签显示文本",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem()
                {
                    Name = "IsDisabled",
                    Description = "是否禁用 默认为 fasle",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                }
            };
        }
    }
}
