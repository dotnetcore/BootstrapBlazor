// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// Index 文档
/// </summary>
public partial class Index
{

    private static AttributeItem[] GetDockContentAttributes() =>
    [
        new()
        {
            Name = "Type",
            Description = "渲染类型",
            Type = "DockContentType",
            ValueList = "Row|Column|Stack|Component",
            DefaultValue = "Component"
        },
        new()
        {
            Name = "Width",
            Description = "组件宽度百分比",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = "组件高度百分比",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowHeader",
            Description = "组件是否显示 Header",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        }
    ];

    private static AttributeItem[] GetDockComponentAttributes() =>
    [
        new()
        {
            Name = "Title",
            Description = "组件 Title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = "组件 Class",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Visible",
            Description = "组件是否可见",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowClose",
            Description = "组件是否允许关闭",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsLock",
            Description = "是否锁定",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Width",
            Description = "组件宽度百分比",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = "组件高度百分比",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Key",
            Description = "组件唯一标识值 未设置时取 Title 参数",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
    ];
}
