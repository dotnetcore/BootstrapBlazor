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
                 Description = Localizer["Orientation"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.HorizontalAlignment),
                 Type = "Enum",
                 DefaultValue = nameof(HorizontalAlignment.Left),
                 Description = Localizer["HorizontalAlignment"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.VerticalAlignment),
                 Type = "Enum",
                 DefaultValue = nameof(VerticalAlignment.Top),
                 Description = Localizer["VerticalAlignment"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.Width),
                 Type = "string",
                 DefaultValue = "100%",
                 Description = Localizer["Width"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.Wrap),
                 Type = "boolean",
                 DefaultValue = "false",
                 Description = Localizer["Wrap"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.HorizontalGap),
                 Type = "int?",
                 DefaultValue = "10",
                 Description = Localizer["HorizontalGap"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.VerticalGap),
                 Type = "int?",
                 DefaultValue = "10",
                 Description = Localizer["VerticalGap"]
            },
            new AttributeItem()
            {
                 Name = nameof(Stack.ChildContent),
                 Type = "RenderFragment?",
                 DefaultValue = "-",
                 Description = "ChildContent"
            }
        };
    }
}
