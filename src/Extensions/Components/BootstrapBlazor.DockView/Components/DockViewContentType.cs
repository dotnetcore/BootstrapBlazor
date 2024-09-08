// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContent 类型
/// </summary>
[JsonEnumConverter(true)]
public enum DockViewContentType
{
    /// <summary>
    /// 行排列 水平排列
    /// </summary>
    Row,

    /// <summary>
    /// 列排列 垂直排列
    /// </summary>
    Column,

    /// <summary>
    /// 标签排列
    /// </summary>
    Group,

    /// <summary>
    /// 组件
    /// </summary>
    Component,
}
