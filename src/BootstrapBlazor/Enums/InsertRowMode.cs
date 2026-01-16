// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Table 组件 <see cref="EditMode"/> 为非弹窗模式时新建行模式</para>
///  <para lang="en">Table component <see cref="EditMode"/> 为非弹窗模式时新建行模式</para>
/// </summary>
public enum InsertRowMode
{
    /// <summary>
    ///  <para lang="zh">第一行</para>
    ///  <para lang="en">第一行</para>
    /// </summary>
    First,

    /// <summary>
    ///  <para lang="zh">最后一行</para>
    ///  <para lang="en">最后一行</para>
    /// </summary>
    Last
}
