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

}
