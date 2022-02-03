// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Skeletons
{

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(SkeletonTable.Round),
            Description = "是否显示圆角",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(SkeletonTable.Active),
            Description = "是否显示动画",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(SkeletonTable.Rows),
            Description = "骨架屏默认显示行数",
            Type = "bool",
            ValueList = "int",
            DefaultValue = "7"
        },
        new AttributeItem() {
            Name = nameof(SkeletonTable.Columns),
            Description = "骨架屏默认显示列数",
            Type = "bool",
            ValueList = "int",
            DefaultValue = "3"
        },
        new AttributeItem() {
            Name = nameof(SkeletonTable.ShowToolbar),
            Description = "是否显示 Toolbar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        }
    };
}
