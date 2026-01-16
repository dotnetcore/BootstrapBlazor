// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Table 组件编辑模式枚举类型</para>
///  <para lang="en">Table Edit Mode Enum</para>
/// </summary>
public enum EditMode
{
    /// <summary>
    ///  <para lang="zh">弹窗式编辑模式</para>
    ///  <para lang="en">Popup Edit Mode</para>
    /// </summary>
    Popup,

    /// <summary>
    ///  <para lang="zh">行内编辑模式</para>
    ///  <para lang="en">Edit Form Mode</para>
    /// </summary>
    EditForm,

    /// <summary>
    ///  <para lang="zh">单元格内编辑模式</para>
    ///  <para lang="en">In Cell Edit Mode</para>
    /// </summary>
    InCell,

    /// <summary>
    ///  <para lang="zh">抽屉编辑模式</para>
    ///  <para lang="en">Drawer Edit Mode</para>
    /// </summary>
    Drawer
}
