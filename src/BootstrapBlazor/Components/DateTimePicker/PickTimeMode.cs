// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="DateTimePicker{TValue}"/> 组件选择时间方式枚举</para>
/// <para lang="en">Enum for Pick Time Mode of <see cref="DateTimePicker{TValue}"/> Component</para>
/// </summary>
public enum PickTimeMode
{
    /// <summary>
    /// <para lang="zh">使用 Dropdown 下拉方式选择时间</para>
    /// <para lang="en">Pick Time via Dropdown</para>
    /// </summary>
    Dropdown,

    /// <summary>
    /// <para lang="zh">使用 Clock 拖拽指针方式选择时间</para>
    /// <para lang="en">Pick Time via Clock</para>
    /// </summary>
    Clock
}
