// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Circles
{
    private int CircleValue = 0;

    private void Add(int interval)
    {
        CircleValue += interval;
        CircleValue = Math.Min(100, Math.Max(0, CircleValue));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem(){
                    Name = "Width",
                    Description = Localizer["Width"],
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "120"
                },
                new AttributeItem(){
                    Name = "StrokeWidth",
                    Description = Localizer["StrokeWidth"],
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "2"
                },
                new AttributeItem()
                {
                    Name = "Value",
                    Description = Localizer["Value"],
                    Type = "int",
                    ValueList = "0-100",
                    DefaultValue = "0"
                },
                new AttributeItem(){
                    Name = "Color",
                    Description = Localizer["Color"],
                    Type = "Color",
                    ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                    DefaultValue = "Primary"
                },
                new AttributeItem()
                {
                    Name = "ShowProgress",
                    Description = Localizer["ShowProgress"],
                    Type = "bool",
                    ValueList = "true / false",
                    DefaultValue = "true"
                },
                new AttributeItem()
                {
                    Name = "ChildContent",
                    Description = Localizer["ChildContent"],
                    Type = "RenderFragment",
                    ValueList = "",
                    DefaultValue = ""
                }
        };
    }
}

