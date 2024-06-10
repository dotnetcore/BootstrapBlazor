// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContent 类型
/// </summary>
[JsonConverter(typeof(DockViewTypeConverter))]
public enum DockViewContentType
{
    /// <summary>
    /// 行排列 水平排列
    /// </summary>
    [Description("row")]
    Row,

    /// <summary>
    /// 列排列 垂直排列
    /// </summary>
    [Description("column")]
    Column,

    /// <summary>
    /// 标签排列
    /// </summary>
    [Description("group")]
    Group,

    /// <summary>
    /// 组件
    /// </summary>
    [Description("component")]
    Component,
}
