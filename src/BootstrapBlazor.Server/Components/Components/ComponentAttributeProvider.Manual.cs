// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#nullable enable

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// Component attribute provider - Manual overrides for accurate defaults
/// </summary>
public static partial class ComponentAttributeProvider
{
    // This partial class supplements the auto-generated ComponentAttributeProvider
    // with manually maintained attribute metadata that includes accurate default values
    
    static ComponentAttributeProvider()
    {
        // Override Circle component attributes with accurate defaults
        _attributes["Circle"] = new AttributeItem[]
        {
            new()
            {
                Name = "Width",
                Description = "获得/设置 文件预览框宽度",
                Type = "int",
                ValueList = "",
                DefaultValue = "120"
            },
            new()
            {
                Name = "StrokeWidth",
                Description = "获得/设置 进度条宽度 默认为 2",
                Type = "int",
                ValueList = "",
                DefaultValue = "2"
            },
            new()
            {
                Name = "Color",
                Description = "获得/设置 组件进度条颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new()
            {
                Name = "ShowProgress",
                Description = "获得/设置 是否显示进度百分比 默认显示",
                Type = "bool",
                ValueList = "true / false",
                DefaultValue = "true"
            },
            new()
            {
                Name = "ChildContent",
                Description = "获得/设置 子组件",
                Type = "RenderFragment?",
                ValueList = "",
                DefaultValue = ""
            },
            new()
            {
                Name = "Value",
                Description = "获得/设置 当前值",
                Type = "int",
                ValueList = "0-100",
                DefaultValue = "0"
            }
        };
    }
}
