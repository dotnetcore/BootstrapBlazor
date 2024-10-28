// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件编辑模式枚举类型
/// </summary>
public enum EditMode
{
    /// <summary>
    /// 弹窗式编辑模式
    /// </summary>
    Popup,

    /// <summary>
    /// 行内编辑模式
    /// </summary>
    EditForm,

    /// <summary>
    /// 单元格内编辑模式
    /// </summary>
    InCell,

    /// <summary>
    /// 抽屉编辑模式
    /// </summary>
    Drawer
}
