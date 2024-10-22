// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 搜索栏渲染模式 默认 Popup 弹窗形式
/// </summary>
public enum SearchMode
{
    /// <summary>
    /// 弹窗模式
    /// </summary>
    Popup,

    /// <summary>
    /// Table 组件上方
    /// </summary>
    Top
}
