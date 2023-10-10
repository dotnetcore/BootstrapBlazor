// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Stacks
{
    private IEnumerable<AttributeItem> GetAttributeItems()
    {
        return new List<AttributeItem>()
        {
            new AttributeItem()
            {
                 Name = nameof(Stack.Orientation),
                 Type = "Enum",
                 DefaultValue = nameof(Orientation.Horizontal),
                 Description = "获取或设置堆叠组件的方向"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.HorizontalAlignment),
                 Type = "Enum",
                 DefaultValue = nameof(HorizontalAlignment.Left),
                 Description = "堆叠组件中组件的水平对齐方式"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.VerticalAlignment),
                 Type = "Enum",
                 DefaultValue = nameof(VerticalAlignment.Top),
                 Description = "堆叠组件中组件的垂直对齐"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.Width),
                 Type = "string",
                 DefaultValue = "100%",
                 Description = "以百分比字符串表示的堆栈宽度"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.Wrap),
                 Type = "boolean",
                 DefaultValue = "false",
                 Description = "获取或设置堆栈是否换行"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.HorizontalGap),
                 Type = "int?",
                 DefaultValue = "10",
                 Description = "获取或设置水平堆叠组件之间的间隙"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.VerticalGap),
                 Type = "int?",
                 DefaultValue = "10",
                 Description = "获取或设置垂直堆叠组件之间的间距"
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.ChildContent),
                 Type = "RenderFragment?",
                 DefaultValue = "-",
                 Description = ""
            }
        };
    }
}
