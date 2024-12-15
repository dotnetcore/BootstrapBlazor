// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Skeletons
/// </summary>
public partial class Skeletons
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(SkeletonTable.Round),
            Description = "是否显示圆角",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(SkeletonTable.Active),
            Description = "是否显示动画",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(SkeletonTable.Rows),
            Description = "骨架屏默认显示行数",
            Type = "bool",
            ValueList = "int",
            DefaultValue = "7"
        },
        new()
        {
            Name = nameof(SkeletonTable.Columns),
            Description = "骨架屏默认显示列数",
            Type = "bool",
            ValueList = "int",
            DefaultValue = "3"
        },
        new()
        {
            Name = nameof(SkeletonTable.ShowToolbar),
            Description = "是否显示 Toolbar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        }
    ];
}
