// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// Index 文档
/// </summary>
public partial class Index
{
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Name",
            Description = "DockView 本地化存储识别键值",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnInitializedCallbackAsync",
            Description = "客户端组件脚本初始化完成后回调此方法",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnVisibleStateChangedAsync",
            Description = "标签切换 Visible 状态时回调此方法",
            Type = "Func<string, bool, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnTabDropCallbackAsync",
            Description = "标签页拖动完成时回调此方法",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnSplitterCallbackAsync",
            Description = "标签页调整大小完成时回调此方法",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnLockChangedCallbackAsync",
            Description = "锁定状态回调此方法",
            Type = "Func<bool, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnResizeCallbackAsync",
            Description = "标签页位置变化时回调此方法",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
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
            Name = "LayoutConfig",
            Description = "布局配置",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Version",
            Description = "版本设置",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EnableLocalStorage",
            Description = "是否启用本地存储布局",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "LocalStoragePrefix",
            Description = "本地存储前缀",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

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
