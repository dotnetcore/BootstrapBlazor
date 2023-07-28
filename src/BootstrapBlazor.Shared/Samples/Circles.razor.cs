// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Circles
/// </summary>
public sealed partial class Circles
{
    private int _circleValue = 0;

    private void Add(int interval)
    {
        _circleValue += interval;
        _circleValue = Math.Min(100, Math.Max(0, _circleValue));
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new(){
                    Name = "Width",
                    Description = Localizer["Width"],
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "120"
                },
                new(){
                    Name = "StrokeWidth",
                    Description = Localizer["StrokeWidth"],
                    Type = "int",
                    ValueList = "",
                    DefaultValue = "2"
                },
                new()
                {
                    Name = "Value",
                    Description = Localizer["Value"],
                    Type = "int",
                    ValueList = "0-100",
                    DefaultValue = "0"
                },
                new(){
                    Name = "Color",
                    Description = Localizer["Color"],
                    Type = "Color",
                    ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                    DefaultValue = "Primary"
                },
                new()
                {
                    Name = "ShowProgress",
                    Description = Localizer["ShowProgress"],
                    Type = "bool",
                    ValueList = "true / false",
                    DefaultValue = "true"
                },
                new()
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

