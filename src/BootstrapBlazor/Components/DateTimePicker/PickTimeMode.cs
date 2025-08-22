// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="DateTimePicker{TValue}"/> 组件选择时间方式枚举
/// </summary>
public enum PickTimeMode
{
    /// <summary>
    /// 使用 Dropdown 下拉方式选择时间
    /// </summary>
    Dropdown,

    /// <summary>
    /// 使用 Clock 拖拽指针方式选择时间
    /// </summary>
    Clock
}
